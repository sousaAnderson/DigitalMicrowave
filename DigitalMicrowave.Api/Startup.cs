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
using System.Web.Http;

[assembly: OwinStartup(typeof(Startup))]
namespace DigitalMicrowave.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            WebApiConfig.Register(config);

            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "Digital Microwave API");
            })
            .EnableSwaggerUi();
            var secret = "SUA_CHAVE_SUPER_SECRETA_256_BITS_AQUI"; // mesma usada pra gerar o token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "DigitalMicrowave",           // MESMO issuer usado no token!
                    ValidAudience = "DigitalMicrowaveWebApp",   // MESMO audience usado no token!
                    IssuerSigningKey = key
                }
            });

            app.UseWebApi(config);
        }
    }
}