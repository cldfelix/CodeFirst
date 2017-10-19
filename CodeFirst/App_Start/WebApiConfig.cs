using System.Web.Http;
using Newtonsoft.Json;

namespace CodeFirst
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            GlobalConfiguration.Configuration.Formatters.JsonFormatter
                    .SerializerSettings.ReferenceLoopHandling =
                ReferenceLoopHandling.Ignore;

            var jsonFormatter = config.Formatters.JsonFormatter;
            jsonFormatter.Indent = true;

            jsonFormatter.SerializerSettings.DateFormatHandling
                = DateFormatHandling.MicrosoftDateFormat;
            jsonFormatter.SerializerSettings.StringEscapeHandling
                = StringEscapeHandling.EscapeHtml;
            jsonFormatter.SerializerSettings.DefaultValueHandling
                = DefaultValueHandling.Ignore;
        }
    }
}
