using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web.Configuration;
using System.Web.Http;
using DigitalMicrowave.Api.Auth;
using DigitalMicrowave.Application.Services;
using DigitalMicrowave.Domain.Entities;
using DigitalMicrowave.Infrastructure.Repositories;
using Microsoft.Graph;
using Microsoft.IdentityModel.Tokens;

namespace DigitalMicrowave.Api.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private readonly UserServices _userServices;
        public AuthController()
        {
            _userServices = new UserServices(new UserRepository());
        }
        [HttpPost, Route("login")]
        [AllowAnonymous]
        public IHttpActionResult Login(Users request)
        {
            string hash = SHA256(request.Password);

            var exists = _userServices.GetUser(request.User, request.Password);
            if (exists == null)
                return Unauthorized();

            var token = JwtTokenProvider.GenerateToken(request.User);
            return Ok(new { token });

            //if (request.User != "admin" || request.Password != "123456")
            //    return Unauthorized();

            //var secret = WebConfigurationManager.AppSettings["JwtSecret"];
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //var token = new JwtSecurityToken(
            //    issuer: "DigitalMicrowave",
            //    audience: "DigitalMicrowave",
            //    claims: new[] { new Claim(ClaimTypes.Name, "admin") },
            //    expires: DateTime.Now.AddMinutes(60),
            //    signingCredentials: creds
            //);

            //return Ok(new
            //{
            //    token = new JwtSecurityTokenHandler().WriteToken(token)
            //});
        }

        private string SHA256(string input)
        {
            using (var sha = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder();
                foreach (byte b in bytes) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
    }
}
