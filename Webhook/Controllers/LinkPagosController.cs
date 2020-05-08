using Application;
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
        public void GenerarLinkPagos([FromBody]dynamic request)
        {
            LinkPagosApp app = new LinkPagosApp();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            string idConv = sessionId[0];
            int idCita = request["idCita"];
            app.GenerarLink(idConv, idCita);
        }
    }
}
