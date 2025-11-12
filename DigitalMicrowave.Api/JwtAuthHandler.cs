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
            var path = request.RequestUri.AbsolutePath.ToLower();

            // ✅ Ignorar validação para Swagger e CORS preflight
            if (request.Method == HttpMethod.Options ||
                path.Contains("swagger") ||
                path.Contains("swagger/docs") ||
                path.Contains("swagger/ui"))
            {
                return await base.SendAsync(request, cancellation);
            }

            if (!request.Headers.Contains("Authorization"))
                return UnauthorizedResponse(request, "Missing token");

            try
            {
                var token = request.Headers.Authorization.Parameter;
                var handler = new JwtSecurityTokenHandler();

                handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(_key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out _);

                return await base.SendAsync(request, cancellation);
            }
            catch
            {
                return UnauthorizedResponse(request, "Invalid token");
            }
        }

        private HttpResponseMessage UnauthorizedResponse(HttpRequestMessage request, string message)
        {
            var resp = request.CreateResponse(HttpStatusCode.Unauthorized);
            resp.Content = new StringContent(message, System.Text.Encoding.UTF8, "text/plain");
            return resp;
        }
    }
}