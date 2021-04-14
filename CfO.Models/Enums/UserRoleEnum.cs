using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfO.Models.Enums
{
    public enum UserRoleEnum
    {
        Unknown = 0,

        [Description("ProveCFO - Admin")]
        ProvenCfoAdmin = 1,

        //[Description("TPA - Admin")]
        //TpaAdmin = 2,

        //[Description("Agency - Agent")]
        //AgencyAgent = 3,

        [Description("ProveCFO - Accountant")]
        ProvenCfoAccountant = 2,

        [Description("ProveCFO - Partner")]
        ProvenCfoPartner = 3,

        [Description("ProveCFO - Controller")]
        ProvenCfoController = 4,

        //[Description("Dealer - Service With Sales")]
        //DealerServiceWithSales = 14,

        //[Description("Agency - Admin")]
        //AgencyAdmin = 18,

        //[Description("Dealer Group - Admin")]
        //DealerGroupAdmin = 51,

        //[Description("Dealer Group - Accounting")]
        //DealerGroupAccounting = 52,

        //[Description("Dealer - Admin")]
        //DealerAdmin = 53
    }
}
