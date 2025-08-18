namespace ExactOnline.Api.Client.Models;
public partial class ReadFinancialOutstandingInvoicesOverview
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(CurrencyCode), "CurrencyCode" },
        { nameof(Metadata), "__metadata" },
        { nameof(OutstandingPayableInvoiceAmount), "OutstandingPayableInvoiceAmount" },
        { nameof(OutstandingPayableInvoiceCount), "OutstandingPayableInvoiceCount" },
        { nameof(OutstandingReceivableInvoiceAmount), "OutstandingReceivableInvoiceAmount" },
        { nameof(OutstandingReceivableInvoiceCount), "OutstandingReceivableInvoiceCount" },
        { nameof(OverduePayableInvoiceAmount), "OverduePayableInvoiceAmount" },
        { nameof(OverduePayableInvoiceCount), "OverduePayableInvoiceCount" },
        { nameof(OverdueReceivableInvoiceAmount), "OverdueReceivableInvoiceAmount" },
        { nameof(OverdueReceivableInvoiceCount), "OverdueReceivableInvoiceCount" }
    };
}
