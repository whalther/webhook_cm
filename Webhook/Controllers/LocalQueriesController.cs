using Application;
using Domain.DTOs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;

namespace Webhook.Controllers
{
    [RoutePrefix("api/localqueries")]
    public class LocalQueriesController : ApiController
    {
        [HttpPost]
        [Route("levantarApp")]
        public IHttpActionResult LevantarApp()
        {
            Replay respuesta = new Replay()
            {
                Status = "OK",
            };
            return Json(respuesta);
        }
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
        [HttpPost]
        [Route("updateEspacioCita")]
        public IHttpActionResult UpdateEspacioCitaBd([FromBody]dynamic request)
        {
            LocalQueriesApp app = new LocalQueriesApp();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            string idConv = sessionId[0];
            string espacioCita = request["espacioCita"];
            Boolean res = app.UpdateCitaBd(idConv,"numEspacioCita",espacioCita);
            Replay respuesta = new Replay()
            {
                IdConv = idConv,
                Status = res == true ? "OK" : "error"
            };
            return Json(respuesta);
        }
        [HttpPost]
        [Route("updateDocumentoCita")]
        public IHttpActionResult UpdateDocumentoCitaBd([FromBody]dynamic request)
        {
            LocalQueriesApp app = new LocalQueriesApp();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            string idConv = sessionId[0];
            string documento = request["tipoDoc"]+"*"+request["numDoc"];
            Boolean res = app.UpdateCitaBd(idConv, "documento", documento);
            Replay respuesta = new Replay()
            {
                IdConv = idConv,
                Status = res == true ? "OK" : "error"
            };
            return Json(respuesta);
        }
        [HttpPost]
        [Route("updateMedicoCita")]
        public IHttpActionResult UpdateMedicoCitaBd([FromBody]dynamic request)
        {
            LocalQueriesApp app = new LocalQueriesApp();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            string idConv = sessionId[0];
            string idMedico = request["idMedico"];
            Boolean res = app.UpdateCitaBd(idConv, "idMedico", idMedico);
            Replay respuesta = new Replay()
            {
                IdConv = idConv,
                Status = res == true ? "OK" : "error"
            };
            return Json(respuesta);
        }
        [HttpPost]
        [Route("updateCentroMedicoCita")]
        public IHttpActionResult UpdateCentroMedicoCitaBd([FromBody]dynamic request)
        {
            LocalQueriesApp app = new LocalQueriesApp();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            string idConv = sessionId[0];
            string idCentroMedico = request["idCentroMedico"];
            Boolean res = app.UpdateCitaBd(idConv, "centroMedico", idCentroMedico);
            Replay respuesta = new Replay()
            {
                IdConv = idConv,
                Status = res == true ? "OK" : "error"
            };
            return Json(respuesta);
        }
        [HttpPost]
        [Route("updateEspecialidadCita")]
        public IHttpActionResult UpdateEspecialidadCita([FromBody]dynamic request)
        {
            LocalQueriesApp app = new LocalQueriesApp();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            string idConv = sessionId[0];
            string especialidad = request["especialidad"];
            Boolean res = app.UpdateCitaBd(idConv, "especialidad", especialidad);
            Replay respuesta = new Replay()
            {
                IdConv = idConv,
                Status = res == true ? "OK" : "error"
            };
            return Json(respuesta);
        }
        [HttpPost]
        [Route("limpiarTablas")]
        public IHttpActionResult LimpiarTablas([FromBody]dynamic request)
        {
            LocalQueriesApp app = new LocalQueriesApp();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            string idConv = sessionId[0];
            Boolean res = app.LimpiarTablas(idConv);
            Replay respuesta = new Replay() {
                IdConv = idConv,
                Status = res == true ? "OK" : "error"
            };
            return Json(respuesta);
        }
        [HttpPost]
        [Route("getInfoCita")]
        public IHttpActionResult GetInfoCita([FromBody]dynamic request)
        {
            LocalQueriesApp app = new LocalQueriesApp();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            string idConv = sessionId[0];
            dynamic res = app.GetInfoCita(idConv);
            Replay respuesta = new Replay()
            {
                IdConv = idConv,
                Status = res !="" ? "OK" : "error",
                Info = new Dictionary<string, object> { { "data", res } }
            };
            return Json(respuesta);
        }
        [HttpPost]
        [Route("asignarCita")]
        public IHttpActionResult AsignarCita([FromBody]dynamic request)
        {
            LocalQueriesApp app = new LocalQueriesApp();
            Utilities utilidad = new Utilities();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            string idConv = sessionId[0];
            string numeroCelular = utilidad.GetNumero(sessionId[1]);
            string tipoDoc = request["tipoDoc"];
            string numDoc = request["numDoc"];
            string token = request["token"];
            dynamic res = app.AsignarCita(idConv,numDoc,tipoDoc,numeroCelular,token);
            Replay respuesta = new Replay() {
            IdConv = idConv
            };
            if (res != "error_parametros" && res != "error_desconocido")
            {
                respuesta.Status = "OK";
                respuesta.Info.Add("data", JToken.Parse(res));
            }
            else
            {
                respuesta.Status = "error";
                respuesta.Info.Add("data", res);
            }
            return Json(respuesta);
        }
    }
}
