﻿using Application;
using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Webhook.Controllers
{
    [RoutePrefix("api/linkpagos")]
    public class LinkPagosController : ApiController
    {

        [HttpPost]
        [Route("generarLinkPago")]
        public IHttpActionResult GenerarLinkPagos([FromBody]dynamic request)
        {
            LinkPagosApp app = new LinkPagosApp();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            string idConv = sessionId[0];
            int idCita = request["idCita"];
            string flag = request["flag"];
            app.GenerarLink(idConv, idCita, flag);
            return GetLinkPago(request);
        }
        [HttpPost]
        [Route("getLinkPago")]
        public IHttpActionResult GetLinkPago([FromBody]dynamic request)
        {
            LinkPagosApp app = new LinkPagosApp();
            Replay respuesta = new Replay();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            string idConv = sessionId[0];
            int idCita = request["idCita"];
            string flag = request["flag"];
            string link = app.GetLinkPago(idConv, idCita, flag);
            respuesta.IdConv = idConv;
            if (link != "error_interno_fenix" && link != "error_bd" && link != "error_valor" && !string.IsNullOrEmpty(link))
            {
                if (link.Substring(0, 5) == "error")
                {
                    respuesta.Status = "error";
                    respuesta.Info.Add("data", link.Substring(6));
                }
                else 
                {
                    respuesta.Status = "ok";
                    respuesta.Info.Add("data", link);
                }
               
            }
            else if (string.IsNullOrEmpty(link))
            {
                respuesta.Status = "processing";
            }
            else
            {
                respuesta.Status = link;
                respuesta.Info.Add("data", link);
            }

            return Json(respuesta);
        }
    }
}
