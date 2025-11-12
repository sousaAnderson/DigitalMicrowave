using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Web;

namespace DigitalMicrowave.Api
{
    public class JwtAuthHandler : DelegatingHandler
    {
        private readonly byte[] _key;
        public JwtAuthHandler(byte[] key) { _key = key; }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellation)
        {
            if (!request.Headers.Contains("Authorization"))
                return request.CreateResponse(HttpStatusCode.Unauthorized, "Missing token");

            try
            {
                var token = request.Headers.Authorization.Parameter;
                var handler = new JwtSecurityTokenHandler();
                handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(_key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out _);

                return await base.SendAsync(request, cancellation);
            }
            catch
            {
                return request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid token");
            }
        }
    }
}