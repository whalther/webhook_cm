using System.Collections.Generic;
using System.Web.Http;
using Application;
using Domain.DTOs;

namespace Webhook.Controllers
{

    [RoutePrefix("api/scheduling")]
    public class SchedulingPetitionsController : ApiController
    {
        readonly Utilities utilidad = new Utilities();
        [HttpPost]
        [Route("validarUsuario")]
        public IHttpActionResult ValidarUsuario([FromBody]dynamic request)
        {
            SchedulingPetitionsApp app = new SchedulingPetitionsApp();
            Replay respuesta = new Replay();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            string numeroCelular = utilidad.GetNumero(sessionId[1]);
            string numDoc = request["numDoc"];
            string tipoDoc = request["tipoDoc"];
            string identificacionBeneficiario = request["tipoDocBeneficiario"]+ request["numDocBeneficiario"];
            string idConv = sessionId[0];
            string token = request["token"];

            respuesta.IdConv = idConv;
            Resultado res = app.ValidarUsuario(numDoc,tipoDoc,identificacionBeneficiario, numeroCelular, token, idConv);
            respuesta.Token = res.Token;
            Usuario usuario = (Usuario)res.Result;
            if (usuario.Mensaje != "error_parametros" && usuario.Mensaje != "error_desconocido")
            {
                respuesta.Status = "ok";
                respuesta.Info.Add("data", usuario);
            }
            else {
                respuesta.Status = "error";
                respuesta.Info.Add("data", usuario.Mensaje);
            }
            

            return Json(respuesta);
        }
        [HttpPost]
        [Route("procesarBeneficiariosCiudades")]
        public void ProcesarBeneficiariosCiudades([FromBody]dynamic request)
        {
            SchedulingPetitionsApp app = new SchedulingPetitionsApp();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            string numeroCelular = utilidad.GetNumero(sessionId[1]);
            string numDoc = request["numDoc"];
            string tipoDoc = request["tipoDoc"];
            string idConv = sessionId[0];
            string token = request["token"];
            app.ProcesarBeneficiariosCiudades(numDoc,tipoDoc,token,idConv,numeroCelular);
        }
        [HttpPost]
        [Route("procesarEspecialidades")]
        public IHttpActionResult ProcesarEspecialidades([FromBody]dynamic request)
        {
            string[] sessionId = request["sessionId"].ToString().Split('*');
            string numeroCelular = utilidad.GetNumero(sessionId[1]);
            string identificacion = request["numDocBeneficiario"];
            string tipoDoc = request["tipoDocBeneficiario"];
            string idConv = sessionId[0];
            string token = request["token"];
            string numDocChat = request["numDoc"];
            string tipoDocChat = request["tipoDoc"];
            int ciudad = (int)request["ciudad"];
            Replay respuesta = new Replay();
            SchedulingPetitionsApp app = new SchedulingPetitionsApp();
            Resultado res= app.ProcesarEspecialidadesCiudad(identificacion, tipoDoc, ciudad, token, idConv, numDocChat,tipoDocChat, numeroCelular);
            if (res.Result.ToString() != "error_credenciales" && res.Result.ToString() != "error_parametros" && res.Result.ToString() != "error_desconocido" && res.Result.ToString() != "error_bd")
            {
                List<Especialidad> espes = (List<Especialidad>)res.Result;
                respuesta.IdConv = idConv;
                respuesta.Token = res.Token;
                if(espes.Count==0)
                {
                    respuesta.Status = "empty";
                    respuesta.Info.Add("data", "");
                }
                else if (espes[0].Nombre != "error_parametros" && espes[0].Nombre != "error_desconocido" && espes[0].Nombre != "error_credenciales" && espes[0].Nombre != "error_token")
                {
                    respuesta.Status = "ok";
                    respuesta.Info.Add("data", espes);
                }
                else
                {
                    respuesta.Status = "error";
                    respuesta.Info.Add("data", espes[0].Nombre);
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
        [Route("procesarCitas")]
        public void ProcesarCitas([FromBody]dynamic request)
        {
            string[] sessionId = request["sessionId"].ToString().Split('*');
            string numeroCelular = utilidad.GetNumero(sessionId[1]);
            string numDoc = request["numDoc"];
            string tipoDoc = request["tipoDoc"];
            string idConv = sessionId[0];
            string token = request["token"];
            int ciudad = (int)request["ciudad"];
            string especialidad = request["especialidad"];
            SchedulingPetitionsApp app = new SchedulingPetitionsApp();
            app.ProcesarCitas(ciudad, especialidad, token, idConv, numeroCelular, numDoc,tipoDoc);
        }
        [HttpPost]
        [Route("procesarCitasBeneficiario")]
        public IHttpActionResult ProcesarCitasBeneficiario([FromBody]dynamic request)
        {
            string[] sessionId = request["sessionId"].ToString().Split('*');
            string numeroCelular = utilidad.GetNumero(sessionId[1]);
            string numDoc = request["numDoc"];
            string tipoDoc = request["tipoDoc"];
            string idConv = sessionId[0];
            string token = request["token"];
            string idUsuario = request["idUsuario"];
            SchedulingPetitionsApp app = new SchedulingPetitionsApp();
            app.ProcesarCitasBeneficiario(numDoc,tipoDoc,token,idConv,numeroCelular,idUsuario);
            LocalQueriesApp appLq = new LocalQueriesApp();
            List<CitaBeneficiario> citas = appLq.GetCitasBeneficiario(sessionId[0]);
            Replay respuesta = new Replay()
            {
                Status = citas.Count > 0 ? "ok" : "empty",
                Info = new Dictionary<string, object> { { "data", citas } },
                IdConv = request["sessionId"]
            };
            return Json(respuesta);
        }
    }
}
