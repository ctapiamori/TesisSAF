using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Web.Http;

namespace SOCAUDAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            RegisterNullEmptyValue(config);
            RegisterHttpAttributes(config);
            RegisterCulture();
            RegisterMapRoute(config);
        }

        public static void RegisterCulture() {
            //ACEPTAR punto como separador de decimales
            //Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("es-PE");
            //System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            //customCulture.NumberFormat.NumberDecimalSeparator = ".";
            //System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings =
                new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    DateTimeZoneHandling = DateTimeZoneHandling.Unspecified,
                    Culture = CultureInfo.GetCultureInfo("es-PE")
                };
        }

        public static void DisabledBehaviors(HttpConfiguration current)
        {
            var formatter = current.Formatters.OfType<JsonMediaTypeFormatter>().First();

            formatter.SerializerSettings = new JsonSerializerSettings
            {
               
                Formatting = Formatting.Indented 
            };
            formatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            current.Formatters.Remove(current.Formatters.XmlFormatter);
            //current.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //current.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;

            // ANTES
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Populate;
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

        }

        public static void RegisterNullEmptyValue(HttpConfiguration config)
        {
            DisabledBehaviors(config);
            
        }


        public static void RegisterHttpAttributes(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();
        }

        public static void RegisterMapRoute(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }


    }
}
