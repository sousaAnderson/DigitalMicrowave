using DigitalMicrowave.Application.Services;
using DigitalMicrowave.Domain.Entities;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DigitalMicrowave.Api.Controllers
{
    public class MicrowaveController : ApiController
    {
        private MicrowaveServices _microwave;

        [HttpGet]
        [Route("Start/{seconds}/{power}")]
        public HttpResponseMessage Start(int seconds, int power)
        {
            try
            {
                _microwave.Start(seconds, power);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex) 
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [Route("PauseOrCance")]
        public HttpResponseMessage PauseOrCancel()
        {
            try
            {
                _microwave.PauseOrCancel();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetFormattedTime")]
        public HttpResponseMessage GetFormattedTime()
        {
            try
            {
               var response = _microwave.GetFormattedTime();
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [Route("ProcessTick/{heatingChar}")]
        public HttpResponseMessage ProcessTick(string heatingChar)
        {
            try
            {
                _microwave.ProcessTick(heatingChar);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}