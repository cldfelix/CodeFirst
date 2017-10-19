using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using CodeFirst.Models;
using CodeFirst.Models.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;

namespace CodeFirst
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration httpConfig = new HttpConfiguration();

            ConfigureOAuthTokenGeneration(app);

            ConfigureWebApi(httpConfig);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            app.UseWebApi(httpConfig);

        }


        private void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(SegurancaDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Plugin the OAuth bearer JSON Web Token tokens generation and Consumption will be here

        }

        private void ConfigureWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            //var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            GlobalConfiguration.Configuration.Formatters.JsonFormatter
                    .SerializerSettings.ReferenceLoopHandling =
                ReferenceLoopHandling.Ignore;

            JsonMediaTypeFormatter jsonFormatter = config.Formatters.JsonFormatter;
            jsonFormatter.Indent = true;

            jsonFormatter.SerializerSettings.DateFormatHandling
                = DateFormatHandling.MicrosoftDateFormat;
            jsonFormatter.SerializerSettings.StringEscapeHandling
                = StringEscapeHandling.EscapeHtml;
            jsonFormatter.SerializerSettings.DefaultValueHandling
                = DefaultValueHandling.Ignore;
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
 
    }
}