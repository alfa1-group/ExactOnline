using ExactOnline.Api.Client.Authentication.Abstractions;

namespace ConsoleApp;

internal class ExactTokenStorageServiceFilesystem : IExactTokenStorageService
{
    private readonly string _filePath = Path.Combine("c:", "temp", "Exact", "refreshtoken.txt");

    public Task StoreRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        return File.WriteAllTextAsync(_filePath, refreshToken, cancellationToken);
    }

    public Task<string> RetrieveRefreshTokenAsync(CancellationToken cancellationToken = default)
    {
        return File.ReadAllTextAsync(_filePath, cancellationToken);
    }
}