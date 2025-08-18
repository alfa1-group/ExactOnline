namespace ExactOnline.Api.Client.Authentication.Abstractions;

public interface IExactTokenStorageService
{
    Task StoreRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);

    Task<string> RetrieveRefreshTokenAsync(CancellationToken cancellationToken = default);

    Task<string> StoreAccessTokenAsync(string accessToken, TimeSpan absoluteExpirationRelativeToUtcNow, CancellationToken cancellationToken = default);
    
    Task<string> RetrieveAccessTokenAsync(CancellationToken cancellationToken = default);
}