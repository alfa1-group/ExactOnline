namespace ExactOnline.Api.Client.Authentication.Interfaces;

public interface IExactRefreshTokenStorageService
{
    Task StoreAsync(string refreshToken, CancellationToken cancellationToken = default);

    Task<string> RetrieveAsync(CancellationToken cancellationToken = default);
}