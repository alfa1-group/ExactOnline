namespace ExactOnline.Api.Client.Authentication.Interfaces;

public interface IExactRefreshTokenStorageService
{
    Task SaveRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);

    Task<string> GetRefreshTokenAsync(CancellationToken cancellationToken = default);
}