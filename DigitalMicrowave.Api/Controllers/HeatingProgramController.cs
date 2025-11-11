using DigitalMicrowave.Application.Services;
using DigitalMicrowave.Domain.Entities;
using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace DigitalMicrowave.Api.Controllers
{
    public class HeatingProgramController : ApiController
    {
        private readonly HeatingProgramService _service;
        [HttpGet]
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

        [HttpPost]
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

        [HttpPut]
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

        [HttpDelete]
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