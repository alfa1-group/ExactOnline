namespace ExactOnline.Api.Client.Authentication.Interfaces;

public interface IExactTokenService
{
    Task<string> GetAccessTokenAsync(CancellationToken cancellationToken = default);

    Task<string> RefreshTokenAsync(CancellationToken cancellationToken = default);
}