using ExactOnline.Api.Client.Authentication.Interfaces;

namespace ConsoleApp;

internal class ExactRefreshTokenFileStorageService : IExactRefreshTokenStorageService
{
    private readonly string _filePath = Path.Combine("c:", "temp", "exact_refresh_token.txt");

    public Task StoreAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        return File.AppendAllTextAsync(_filePath, refreshToken, cancellationToken);
    }

    public Task<string> RetrieveAsync(CancellationToken cancellationToken = default)
    {
        return File.ReadAllTextAsync(_filePath, cancellationToken);
    }
}