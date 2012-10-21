using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using Newtonsoft.Json;

namespace WeenyBoard
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "UpdateItem", 
                routeTemplate: "api/{controller}/updateitemdescription", 
                defaults: new { controller = "board", action = "updateitemdescription", httpMethod = new HttpMethodConstraint(HttpMethod.Post) });

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            ForceGoogleChromeToUseJsonAsDefaultSerializationFormat(config);

            ApplyCustomJsonCasingConvention(config);
        }

        private static void ForceGoogleChromeToUseJsonAsDefaultSerializationFormat(HttpConfiguration config)
        {
            // This will make chrome use JSON instead of XML when requesting data. However, it is still possible
            // to get XML by specifying text/xml in the request accept header
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
        }

        private static void ApplyCustomJsonCasingConvention(HttpConfiguration config)
        {
            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new LowercaseContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings = settings;
        }
    }
}
