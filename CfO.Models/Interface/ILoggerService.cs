using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfO.Models.Interface
{
    public interface ILoggerService
    {
        void Log(Exception exception);
        void Log(string message, Exception exception);

        //void Log(string message, TronixErrorLevel tronixErrorLevel = TronixErrorLevel.Debug);
        //void Log(TronixEvent tronixEvent);
        //void AddTrail(TronixBreadcrumb breadcrumb);
        //void AddTrail(string message, TronixBreadcrumbLevel level = TronixBreadcrumbLevel.Info, [System.Runtime.CompilerServices.CallerMemberName] string category = "");

    }
}
