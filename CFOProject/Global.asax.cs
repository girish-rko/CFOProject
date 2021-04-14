using StackExchange.Profiling;
using StackExchange.Profiling.EntityFramework6;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;

namespace CFOProject
{
    public class WebApiApplication : System.Web.HttpApplication
    {
       

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MiniProfilerEF6.Initialize();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //MapConfig.RegisterMaps();
            //Bootstrapper.MapConfig.RegisterMaps();
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            //ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());

            MvcHandler.DisableMvcResponseHeader = true;

            // miniprofiler setup
            //MiniProfiler.Settings.Results_Authorize = IsUserAllowedToSeeMiniProfilerResults;
            //MiniProfiler.Settings.Results_List_Authorize = IsUserAllowedToSeeMiniProfilerResults;
            //MiniProfiler.Settings.Storage = new HttpRuntimeCacheStorage(TimeSpan.FromDays(1));
            //MiniProfiler.Settings.PopupRenderPosition = RenderPosition.BottomRight;

            //GlobalFilters.Filters.Add(new ProfilingActionFilter());
            //MiniProfiler.Settings.MaxJsonResponseSize = 2147483647;
            //MiniProfiler.Settings.ExcludeAssembly("Tronix.Background");
            //var ignoredPaths = MiniProfiler.Settings.IgnoredPaths.ToList();
            //ignoredPaths.Add("hangfire/");
            //MiniProfiler.Settings.IgnoredPaths = ignoredPaths.ToArray();
            //var copy = ViewEngines.Engines.ToList();
            //ViewEngines.Engines.Clear();
            //foreach (var item in copy)
            //{
            //    ViewEngines.Engines.Add(new ProfilingViewEngine(item));
            //}

            //ControllerBuilder.Current.SetControllerFactory(typeof(SessionControllerFactory));

            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            //ScriptManager.ScriptResourceMapping.AddDefinition("jquery", null, new ScriptResourceDefinition { Path = "~/Scripts/fakejquery.js" });

            //ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) => true;
            //ModelMetadataProviders.Current = new Infrastructure.Providers.TronixModelMetadataProvider();
            //var providers = new EntityFramework.Utilities.IQueryProvider[EntityFramework.Utilities.Configuration.Providers.Count];
            //EntityFramework.Utilities.Configuration.Providers.CopyTo(providers, 0);
            //EntityFramework.Utilities.Configuration.Providers.Clear();

            //for (var i = 0; i < providers.Length; i++)
            //{
            //    EntityFramework.Utilities.Configuration.Providers.Add(new ProfiledQueryProvider(providers[i]));
            //}
            //if ((ConfigurationManager.AppSettings["Hangfire.Run"] ?? "").Equals("true", StringComparison.OrdinalIgnoreCase))
            //{
            //    KeepAlive.Instance.Start();
            //}

        }

        protected void Application_BeginRequest()
        {
            //if (IsMiniProfilerAllowedToInitialize(Request))
            //{
            //    MiniProfiler.Start();
            //}
        }

        protected void Application_EndRequest()
        {
            //MiniProfiler.Stop();
        }

        public void Application_AcquireRequestState(object sender, EventArgs e)
        {
            var session = HttpContext.Current.Session;
            if (session == null) return;

            if (HttpContext.Current?.User?.Identity?.IsAuthenticated != true || string.IsNullOrWhiteSpace(Session.SessionID)) return;

            // set a redis value for the session id
            // this is basically a sliding window of X minutes
            // each request, if authenticated, will set a key that is expected to be in redis by the node reporting service
           
        }

        //protected void Application_AuthenticateRequest(object sender, EventArgs e)
        //{
        //    //extra security for miniprofiler - if the user is not correct, stop it after it's intialized
        //    if (!IsMiniProfilerAllowedToRun(HttpContext.Current.Request))
        //    {
        //        MiniProfiler.Stop(discardResults: true);
        //    }
        //}

        public void Application_PostAuthorizeRequest(object sender, EventArgs e)
        {

            //Session state must be required for the session object to change in our current setup
            //We otherwise use read-only to stop microsoft from queueing all server-side calls by the same user and bottlenecking the UX.
            //For MVC controllers we have attributes set up that can override default session settings
            //this manages it for the few already-existing WepApi actions that set session but are called by external mechanisms
            var urlsForApiControllersThatChangeSessionObjects = new List<string>()
            {
                //WebApiConfig.UrlPrefixRelative+"/DealJacket/AuthorizeUsingQrToken",
                //WebApiConfig.UrlPrefixRelative+"/DealJacket/GetDealId",
                //WebApiConfig.UrlPrefixRelative+"/DriverLicense/AuthorizeUsingQrToken",
                //WebApiConfig.UrlPrefixRelative+"/DriverLicense/GetDealId",
            };
            //session state is readonly by default for web api so that session can be accessed, but does not lock async calls
            if (HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath != null &&
                urlsForApiControllersThatChangeSessionObjects.Any(x => HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith(x)))
            {
                HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
            }
            else
            {
                HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.ReadOnly);
                //HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);

            }
        }

        public void Session_End(object sender, EventArgs e)
        {
            DoLogout();
        }

        public void Application_End(object sender, EventArgs e)
        {

            DoLogout();
            //KeepAlive.Instance.PingServer();
        }
        public override string GetVaryByCustomString(HttpContext context, string arg)
        {
            //NOTE: I spent about a day exploring custom action-method level caching
            //and what i came to is: Because we frequently change lenders and put lenders, dealers and dealer groups in dropdowns (including select-a-dealer)
            //the pages we can cache (if we donut select-a-dealer) such as the mvcs for the dashboards already load so fast that there's really no justification for the additional code overhead.
            //I looked at global cache breakers for the admins but they won't work with our distributed environment - will break cache for only one server, not all.
            //we seem to do better at the moment with performance gains from indexes, EF optimizations and some individual targeted caching of non-changing dropdown lists (such as states or deal types).
            //- pg  2019.01.10
            return base.GetVaryByCustomString(context, arg);
        }


        #region private methods
        private void DoLogout()
        {
            Request.GetOwinContext().Authentication.SignOut("Application");
            Session.Abandon();
        }
        //private bool IsMiniProfilerAllowedToRun(HttpRequest httpRequest)
        //{
        //    if (IsMiniProfilerAllowedToInitialize(httpRequest))
        //    {
        //        var principal = httpRequest.RequestContext.HttpContext.User;
        //        return principal == null ? false : principal.Identity.IsAuthenticated;
        //    }
        //    return false;
        //}
        //private bool IsMiniProfilerAllowedToInitialize(HttpRequest httpRequest)
        //{
        //    var localOverrideSetting = ConfigurationManager.AppSettings["MiniProfiler.HideWhenLocal"];
        //    var overrideLocal = localOverrideSetting != null && localOverrideSetting.ToString().Trim().ToLower() == "true";

        //    return (httpRequest.IsLocal && !overrideLocal) || EnvironmentHelper.IsStagingSite();
        //    //return true //if we want limited access on production
        //}

        private bool IsUserAllowedToSeeMiniProfilerResults(HttpRequest httpRequest)
        {

            var principal = httpRequest.RequestContext.HttpContext.User;
            //var user = (UserDTO)HttpContext.Current.Session["User"];
            return principal.Identity.IsAuthenticated;
            //return httpRequest.IsLocal || EnvironmentHelper.IsDevSite() || user.Role == Business.Enums.UserRoleEnum.AdminTroni;

        }
        #endregion

    }
}
