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
        public IHttpActionResult GetTiposDocumento()
        {
            LocalQueriesApp app = new LocalQueriesApp();
            Replay respuesta = new Replay()
            {
                Status = "OK",
                Info = new Dictionary<string, object> { { "data", app.GetTiposDocumento() } },
            };
            return Json(respuesta);
        }
        [HttpPost]
        [Route("getContratos")]
        public IHttpActionResult GetContratos([FromBody]dynamic request)
        {
            LocalQueriesApp app = new LocalQueriesApp();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            Replay respuesta = new Replay() {
                Status = "OK",
                Info = new Dictionary<string, object> { { "data", app.GetContratos(sessionId[0]) } },
                IdConv = sessionId[0]
            };
           
            return Json(respuesta);
        }
        [HttpPost]
        [Route("getBeneficiariosContrato")]
        public IHttpActionResult GetBeneficiariosContratos([FromBody]dynamic request)
        {
            LocalQueriesApp app = new LocalQueriesApp();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            int idContrato = request["idContrato"];
            Replay respuesta = new Replay()
            {
                Status = "OK",
                Info = new Dictionary<string, object> { { "data", app.GetBeneficiariosContrato(idContrato, sessionId[0]) } },
                IdConv = sessionId[0]
            };
            return Json(respuesta);
        }
        [HttpPost]
        [Route("getCiudadBeneficiario")]
        public IHttpActionResult GetCiudadBeneficiario([FromBody]dynamic request)
        {
            LocalQueriesApp app = new LocalQueriesApp();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            int idUsuario = request["idUsuario"];
            Replay respuesta = new Replay()
            {
                Status = "OK",
                Info = new Dictionary<string, object> { { "data", app.GetCiudadBeneficiario(idUsuario, sessionId[0]) } },
                IdConv = sessionId[0]
            };
            return Json(respuesta);
        }
        [HttpPost]
        [Route("getCiudadesBeneficiario")]
        public IHttpActionResult GetCiudadesBeneficiario([FromBody]dynamic request)
        {
            LocalQueriesApp app = new LocalQueriesApp();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            int idUsuario = request["idUsuario"];
            Replay respuesta = new Replay()
            {
                Status = "OK",
                Info = new Dictionary<string, object> { { "data", app.GetCiudadesBeneficiario(idUsuario, sessionId[0]) } },
                IdConv = sessionId[0]
            };
            return Json(respuesta);
        }
        [HttpPost]
        [Route("getEspecialidades")]
        public IHttpActionResult GetEspecialidades([FromBody]dynamic request)
        {
            LocalQueriesApp app = new LocalQueriesApp();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            Replay respuesta = new Replay()
            {
                Status = "OK",
                Info = new Dictionary<string, object> { { "data", app.GetEspecialidades(sessionId[0]) } },
                IdConv = sessionId[0]
            };
            return Json(respuesta);
        }
        [HttpPost]
        [Route("getMedicos")]
        public IHttpActionResult GetMedicos([FromBody]dynamic request)
        {
            LocalQueriesApp app = new LocalQueriesApp();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            Replay respuesta = new Replay()
            {
                Status = "OK",
                Info = new Dictionary<string, object> { { "data", app.GetMedicos(sessionId[0]) } },
                IdConv = sessionId[0]
            };
            return Json(respuesta);
        }
        [HttpPost]
        [Route("getCentrosMedicos")]
        public IHttpActionResult GetCentrosMedicos([FromBody]dynamic request)
        {
            LocalQueriesApp app = new LocalQueriesApp();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            Replay respuesta = new Replay()
            {
                Status = "OK",
                Info = new Dictionary<string, object> { { "data", app.GetCentrosMedicos(sessionId[0]) } },
                IdConv = sessionId[0]
            };
            return Json(respuesta);
        }
        [HttpPost]
        [Route("getProximasCitas")]
        public IHttpActionResult GetProximasCitas([FromBody]dynamic request)
        {
            LocalQueriesApp app = new LocalQueriesApp();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            string fecha = request["fecha"];
            Replay respuesta = new Replay()
            {
                Status = "OK",
                Info = new Dictionary<string, object> { { "data", app.GetCitasProximas(fecha,sessionId[0]) } },
                IdConv = sessionId[0]
            };
            return Json(respuesta);
        }
        [HttpPost]
        [Route("getCitasMedico")]
        public IHttpActionResult GetCitasMedico([FromBody]dynamic request)
        {
            LocalQueriesApp app = new LocalQueriesApp();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            int idMedico = (int) request["idMedico"];
            Replay respuesta = new Replay()
            {
                Status = "OK",
                Info = new Dictionary<string, object> { { "data", app.GetCitasMedico(idMedico, sessionId[0]) } },
                IdConv = sessionId[0]
            };
            return Json(respuesta);
        }
        [HttpPost]
        [Route("getCitasCentroMedico")]
        public IHttpActionResult GetCitasCentroMedico([FromBody]dynamic request)
        {
            LocalQueriesApp app = new LocalQueriesApp();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            int idCentroMedico = (int)request["idCentroMedico"];
            Replay respuesta = new Replay()
            {
                Status = "OK",
                Info = new Dictionary<string, object> { { "data", app.GetCitasCentroMedico(idCentroMedico, sessionId[0]) } },
                IdConv = sessionId[0]
            };
            return Json(respuesta);
        }
    }
}
