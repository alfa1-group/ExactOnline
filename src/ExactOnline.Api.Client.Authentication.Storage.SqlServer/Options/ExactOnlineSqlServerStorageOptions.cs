using System.ComponentModel.DataAnnotations;

namespace ExactOnline.Api.Client.Authentication.Storage.SqlServer.Options;

public class ExactOnlineSqlServerStorageOptions
{
    //[Required]
    //public string ConnectionString { get; set; } = null!;

    [Required] public string TableName { get; set; } = "Exact";

    [Required]
    public string ColumnName { get; set; } = "RefreshToken";
}