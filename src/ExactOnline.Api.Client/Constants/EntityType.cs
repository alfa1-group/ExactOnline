namespace ExactOnline.Api.Client.Constants;

/// <summary>
/// A list of all entity types with their corresponding integer values (not related to ExactOnline, except for the Sync section).
/// </summary>
public static class EntityType
{
    // Accountancy
    public const int AccountancyAccountInvolvedAccounts = 10100;
    public const int AccountancyAccountOwners = 10200;
    public const int AccountancyInvolvedUserRoles = 10300;
    public const int AccountancyInvolvedUsers = 10400;
    public const int AccountancySolutionLinks = 10500;
    public const int AccountancyTaskTypes = 10600;

    // Activities
    public const int ActivitiesCommunicationNotes = 20100;
    public const int ActivitiesComplaints = 20200;
    public const int ActivitiesEvents = 20300;
    public const int ActivitiesServiceRequests = 20400;
    public const int ActivitiesTasks = 20500;

    // Assets
    public const int AssetsAssetGroups = 30100;
    public const int AssetsAssets = 30200;
    public const int AssetsCommercialBuildingValues = 30300;
    public const int AssetsDepreciationMethods = 30400;

    // Budget
    public const int BudgetBudgets = 40100;
    public const int BudgetBudgetScenarios = 40200;

    // Bulk
    public const int BulkCashflowPayments = 50100;
    public const int BulkCashflowReceivables = 50101;
    public const int BulkCRMAccounts = 50200;
    public const int BulkCRMAddresses = 50201;
    public const int BulkCRMContacts = 50202;
    public const int BulkCRMQuotationLines = 50203;
    public const int BulkCRMQuotations = 50204;
    public const int BulkDocumentsDocumentAttachments = 50300;
    public const int BulkDocumentsDocuments = 50301;
    public const int BulkFinancialGLAccounts = 50400;
    public const int BulkFinancialGLClassifications = 50401;
    public const int BulkFinancialTransactionLines = 50402;
    public const int BulkLogisticsItems = 50500;
    public const int BulkLogisticsSalesItemPrices = 50501;
    public const int BulkProjectProjectWBS = 50600;
    public const int BulkSalesInvoiceSalesInvoiceLines = 50700;
    public const int BulkSalesInvoiceSalesInvoices = 50701;
    public const int BulkSalesOrderGoodsDeliveries = 50800;
    public const int BulkSalesOrderGoodsDeliveryLines = 50801;
    public const int BulkSalesOrderSalesOrderLines = 50802;
    public const int BulkSalesOrderSalesOrders = 50803;

    // Cashflow
    public const int CashflowAllocationRule = 60100;
    public const int CashflowBanks = 60200;
    public const int CashflowDirectDebitMandates = 60300;
    public const int CashflowPaymentConditions = 60400;
    public const int CashflowPayments = 60500;
    public const int CashflowProcessPayments = 60600;
    public const int CashflowReceivables = 60700;

    // CRM
    public const int CRMAcceptQuotation = 70100;
    public const int CRMAccountClasses = 70200;
    public const int CRMAccountClassificationNames = 70300;
    public const int CRMAccountClassifications = 70400;
    public const int CRMAccountDocumentFolders = 70500;
    public const int CRMAccountDocuments = 70600;
    public const int CRMAccountDocumentsCount = 70700;
    public const int CRMAccounts = 70800;
    public const int CRMAddresses = 70900;
    public const int CRMAddressStates = 71000;
    public const int CRMBankAccounts = 71100;
    public const int CRMContacts = 71200;
    public const int CRMDefaultAddressForAccount = 71300;
    public const int CRMDocuments = 71400;
    public const int CRMDocumentsAttachments = 71500;
    public const int CRMEmailWithSignOffQuotation = 71600;
    public const int CRMLeadPurposes = 71700;
    public const int CRMLeadSources = 71800;
    public const int CRMOpportunities = 71900;
    public const int CRMOpportunityContacts = 72000;
    public const int CRMOpportunityDocuments = 72100;
    public const int CRMOpportunityDocumentsCount = 72200;
    public const int CRMOptionalQuotationLineID = 72300;
    public const int CRMPrintQuotation = 72400;
    public const int CRMQuotationLines = 72500;
    public const int CRMQuotationOrderChargeLines = 72600;
    public const int CRMQuotations = 72700;
    public const int CRMReasonCodes = 72800;
    public const int CRMRejectQuotation = 72900;
    public const int CRMReopenQuotation = 73000;
    public const int CRMReviewQuotation = 73100;

    // CustomField
    public const int CustomFieldCustomFields = 80100;
    public const int CustomFieldUpdateCustomField = 80200;

    // Documents
    public const int DocumentsDocumentAttachments = 90100;
    public const int DocumentsDocumentCategories = 90200;
    public const int DocumentsDocumentFolders = 90300;
    public const int DocumentsDocuments = 90400;
    public const int DocumentsDocumentTypeCategories = 90500;
    public const int DocumentsDocumentTypeFolders = 90600;
    public const int DocumentsDocumentTypes = 90700;

    // Financial
    public const int FinancialAgingOverview = 100100;
    public const int FinancialAgingOverviewByAccount = 100200;
    public const int FinancialAgingPayablesList = 100300;
    public const int FinancialAgingPayablesListByAgeGroup = 100400;
    public const int FinancialAgingReceivablesList = 100500;
    public const int FinancialAgingReceivablesListByAgeGroup = 100600;
    public const int FinancialDeductibilityPercentages = 100700;
    public const int FinancialExchangeRates = 100800;
    public const int FinancialFinancialPeriods = 100900;
    public const int FinancialGLAccountClassificationMappings = 101000;
    public const int FinancialGLAccounts = 101100;
    public const int FinancialGLClassifications = 101200;
    public const int FinancialGLSchemes = 101300;
    public const int FinancialGLTransactionSources = 101400;
    public const int FinancialGLTransactionTypes = 101500;
    public const int FinancialJournals = 101600;
    public const int FinancialJournalStatusByFinancialPeriod = 101700;
    public const int FinancialJournalStatusList = 101800;
    public const int FinancialOfficialReturns = 101900;
    public const int FinancialOutstandingInvoicesOverview = 102000;
    public const int FinancialPayablesList = 102100;
    public const int FinancialPayablesListByAccount = 102200;
    public const int FinancialPayablesListByAccountAndAgeGroup = 102300;
    public const int FinancialPayablesListByAgeGroup = 102400;
    public const int FinancialProcessReturn = 102500;
    public const int FinancialProfitLossOverview = 102600;
    public const int FinancialReceivablesList = 102700;
    public const int FinancialReceivablesListByAccount = 102800;
    public const int FinancialReceivablesListByAccountAndAgeGroup = 102900;
    public const int FinancialReceivablesListByAgeGroup = 103000;
    public const int FinancialReportingBalance = 103100;
    public const int FinancialReportingBalanceByClassification = 103200;
    public const int FinancialReturns = 103300;
    public const int FinancialRevenueList = 103400;
    public const int FinancialRevenueListByYear = 103500;
    public const int FinancialRevenueListByYearAndStatus = 103600;

    // FinancialTransaction
    public const int FinancialTransactionBankEntries = 110100;
    public const int FinancialTransactionBankEntryLines = 110200;
    public const int FinancialTransactionCashEntries = 110300;
    public const int FinancialTransactionCashEntryLines = 110400;
    public const int FinancialTransactionTransactionLines = 110500;

    // General
    public const int GeneralCurrencies = 120100;
    public const int GeneralLayouts = 120200;

    // GeneralJournalEntry
    public const int GeneralJournalEntryGeneralJournalEntries = 130100;
    public const int GeneralJournalEntryGeneralJournalEntryLines = 130200;

    // HRM
    public const int HRMAbsenceRegistrations = 140100;
    public const int HRMAbsenceRegistrationTransactions = 140200;
    public const int HRMCostcenters = 140300;
    public const int HRMCostunits = 140400;
    public const int HRMDepartments = 140500;
    public const int HRMDivisionClasses = 140600;
    public const int HRMDivisionClassNames = 140700;
    public const int HRMDivisionClassValues = 140800;
    public const int HRMDivisions = 140900;
    public const int HRMJobGroups = 141000;
    public const int HRMJobTitles = 141100;
    public const int HRMLeaveAbsenceHoursByDay = 141200;
    public const int HRMLeaveBuildUpRegistrations = 141300;
    public const int HRMLeaveRegistrations = 141400;
    public const int HRMSchedules = 141500;

    // Inventory
    public const int InventoryAssemblyBillOfMaterialHeader = 150100;
    public const int InventoryAssemblyBillOfMaterialMaterials = 150200;
    public const int InventoryAssemblyOrders = 150300;
    public const int InventoryBatchNumbers = 150400;
    public const int InventoryFinishAssemblyOrder = 150500;
    public const int InventoryItemWarehousePlanningDetails = 150600;
    public const int InventoryItemWarehouses = 150700;
    public const int InventoryItemWarehouseStorageLocations = 150800;
    public const int InventoryProcessStockCount = 150900;
    public const int InventoryProcessWarehouseTransfer = 151000;
    public const int InventorySerialNumbers = 151100;
    public const int InventoryStockBatchNumbers = 151200;
    public const int InventoryStockCountLines = 151300;
    public const int InventoryStockCounts = 151400;
    public const int InventoryStockSerialNumbers = 151500;
    public const int InventoryStorageLocations = 151600;
    public const int InventoryWarehouses = 151700;
    public const int InventoryWarehouseTransferLines = 151800;
    public const int InventoryWarehouseTransfers = 151900;

    // Logistics
    public const int LogisticsAccountItems = 160100;
    public const int LogisticsCustomerItems = 160200;
    public const int LogisticsIncoterms = 160300;
    public const int LogisticsItemAssortment = 160400;
    public const int LogisticsItemAssortmentProperty = 160500;
    public const int LogisticsItemChargeRelation = 160600;
    public const int LogisticsItemDetailsByID = 160700;
    public const int LogisticsItemExtraField = 160800;
    public const int LogisticsItemGroups = 160900;
    public const int LogisticsItems = 161000;
    public const int LogisticsItemVersions = 161100;
    public const int LogisticsReasonCodes = 161200;
    public const int LogisticsReasonCodesLinkTypes = 161300;
    public const int LogisticsSalesItemPrice = 161400;
    public const int LogisticsSalesItemPrices = 161500;
    public const int LogisticsSelectionCodes = 161600;
    public const int LogisticsStockPosition = 161700;
    public const int LogisticsSupplierItem = 161800;
    public const int LogisticsUnits = 161900;

    // Mailbox
    public const int MailboxDefaultMailbox = 170100;
    public const int MailboxMailboxes = 170200;
    public const int MailboxMailMessageAttachments = 170300;
    public const int MailboxMailMessagesSent = 170400;
    public const int MailboxPreferredMailbox = 170500;
    public const int MailboxPreferredMailboxForOperation = 170600;

    // Manufacturing
    public const int ManufacturingBillOfMaterialMaterials = 180100;
    public const int ManufacturingBillOfMaterialRoutings = 180200;
    public const int ManufacturingBillOfMaterialVersions = 180300;
    public const int ManufacturingByProductReceipts = 180400;
    public const int ManufacturingByProductReversals = 180500;
    public const int ManufacturingManufacturingSettings = 180600;
    public const int ManufacturingMaterialIssues = 180700;
    public const int ManufacturingMaterialReversals = 180800;
    public const int ManufacturingOperationResources = 180900;
    public const int ManufacturingOperations = 181000;
    public const int ManufacturingProductionAreas = 181100;
    public const int ManufacturingRecentTimeTransactions = 181200;
    public const int ManufacturingShopOrderMaterialPlanDetails = 181300;
    public const int ManufacturingShopOrderMaterialPlans = 181400;
    public const int ManufacturingShopOrderPriorities = 181500;
    public const int ManufacturingShopOrderReceipts = 181600;
    public const int ManufacturingShopOrderReversals = 181700;
    public const int ManufacturingShopOrderRoutingStepPlans = 181800;
    public const int ManufacturingShopOrderRoutingStepPlansAvailableToWork = 181900;
    public const int ManufacturingShopOrders = 182000;
    public const int ManufacturingStageForDeliveryReceipts = 182100;
    public const int ManufacturingStageForDeliveryReversals = 182200;
    public const int ManufacturingStartedTimedTimeTransactions = 182300;
    public const int ManufacturingSubOrderReceipts = 182400;
    public const int ManufacturingSubOrderReversals = 182500;
    public const int ManufacturingTimedTimeTransactions = 182600;
    public const int ManufacturingTimeTransactions = 182700;
    public const int ManufacturingWorkcenters = 182800;

    // OpeningBalance
    public const int OpeningBalanceCurrentYearAfterEntry = 190100;
    public const int OpeningBalanceCurrentYearProcessed = 190101;
    public const int OpeningBalancePreviousYearAfterEntry = 190200;
    public const int OpeningBalancePreviousYearProcessed = 190201;

    // Payroll
    public const int PayrollActiveEmployments = 200100;
    public const int PayrollEmployees = 200200;
    public const int PayrollEmploymentConditionGroups = 200300;
    public const int PayrollEmploymentContractFlexPhases = 200400;
    public const int PayrollEmploymentContractFlexPhasesOnFocusDate = 200500;
    public const int PayrollEmploymentContracts = 200600;
    public const int PayrollEmploymentEndReasons = 200700;
    public const int PayrollEmploymentEndReasonsOnFocusDate = 200800;
    public const int PayrollEmploymentOrganizations = 200900;
    public const int PayrollEmployments = 201000;
    public const int PayrollEmploymentSalaries = 201100;
    public const int PayrollEmploymentTaxAuthoritiesGeneral = 201200;
    public const int PayrollPayrollComponents = 201300;
    public const int PayrollPayrollTransactionsByPayrollYear = 201400;
    public const int PayrollTaxEmploymentEndFlexCodes = 201500;
    public const int PayrollVariableMutations = 201600;

    // Project
    public const int ProjectCostEntryExpensesByProject = 210100;
    public const int ProjectCostEntryRecentAccounts = 210200;
    public const int ProjectCostEntryRecentAccountsByProject = 210300;
    public const int ProjectCostEntryRecentCostTypes = 210400;
    public const int ProjectCostEntryRecentCostTypesByProject = 210500;
    public const int ProjectCostEntryRecentExpensesByProject = 210600;
    public const int ProjectCostEntryRecentProjects = 210700;
    public const int ProjectCostsByDate = 210800;
    public const int ProjectCostsById = 210900;
    public const int ProjectCostTransactions = 211000;
    public const int ProjectCostTypes = 211100;
    public const int ProjectCostTypesByDate = 211200;
    public const int ProjectCostTypesByProjectAndDate = 211300;
    public const int ProjectEmployeeRestrictionItems = 211400;
    public const int ProjectEmploymentInternalRates = 211500;
    public const int ProjectHourCostTypes = 211600;
    public const int ProjectHourEntryActivitiesByProject = 211700;
    public const int ProjectHourEntryRecentAccounts = 211800;
    public const int ProjectHourEntryRecentAccountsByProject = 211900;
    public const int ProjectHourEntryRecentActivitiesByProject = 212000;
    public const int ProjectHourEntryRecentHourTypes = 212100;
    public const int ProjectHourEntryRecentHourTypesByProject = 212200;
    public const int ProjectHourEntryRecentProjects = 212300;
    public const int ProjectHoursByDate = 212400;
    public const int ProjectHoursById = 212500;
    public const int ProjectHourTypes = 212600;
    public const int ProjectHourTypesByDate = 212700;
    public const int ProjectHourTypesByProjectAndDate = 212800;
    public const int ProjectInvoiceTerms = 212900;
    public const int ProjectProjectAccountMutations = 213000;
    public const int ProjectProjectBudgetTypes = 213100;
    public const int ProjectProjectClassifications = 213200;
    public const int ProjectProjectHourBudgets = 213300;
    public const int ProjectProjectPlanning = 213400;
    public const int ProjectProjectPlanningRecurring = 213500;
    public const int ProjectProjectRestrictionEmployeeItems = 213600;
    public const int ProjectProjectRestrictionEmployees = 213700;
    public const int ProjectProjectRestrictionItems = 213800;
    public const int ProjectProjectRestrictionRebillings = 213900;
    public const int ProjectProjects = 214000;
    public const int ProjectProjectWBSByProject = 214100;
    public const int ProjectProjectWBSByProjectAndWBS = 214200;
    public const int ProjectRecentCosts = 214300;
    public const int ProjectRecentCostsByNumberOfWeeks = 214400;
    public const int ProjectRecentCostsByNumberOfWeeksByDate = 214500;
    public const int ProjectRecentHours = 214600;
    public const int ProjectRecentHoursByNumberOfWeeks = 214700;
    public const int ProjectRecentHoursByNumberOfWeeksByDate = 214800;
    public const int ProjectTimeAndBillingAccountDetails = 214900;
    public const int ProjectTimeAndBillingAccountDetailsByID = 215000;
    public const int ProjectTimeAndBillingActivitiesAndExpenses = 215100;
    public const int ProjectTimeAndBillingEntryAccounts = 215200;
    public const int ProjectTimeAndBillingEntryAccountsByDate = 215300;
    public const int ProjectTimeAndBillingEntryAccountsByProjectAndDate = 215400;
    public const int ProjectTimeAndBillingEntryProjects = 215500;
    public const int ProjectTimeAndBillingEntryProjectsByAccountAndDate = 215600;
    public const int ProjectTimeAndBillingEntryProjectsByDate = 215700;
    public const int ProjectTimeAndBillingEntryRecentAccounts = 215800;
    public const int ProjectTimeAndBillingEntryRecentActivitiesAndExpenses = 215900;
    public const int ProjectTimeAndBillingEntryRecentHourCostTypes = 216000;
    public const int ProjectTimeAndBillingEntryRecentProjects = 216100;
    public const int ProjectTimeAndBillingItemDetails = 216200;
    public const int ProjectTimeAndBillingItemDetailsByID = 216300;
    public const int ProjectTimeAndBillingProjectDetails = 216400;
    public const int ProjectTimeAndBillingProjectDetailsByID = 216500;
    public const int ProjectTimeAndBillingRecentProjects = 216600;
    public const int ProjectTimeCorrections = 216700;
    public const int ProjectTimeTransactions = 216800;
    public const int ProjectWBSActivities = 216900;
    public const int ProjectWBSDeliverables = 217000;
    public const int ProjectWBSExpenses = 217100;

    // Purchase
    public const int PurchasePurchaseInvoiceLines = 220100;
    public const int PurchasePurchaseInvoices = 220200;

    // PurchaseEntry
    public const int PurchaseEntryPurchaseEntries = 230100;
    public const int PurchaseEntryPurchaseEntryLines = 230200;

    // PurchaseOrder
    public const int PurchaseOrderGoodsReceiptLines = 240100;
    public const int PurchaseOrderGoodsReceipts = 240200;
    public const int PurchaseOrderPurchaseOrderLines = 240300;
    public const int PurchaseOrderPurchaseOrders = 240400;
    public const int PurchaseOrderPurchaseReturnLines = 240500;
    public const int PurchaseOrderPurchaseReturns = 240600;

    // Sales
    public const int SalesOrderCharges = 250100;
    public const int SalesSalesChannels = 250200;
    public const int SalesSalesPriceListLinkedAccounts = 250300;
    public const int SalesSalesPriceListPeriods = 250400;
    public const int SalesSalesPriceLists = 250500;
    public const int SalesSalesPriceListVolumeDiscounts = 250600;
    public const int SalesShippingMethods = 250700;

    // SalesEntry
    public const int SalesEntrySalesEntries = 260100;
    public const int SalesEntrySalesEntryLines = 260200;

    // SalesInvoice
    public const int SalesInvoiceInvoiceSalesOrders = 270100;
    public const int SalesInvoiceLayouts = 270200;
    public const int SalesInvoicePrintedSalesInvoices = 270300;
    public const int SalesInvoiceSalesInvoiceLines = 270400;
    public const int SalesInvoiceSalesInvoiceOrderChargeLines = 270500;
    public const int SalesInvoiceSalesInvoices = 270600;
    public const int SalesInvoiceSalesOrderID = 270700;

    // SalesOrder
    public const int SalesOrderCompleteSalesOrder = 280100;
    public const int SalesOrderCompleteSalesOrderLine = 280200;
    public const int SalesOrderDropShipmentLines = 280300;
    public const int SalesOrderDropShipments = 280400;
    public const int SalesOrderGoodsDeliveries = 280500;
    public const int SalesOrderGoodsDeliveryLines = 280600;
    public const int SalesOrderPlannedSalesReturnLines = 280700;
    public const int SalesOrderPlannedSalesReturns = 280800;
    public const int SalesOrderPrintedSalesOrders = 280900;
    public const int SalesOrderSalesOrderLines = 281000;
    public const int SalesOrderSalesOrderOrderChargeLines = 281100;
    public const int SalesOrderSalesOrders = 281200;

    // Subscription
    public const int SubscriptionSubscriptionLines = 290100;
    public const int SubscriptionSubscriptionLineTypes = 290200;
    public const int SubscriptionSubscriptionReasonCodes = 290300;
    public const int SubscriptionSubscriptionRestrictionEmployees = 290400;
    public const int SubscriptionSubscriptionRestrictionItems = 290500;
    public const int SubscriptionSubscriptions = 290600;
    public const int SubscriptionSubscriptionTypes = 290700;

    // Sync (same as DeletedEntityType)
    public const int SyncCashflowPaymentTerms = DeletedEntityType.PaymentTerms;
    public const int SyncCRMAccounts = DeletedEntityType.Accounts;
    public const int SyncCRMAddresses = DeletedEntityType.Addresses;
    public const int SyncCRMContacts = DeletedEntityType.Contacts;
    public const int SyncCRMQuotationHeaders = DeletedEntityType.QuotationHeaders;
    public const int SyncCRMQuotationLines = DeletedEntityType.QuotationLines;
    public const int SyncDeleted = DeletedEntityType.Deleted;
    public const int SyncDocumentsDocumentAttachments = DeletedEntityType.Attachments;
    public const int SyncDocumentsDocuments = DeletedEntityType.Documents;
    public const int SyncFinancialGLAccounts = DeletedEntityType.GLAccounts;
    public const int SyncFinancialGLClassifications = DeletedEntityType.GLClassifications;
    public const int SyncFinancialTransactionLines = DeletedEntityType.TransactionLines;
    public const int SyncHRMAbsenceRegistrations = DeletedEntityType.AbsenceRegistrations;
    public const int SyncHRMAbsenceRegistrationTransactions = DeletedEntityType.AbsenceRegistrationTransactions;
    public const int SyncHRMLeaveAbsenceHoursByDay = DeletedEntityType.LeaveAbsenceHoursByDay;
    public const int SyncHRMLeaveBuildUpRegistrations = DeletedEntityType.LeaveBuildUpRegistrations;
    public const int SyncHRMLeaveRegistrations = DeletedEntityType.LeaveRegistrations;
    public const int SyncHRMScheduleEntries = DeletedEntityType.ScheduleEntries;
    public const int SyncHRMSchedules = DeletedEntityType.Schedules;
    public const int SyncInventoryItemStorageLocations = DeletedEntityType.ItemStorageLocations;
    public const int SyncInventoryItemWarehouses = DeletedEntityType.ItemWarehouses;
    public const int SyncInventorySerialBatchNumbers = DeletedEntityType.SerialBatchNumbers;
    public const int SyncInventoryStockPositions = DeletedEntityType.StockPositions;
    public const int SyncInventoryStockSerialBatchNumbers = DeletedEntityType.StockSerialBatchNumbers;
    public const int SyncInventoryStorageLocationStockPositions = DeletedEntityType.StorageLocationStockPositions;
    public const int SyncLogisticsItems = DeletedEntityType.Items;
    public const int SyncLogisticsPurchaseItemPrices = DeletedEntityType.ItemPrices;
    public const int SyncLogisticsSalesItemPrices = DeletedEntityType.ItemPrices;
    public const int SyncLogisticsSupplierItem = DeletedEntityType.ItemAccounts;
    public const int SyncManufacturingBillOfMaterialMaterials = DeletedEntityType.BillOfMaterialMaterials;
    public const int SyncManufacturingBillOfMaterialVersions = DeletedEntityType.BillOfMaterialVersions;
    public const int SyncManufacturingMaterialIssues = DeletedEntityType.RequirementIssues;
    public const int SyncManufacturingShopOrderMaterialPlans = DeletedEntityType.ShopOrderMaterialPlans;
    public const int SyncManufacturingShopOrderPurchasePlanning = DeletedEntityType.ShopOrderPurchasePlanning;
    public const int SyncManufacturingShopOrderRoutingStepPlans = DeletedEntityType.ShopOrderRoutingStepPlans;
    public const int SyncManufacturingShopOrders = DeletedEntityType.ShopOrders;
    public const int SyncManufacturingShopOrderSubOrders = DeletedEntityType.ShopOrderSubOrders;
    public const int SyncPayrollBankAccounts = DeletedEntityType.BankAccounts;
    public const int SyncPayrollEmployees = DeletedEntityType.Employees;
    public const int SyncPayrollEmploymentCLAs = DeletedEntityType.EmploymentCLAs;
    public const int SyncPayrollEmploymentContracts = DeletedEntityType.EmploymentContracts;
    public const int SyncPayrollEmploymentOrganizations = DeletedEntityType.EmploymentOrganizations;
    public const int SyncPayrollEmployments = DeletedEntityType.Employments;
    public const int SyncPayrollEmploymentSalaries = DeletedEntityType.EmploymentSalaries;
    public const int SyncPayrollEmploymentTaxAuthoritiesGeneral = DeletedEntityType.EmploymentTaxAuthoritiesGeneral;
    public const int SyncProjectProjectPlanning = DeletedEntityType.ProjectPlanning;
    public const int SyncProjectProjects = DeletedEntityType.Projects;
    public const int SyncProjectProjectWBS = DeletedEntityType.ProjectWBS;
    public const int SyncProjectTimeCostTransactions = DeletedEntityType.TimeCostTransactions;
    public const int SyncPurchaseOrderPurchaseOrders = DeletedEntityType.PurchaseOrders;
    public const int SyncSalesSalesPriceListVolumeDiscounts = DeletedEntityType.DiscountTables;
    public const int SyncSalesInvoiceSalesInvoices = DeletedEntityType.SalesInvoices;
    public const int SyncSalesOrderGoodsDeliveries = DeletedEntityType.GoodsDeliveries;
    public const int SyncSalesOrderGoodsDeliveryLines = DeletedEntityType.GoodsDeliveryLines;
    public const int SyncSalesOrderSalesOrderHeaders = DeletedEntityType.SalesOrderHeaders;
    public const int SyncSalesOrderSalesOrderLines = DeletedEntityType.SalesOrderLines;
    public const int SyncSubscriptionSubscriptionLines = DeletedEntityType.SubscriptionLines;
    public const int SyncSubscriptionSubscriptions = DeletedEntityType.Subscriptions;
    public const int SyncSyncSyncTimestamp = 300000;

    // System
    public const int SystemAccountantInfo = 310100;
    public const int SystemAllDivisions = 310200;
    public const int SystemAvailableFeatures = 310300;
    public const int SystemDivisions = 310400;
    public const int SystemGetMostRecentlyUsedDivisions = 310500;
    public const int SystemMe = 310600;

    // Users
    public const int UsersUserHasRights = 320100;
    public const int UsersUserRoles = 320200;
    public const int UsersUserRolesPerDivision = 320300;
    public const int UsersUsers = 320400;

    // VAT
    public const int VATVATCodes = 330100;
    public const int VATVatPercentages = 330200;

    // Webhooks
    public const int WebhookSubscriptions = 340100;

    // Workflow
    public const int WorkflowRequestAttachments = 350100;
}