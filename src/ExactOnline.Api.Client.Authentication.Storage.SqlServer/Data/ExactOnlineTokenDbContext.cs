using ExactOnline.Api.Client.Authentication.Storage.SqlServer.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ExactOnline.Api.Client.Authentication.Storage.SqlServer.Data;

public class ExactOnlineTokenDbContext(DbContextOptions<ExactOnlineTokenDbContext> options, IOptions<ExactOnlineSqlServerStorageOptions> storageOptions) : DbContext(options)
{
    private readonly ExactOnlineSqlServerStorageOptions _storageOptions = storageOptions.Value;

    public DbSet<ExactOnlineToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ExactOnlineToken>().ToTable(_storageOptions.TableName);
        modelBuilder.Entity<ExactOnlineToken>().Property(p => p.RefreshToken).HasColumnName(_storageOptions.RefreshTokenColumnName);
        modelBuilder.Entity<ExactOnlineToken>().Property(p => p.RefreshTokenUpdatedAt).HasColumnName(_storageOptions.RefreshTokenUpdatedAtColumnName);
    }
}