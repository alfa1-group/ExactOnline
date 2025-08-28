namespace ExactOnline.Api.Client.Constants;

/// <summary>
/// Rolelevel sets the level on which a role for a user is active.
/// </summary>
public class RoleLevel
{
    public const int Database = 1;

    public const int Customer = 2;

    public const int Division = 3;

    public const int TransferredToAccountant = 100;
}