namespace ExactOnline.Api.Client.Models;
public partial class CashflowProcessPayments
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(BankExportDocumentsUrl), "BankExportDocumentsUrl" },
        { nameof(ErrorMessage), "ErrorMessage" },
        { nameof(ID), "ID" },
        { nameof(Metadata), "__metadata" },
        { nameof(PaymentIDs), "PaymentIDs" },
        { nameof(SuccessMessage), "SuccessMessage" }
    };
}
