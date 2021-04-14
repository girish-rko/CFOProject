using CfO.Models.Interface;
using SharpRaven;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfO.Models.Services
{
    public class LoggerService : ILoggerService
    {
        private static RavenClient _ravenClient;
        //private readonly List<TronixBreadcrumb> _breadcrumbs = new List<TronixBreadcrumb>();

        public static RavenClient RavenClient
        {
            get
            {
                //var sentryDsn = ConfigurationManager.AppSettings["SentryDsn"];
                //if (string.IsNullOrEmpty(sentryDsn)) return null;

                //if (_ravenClient != null) return _ravenClient;

                //RavenClient = new RavenClient(sentryDsn);
                return _ravenClient;
            }
            set
            {
                _ravenClient = value;
            }
        }

        public void Log(Exception exception)
        {
            Log(new CfoEvent(exception));
        }
        public void Log(string message, Exception exception)
        {
            var exceptionWrapper = new Exception(message, exception);
            Log(new CfoEvent(exceptionWrapper));
        }
        public void Log(CfoEvent tronixEvent)
        {
            if (RavenClient != null)
            {
                //if (_breadcrumbs.Any()) tronixEvent.Breadcrumbs = _breadcrumbs.Concat(tronixEvent.Breadcrumbs).ToList();
                //RavenClient.Capture(tronixEvent.Convert());
            }
            else
            {
                //Debug.WriteLine($"----------\n{tronixEvent.Level}: {DateTime.UtcNow.ToShortTimeString()}\n{(tronixEvent.Exception?.ToString() ?? tronixEvent.Message)}\n----------");
            }
        }

    }
}
