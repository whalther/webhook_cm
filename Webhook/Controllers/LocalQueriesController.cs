using Application;
using Domain.DTOs;
using Newtonsoft.Json;
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
                Status = "ok",
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
                Status = ct.Count > 0 ? "ok" : "empty",
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
            string idContrato = request["idContrato"];
            ResultBeneficiarios bens = app.GetBeneficiariosContrato(idContrato, sessionId[0]);
            Replay respuesta = new Replay()
            {
                Status = bens.Beneficiarios.Count > 0 ? "ok" : "empty",
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
                Status = !(ciu is null) ? "ok" : "empty",
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
                Status = !(cius is null) ? "ok" : "empty",
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
                Status = espes.Count > 0 ? "ok" : "empty",
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
                Status = meds.Count > 0 ? "ok" : "empty",
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
                Status = cms.Count > 0 ? "ok" : "empty",
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
                Status = citas.Count > 0 ? "ok" : "empty",
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
                Status = citas.Count > 0 ? "ok" : "empty",
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
                Status = citas.Count > 0 ? "ok" : "empty",
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
                Status = res ? "ok" : "error"
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
                Status = res ? "ok" : "error"
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
                Status = res ? "ok" : "error"
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
                Status = res ? "ok" : "error"
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
                Status = res !="" ? "ok" : "error",
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
            app.AsignarCita(idConv,numDoc,tipoDoc,numeroCelular,token);
            return GetInfoCitaAgendada(request);
           
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
                Info = new Dictionary<string, object> { { "data", resultEncode },{ "statusCita",statusCita }, { "numEspacioCita", res.numEspacioCita } }
            };
            return Json(respuesta);
        }
        [HttpPost]
        [Route("getCitasBeneficiario")]
        public IHttpActionResult GetCitasBeneficiario([FromBody]dynamic request)
        {
            LocalQueriesApp app = new LocalQueriesApp();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            List<CitaBeneficiario> citas = app.GetCitasBeneficiario(sessionId[0]);
            Replay respuesta = new Replay()
            {
                Status = citas.Count > 0 ? "ok" : "empty",
                Info = new Dictionary<string, object> { { "data", citas } },
                IdConv = sessionId[0]
            };
            return Json(respuesta);
        }
        [HttpPost]
        [Route("getInfoCitaBeneficiario")]
        public IHttpActionResult GetInfoCitaBeneficiario([FromBody]dynamic request)
        {
            LocalQueriesApp app = new LocalQueriesApp();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            int idCita = request["idCita"];
            CitaBeneficiario cita = app.GetInfoCitaBeneficiario(sessionId[0],idCita);
            Replay respuesta = new Replay()
            {
                Status = cita != null  ? "ok" : "empty",
                Info = new Dictionary<string, object> { { "data", cita } },
                IdConv = sessionId[0]
            };
            return Json(respuesta);
        }
        [HttpPost]
        [Route("procesarCancelacionCita")]
        public void ProcesarCancelacionCita([FromBody]dynamic request)
        {
            LocalQueriesApp app = new LocalQueriesApp();
            Utilities utilidad = new Utilities();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            string idConv = sessionId[0];
            string numeroCelular = utilidad.GetNumero(sessionId[1]);
            string numDocConv = request["numDoc"] ;
            string tipoDocConv = request["tipoDoc"];
            string identificacionCotizante = request["tipoDocCotizante"] + request["numDocCotizante"];
            string identificacionBeneficiario = request["tipoDocBeneficiario"] + request["numDocBeneficiario"];
            string token = request["token"];
            int idCita = request["idCita"];
            app.CancelarCitaBeneficiario(idConv,numDocConv,tipoDocConv,identificacionBeneficiario,identificacionCotizante,idCita,numeroCelular,token);
        }
        [HttpPost]
        [Route("getEstadoCancelacionCita")]
        public IHttpActionResult GetEstadoCancelacionCita([FromBody]dynamic request)
        {
            LocalQueriesApp app = new LocalQueriesApp();
            Replay respuesta = new Replay();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            string idConv = sessionId[0];
            int idCita = (int)request["idCita"];
            respuesta.IdConv = idConv;
            string res = app.GetEstadoCancelacion(idConv,idCita);
           
                if (string.IsNullOrEmpty(res))
                {
                    respuesta.Status = "processing";
                }
                else if (res != "error_credenciales" && res != "error_parametros" && res != "error_desconocido" && res.Length>15)
                {
                dynamic jsonRes = JObject.Parse(res);
                if (jsonRes.Resultado == "1")
                    {
                        respuesta.Status = "ok";
                    }
                    else
                    {
                        respuesta.Status = "error_cancelar";
                    }
                respuesta.Message = jsonRes.Mensaje;
                respuesta.Info.Add("data",jsonRes.Resultado);
                 }
                else
                {
                    respuesta.Status = "error";
                    respuesta.Info.Add("data",res);
                }
            return Json(respuesta);
        }
    }
}
