using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfO.Models.DTO
{
    [Serializable]
    public class UserDTO
    {

        public int RoleId { get; set; }

        public int RoleTypeId { get; set; }
        //public int? LookerUserRoleId { get; set; }

        public int Id { get; set; }

        //public int? ImpersonatorId { get; set; }

        public string UserName { get; set; }

        public bool IsAdmin { get; set; }

        //public int AgencyID { get; set; }

        //public string AgencyName { get; set; }

        //public string ProducerCode { get; set; }

        //public string DMSProducerNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string MiddleInitial { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string FaxNumber { get; set; }

        //public int PrimaryDealerCode { get; set; }

        public bool RequirePasswordChange { get; set; }

        public bool RequireProfileUpdate { get; set; }

        public bool IsInactive { get; set; }


        //public int? CgwUserId { get; set; }
        //public int SelectedOwnerGroup { get; set; }

        //public DateTime? SawReleaseNoticeDate { get; set; }



        //public List<SellingStyleSerializableDTO> ActiveSellingStyles { get; set; }
        //public List<SellingStyleSerializableDTO> AllSellingStyles { get; set; }

        ////This represents all the dealers the user is associated with, either through UserDeaers table or AgentDealers
        //public List<int> Dealers { get; set; } = new List<int>();


        //public UserAuthorizations Authorizations { get; set; } = new UserAuthorizations();



    }
}
