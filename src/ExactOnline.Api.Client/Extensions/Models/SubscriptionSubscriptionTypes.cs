namespace ExactOnline.Api.Client.Models;
public partial class SubscriptionSubscriptionTypes
{
    internal static readonly Dictionary<string, string> PropertyMapping = new()
    {
        { nameof(AutomaticGenerateInvoiceDays), "AutomaticGenerateInvoiceDays" },
        { nameof(AutomaticGenerateInvoiceDescription), "AutomaticGenerateInvoiceDescription" },
        { nameof(AutomaticGenerateInvoiceType), "AutomaticGenerateInvoiceType" },
        { nameof(AutomaticSendInvoiceDays), "AutomaticSendInvoiceDays" },
        { nameof(AutomaticSendInvoiceMethod), "AutomaticSendInvoiceMethod" },
        { nameof(AutomaticSendInvoiceSender), "AutomaticSendInvoiceSender" },
        { nameof(AutomaticSendInvoiceSenderMailbox), "AutomaticSendInvoiceSenderMailbox" },
        { nameof(AutomaticSendInvoiceType), "AutomaticSendInvoiceType" },
        { nameof(CancellationPeriod), "CancellationPeriod" },
        { nameof(CancellationPeriodUnit), "CancellationPeriodUnit" },
        { nameof(Code), "Code" },
        { nameof(Created), "Created" },
        { nameof(Creator), "Creator" },
        { nameof(CreatorFullName), "CreatorFullName" },
        { nameof(CustomField), "CustomField" },
        { nameof(Description), "Description" },
        { nameof(Division), "Division" },
        { nameof(EnablePaymentLink), "EnablePaymentLink" },
        { nameof(ID), "ID" },
        { nameof(InvoiceCorrectionMethod), "InvoiceCorrectionMethod" },
        { nameof(InvoicePeriod), "InvoicePeriod" },
        { nameof(InvoicePeriodUnit), "InvoicePeriodUnit" },
        { nameof(ManualRenewalMethod), "ManualRenewalMethod" },
        { nameof(Metadata), "__metadata" },
        { nameof(Modified), "Modified" },
        { nameof(Modifier), "Modifier" },
        { nameof(ModifierFullName), "ModifierFullName" },
        { nameof(Notes), "Notes" },
        { nameof(ProlongationType), "ProlongationType" },
        { nameof(RenewalCancellationPeriod), "RenewalCancellationPeriod" },
        { nameof(RenewalCancellationPeriodUnit), "RenewalCancellationPeriodUnit" },
        { nameof(RenewalPeriod), "RenewalPeriod" },
        { nameof(RenewalPeriodUnit), "RenewalPeriodUnit" },
        { nameof(SubscriptionPeriod), "SubscriptionPeriod" },
        { nameof(SubscriptionPeriodUnit), "SubscriptionPeriodUnit" }
    };
}
