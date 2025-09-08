namespace ExactOnline.Api.Client.Authentication.Abstractions;

public interface IExactTokenStorageService
{
    Task<string> StoreRefreshTokenAsync(string currentRefreshToken, string newRefreshToken, CancellationToken cancellationToken = default);

    Task<string> RetrieveRefreshTokenAsync(CancellationToken cancellationToken = default);

    Task<string> StoreAccessTokenAsync(string? currentAccessToken, string newAccessToken, TimeSpan absoluteExpirationRelativeToUtcNow, CancellationToken cancellationToken = default);

    Task<string> RetrieveAccessTokenAsync(CancellationToken cancellationToken = default);
}