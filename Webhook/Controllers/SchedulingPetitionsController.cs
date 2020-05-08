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
        public void ProcesarEspecialidades([FromBody]dynamic request)
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
            SchedulingPetitionsApp app = new SchedulingPetitionsApp();
            app.ProcesarEspecialidadesCiudad(identificacion, tipoDoc, ciudad, token, idConv, numDocChat,tipoDocChat, numeroCelular);
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
            int especialidad = (int)request["especialidad"];
            SchedulingPetitionsApp app = new SchedulingPetitionsApp();
            app.ProcesarCitas(ciudad, especialidad, token, idConv, numeroCelular, numDoc,tipoDoc);
        }
        [HttpPost]
        [Route("procesarCitasBeneficiario")]
        public void ProcesarCitasBeneficiario([FromBody]dynamic request)
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
        }
    }
}
