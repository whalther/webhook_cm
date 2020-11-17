using Application;
using Domain.DTOs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Web.Http;

namespace Webhook.Controllers
{
    

    [RoutePrefix("api/authentication")]
    public class AuthenticationController : ApiController
    {
       readonly Utilities utilidad = new Utilities();
        [HttpPost]
        [Route("validarCliente")]
        public IHttpActionResult ValidarCliente([FromBody]dynamic request)
        {
            AuthenticationApp app = new AuthenticationApp();
            Replay respuesta = new Replay();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            string idConv = sessionId[0];
            string numeroCelular = utilidad.GetNumero(sessionId[1]);
            string numDoc = request["numDoc"];
            string tipoDoc = request["tipoDoc"];
            string resp = app.ValidarCliente(numeroCelular, numDoc, tipoDoc , idConv);
            respuesta.IdConv = idConv;
            dynamic resultEncode;
            if (resp != "error_credenciales" && resp != "error_prohibido" && resp != "error_desconocido" && resp != "error_no_encontrado")
            {
                respuesta.Status = "ok";
                resultEncode = JToken.Parse(resp);

            }
            else {
                resultEncode = resp;
                respuesta.Status = "error";
            }
            respuesta.Info.Add("data", resultEncode);
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
            string otp = request["otp"];
            string numDoc = request["numDoc"];
            string tipoDoc = request["tipoDoc"];
            respuesta.IdConv = idConv;
            string res = app.ValidarOtp(otp, numDoc,tipoDoc, idConv);
            if (res != "otp_utilizado" && res != "otp_invalido" && res != "otp_vencido" && res != "error_desconocido")
            {
                respuesta.Status = "ok";
            }
            else
            {
                switch (res)
                {
                    case "otp_utilizado":
                        respuesta.Status = "used";
                        break;
                    case "otp_invalido":
                        respuesta.Status = "invalid";
                        break;
                    case "otp_vencido":
                        respuesta.Status = "expired";
                        break;
                    default :
                        respuesta.Status = "error";
                        break;
                        
                }
            }
            respuesta.Token = res;
            respuesta.Info.Add("data", res);
            return Json(respuesta);
        }
        [HttpPost]
        [Route("getEstadoValidacion")]
        public IHttpActionResult GetValidacion([FromBody]dynamic request)
        {
            AuthenticationApp app = new AuthenticationApp();
            Replay respuesta = new Replay();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            string idConv = sessionId[0];
            dynamic resultEncode;
            respuesta.IdConv = idConv;
            dynamic res = app.GetValidacion(idConv);
            if (res != null)
            {
                if (res.resultValida.ToString() != "error_credenciales" && res.resultValida.ToString() != "error_prohibido" && res.resultValida.ToString() != "error_desconocido" && res.resultValida.ToString() != "error_no_encontrado")
                {
                    respuesta.Status = "ok";
                    resultEncode = JToken.Parse(res.resultValida);
                }
                else
                {
                    respuesta.Status = "error";
                    resultEncode = res;
                }
                respuesta.Info.Add("data", resultEncode);
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
            dynamic res = app.GetValidacion(idConv);
            if (res != null)
            {
                if ( string.IsNullOrEmpty(res.token))
                {
                    respuesta.Status = "processing";
                }
                else if (res.token != "otp_utilizado" && res.token != "otp_invalido" && res.token != "otp_vencido" && res.token != "error_desconocido")
                {
                    respuesta.Status = "ok";
                }
                else
                {
                    switch (res.token)
                    {
                        case "otp_utilizado":
                            respuesta.Status = "used";
                            break;
                        case "otp_invalido":
                            respuesta.Status = "invalid";
                            break;
                        case "otp_vencido":
                            respuesta.Status = "expired";
                            break;
                        default:
                            respuesta.Status = "error";
                            break;
                    }
                }
                respuesta.Token = res.token;
                respuesta.Info.Add("data", res.token);
            }
            else
            {
                respuesta.Status = "processing";
            }

            return Json(respuesta);
        }
    }
}
