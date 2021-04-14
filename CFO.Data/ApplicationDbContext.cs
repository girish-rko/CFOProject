using CfO.Models;
using CfO.Models.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFO.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ISessionProviderService _sessionProviderService;

        static ApplicationDbContext()
        {
            Database.SetInitializer<ApplicationDbContext>(null);
        }

        //NOTE: This is used to create migrations
        public ApplicationDbContext() : base("DefaultConnection")
        {
            
        }

        //NOTE: This should be used by AutoFac for DI
        public ApplicationDbContext(ISessionProviderService sessionProviderService) : base(ConfigurationManager.AppSettings["DefaultConnection"])
        {
            string connection = ConfigurationManager.AppSettings["DefaultConnection"];
            _sessionProviderService = sessionProviderService;

            SetCommandTimeOut();
            

            //System.Data.Entity.Infrastructure.Interception.DbInterception.Add(new ActivityTrackerInterceptor(sessionProviderService));
        }

        public ApplicationDbContext(string connectionString)
            : base(connectionString)
        {
            SetCommandTimeOut();
            

        }

        public ApplicationDbContext(string connectionString, DbCompiledModel model)
            : base(connectionString, model)
        {
            SetCommandTimeOut();
            
        }

        public ApplicationDbContext(System.Data.Common.DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
            SetCommandTimeOut();
            
        }

        public ApplicationDbContext(System.Data.Common.DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
            SetCommandTimeOut();
            
        }
        public void SetCommandTimeOut(int? seconds = null)
        {
            seconds = seconds ?? 30;
            var objectContext = (this as IObjectContextAdapter).ObjectContext;
            objectContext.CommandTimeout = seconds;
        }
        //public override int SaveChanges()
        //{
        //    try
        //    {
        //        return base.SaveChanges();
        //    }
        //    catch (DbEntityValidationException exception)
        //    {
        //        try
        //        {
        //            var entityValidation = new DbEntityValidationExceptionExtra();

        //            foreach (var validationResult in exception.EntityValidationErrors)
        //            {
        //                var validationResultEntry = validationResult.Entry;

        //                var entityError = new DbEntityValidationExceptionExtraError
        //                {
        //                    EntityType = validationResultEntry.Entity.GetType().Name,
        //                    State = validationResultEntry.State
        //                };

        //                foreach (var validationError in validationResult.ValidationErrors)
        //                {
        //                    entityError.Validations.Add(new DbEntityValidationExceptionExtraValidation
        //                    {
        //                        Property = validationError.PropertyName,
        //                        Value = validationResultEntry.CurrentValues.GetValue<object>(validationError
        //                            .PropertyName),
        //                        Error = validationError.ErrorMessage
        //                    });
        //                }
        //            }

        //            exception.Data.Add("DbEntityValidationException",
        //                JsonConvert.SerializeObject(entityValidation, EfJsonSerializerSettings.Settings));
        //        }
        //        catch { } //NOTE: The reason for the catch is to provide a clear single error message

        //        throw;

        //    }
        //    catch (DbUpdateException exception)
        //    {
        //        var details = JsonConvert.SerializeObject(exception.Entries.FirstOrDefault().Entity, EfJsonSerializerSettings.Settings);
        //        exception.Data.Add("DbUpdateException", details);
        //        throw;
        //    }
        //    // ReSharper disable once RedundantCatchClause
        //    // ReSharper disable once RedundantCatchClause
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        public void Detach(object entity)
        {
            ((IObjectContextAdapter)this).ObjectContext.Detach(entity);
        }
        //public DbSet<Agency> Agencies { get; set; }
        //public DbSet<AgentDealer> AgentDealers { get; set; }
        //public DbSet<Audit> Audits { get; set; }
        //public DbSet<ContractHistoryReason> CancelReasons { get; set; }
        //public DbSet<CasActivatedContract> CasActivatedContracts { get; set; }
        //public DbSet<ContractHistory> ContractHistories { get; set; }
        //public DbSet<ContractHistoryType> ContractHistoryTypes { get; set; }
        //public DbSet<DataChangeLog> DataChangeLogs { get; set; }
        //public DbSet<DeliveryFailureLog> DeliveryFailureLogs { get; set; }
        //public DbSet<DeliveryFailureContractInfo> DeliveryFailureContractInfos { get; set; }
        //public DbSet<CasPaymentProcessor> CasPaymentProcessors { get; set; }
        //public DbSet<CasRequestLog> CasRequestLogs { get; set; }
        //public DbSet<ContractAdminSystem> ContractAdminSystems { get; set; }
        //public DbSet<ContractAdminSystemCoverageType> ContractAdminSystemCoverageTypes { get; set; }
        //public DbSet<ContractAdminSystemIndustryProductType> ContractAdminSystemIndustryProductTypes { get; set; }
        //public DbSet<ContractStatus> ContractStatuses { get; set; }
        //public DbSet<Customer> Customers { get; set; }
        //public DbSet<DealAuditLog> DealAuditLogs { get; set; }
        //public DbSet<DealDeliverySelection> DealDeliverySelections { get; set; }
        //public DbSet<DealDeliverySelectionSurcharge> DealDeliverySelectionSurcharges { get; set; }
        //public DbSet<DealDetail> DealDetails { get; set; }
        //public DbSet<Dealer> Dealers { get; set; }
        //public DbSet<DealerAdhocProduct> DealerAdhocProducts { get; set; }
        //public DbSet<DealerAdhocRate> DealerAdhocRates { get; set; }
        //public DbSet<AgencyDealer> DealerAgencies { get; set; }
        //public DbSet<DealerDocumentTemplate> DealerDocumentTemplates { get; set; }
        //public DbSet<DealerIndustryProduct> DealerIndustryProducts { get; set; }
        //public DbSet<DealerLender> DealerLenders { get; set; }
        //public DbSet<DealerPaymentProcessor> DealerPaymentProcessors { get; set; }
        //public DbSet<DealerSellingStyle> DealerSellingStyles { get; set; }
        //public DbSet<DealerTpaConfiguration> DealerTpaConfigurations { get; set; }
        //public DbSet<DealerUserTitle> DealerUserTitles { get; set; }
        //public DbSet<Document> Documents { get; set; }
        //public DbSet<DealStatus> DealStatuses { get; set; }
        //public DbSet<DmsDealStatus> DmsDealStatuses { get; set; }
        //public DbSet<DealJacketType> DealJacketTypes { get; set; }
        //public DbSet<DocSysFieldMap> DocSysFieldMaps { get; set; }
        //public DbSet<DocumentType> DocumentTypes { get; set; }
        //public DbSet<DocumentCategory> DocumentCategories { get; set; }
        //public DbSet<DocSysTemplate> DocSysTemplates { get; set; }
        //public DbSet<DocSysTemplateForm> DocSysTemplateForms { get; set; }
        //public DbSet<DocSysTemplateState> DocSysTemplateStates { get; set; }
        //public DbSet<DocSysOwner> DocSysOwners { get; set; }
        //public DbSet<DocSysOwnerType> DocSysOwnerTypes { get; set; }
        //public DbSet<DocumentTemplate> DocumentTemplates { get; set; }
        //public DbSet<DocSysTemplateFormFieldAudit> DocSysTemplateFormFieldAudits { get; set; }
        //public DbSet<DocumentTemplateField> DocumentTemplateFields { get; set; }
        //public DbSet<DocumentTemplateLanguageMapping> DocumentTemplateLanguageMappings { get; set; }
        //public DbSet<DocuSignEnvelope> DocuSignEnvelopes { get; set; }
        //public DbSet<FreeFactoryMaintenanceDefault> FreeFactoryMaintenanceDefaults { get; set; }
        //public DbSet<FreeFactoryMaintenance> FreeFactoryMaintenances { get; set; }
        //public DbSet<IndustryProductType> IndustryProductTypes { get; set; }
        //public DbSet<IndustryProductTypeDocumentTemplate> IndustryProductTypeDocumentTemplates { get; set; }
        //public DbSet<Language> Languages { get; set; }
        //public DbSet<Lender> Lenders { get; set; }
        //public DbSet<LenderDocumentTemplate> LenderDocumentTemplates { get; set; }
        //public DbSet<ManualVehicleEntry> ManualVehicleEntries { get; set; }
        //public DbSet<MenuCatName> MenuCatNames { get; set; }
        //public DbSet<MenuItem> MenuItems { get; set; }
        //public DbSet<MenuTemplate> MenuTemplates { get; set; }
        //public DbSet<OwnerGroup> OwnerGroups { get; set; }
        //public DbSet<OwnerGroupSellingStyle> OwnerGroupSellingStyles { get; set; }
        //public DbSet<PaymentProcessor> PaymentProcessors { get; set; }
        //public DbSet<PaymentTransaction> PaymentTransactions { get; set; }
        //public DbSet<PaymentTransactionParty> PaymentTransactionParties { get; set; }
        //public DbSet<PaymentTransactionPartyProcessor> PaymentTransactionPartyProcessors { get; set; }
        //public DbSet<PriceOverrideReason> PriceOverrideReasons { get; set; }
        //public DbSet<RouteOneMessage> RouteOneMessages { get; set; }
        //public DbSet<MessageDirection> MessageDirections { get; set; }
        //public DbSet<RouteOneDealStatus> RouteOneDealStatuses { get; set; }
        //public DbSet<RouteOneMessageType> RouteOneMessageTypes { get; set; }
        //public DbSet<RouteOneMessageSubType> RouteOneMessageSubTypes { get; set; }
        //public DbSet<RouteOneConversation> RouteOneConversations { get; set; }
        //public DbSet<SellingStyle> SellingStyles { get; set; }
        //public DbSet<State> States { get; set; }
        //public DbSet<TermOption> TermOptions { get; set; }
        //public DbSet<TcarRateBuildWip> TcarRateBuildWips { get; set; }
        //public DbSet<TcarDealerRateWip> TcarDealerRateWips { get; set; }
        //public DbSet<TcarDealerSurchargeWip> TcarDealerSurchargeWips { get; set; }
        //public DbSet<TcarSurchargeWip> TcarSurchargeWips { get; set; }
        //public DbSet<TcarBaseReserveWip> TcarBaseReserveWips { get; set; }
        //public DbSet<TcarRateWip> TcarRateWips { get; set; }
        //public DbSet<TcarDrbpWip> TcarDrbpWips { get; set; }
        //public DbSet<Tpa> Tpas { get; set; }
        //public DbSet<TpaAgency> TpaAgencies { get; set; }
        //public DbSet<TpaContractAdminSystem> TpaContractAdminSystems { get; set; }
        //public DbSet<TpaDealRatesCache> TpaDealRatesCaches { get; set; }
        //public DbSet<TpaDealRatesCacheHeader> TpaDealRatesCacheHeaders { get; set; }
        //public DbSet<TpaDealSurchargeRatesCache> TpaDealSurchargeRatesCaches { get; set; }
        //public DbSet<TpaPaymentProcessor> TpaPaymentProcessors { get; set; }
        //public DbSet<TpaPaymentProcessorStatus> TpaPaymentProcessorStatuses { get; set; }
        //public DbSet<Transmittal> Transmittals { get; set; }
        public DbSet<User> Users { get; set; }
        //public DbSet<UserAgency> UserAgencies { get; set; }
        //public DbSet<UserDealer> UserDealers { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserRoleType> UserRoleTypes { get; set; }
        //public DbSet<UserModType> UserModTypes { get; set; }
        //public DbSet<UserProfileMod> UserProfileMods { get; set; }
        //public DbSet<UserSellingStyle> UserSellingStyles { get; set; }
        //public DbSet<UserTpa> UserTpas { get; set; }
        //public DbSet<VehicleFwDefault> VehicleFwDefaults { get; set; }
        //public DbSet<VehicleFwDefaultLog> VehicleFwDefaultLogs { get; set; }
        //public DbSet<VehicleFwInfo> VehicleFwInfos { get; set; }
        //public DbSet<VehicleFwInfoPending> VehicleFwInfoPendings { get; set; }
        //public DbSet<VehicleManufacturer> VehicleManufacturers { get; set; }
        //public DbSet<VehicleTradeIn> VehicleTradeIns { get; set; }
        //public DbSet<Vin> Vins { get; set; }
        //public DbSet<VinError> VinErrors { get; set; }
        //public DbSet<DealJacketOption> DealJacketOptions { get; set; }
        //public DbSet<EpicPayLog> EpicPayLogs { get; set; }
        //public DbSet<TpaAchTransmittalFee> TpaAchTransmittalFees { get; set; }
        //public DbSet<ReleaseVersionDetails> ReleaseVersionDetails { get; set; }
        //public DbSet<DealCatTotals> DealCatTotals { get; set; }
        //public DbSet<DealerCoverageTypeOverride> DealerCoverageTypeOverrides { get; set; }
        //public DbSet<BackgroundJobConfig> BackgroundJobConfigs { get; set; }
        //public DbSet<BackgroundJobEndReason> BackgroundJobEndReasons { get; set; }
        //public DbSet<BackgroundJobLog> BackgroundJobLogs { get; set; }
        //public DbSet<DealDmsSyncLog> DealDmsSyncLogs { get; set; }
        //public DbSet<FAndIReportDmsDealerGroup> FAndIReportDmsDealerGroups { get; set; }
        //public DbSet<TcarCgwServiceSwitch> TcarCgwServiceSwitch { get; set; }
        //public DbSet<UserLookerRole> UserLookerRole { get; set; }
        //public DbSet<LookerReportActivityLog> LookerReportActivityLog { get; set; }
        //public DbSet<TransmittalPaymentType> TransmittalPaymentTypes { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Configurations.Add(new AgencyConfiguration());
            //modelBuilder.Configurations.Add(new AgentDealerConfiguration());
            //modelBuilder.Configurations.Add(new AuditConfiguration());
            //modelBuilder.Configurations.Add(new CancelReasonConfiguration());
            //modelBuilder.Configurations.Add(new CasActivatedContractConfiguration());
            //modelBuilder.Configurations.Add(new ContractHistoryConfiguration());
            //modelBuilder.Configurations.Add(new ContractHistorySourceConfiguration());
            //modelBuilder.Configurations.Add(new ContractHistoryTypeConfiguration());
            //modelBuilder.Configurations.Add(new DeliveryFailureLogConfiguration());
            //modelBuilder.Configurations.Add(new DeliveryFailureContractInfoConfiguration());
            //modelBuilder.Configurations.Add(new CasPaymentProcessorConfiguration());
            //modelBuilder.Configurations.Add(new CasRequestLogConfiguration());
            //modelBuilder.Configurations.Add(new ContractAdminSystemConfiguration());
            //modelBuilder.Configurations.Add(new ContractAdminSystemCoverageTypeConfiguration());
            //modelBuilder.Configurations.Add(new ContractAdminSystemIndustryProductTypeConfiguration());
            //modelBuilder.Configurations.Add(new CustomerConfiguration());
            //modelBuilder.Configurations.Add(new DealAuditLogConfiguration());
            //modelBuilder.Configurations.Add(new DealDeliverySelectionConfiguration());
            //modelBuilder.Configurations.Add(new DealDeliverySelectionSurchargeConfiguration());
            //modelBuilder.Configurations.Add(new DealDetailConfiguration());
            //modelBuilder.Configurations.Add(new DealerConfiguration());
            //modelBuilder.Configurations.Add(new DealerAdhocProductConfiguration());
            //modelBuilder.Configurations.Add(new DealerAdhocRateConfiguration());
            //modelBuilder.Configurations.Add(new AgencyDealerConfiguration());
            //modelBuilder.Configurations.Add(new DealerDocumentTemplateConfiguration());
            //modelBuilder.Configurations.Add(new DealerIndustryProductConfiguration());
            //modelBuilder.Configurations.Add(new DealerPaymentProcessorConfiguration());
            //modelBuilder.Configurations.Add(new DealerSellingStyleConfiguration());
            //modelBuilder.Configurations.Add(new DealerTpaConfigurationConfiguration());
            //modelBuilder.Configurations.Add(new DealerUserTitleConfiguration());
            //modelBuilder.Configurations.Add(new DocumentConfiguration());
            //modelBuilder.Configurations.Add(new DmsDealStatuConfiguration());
            //modelBuilder.Configurations.Add(new DealJacketTypeConfiguration());
            //modelBuilder.Configurations.Add(new DocSysFieldMapConfiguration());
            //modelBuilder.Configurations.Add(new DocumentCategoryConfiguration());
            //modelBuilder.Configurations.Add(new DocumentTypeConfiguration());
            //modelBuilder.Configurations.Add(new DocSysOwnerConfiguration());
            //modelBuilder.Configurations.Add(new DocSysOwnerTypeConfiguration());
            //modelBuilder.Configurations.Add(new DocSysTemplateConfiguration());
            //modelBuilder.Configurations.Add(new DocSysTemplateFormFieldAuditConfiguration());
            //modelBuilder.Configurations.Add(new DocSysTemplateFormConfiguration());
            //modelBuilder.Configurations.Add(new DocSysTemplateStateConfiguration());
            //modelBuilder.Configurations.Add(new DocumentTemplateConfiguration());
            //modelBuilder.Configurations.Add(new DocumentTemplateFieldConfiguration());
            //modelBuilder.Configurations.Add(new DocumentTemplateLanguageMappingConfiguration());
            //modelBuilder.Configurations.Add(new DocuSignEnvelopeConfiguration());
            //modelBuilder.Configurations.Add(new FreeFactoryMaintDefaultConfiguration());
            //modelBuilder.Configurations.Add(new FreeFactoryMaintenanceConfiguration());
            //modelBuilder.Configurations.Add(new IndustryProductTypeConfiguration());
            //modelBuilder.Configurations.Add(new IndustryProductTypeDocumentTemplateConfiguration());
            //modelBuilder.Configurations.Add(new LanguageConfiguration());
            //modelBuilder.Configurations.Add(new LenderConfiguration());
            //modelBuilder.Configurations.Add(new LenderDocumentTemplateConfiguration());
            //modelBuilder.Configurations.Add(new ManualVehicleEntryConfiguration());
            //modelBuilder.Configurations.Add(new MenuCatNameConfiguration());
            //modelBuilder.Configurations.Add(new MenuItemConfiguration());
            //modelBuilder.Configurations.Add(new MenuTemplateConfiguration());
            //modelBuilder.Configurations.Add(new OwnerGroupConfiguration());
            //modelBuilder.Configurations.Add(new OwnerGroupSellingStyleConfiguration());
            //modelBuilder.Configurations.Add(new PaymentProcessorConfiguration());
            //modelBuilder.Configurations.Add(new PaymentTransactionConfiguration());
            //modelBuilder.Configurations.Add(new PaymentTransactionPartyConfiguration());
            //modelBuilder.Configurations.Add(new PaymentTransactionPartyProcessorConfiguration());
            //modelBuilder.Configurations.Add(new SellingStyleConfiguration());
            //modelBuilder.Configurations.Add(new StateConfiguration());
            //modelBuilder.Configurations.Add(new TermOptionConfiguration());
            //modelBuilder.Configurations.Add(new TpaConfiguration());
            //modelBuilder.Configurations.Add(new TpaAgencyConfiguration());
            //modelBuilder.Configurations.Add(new TpaContractAdminSystemConfiguration());
            //modelBuilder.Configurations.Add(new TpaDealRatesCacheConfiguration());
            //modelBuilder.Configurations.Add(new TpaDealRatesCacheHeaderConfiguration());
            //modelBuilder.Configurations.Add(new TpaDealSurchargeRatesCacheConfiguration());
            //modelBuilder.Configurations.Add(new TpaPaymentProcessorConfiguration());
            //modelBuilder.Configurations.Add(new TpaPaymentProcessorStatuConfiguration());
            //modelBuilder.Configurations.Add(new TransmittalConfiguration());
            //modelBuilder.Configurations.Add(new UserConfiguration());
            //modelBuilder.Configurations.Add(new UserAgencyConfiguration());
            //modelBuilder.Configurations.Add(new UserDealerConfiguration());
            //modelBuilder.Configurations.Add(new UserRoleConfiguration());
            //modelBuilder.Configurations.Add(new UserRoleTypeConfiguration());
            //modelBuilder.Configurations.Add(new UserModTypeConfiguration());
            //modelBuilder.Configurations.Add(new UserProfileModConfiguration());
            //modelBuilder.Configurations.Add(new UserSellingStyleConfiguration());
            //modelBuilder.Configurations.Add(new UserTpaConfiguration());
            //modelBuilder.Configurations.Add(new VehicleFwDefaultConfiguration());
            //modelBuilder.Configurations.Add(new VehicleFwDefaultLogConfiguration());
            //modelBuilder.Configurations.Add(new VehicleFwInfoConfiguration());
            //modelBuilder.Configurations.Add(new VehicleFwInfoPendingConfiguration());
            //modelBuilder.Configurations.Add(new VehicleManfacturerConfiguration());
            //modelBuilder.Configurations.Add(new VehicleTradeInConfiguration());
            //modelBuilder.Configurations.Add(new VinConfiguration());
            //modelBuilder.Configurations.Add(new VinErrorConfiguration());
            //modelBuilder.Configurations.Add(new ContractStatusConfiguration());
            //modelBuilder.Configurations.Add(new DealJacketOptionConfiguration());
            //modelBuilder.Configurations.Add(new RouteOneMessageConfiguration());
            //modelBuilder.Configurations.Add(new MessageDirectionConfiguration());
            //modelBuilder.Configurations.Add(new RouteOneMessageTypeConfiguration());
            //modelBuilder.Configurations.Add(new RouteOneMessageSubTypeConfiguration());
            //modelBuilder.Configurations.Add(new RouteOneConversationConfiguration());
            //modelBuilder.Configurations.Add(new RouteOneDealStatusConfiguration());
            //modelBuilder.Configurations.Add(new DealStatusConfiguration());
            //modelBuilder.Configurations.Add(new PriceOverrideReasonConfiguration());
            //modelBuilder.Configurations.Add(new DataChangeLogConfiguration());
            //modelBuilder.Configurations.Add(new DealerLenderConfiguration());
            //modelBuilder.Configurations.Add(new TcarRateBuildWipConfiguration());
            //modelBuilder.Configurations.Add(new TcarRateWipConfiguration());
            //modelBuilder.Configurations.Add(new TcarDrbpWipConfiguration());
            //modelBuilder.Configurations.Add(new TcarDealerRateWipConfiguration());
            //modelBuilder.Configurations.Add(new TcarDealerSurchargeWipConfiguration());
            //modelBuilder.Configurations.Add(new EpicPayLogConfiguration());
            //modelBuilder.Configurations.Add(new TcarSurchargeWipConfiguration());
            //modelBuilder.Configurations.Add(new TcarBaseReserveWipConfiguration());
            //modelBuilder.Configurations.Add(new TpaAchTransmittalFeeConfiguration());

            //modelBuilder.Configurations.Add(new ReleaseVersionDetailConfiguration());
            //modelBuilder.Configurations.Add(new DealCatTotalConfiguration());
            //modelBuilder.Configurations.Add(new DealerCoverageTypeOverrideConfiguration());
            //modelBuilder.Configurations.Add(new BackgroundJobConfigConfiguration());
            //modelBuilder.Configurations.Add(new BackgroundJobEndReasonConfiguration());
            //modelBuilder.Configurations.Add(new BackgroundJobLogConfiguration());
            //modelBuilder.Configurations.Add(new DealDmsSyncLogConfiguration());
            //modelBuilder.Configurations.Add(new FAndIReportDmsDealerGroupConfiguration());
            //modelBuilder.Configurations.Add(new TcarCgwServiceSwitchConfiguration());
            //modelBuilder.Configurations.Add(new UserLookerRolesConfiguration());
            //modelBuilder.Configurations.Add(new LookerReportActivityLogConfiguration());
        }

        public static DbModelBuilder CreateModel(DbModelBuilder modelBuilder, string schema)
        {
            //modelBuilder.Configurations.Add(new AgencyConfiguration(schema));
            //modelBuilder.Configurations.Add(new AgentDealerConfiguration(schema));
            //modelBuilder.Configurations.Add(new AuditConfiguration(schema));
            //modelBuilder.Configurations.Add(new CancelReasonConfiguration(schema));
            //modelBuilder.Configurations.Add(new CasActivatedContractConfiguration(schema));
            //modelBuilder.Configurations.Add(new ContractHistoryConfiguration(schema));
            //modelBuilder.Configurations.Add(new ContractHistorySourceConfiguration());
            //modelBuilder.Configurations.Add(new ContractHistoryTypeConfiguration(schema));
            //modelBuilder.Configurations.Add(new DeliveryFailureLogConfiguration(schema));
            //modelBuilder.Configurations.Add(new DeliveryFailureContractInfoConfiguration(schema));
            //modelBuilder.Configurations.Add(new CasPaymentProcessorConfiguration(schema));
            //modelBuilder.Configurations.Add(new CasRequestLogConfiguration(schema));
            //modelBuilder.Configurations.Add(new ContractAdminSystemConfiguration(schema));
            //modelBuilder.Configurations.Add(new ContractAdminSystemCoverageTypeConfiguration(schema));
            //modelBuilder.Configurations.Add(new ContractAdminSystemIndustryProductTypeConfiguration(schema));
            //modelBuilder.Configurations.Add(new CustomerConfiguration(schema));
            //modelBuilder.Configurations.Add(new DealAuditLogConfiguration(schema));
            //modelBuilder.Configurations.Add(new DealDeliverySelectionConfiguration(schema));
            //modelBuilder.Configurations.Add(new DealDeliverySelectionSurchargeConfiguration(schema));
            //modelBuilder.Configurations.Add(new DealDetailConfiguration(schema));
            //modelBuilder.Configurations.Add(new DealerConfiguration(schema));
            //modelBuilder.Configurations.Add(new DealerAdhocProductConfiguration(schema));
            //modelBuilder.Configurations.Add(new DealerAdhocRateConfiguration(schema));
            //modelBuilder.Configurations.Add(new AgencyDealerConfiguration(schema));
            //modelBuilder.Configurations.Add(new DealerDocumentTemplateConfiguration(schema));
            //modelBuilder.Configurations.Add(new DealerIndustryProductConfiguration(schema));
            //modelBuilder.Configurations.Add(new DealerPaymentProcessorConfiguration(schema));
            //modelBuilder.Configurations.Add(new DealerSellingStyleConfiguration(schema));
            //modelBuilder.Configurations.Add(new DealerTpaConfigurationConfiguration(schema));
            //modelBuilder.Configurations.Add(new DealerUserTitleConfiguration(schema));
            //modelBuilder.Configurations.Add(new DocumentConfiguration(schema));
            //modelBuilder.Configurations.Add(new DmsDealStatuConfiguration(schema));
            //modelBuilder.Configurations.Add(new DealJacketTypeConfiguration(schema));
            //modelBuilder.Configurations.Add(new DocSysFieldMapConfiguration(schema));
            //modelBuilder.Configurations.Add(new DocumentCategoryConfiguration(schema));
            //modelBuilder.Configurations.Add(new DocumentTypeConfiguration(schema));
            //modelBuilder.Configurations.Add(new DocSysOwnerConfiguration(schema));
            //modelBuilder.Configurations.Add(new DocSysOwnerTypeConfiguration(schema));
            //modelBuilder.Configurations.Add(new DocSysTemplateConfiguration(schema));
            //modelBuilder.Configurations.Add(new DocSysTemplateFormFieldAuditConfiguration(schema));
            //modelBuilder.Configurations.Add(new DocSysTemplateFormConfiguration(schema));
            //modelBuilder.Configurations.Add(new DocSysTemplateStateConfiguration(schema));
            //modelBuilder.Configurations.Add(new DocumentTemplateConfiguration(schema));
            //modelBuilder.Configurations.Add(new DocumentTemplateFieldConfiguration(schema));
            //modelBuilder.Configurations.Add(new DocumentTemplateLanguageMappingConfiguration(schema));
            //modelBuilder.Configurations.Add(new DocuSignEnvelopeConfiguration(schema));
            //modelBuilder.Configurations.Add(new FreeFactoryMaintDefaultConfiguration(schema));
            //modelBuilder.Configurations.Add(new FreeFactoryMaintenanceConfiguration(schema));
            //modelBuilder.Configurations.Add(new IndustryProductTypeConfiguration(schema));
            //modelBuilder.Configurations.Add(new IndustryProductTypeDocumentTemplateConfiguration(schema));
            //modelBuilder.Configurations.Add(new LanguageConfiguration(schema));
            //modelBuilder.Configurations.Add(new LenderConfiguration(schema));
            //modelBuilder.Configurations.Add(new LenderDocumentTemplateConfiguration(schema));
            //modelBuilder.Configurations.Add(new ManualVehicleEntryConfiguration(schema));
            //modelBuilder.Configurations.Add(new MenuCatNameConfiguration(schema));
            //modelBuilder.Configurations.Add(new MenuItemConfiguration(schema));
            //modelBuilder.Configurations.Add(new MenuTemplateConfiguration(schema));
            //modelBuilder.Configurations.Add(new OwnerGroupConfiguration(schema));
            //modelBuilder.Configurations.Add(new OwnerGroupSellingStyleConfiguration(schema));
            //modelBuilder.Configurations.Add(new PaymentProcessorConfiguration(schema));
            //modelBuilder.Configurations.Add(new PaymentTransactionConfiguration(schema));
            //modelBuilder.Configurations.Add(new PaymentTransactionPartyConfiguration(schema));
            //modelBuilder.Configurations.Add(new PaymentTransactionPartyProcessorConfiguration(schema));
            //modelBuilder.Configurations.Add(new SellingStyleConfiguration(schema));
            //modelBuilder.Configurations.Add(new StateConfiguration(schema));
            //modelBuilder.Configurations.Add(new TermOptionConfiguration(schema));
            //modelBuilder.Configurations.Add(new TpaConfiguration(schema));
            //modelBuilder.Configurations.Add(new TpaAgencyConfiguration(schema));
            //modelBuilder.Configurations.Add(new TpaContractAdminSystemConfiguration(schema));
            //modelBuilder.Configurations.Add(new TpaDealRatesCacheConfiguration(schema));
            //modelBuilder.Configurations.Add(new TpaDealRatesCacheHeaderConfiguration(schema));
            //modelBuilder.Configurations.Add(new TpaDealSurchargeRatesCacheConfiguration(schema));
            //modelBuilder.Configurations.Add(new TpaPaymentProcessorConfiguration(schema));
            //modelBuilder.Configurations.Add(new TpaPaymentProcessorStatuConfiguration(schema));
            //modelBuilder.Configurations.Add(new TransmittalConfiguration(schema));
            //modelBuilder.Configurations.Add(new UserConfiguration(schema));
            //modelBuilder.Configurations.Add(new UserAgencyConfiguration(schema));
            //modelBuilder.Configurations.Add(new UserDealerConfiguration(schema));
            //modelBuilder.Configurations.Add(new UserRoleConfiguration(schema));
            //modelBuilder.Configurations.Add(new UserRoleTypeConfiguration(schema));
            //modelBuilder.Configurations.Add(new UserModTypeConfiguration(schema));
            //modelBuilder.Configurations.Add(new UserProfileModConfiguration(schema));
            //modelBuilder.Configurations.Add(new UserSellingStyleConfiguration(schema));
            //modelBuilder.Configurations.Add(new UserTpaConfiguration(schema));
            //modelBuilder.Configurations.Add(new VehicleFwDefaultConfiguration(schema));
            //modelBuilder.Configurations.Add(new VehicleFwDefaultLogConfiguration(schema));
            //modelBuilder.Configurations.Add(new VehicleFwInfoConfiguration(schema));
            //modelBuilder.Configurations.Add(new VehicleFwInfoPendingConfiguration(schema));
            //modelBuilder.Configurations.Add(new VehicleManfacturerConfiguration(schema));
            //modelBuilder.Configurations.Add(new VehicleTradeInConfiguration(schema));
            //modelBuilder.Configurations.Add(new VinConfiguration(schema));
            //modelBuilder.Configurations.Add(new VinErrorConfiguration(schema));
            //modelBuilder.Configurations.Add(new ContractStatusConfiguration(schema));
            //modelBuilder.Configurations.Add(new DealJacketOptionConfiguration(schema));
            //modelBuilder.Configurations.Add(new DealStatusConfiguration(schema));
            //modelBuilder.Configurations.Add(new RouteOneMessageConfiguration(schema));
            //modelBuilder.Configurations.Add(new MessageDirectionConfiguration(schema));
            //modelBuilder.Configurations.Add(new RouteOneMessageSubTypeConfiguration(schema));
            //modelBuilder.Configurations.Add(new RouteOneMessageTypeConfiguration(schema));
            //modelBuilder.Configurations.Add(new RouteOneConversationConfiguration(schema));
            //modelBuilder.Configurations.Add(new RouteOneDealStatusConfiguration(schema));
            //modelBuilder.Configurations.Add(new PriceOverrideReasonConfiguration(schema));
            //modelBuilder.Configurations.Add(new DataChangeLogConfiguration(schema));
            //modelBuilder.Configurations.Add(new DealerLenderConfiguration(schema));
            //modelBuilder.Configurations.Add(new TcarRateBuildWipConfiguration(schema));
            //modelBuilder.Configurations.Add(new TcarRateWipConfiguration(schema));
            //modelBuilder.Configurations.Add(new TcarDrbpWipConfiguration(schema));
            //modelBuilder.Configurations.Add(new TcarDealerRateWipConfiguration(schema));
            //modelBuilder.Configurations.Add(new TcarDealerSurchargeWipConfiguration(schema));
            //modelBuilder.Configurations.Add(new TcarSurchargeWipConfiguration(schema));
            //modelBuilder.Configurations.Add(new EpicPayLogConfiguration(schema));
            //modelBuilder.Configurations.Add(new TcarBaseReserveWipConfiguration(schema));
            //modelBuilder.Configurations.Add(new ReleaseVersionDetailConfiguration(schema));
            //modelBuilder.Configurations.Add(new DealCatTotalConfiguration(schema));
            //modelBuilder.Configurations.Add(new DealerCoverageTypeOverrideConfiguration(schema));
            //modelBuilder.Configurations.Add(new TpaAchTransmittalFeeConfiguration(schema));
            //modelBuilder.Configurations.Add(new BackgroundJobConfigConfiguration(schema));
            //modelBuilder.Configurations.Add(new BackgroundJobEndReasonConfiguration(schema));
            //modelBuilder.Configurations.Add(new BackgroundJobLogConfiguration(schema));
            //modelBuilder.Configurations.Add(new DealDmsSyncLogConfiguration(schema));
            //modelBuilder.Configurations.Add(new FAndIReportDmsDealerGroupConfiguration(schema));
            //modelBuilder.Configurations.Add(new TcarCgwServiceSwitchConfiguration(schema));
            //modelBuilder.Configurations.Add(new UserLookerRolesConfiguration());
            //modelBuilder.Configurations.Add(new LookerReportActivityLogConfiguration());

            return modelBuilder;
        }
    }
}
