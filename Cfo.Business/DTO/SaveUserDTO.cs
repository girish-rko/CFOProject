using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cfo.Business.DTO
{
    public class SaveUserDTO
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Phone2 { get; set; }
        public string FaxNumber { get; set; }
        public string Email { get; set; }
        public int ModBy { get; set; }
        public DateTime ModDate { get; set; }
        public int UserRoleId { get; set; }
        public bool CanSeeDealerCost { get; set; }
        public bool CanOverrideCustomerPrice { get; set; }
        public bool CanFinalizeDeals { get; set; }
        public bool CanFinalizeDealsForOthers { get; set; }
        public bool AnalysisReporting { get; set; }
        public bool ChangeLenderFinanceReserve { get; set; }
        public bool CanIssueContracts { get; set; }
        public bool CanModifyAllUsersDeals { get; set; }
        public bool CanDisableDmsPush { get; set; }
        public bool CanSetDefaultTemplate { get; set; }
        public int AssociatedTPAId { get; set; }
        public bool IsIpRestricted { get; set; } = false;
        public int? AgencyId { get; set; }

        public string ProducerCode { get; set; }

        public string DMSProducerNumber { get; set; }
        //this is for billing for access and an external accouting system
        //and is not used internally by the Tronix system.
        public bool IsLookerUser { get; set; } = false;
        public int? LookerUserRoleId { get; set; }

        //Agency Users
        //public AgentDealerAssociationDTO AgentDealerAssociations { get; set; } = new AgentDealerAssociationDTO();
        //public IEnumerable<SellingStyleDTO> SellingStyles { get; set; }


        ////Dealer Users
        //public UserDealerAssociationDTO DealerAssociations { get; set; }
    }
}
