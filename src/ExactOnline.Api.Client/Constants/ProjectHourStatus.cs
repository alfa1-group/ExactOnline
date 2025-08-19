namespace ExactOnline.Api.Client.Constants;

public static class ProjectHourStatus
{
    public const int Draft = 1;

    public const int Rejected = 2;

    public const int Submitted = 10;

    public const int FailedOnApproval = 11;

    public const int Processing = 14;

    public const int AlsoProcessing = 16;

    public const int FailedWhileUndoingApproval = 19;

    public const int Final = 20;
}