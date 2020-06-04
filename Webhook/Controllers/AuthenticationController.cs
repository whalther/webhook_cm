using Application;
using Domain.DTOs;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Http;

namespace Webhook.Controllers
{
    [RoutePrefix("api/authentication")]
    public class AuthenticationController : ApiController
    {
       readonly Utilities utilidad = new Utilities();
        [HttpPost]
        [Route("getToken")]
        public IHttpActionResult GetToken([FromBody]dynamic request)
        {
            AuthenticationApp app = new AuthenticationApp();
            Replay respuesta = new Replay();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            string idConv = sessionId[0];
            string numeroCelular = utilidad.GetNumero(sessionId[1]);
            string numDoc = request["numDoc"];
            string tipoDoc = request["tipoDoc"];
            string resp = app.GetToken(numeroCelular, numDoc, tipoDoc , idConv);
            respuesta.IdConv = idConv;

            if (resp != "error_credenciales" && resp != "error_prohibido" && resp != "error_desconocido" && resp != "error_no_encontrado")
            {
                respuesta.Status = "ok";
            }
            else {
                
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
            string numDoc = request["numDoc"];
            string tipoDoc = request["tipoDoc"];
            respuesta.IdConv = idConv;
            Resultado res = app.ValidarOtp(token, otp, numDoc,tipoDoc, numeroCelular,idConv);
            if (res.Result.ToString() != "error_credenciales" && res.Result.ToString() != "error_parametros" && res.Result.ToString() != "error_desconocido")
            {
                if (res.Result.ToString() == "0")
                {
                    respuesta.Status = "invalid";
                }
                else if (res.Result.ToString() == "1")
                {
                    respuesta.Status = "ok";
                }
                else if (res.Result.ToString() == "2")
                {
                    respuesta.Status = "used";
                }
                else if (res.Result.ToString() == "3")
                {
                    respuesta.Status = "expired";
                }
                else 
                {
                    respuesta.Status = "error";
                }
            }
            else
            {
                respuesta.Status = "error";
            }
            respuesta.Token = res.Token;
            respuesta.Info.Add("data", res.Result);
            return Json(respuesta);
        }
        [HttpPost]
        [Route("getEstadoAuth")]
        public IHttpActionResult GetAuthentication([FromBody]dynamic request)
        {
            AuthenticationApp app = new AuthenticationApp();
            Replay respuesta = new Replay();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            string idConv = sessionId[0];
            
            respuesta.IdConv = idConv;
            dynamic res = app.GetAuthentication(idConv);
            if (res != null)
            {
                if (res.token.ToString() != "error_credenciales" && res.token.ToString() != "error_prohibido" && res.token.ToString() != "error_desconocido" && res.token.ToString() != "error_no_encontrado")
                {
                    respuesta.Status = "ok";
                }
                else
                {
                    respuesta.Status = "error";
                }
                respuesta.Token = res.token;
                respuesta.Info.Add("data", res);
            }
            else {
                respuesta.Status = "processing";
            }
            
            return Json(respuesta);
        }
        [HttpPost]
        [Route("getEstadoOtp")]
        public IHttpActionResult GetEstadoOtp([FromBody]dynamic request)
        {
            AuthenticationApp app = new AuthenticationApp();
            Replay respuesta = new Replay();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            string idConv = sessionId[0];

            respuesta.IdConv = idConv;
            dynamic res = app.GetAuthentication(idConv);
            if (res != null)
            {
                if ( string.IsNullOrEmpty(res.otp))
                {
                    respuesta.Status = "processing";
                }
                else if (res.otp.ToString() != "error_credenciales" && res.otp.ToString() != "error_parametros" && res.otp.ToString() != "error_desconocido")
                {
                     if (res.otp.ToString() == "0")
                    {
                        respuesta.Status = "invalid";
                    }
                    else if (res.otp.ToString() == "1")
                    {
                        respuesta.Status = "ok";
                    }
                    else if (res.otp.ToString() == "2")
                    {
                        respuesta.Status = "used";
                    }
                    else if (res.otp.ToString() == "3")
                    {
                        respuesta.Status = "expired";
                    }
                    else
                    {
                        respuesta.Status = "error";
                    }
                }
                else
                {
                    respuesta.Status = "error";
                }
                respuesta.Token = res.token;
                respuesta.Info.Add("data", res.otp);
            }
            else
            {
                respuesta.Status = "processing";
            }

            return Json(respuesta);
        }
    }
}
