using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using System.Web.Http;

namespace DigitalMicrowave.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuração e serviços de API Web
            var secret = WebConfigurationManager.AppSettings["JwtSecret"];
            var key = Encoding.UTF8.GetBytes(secret);

            var formatters = config.Formatters;
            formatters.Remove(formatters.XmlFormatter);

            // Rotas de API Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "swagger",
                routeTemplate: "",
                defaults: null,
                constraints: null,
                handler: new Swashbuckle.Application.RedirectHandler((message => message.RequestUri.ToString()), "swagger")
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
