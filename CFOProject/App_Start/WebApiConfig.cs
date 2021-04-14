using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using CFOProject.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace CFOProject
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes(); 
            config.Services.Add(typeof(IExceptionLogger), new WebApiExceptionLogger());
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Adding formatter for XML  - it is first so it will be the default
            config.Formatters.Add(new XmlMediaTypeFormatter());

            // Now the json formatter!
            var jsonFormatter = new JsonMediaTypeFormatter();
            jsonFormatter.MediaTypeMappings.Add(
                new QueryStringMapping("type", "json", new MediaTypeHeaderValue("application/json")));
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonFormatter.UseDataContractJsonSerializer = false;
            config.Formatters.Add(jsonFormatter);
        }
    }
}
