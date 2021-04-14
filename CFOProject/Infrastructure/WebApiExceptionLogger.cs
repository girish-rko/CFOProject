using CfO.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ExceptionHandling;
using System.Web.Mvc;

namespace CFOProject.Infrastructure
{
    public class WebApiExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            var loggerService = (ILoggerService)DependencyResolver.Current.GetService(typeof(ILoggerService));
            loggerService.Log(context.Exception);
        }
    }
   
}