using DigitalMicrowave.Api;
using DigitalMicrowave.Api.Auth;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Owin;
using Swashbuckle.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;

[assembly: OwinStartup(typeof(Startup))]
namespace DigitalMicrowave.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            var secret = WebConfigurationManager.AppSettings["JwtSecret"];
            var key = Encoding.UTF8.GetBytes(secret);

            config.MessageHandlers.Add(new JwtAuthHandler(key));

            config.Formatters.Remove(config.Formatters.XmlFormatter); // remove o XML

            WebApiConfig.Register(config);

            app.UseWebApi(config);
        }

    }
}