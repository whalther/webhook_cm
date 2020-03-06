using Application;
using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Webhook.Controllers
{
    [RoutePrefix("api/authentication")]
    public class AuthenticationController : ApiController
    {
        [HttpPost]
        [Route("getToken")]
        public IHttpActionResult GetToken()
        {
            AuthenticationApp app = new AuthenticationApp();
            Replay respuesta = new Replay();
           // var request = HttpContext.Current.Request.Params;
            //string Nombre = request["Nombre"],
            string numeroCelular = "3194198375";
            string identificacion = "CC79880800";
            string idConv = "98fddusfh89udf-sf98df-9"; 
            respuesta.Status = "OK";
            respuesta.IdConv = idConv;
            respuesta.Token = app.GetToken(numeroCelular, identificacion);
            respuesta.Info.Add("data", "");
            return Json(respuesta);
        }
        [HttpPost]
        [Route("validarOtp")]
        public IHttpActionResult ValidarOtp()
        {
            AuthenticationApp app = new AuthenticationApp();
            Replay respuesta = new Replay();
           // var request = HttpContext.Current.Request.Params;
            //string Nombre = request["Nombre"],
            string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c3VhcmlvIjoiTkk5MDA4MDE0NTkiLCJjbGllbnRlIjoiQ0M3OTg4MDgwMCIsImV4cCI6MTU4MzQyMDQ3My4wfQ.XXZ3yt0VuHkVFTbcUhee-j02Ll4Su8Jy6e6ALXbJG9Y";
            string otp = "355694";
            string idConv = "98fddusfh89udf-sf98df-9";
            string numeroCelular = "3194198375";
            string identificacion = "CC79880800";
            respuesta.Status = "OK";
            respuesta.IdConv = idConv;
            Resultado res = app.ValidarOtp(token, otp, identificacion, numeroCelular);
            respuesta.Token = res.Token;
            respuesta.Info.Add("data", res.Result);
            return Json(respuesta);
        }

    }
}
