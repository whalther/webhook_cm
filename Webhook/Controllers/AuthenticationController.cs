using Application;
using Domain.DTOs;
using Newtonsoft.Json;
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
    [RequestFilter]
    public class AuthenticationController : ApiController
    {
        Utilities utilidad = new Utilities();
        [HttpPost]
        [Route("getToken")]
        public IHttpActionResult GetToken([FromBody]dynamic request)
        {
            AuthenticationApp app = new AuthenticationApp();
            Replay respuesta = new Replay();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            string idConv = sessionId[0];
            string numeroCelular = utilidad.GetNumero(sessionId[1]);
            string identificacion = request["tipoDoc"]+ request["numDoc"];
            string resp = app.GetToken(numeroCelular, identificacion);
            respuesta.IdConv = idConv;

            if (resp != "error_credenciales" && resp != "error_parametros" && resp != "error_desconocido")
            {
                respuesta.Status = "OK";
            }
            else {
                LogApp log = new LogApp();
                Dictionary<string, string> param = new Dictionary<string, string>() {
                {"numeroCelular",numeroCelular },
                {"identificacion",identificacion},
                {"idConv", idConv }
              };
                log.GuardarErrorLogPeticion(resp, JsonConvert.SerializeObject(param),"GetToken");
                respuesta.Status = "error";
            }
            respuesta.Token = resp;
            respuesta.Info.Add("data", "");
            return Json(respuesta);
        }
        [HttpPost]
        [Route("validarOtp")]
        public IHttpActionResult ValidarOtp([FromBody]dynamic request)
        {
            AuthenticationApp app = new AuthenticationApp();
            Replay respuesta = new Replay();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            string idConv = sessionId[0];
            string numeroCelular = utilidad.GetNumero(sessionId[1]);
            string token = request["token"];
            string otp = request["otp"];
            string identificacion = request["tipoDoc"] + request["numDoc"];
            respuesta.IdConv = idConv;
            Resultado res = app.ValidarOtp(token, otp, identificacion, numeroCelular,idConv);
            if (res.Result.ToString() != "error_credenciales" && res.Result.ToString() != "error_parametros" && res.Result.ToString() != "error_desconocido")
            {
                respuesta.Status = "OK";
            }
            else
            {
                respuesta.Status = "error";
            }
            respuesta.Token = res.Token;
            respuesta.Info.Add("data", res.Result);
            return Json(respuesta);
        }

    }
}
