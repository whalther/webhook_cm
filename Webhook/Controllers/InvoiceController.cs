using Application;
using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Webhook.Controllers
{
    [RoutePrefix("api/invoice")]
    public class InvoiceController : ApiController
    {
        readonly Utilities utilidad = new Utilities();
        [HttpPost]
        [Route("getContratosFactura")]
        public IHttpActionResult GetContratosFactura([FromBody]dynamic request)
        {
            string[] sessionId = request["sessionId"].ToString().Split('*');
            string numeroCelular = utilidad.GetNumero(sessionId[1]);
            string idConv = sessionId[0];
            string token = request["token"];
            string numDoc = request["numDoc"];
            string tipoDoc = request["tipoDoc"];
            
            Replay respuesta = new Replay();
            InvoiceApp app = new InvoiceApp();
            Resultado res = app.GetContratosFactura(token, numeroCelular, numDoc, tipoDoc, idConv);
            if (res.Result.ToString() != "error_credenciales" && res.Result.ToString() != "error_parametros" && res.Result.ToString() != "error_desconocido" && res.Result.ToString() != "error_bd")
            {
                List<ContratoFactura> cons = (List<ContratoFactura>)res.Result;
                respuesta.IdConv = idConv;
                respuesta.Token = res.Token;
                if (cons.Count == 0)
                {
                    respuesta.Status = "empty";
                    respuesta.Info.Add("data", "");
                }
                else if (cons[0].PlanContrato != "error_parametros" && cons[0].PlanContrato != "error_desconocido" && cons[0].PlanContrato != "error_credenciales" && cons[0].PlanContrato != "error_token")
                {
                    respuesta.Status = "ok";
                    respuesta.Info.Add("data", cons);
                }
                else
                {
                    respuesta.Status = "error";
                    respuesta.Info.Add("data", cons[0].PlanContrato);
                }
            }
            else
            {
                respuesta.Status = "error";
                respuesta.Info.Add("data", res.Result);
            }
            return Json(respuesta);

        }

        [HttpPost]
        [Route("getContratosFacturaBd")]
        public IHttpActionResult GetContratosFacturaBd([FromBody]dynamic request)
        {
            InvoiceApp app = new InvoiceApp();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            List<ContratoFactura> ct = app.GetContratoFacturasBd(sessionId[0]);
            Replay respuesta = new Replay()
            {
                Status = ct.Count > 0 ? "ok" : "empty",
                Info = new Dictionary<string, object> { { "data", ct } },
                IdConv = sessionId[0]
            };

            return Json(respuesta);
        }
        [HttpPost]
        [Route("sendEmailRetefuente")]
        public IHttpActionResult SendEmailRetefuente([FromBody]dynamic request)
        {
            InvoiceApp app = new InvoiceApp();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            string numDoc = request["numDoc"];
            string tipoDoc = request["tipoDoc"];
            string token = request["token"];
           // app.SendEmailRetefuente(tipoDoc, numDoc, sessionId[0], token,sessionId[1]);
            return this.GetResultEmailRetefuente(request);
        }
        [HttpPost]
        [Route("getResultEmailRetefuente")]
        public IHttpActionResult GetResultEmailRetefuente([FromBody]dynamic request)
        {
            InvoiceApp app = new InvoiceApp();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            Replay respuesta = new Replay();
            Dictionary<string, int> res  = new Dictionary<string, int> { { "Enviado", 1 } };

            /* dynamic res = app.GetResultEmailRetefuente(sessionId[0]);

                 if (res.Enviado == 1)
                 {
                     respuesta.Status = "ok";
                 }
                 else
                 {
                     respuesta.Status = "error";
                 }*/
            respuesta.Info.Add("data",res);
            respuesta.Status = "ok";
            respuesta.Token = request["token"];
            respuesta.IdConv = sessionId[0];
           // respuesta.Message = res.Mensaje;
            return Json(respuesta);
        }
    }
}
