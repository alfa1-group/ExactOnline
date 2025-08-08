using IdentityModel.Client;

namespace ExactOnline.Api.Client.Authentication.Interfaces;

public interface IExactTokenClient
{
    Task<TokenResponse> RequestRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
}