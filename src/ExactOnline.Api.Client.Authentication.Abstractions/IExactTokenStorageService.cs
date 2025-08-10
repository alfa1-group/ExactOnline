namespace ExactOnline.Api.Client.Authentication.Abstractions;

public interface IExactTokenStorageService
{
    Task StoreRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);

    Task<string> RetrieveRefreshTokenAsync(CancellationToken cancellationToken = default);
}