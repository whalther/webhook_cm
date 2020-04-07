using Application;
using Domain.DTOs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
            LocalQueriesApp app = new LocalQueriesApp();
            SchedulingPetitionsApp appS = new SchedulingPetitionsApp();
            app.QueryDummy();
            appS.DummyPetition();
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
            List<TipoDocumento> tps = app.GetTiposDocumento();
            Replay respuesta = new Replay()
            {
                Status = tps.Count > 0 ? "OK" : "empty",
                Info = new Dictionary<string, object> { { "data", tps } },
            };
            return Json(respuesta);
        }
        [HttpPost]
        [Route("getContratos")]
        public IHttpActionResult GetContratos([FromBody]dynamic request)
        {
            LocalQueriesApp app = new LocalQueriesApp();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            List<Contrato> ct = app.GetContratos(sessionId[0]);
            Replay respuesta = new Replay() {
                Status = ct.Count > 0 ? "OK" : "empty",
                Info = new Dictionary<string, object> { { "data", ct } },
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
            ResultBeneficiarios bens = app.GetBeneficiariosContrato(idContrato, sessionId[0]);
            Replay respuesta = new Replay()
            {
                Status = bens.Beneficiarios.Count > 0 ? "OK" : "empty",
                Info = new Dictionary<string, object> { { "data", bens } },
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
            Ciudad ciu = app.GetCiudadBeneficiario(idUsuario, sessionId[0]);
            Replay respuesta = new Replay()
            {
                Status = !(ciu is null) ? "OK" : "empty",
                Info = new Dictionary<string, object> { { "data", ciu } },
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
            List<Ciudad> cius = app.GetCiudadesBeneficiario(idUsuario, sessionId[0]);
            Replay respuesta = new Replay()
            {
                Status = !(cius is null) ? "OK" : "empty",
                Info = new Dictionary<string, object> { { "data", cius } },
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
            List<Especialidad> espes = app.GetEspecialidades(sessionId[0]);
            Replay respuesta = new Replay()
            {
                Status = espes.Count > 0 ? "OK" : "empty",
                Info = new Dictionary<string, object> { { "data", espes } },
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
            List<GlobalResp> meds = app.GetMedicos(sessionId[0]);
            Replay respuesta = new Replay()
            {
                Status = meds.Count > 0 ? "OK" : "empty",
                Info = new Dictionary<string, object> { { "data", meds } },
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
            List<GlobalResp> cms = app.GetCentrosMedicos(sessionId[0]);
            Replay respuesta = new Replay()
            {
                Status = cms.Count > 0 ? "OK" : "empty",
                Info = new Dictionary<string, object> { { "data", cms } },
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
            List<Cita> citas = app.GetCitasProximas(fecha, sessionId[0]);
            Replay respuesta = new Replay()
            {
                Status = citas.Count > 0 ? "OK" : "empty",
                Info = new Dictionary<string, object> { { "data", citas } },
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
            List<Cita> citas = app.GetCitasMedico(idMedico, sessionId[0]);
            Replay respuesta = new Replay()
            {
                Status = citas.Count > 0 ? "OK" : "empty",
                Info = new Dictionary<string, object> { { "data", citas } },
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
            List<Cita> citas = app.GetCitasCentroMedico(idCentroMedico, sessionId[0]);
            Replay respuesta = new Replay()
            {
                Status = citas.Count > 0 ? "OK" : "empty",
                Info = new Dictionary<string, object> { { "data", citas } },
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
            Boolean res = app.UpdateCitaBd(idConv,"cita",espacioCita);
            Replay respuesta = new Replay()
            {
                IdConv = idConv,
                Status = res ? "OK" : "error"
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
                Status = res ? "OK" : "error"
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
                Status = res ? "OK" : "error"
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
                Status = res ? "OK" : "error"
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
        public void AsignarCita([FromBody]dynamic request)
        {
            LocalQueriesApp app = new LocalQueriesApp();
            Utilities utilidad = new Utilities();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            string idConv = sessionId[0];
            string numeroCelular = utilidad.GetNumero(sessionId[1]);
            string tipoDoc = request["tipoDoc"];
            string numDoc = request["numDoc"];
            string token = request["token"];
            app.AsignarCita(idConv,numDoc,tipoDoc,numeroCelular,token);
        }
        [HttpPost]
        [Route("getInfoCitaAgendada")]
        public IHttpActionResult GetInfoCitaAgendada([FromBody]dynamic request)
        {
            LocalQueriesApp app = new LocalQueriesApp();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            string idConv = sessionId[0];
            dynamic res = app.GetInfoCitaAgendada(idConv);
            string resultCita = "";
            string status;
            string statusCita = "processing";
            dynamic resultEncode;
            if (res.estado == 0)
            {
                status = "processing";
            }
            else if (res.estado == 1)
            {
                status = "completed";
                resultCita = res.result;
                if (resultCita.Substring(0, 5) == "error")
                {
                    statusCita = resultCita;
                }
                else
                {
                    statusCita = "ok";

                }
            }
            else { status = "error"; }
            
            if (resultCita.Length > 10 && resultCita.Substring(0, 1) == "{")
            {
                resultEncode = JToken.Parse(resultCita);
                statusCita = resultEncode.Mensaje.ToString().Length != 0 ? "error_agendamiento" : statusCita;
            }
            else
            {
                resultEncode = resultCita;
            }
            Replay respuesta = new Replay()
            {
                IdConv = idConv,
                Status = status,
                Info = new Dictionary<string, object> { { "data", resultEncode },{ "statusCita",statusCita } }
            };
            return Json(respuesta);
        }
    }
}
