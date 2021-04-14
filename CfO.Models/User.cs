using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfO.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        //public string Title { get; set; }
        //public string MInit { get; set; }
        //public string Position { get; set; }
        //public string Company { get; set; }
        public string PhoneNumber { get; set; }
        //public string Phone2 { get; set; }
        //public string FaxNumber { get; set; }
        public string Email { get; set; }
        public bool Inactive { get; set; }
        //public int? ModBy { get; set; }
        public DateTime? ModDate { get; set; }
        //public string Ip1 { get; set; }
        //public string Ip2 { get; set; }
        public int UserRoleId { get; set; }
        //public bool? Admin { get; set; }
        //public bool? HelpDesk { get; set; }
        //public bool? ShowDlrCost { get; set; }
        //public bool? CanOverrideCustomerPrice { get; set; }
        public bool? ReqPwdChange { get; set; }
        //public int? PrimaryDealerCode { get; set; }
        //public string ProducerCode { get; set; }
        public DateTime? RetireDate { get; set; }
        //public string DmsProducerNumber { get; set; }
        //public bool? CanFinalizeDeals { get; set; }
        //public bool? CanFinalizeDealsForOthers { get; set; }
        //public DateTime? ResetDate { get; set; }
        //public bool? AnalysisReporting { get; set; }
        //public bool? ShowDashboard { get; set; }
        //public bool? CanSetDefaultTemplate { get; set; }
        //public bool? CanDisableDmsPush { get; set; }
        //public DateTime? AddDate { get; set; }
        //public int? AddBy { get; set; }
        //public bool? ChangeLenderFinanceReserve { get; set; }
        //public bool? CanIssueContracts { get; set; }
        //public bool? CanModifyProductInfo { get; set; }
        //public bool? CanModifyAllUsersDeals { get; set; }
        //public int? OrigUserId { get; set; }
        //public bool? IsNewUser { get; set; }
        //public int? DealerUserTitleId { get; set; }
        public bool EmailConfirmed { get; set; }
        public string SecurityStamp { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string PasswordHash { get; set; }
        public bool TwoFactorEnabled { get; set; }
        //public int? CgwUserId { get; set; }
        //public bool? IsIpRestricted { get; set; }
        public DateTime? LastLogin { get; set; }
        //NTRON-1572
        //public bool? MarkedAsRemoved { get; set; }

        ////this is for billing for access and an external accounting system
        ////and is not used internally by the Tronix system.
        //public bool IsLookerUser { get; set; } = false;


        //public int? LookerUserRoleId { get; set; }

        ////This is for the bell release notice icon
        //public DateTime? SawReleaseNoticeDate { get; set; }

        // Reverse navigation

        //public virtual ICollection<AgentDealer> AgentDealers { get; set; }
        //public virtual ICollection<Audit> Audits { get; set; }
        //public virtual ICollection<DealDetail> DealDetails { get; set; }
        //public virtual ICollection<Document> Documents { get; set; }
        //public virtual ICollection<UserDealer> UserDealers { get; set; }
        //public virtual ICollection<UserSellingStyle> UserSellingStyles { get; set; }
        //public virtual ICollection<Transmittal> Transmittals { get; set; }
        //public virtual ICollection<DealJacketOption> DealJacketOptions { get; set; }
        //public virtual ICollection<CasActivatedContract> CasActivatedContracts { get; set; }
        //public virtual ICollection<CasActivatedContract> CasActivatedContractAddBys { get; set; }
        //public virtual ICollection<CasActivatedContract> CasActivatedContractModBys { get; set; }
        //public virtual ICollection<ContractHistory> ContractHistoryEventCreated { get; set; }
        //public virtual ICollection<RouteOneConversation> RouteOneConversations { get; set; }
        //public virtual ICollection<RouteOneMessage> RouteOneMessages { get; set; }
        //public virtual ICollection<MessageDirection> MessageDirections { get; set; }
        //public virtual ICollection<RouteOneMessageType> RouteOneMessageTypes { get; set; }
        //public virtual ICollection<RouteOneMessageSubType> RouteOneMessageSubTypes { get; set; }
        //public virtual ICollection<RouteOneDealStatus> RouteOneDealStatuses { get; set; }
        //public virtual ICollection<DeliveryFailureLog> DeliveryFailureLogs { get; set; }
        //public virtual ICollection<EpicPayLog> EpicPayLogs { get; set; }
        //public virtual ICollection<Dealer> DealersWithBypass { get; set; }


        //// Foreign keys
        //public virtual DealerUserTitle DealerUserTitle { get; set; }
        //public virtual UserRole UserRole { get; set; }
        //public virtual UserAgency UserAgency { get; set; }
        //public virtual UserTpa UserTpa { get; set; }
        //[ForeignKey("PrimaryDealerCode")]
        //public virtual Dealer PrimaryDealer { get; set; }
        //public virtual UserLookerRole UserLookerRole { get; set; }
        //public User()
        //{
        //    ReqPwdChange = false;
        //    CanFinalizeDeals = false;
        //    AnalysisReporting = false;
        //    ShowDashboard = false;
        //    CanSetDefaultTemplate = false;
        //    CanDisableDmsPush = false;
        //    //AgentDealers = new List<AgentDealer>();
        //    //Audits = new List<Audit>();
        //    //DealDetails = new List<DealDetail>();
        //    //Documents = new List<Document>();
        //    //UserDealers = new List<UserDealer>();
        //    //UserSellingStyles = new List<UserSellingStyle>();
        //    CanFinalizeDealsForOthers = false;

        //}

        [NotMapped]
        public string FullName => LName + ", " + FName;
    }
}
