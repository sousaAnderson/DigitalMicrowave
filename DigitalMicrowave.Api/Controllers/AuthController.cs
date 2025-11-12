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

        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login([FromBody] Users request)
        {
            if (request == null)
                return BadRequest("Payload inválido");

            var exists = _userServices.GetUser(request.User, request.Password);
            if (exists == null)
                return Unauthorized();

            var token = JwtTokenProvider.GenerateToken(request.User);
            return Ok(new { token });
           
        }
    }
}
