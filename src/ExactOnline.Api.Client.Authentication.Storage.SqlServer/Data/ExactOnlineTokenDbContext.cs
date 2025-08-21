using ExactOnline.Api.Client.Authentication.Storage.SqlServer.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ExactOnline.Api.Client.Authentication.Storage.SqlServer.Data;

public class ExactOnlineTokenDbContext(DbContextOptions<ExactOnlineTokenDbContext> options, IOptions<ExactOnlineEntityFrameworkCoreStorageOptions> storageOptions) : DbContext(options)
{
    private readonly ExactOnlineEntityFrameworkCoreStorageOptions _storageOptions = storageOptions.Value;

    public DbSet<ExactOnlineToken> Tokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ExactOnlineToken>().ToTable(_storageOptions.TableName);
        modelBuilder.Entity<ExactOnlineToken>().Property(p => p.RefreshToken).HasColumnName(_storageOptions.RefreshTokenColumnName);
        modelBuilder.Entity<ExactOnlineToken>().Property(p => p.RefreshTokenUpdatedAt).HasColumnName(_storageOptions.RefreshTokenUpdatedAtColumnName);

        modelBuilder.Entity<ExactOnlineToken>().Property(p => p.AccessToken).HasColumnName(_storageOptions.AccessTokenColumnName);
        modelBuilder.Entity<ExactOnlineToken>().Property(p => p.AccessTokenUpdatedAt).HasColumnName(_storageOptions.AccessTokenUpdatedAtColumnName);
        modelBuilder.Entity<ExactOnlineToken>().Property(p => p.AccessTokenExpire).HasColumnName(_storageOptions.AccessTokenExpireColumnName);
    }
}