using ExactOnline.Api.Client.Authentication.Abstractions;
using ExactOnline.Api.Client.Authentication.Storage.SqlServer.Options;
using Microsoft.Extensions.Options;

namespace ExactOnline.Api.Client.Authentication.Storage.SqlServer;

internal class ExactTokenStorageSqlServer(IOptions<ExactOnlineSqlServerStorageOptions> options) : IExactTokenStorageService
{
    public Task StoreRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<string> RetrieveRefreshTokenAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}