using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;

namespace CVMatch.ConsoleApp;

internal class ExampleTokenAuthenticationProvider : IAuthenticationProvider
{
    private const string AuthorizationHeaderKey = "Authorization";

    private string? _jwt;

    public Task AuthenticateRequestAsync(RequestInformation request, Dictionary<string, object>? additionalAuthenticationContext = null, CancellationToken cancellationToken = default)
    {
        if (!request.Headers.ContainsKey(AuthorizationHeaderKey))
        {
            if (_jwt == null)
            {
                Console.WriteLine("Provide a valid Bearer JWT:");
                _jwt = Console.ReadLine();
                Console.WriteLine();
            }

            request.Headers.Add(AuthorizationHeaderKey, $"Bearer {_jwt}");
        }

        return Task.CompletedTask;
    }
}