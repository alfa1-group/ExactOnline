namespace ExactOnline.Api.Client.Exceptions;

/// <summary>
/// The exception that is thrown when the daily rate limit for the Exact Online API has been reached for a specific company.
/// </summary>
public class ExactOnlineDailyRateLimitReachedException : Exception
{
    /// <summary>
    /// Gets the company code for which the rate limit was reached.
    /// </summary>
    public int CompanyCode { get; }

    /// <summary>
    /// Gets the Coordinated Universal Time (UTC) at which the rate limit will reset.
    /// </summary>
    public DateTimeOffset ResetTimeUtc { get; }

    /// <summary>
    /// Gets the number of hours to wait until the rate limit resets.
    /// </summary>
    public double HoursToWait => (ResetTimeUtc - TimeProvider.System.GetUtcNow()).TotalHours;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExactOnlineDailyRateLimitReachedException"/> class.
    /// </summary>
    public ExactOnlineDailyRateLimitReachedException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExactOnlineDailyRateLimitReachedException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public ExactOnlineDailyRateLimitReachedException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExactOnlineDailyRateLimitReachedException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public ExactOnlineDailyRateLimitReachedException(string message, Exception innerException) : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExactOnlineDailyRateLimitReachedException"/> class with the company code and reset time.
    /// </summary>
    /// <param name="companyCode">The company code for which the rate limit was reached.</param>
    /// <param name="resetTimeUtc">The Coordinated Universal Time (UTC) at which the rate limit will reset.</param>
    public ExactOnlineDailyRateLimitReachedException(int companyCode, DateTimeOffset resetTimeUtc)
        : base($"Daily rate limit of 5000 requests reached for company {companyCode} until {resetTimeUtc}.")
    {
        CompanyCode = companyCode;
        ResetTimeUtc = resetTimeUtc;
    }
}