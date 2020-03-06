using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Application;
using Domain.DTOs;

namespace Webhook.Controllers
{
    [RoutePrefix("api/scheduling")]
    public class SchedulingPetitionsController : ApiController
    {
        [HttpPost]
        [Route("validarUsuario")]
        public IHttpActionResult ValidarUsuario()
        {
            SchedulingPetitionsApp app = new SchedulingPetitionsApp();
            Replay respuesta = new Replay();
            // var request = HttpContext.Current.Request.Params;
            //string Nombre = request["Nombre"],
            //string numeroCelular = "3194198375";
            string numeroCelular = "2332";
            string identificacion = "CC79880800";
            string idConv = "98fddusfh89udf-sf98df-9";
            string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c3VhcmlvIjoiTkk5MDA4MDE0NTkiLCJjbGllbnRlIjoiQ0M3OTg4MDgwMCIsImV4cCI6MTU4MzQyMDQ3My4wfQ.XXZ3yt0VuHkVFTbcUhee-j02Ll4Su8Jy6e6ALXbJG9Y";
            
            respuesta.IdConv = idConv;
            Resultado res = app.ValidarUsuario(identificacion, numeroCelular, token);
            respuesta.Token = res.Token;
            Usuario usuario = (Usuario)res.Result;
            if (usuario.Mensaje != "error_parametros" && usuario.Mensaje != "error_desconocido")
            {
                respuesta.Status = "OK";
                respuesta.Info.Add("data", usuario);
            }
            else {
                respuesta.Status = "error";
                respuesta.Info.Add("data", usuario.Mensaje);
            }
            

            return Json(respuesta);
        }
    }
}
