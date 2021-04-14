using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using CFOProject.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using Owin;

[assembly: OwinStartup(typeof(CFOProject.Startup))]

namespace CFOProject
{
    public partial class Startup
    {
        private ContainerBuilder _builder { get; set; }
        private IContainer _container { get; set; }
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            _builder = new ContainerBuilder();
            //_builder = Bootstrapper.AutoFac.Startup(_builder);
            var config = new HttpConfiguration();
            _builder.RegisterControllers(Assembly.GetExecutingAssembly());
            _builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            _builder.RegisterWebApiFilterProvider(config);

            _builder.RegisterModule<AutofacWebTypesModule>();
            //_builder.RegisterModule<WebInfrastructureModule>();
            _builder.RegisterFilterProvider();

            //identity
            //_builder.RegisterAssemblyTypes(typeof(WebUserService).Assembly).Where(t => t.Name.EndsWith("Service")).InstancePerRequest();
            _builder.RegisterType<UserStore>().As<IUserStore<IdentityUser, int>>().InstancePerRequest();
            //builder.RegisterType<RoleStore>().As<IRoleStore<IdentityRole, string>>().InstancePerRequest();

            _builder.RegisterInstance(app.GetDataProtectionProvider());

            //builder.RegisterType<ApplicationRoleManager>().AsSelf().InstancePerRequest();
            _builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            _builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            //2020.10.05 - this doesn't play well with hangfire.autofac so referencing it directly
            // _builder.RegisterType<DmsGateway>().As<IDmsGateway>().InstancePerRequest();
            _builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            //end identity

            _builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces();

            //var awsBucketName = ConfigurationManager.AppSettings["AwsBucketName"];

            //_builder.RegisterType<Amazon.S3.Transfer.TransferUtility>()
            //    .As<Amazon.S3.Transfer.TransferUtility>().WithParameter("region", Amazon.RegionEndpoint.USEast1)
            //    .InstancePerRequest(AutofacJobActivator.LifetimeScopeTag);

            //_builder.RegisterType<Amazon.S3.AmazonS3Client>()
            //    .As<Amazon.S3.AmazonS3Client>().WithParameter("region", Amazon.RegionEndpoint.USEast1)
            //    .InstancePerRequest(AutofacJobActivator.LifetimeScopeTag);

            //_builder.RegisterType<Amazon.S3.Model.GetObjectRequest>()
            //    .As<Amazon.S3.Model.GetObjectRequest>().WithProperty("BucketName", awsBucketName)
            //    .InstancePerRequest(AutofacJobActivator.LifetimeScopeTag);

            //_builder.RegisterType<Amazon.S3.Model.DeleteObjectRequest>()
            //    .As<Amazon.S3.Model.DeleteObjectRequest>().WithProperty("BucketName", awsBucketName)
            //    .InstancePerRequest(AutofacJobActivator.LifetimeScopeTag);

            //Web Api Registrations
            //_builder.RegisterWebApiFilterProvider(System.Web.Http.GlobalConfiguration.Configuration);
            //_builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            ////builder.RegisterType<WebApiExceptionHandler>().AsSelf().AsImplementedInterfaces();

            _container = _builder.Build();

            //for Web Api
            System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(_container);
            //GlobalConfiguration.Configuration.Services.Add(typeof(IExceptionLogger), new WebApiExceptionHandler());

            //for Web MVC
            DependencyResolver.SetResolver(new AutofacDependencyResolver(_container));



            //hangfire
        //    if ((ConfigurationManager.AppSettings["Hangfire.Run"] ?? "").Equals("true", StringComparison.OrdinalIgnoreCase))
        //    {
        //        app.UseHangfireAspNet(GetHangfireServers);
        //        app.UseHangfireDashboard(ConfigurationManager.AppSettings["HangfireUrl"], new DashboardOptions
        //        {
        //            Authorization = new[] { new HangfireDashboardAuthorizationFilter() }
        //        });
        //        BackgroundConfig.ScheduleRecurringTasks();
        //    }
        }
        //private IEnumerable<IDisposable> GetHangfireServers()
        //{
        //    Hangfire.GlobalConfiguration.Configuration
        //        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        //        .UseSimpleAssemblyNameTypeSerializer()
        //        .UseRecommendedSerializerSettings()
        //        .UseAutofacActivator(_container, true)
        //        .UseSqlServerStorage(ConfigurationManager.AppSettings["DefaultConnection"], new SqlServerStorageOptions
        //        {
        //            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
        //            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
        //            QueuePollInterval = TimeSpan.Zero,
        //            UseRecommendedIsolationLevel = true,
        //            DisableGlobalLocks = true
        //        });
        //    yield return new BackgroundJobServer();
        //}
    }
}

