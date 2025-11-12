using DigitalMicrowave.Application.Services;
using DigitalMicrowave.Domain.Entities;
using DigitalMicrowave.Infrastructure.Repositories;
using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;

namespace DigitalMicrowave.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/heatingprograms")]
    public class HeatingProgramsController : ApiController
    {
        private readonly HeatingProgramService _service;
        public HeatingProgramsController()
        {
            _service = new HeatingProgramService(new HeatingProgramRepository());
        }
        [HttpGet, Route("")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                var response = _service.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }            
        }

        [HttpPost, Route("")]
        public HttpResponseMessage Create(HeatingProgram program)
        {
            try
            {
                _service.Create(program);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPut, Route("")]
        public HttpResponseMessage Update(HeatingProgram program)
        {
            try
            {
                _service.Update(program);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpDelete, Route("{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}