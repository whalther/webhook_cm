using Application;
using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Webhook.Controllers
{
    [RoutePrefix("api/localqueries")]
    public class LocalQueriesController : ApiController
    {
        [HttpPost]
        [Route("getTiposDocumento")]
        public IHttpActionResult GetToken()
        {
            LocalQueriesApp app = new LocalQueriesApp();
            Replay respuesta = new Replay();
            respuesta.Status = "OK";
            respuesta.Info.Add("data", app.GetTiposDocumento());
            return Json(respuesta);
        }
    }
}
