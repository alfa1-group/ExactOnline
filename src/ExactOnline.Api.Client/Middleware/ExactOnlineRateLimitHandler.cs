using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace ExactOnline.Api.Client.Middleware;

[SuppressMessage("ReSharper", "InconsistentlySynchronizedField")]
public class ExactOnlineRateLimitHandler : DelegatingHandler
{
    private const int MaxRequestsPerMinute = 60;
    private readonly ConcurrentDictionary<int, RateLimitState> _companyLimits = new(); // Track limits per company code

    private readonly ILogger<ExactOnlineRateLimitHandler> _logger;

    public ExactOnlineRateLimitHandler() : this(NullLogger<ExactOnlineRateLimitHandler>.Instance)
    {
    }

    public ExactOnlineRateLimitHandler(ILogger<ExactOnlineRateLimitHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (!TryExtractCompanyCode(request.RequestUri, out var companyCode))
        {
            // If we can't extract the company code, no need to apply rate limits.
            return await base.SendAsync(request, cancellationToken);
        }

        var now = TimeProvider.System.GetUtcNow();
        var state = _companyLimits.GetOrAdd(companyCode, _ => new RateLimitState
        {
            MinuteWindowStartUtc = now
        });

        // Check daily rate limit
        if (state is { DailyLimitReached: true, DailyResetUtc: not null } && now < state.DailyResetUtc.Value)
        {
            throw new InvalidOperationException($"Daily rate limit reached for company {companyCode} until {state.DailyResetUtc}");
        }

        // Enforce 60/min proactive limit
        lock (state)
        {
            if (now - state.MinuteWindowStartUtc >= TimeSpan.FromMinutes(1))
            {
                state.RequestsThisMinute = 0;
                state.MinuteWindowStartUtc = now;
            }

            if (state.RequestsThisMinute >= MaxRequestsPerMinute)
            {
                var waitMs = (int)(state.MinuteWindowStartUtc.AddMinutes(1) - now).TotalMilliseconds;
                if (waitMs > 0)
                {
                    _logger.LogDebug("Minutely rate limit reached for company {CompanyCode}. Waiting {WaitMs:F0} ms before next request.", companyCode, waitMs);
                    Task.Delay(waitMs, cancellationToken).Wait(cancellationToken);

                    state.RequestsThisMinute = 0;
                    state.MinuteWindowStartUtc = TimeProvider.System.GetUtcNow();
                }
            }

            state.RequestsThisMinute++;
        }

        var response = await base.SendAsync(request, cancellationToken);

        // Read rate-limit headers (minutely)
        if (response.StatusCode == HttpStatusCode.TooManyRequests && response.Headers.TryGetFirstValueAsLong("X-RateLimit-Minutely-Remaining", out var minutelyRemaining))
        {
            if (minutelyRemaining <= 0)
            {
                var waitUntil = state.MinuteWindowStartUtc.AddMinutes(1);
                var delay = waitUntil - TimeProvider.System.GetUtcNow();
                if (delay > TimeSpan.Zero)
                {
                    _logger.LogDebug("Minutely rate limit reached for company {CompanyCode}. Waiting {Delay:F0} ms before next request.", companyCode, delay.TotalMilliseconds);
                    await Task.Delay(delay, cancellationToken);
                }
            }
        }

        // Read rate-limit headers (daily)
        if (response.StatusCode == HttpStatusCode.TooManyRequests && response.Headers.TryGetFirstValueAsLong("X-RateLimit-Remaining", out var dailyRemaining))
        {
            if (dailyRemaining <= 0)
            {
                state.DailyLimitReached = true;
                _logger.LogDebug("Daily rate limit reached for company {CompanyCode}. No more requests allowed until reset.", companyCode);

                if (response.Headers.TryGetFirstValueAsLong("X-RateLimit-Reset", out var resetEpochMs))
                {
                    state.DailyResetUtc = DateTimeOffset.FromUnixTimeMilliseconds(resetEpochMs);
                    _logger.LogDebug("Daily rate limit for company {CompanyCode} will reset at {ResetTime}.", companyCode, state.DailyResetUtc);
                }
            }
        }

        return response;
    }

    /// <summary>
    /// Try to extract the company code from the request URI ("https://start.exactonline.nl/api/v1/{companyCode}/...").
    /// </summary>
    private static bool TryExtractCompanyCode(Uri? uri, out int companyCode)
    {
        var segments = uri?.Segments ?? [];
        if (segments.Length > 3)
        {
            return int.TryParse(segments[3].TrimEnd('/'), out companyCode);
        }

        companyCode = default;
        return false;
    }

    private record RateLimitState
    {
        public int RequestsThisMinute;

        public DateTimeOffset MinuteWindowStartUtc;

        public bool DailyLimitReached;

        public DateTimeOffset? DailyResetUtc;
    }
}