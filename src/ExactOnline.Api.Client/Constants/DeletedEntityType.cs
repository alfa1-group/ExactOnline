using Microsoft.Kiota.Abstractions.Serialization;

namespace ExactOnline.Api.Client.Constants;

/// <summary>
/// All possible Entity Types.
/// </summary>
public static class DeletedEntityType
{
    private static readonly Dictionary<string, int> EntityTypeMap = new(StringComparer.OrdinalIgnoreCase)
    {
        { "SyncCRMAccounts", Accounts },
        { "SyncCRMAddresses", Addresses },
        { "SyncCRMContacts", Contacts },
        { "SyncCRMQuotationHeaders", QuotationHeaders },
        { "SyncCRMQuotationLines", QuotationLines },
        { "SyncCashflowPaymentTerms", PaymentTerms },
        { "SyncDocumentsDocumentAttachments", Attachments },
        { "SyncDocumentsDocuments", Documents },
        { "SyncFinancialGLAccounts", GLAccounts },
        { "SyncFinancialGLClassifications", GLClassifications },
        { "SyncFinancialTransactionLines", TransactionLines },
        { "SyncHRMAbsenceRegistrationTransactions", AbsenceRegistrationTransactions },
        { "SyncHRMAbsenceRegistrations", AbsenceRegistrations },
        { "SyncHRMLeaveAbsenceHoursByDay", LeaveAbsenceHoursByDay },
        { "SyncHRMLeaveBuildUpRegistrations", LeaveBuildUpRegistrations },
        { "SyncHRMLeaveRegistrations", LeaveRegistrations },
        { "SyncHRMScheduleEntries", ScheduleEntries },
        { "SyncHRMSchedules", Schedules },
        { "SyncInventoryItemStorageLocations", ItemStorageLocations },
        { "SyncInventoryItemWarehouses", ItemWarehouses },
        { "SyncInventorySerialBatchNumbers", SerialBatchNumbers },
        { "SyncInventoryStockPositions", StockPositions },
        { "SyncInventoryStockSerialBatchNumbers", StockSerialBatchNumbers },
        { "SyncInventoryStorageLocationStockPositions", StorageLocationStockPositions },
        { "SyncLogisticsItems", Items },
        { "SyncLogisticsPurchaseItemPrices", ItemPrices },
        { "SyncLogisticsSalesItemPrices", ItemPrices },
        { "SyncLogisticsSupplierItem", ItemAccounts },
        { "SyncManufacturingBillOfMaterialMaterials", BillOfMaterialMaterials },
        { "SyncManufacturingBillOfMaterialVersions", BillOfMaterialVersions },
        { "SyncManufacturingMaterialIssues", RequirementIssues },
        { "SyncManufacturingShopOrderMaterialPlans", ShopOrderMaterialPlans },
        { "SyncManufacturingShopOrderPurchasePlanning", ShopOrderPurchasePlanning },
        { "SyncManufacturingShopOrderRoutingStepPlans", ShopOrderRoutingStepPlans },
        { "SyncManufacturingShopOrderSubOrders", ShopOrderSubOrders },
        { "SyncManufacturingShopOrders", ShopOrders },
        { "SyncPayrollBankAccounts", BankAccounts },
        { "SyncPayrollEmployees", Employees },
        { "SyncPayrollEmploymentCLAs", EmploymentCLAs },
        { "SyncPayrollEmploymentContracts", EmploymentContracts },
        { "SyncPayrollEmploymentOrganizations", EmploymentOrganizations },
        { "SyncPayrollEmploymentSalaries", EmploymentSalaries },
        { "SyncPayrollEmploymentTaxAuthoritiesGeneral", EmploymentTaxAuthoritiesGeneral },
        { "SyncPayrollEmployments", Employments },
        { "SyncProjectProjectPlanning", ProjectPlanning },
        { "SyncProjectProjectWBS", ProjectWBS },
        { "SyncProjectProjects", Projects },
        { "SyncProjectTimeCostTransactions", TimeCostTransactions },
        { "SyncPurchaseOrderPurchaseOrders", PurchaseOrders },
        { "SyncSalesInvoiceSalesInvoices", SalesInvoices },
        { "SyncSalesOrderGoodsDeliveries", GoodsDeliveries },
        { "SyncSalesOrderGoodsDeliveryLines", GoodsDeliveryLines },
        { "SyncSalesOrderSalesOrderHeaders", SalesOrderHeaders },
        { "SyncSalesOrderSalesOrderLines", SalesOrderLines },
        { "SyncSalesSalesPriceListVolumeDiscounts", DiscountTables },
        { "SyncSubscriptionSubscriptionLines", SubscriptionLines },
        { "SyncSubscriptionSubscriptions", Subscriptions }
    };

    public const int TransactionLines = 1;

    public const int Accounts = 2;

    public const int Addresses = 3;

    public const int Attachments = 4;

    public const int Contacts = 5;

    public const int Documents = 6;

    public const int GLAccounts = 7;

    public const int ItemPrices = 8;

    public const int Items = 9;

    public const int PaymentTerms = 10;

    /// <summary>
    /// This entity is going to be removed. Please refer to the new entity SalesOrderHeaders, SalesOrderLines.
    /// </summary>
    public const int SalesOrders = 12;

    public const int SalesInvoices = 13;

    public const int TimeCostTransactions = 14;

    public const int StockPositions = 15;

    public const int GoodsDeliveries = 16;

    public const int GoodsDeliveryLines = 17;

    public const int GLClassifications = 18;

    public const int ItemWarehouses = 19;

    public const int StorageLocationStockPositions = 20;

    public const int Projects = 21;

    public const int PurchaseOrders = 22;

    public const int Subscriptions = 23;

    public const int SubscriptionLines = 24;

    public const int ProjectWBS = 25;

    public const int ProjectPlanning = 26;

    public const int LeaveAbsenceHoursByDay = 27;

    public const int SerialBatchNumbers = 28;

    public const int StockSerialBatchNumbers = 29;

    public const int ItemAccounts = 30;

    public const int DiscountTables = 31;

    public const int SalesOrderHeaders = 32;

    public const int SalesOrderLines = 33;

    public const int QuotationHeaders = 34;

    public const int QuotationLines = 35;

    public const int ShopOrders = 36;

    public const int ShopOrderMaterialPlans = 37;

    public const int ShopOrderRoutingStepPlans = 38;

    public const int Schedules = 39;

    public const int ScheduleEntries = 40;

    public const int ItemStorageLocations = 41;

    public const int Employees = 42;

    public const int Employments = 43;

    public const int EmploymentContracts = 44;

    public const int EmploymentOrganizations = 45;

    public const int EmploymentCLAs = 46;

    public const int EmploymentSalaries = 47;

    public const int BankAccounts = 48;

    public const int EmploymentTaxAuthoritiesGeneral = 49;

    public const int ShopOrderPurchasePlanning = 50;

    public const int ShopOrderSubOrders = 51;

    public const int RequirementIssues = 53;

    public const int BillOfMaterialMaterials = 54;

    public const int BillOfMaterialVersions = 55;

    public const int LeaveRegistrations = 56;

    public const int LeaveBuildUpRegistrations = 57;

    public const int AbsenceRegistrationTransactions = 58;

    public const int AbsenceRegistrations = 59;

    public static bool TryParse(string entityTypeString, out int entityTypeId)
    {
        return EntityTypeMap.TryGetValue(entityTypeString, out entityTypeId);
    }

    public static bool TryParse<T>(out int entityTypeId) where T : IParsable
    {
        return EntityTypeMap.TryGetValue(typeof(T).Name, out entityTypeId);
    }
}