using CfO.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cfo.Business.DTO
{
    [Serializable]
    public class LookerReportActivityLogDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int EntityId { get; set; }
        //public LookerReportActivityLogEnum LogSource { get; set; }
        public ActivityActionEnum LogAction { get; set; }
        public int? OldReportingLevel { get; set; }
        public int? NewReportingLevel { get; set; }
        public int? OldUserReportingRole { get; set; }
        public int? NewUserReportingRole { get; set; }

        public int? OldValue { get; set; }
        public int? NewValue { get; set; }
    }
}
