using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Application;
using Domain.DTOs;

namespace Webhook.Controllers
{

    [RoutePrefix("api/scheduling")]
    [RequestFilter]
    public class SchedulingPetitionsController : ApiController
    {
        Utilities utilidad = new Utilities();
        [HttpPost]
        [Route("validarUsuario")]
        public IHttpActionResult ValidarUsuario([FromBody]dynamic request)
        {
            SchedulingPetitionsApp app = new SchedulingPetitionsApp();
            Replay respuesta = new Replay();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            string numeroCelular = utilidad.GetNumero(sessionId[1]);
            string identificacion = request["tipoDoc"] + request["numDoc"];
            string idConv = sessionId[0];
            string token = request["token"];

            respuesta.IdConv = idConv;
            Resultado res = app.ValidarUsuario(identificacion, numeroCelular, token, idConv);
            respuesta.Token = res.Token;
            Usuario usuario = (Usuario)res.Result;
            if (usuario.Mensaje != "error_parametros" && usuario.Mensaje != "error_desconocido")
            {
                respuesta.Status = "OK";
                respuesta.Info.Add("data", usuario);
            }
            else {
                respuesta.Status = "error";
                respuesta.Info.Add("data", usuario.Mensaje);
            }
            

            return Json(respuesta);
        }

        [HttpPost]
        [Route("getBeneficiariosContratante")]
        public IHttpActionResult GetBeneficiariosContratante([FromBody]dynamic request)
        {
            SchedulingPetitionsApp app = new SchedulingPetitionsApp();
            Replay respuesta = new Replay();

            string[] sessionId = request["sessionId"].ToString().Split('*');
            string numeroCelular = utilidad.GetNumero(sessionId[1]);
            string identificacion = request["tipoDoc"] + request["numDoc"];
            string idConv = sessionId[0];
            string token = request["token"];
            respuesta.IdConv = idConv;
            Resultado res = app.GetBeneficiariosContratante(identificacion,token,idConv,numeroCelular);
            respuesta.Token = res.Token;
            List<BeneficiarioContratante> benes = (List<BeneficiarioContratante>)res.Result;
            if (benes[0].Parentesco != "error_parametros" && benes[0].Parentesco != "error_desconocido")
            {
                respuesta.Status = "OK";
                respuesta.Info.Add("data", benes);
            }
            else
            {
                respuesta.Status = "error";
                respuesta.Info.Add("data", benes[0].Parentesco);
            }
            return Json(respuesta);
        }
       /* [HttpPost]
        [Route("getCiudades")]
        public IHttpActionResult GetCiudades([FromBody]dynamic request)
        {
            SchedulingPetitionsApp app = new SchedulingPetitionsApp();
            Replay respuesta = new Replay();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            string numeroCelular = sessionId[1].Substring(2);
            string identificacion = request["numDoc"];
            string tipoDoc = request["tipoDoc"];
            string idConv = sessionId[0];
            string token = request["token"];

            respuesta.IdConv = idConv;
            Resultado res = app.GetCiudades(identificacion,tipoDoc, token,idConv, numeroCelular);
            respuesta.Token = res.Token;
            List<Ciudad> cius = (List<Ciudad>)res.Result;
            if (cius[0].CiuNombre != "error_parametros" && cius[0].CiuNombre != "error_desconocido")
            {
                respuesta.Status = "OK";
                respuesta.Info.Add("data", cius);
            }
            else
            {
                respuesta.Status = "error";
                respuesta.Info.Add("data", cius[0].CiuNombre);
            }
            return Json(respuesta);
        }*/
       /* [HttpPost]
        [Route("getEspecialidadesCiudad")]
       public IHttpActionResult GetEspecialidadesCiudades([FromBody]dynamic request)
        {
            SchedulingPetitionsApp app = new SchedulingPetitionsApp();
            Replay respuesta = new Replay();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            string numeroCelular = sessionId[1].Substring(2);
            string identificacion = request["numDoc"];
            string tipoDoc = request["tipoDoc"];
            string idConv = sessionId[0];
            string token = request["token"];
            string identificacionChat = request["identificacionChat"];
            int ciudad = (int)request["ciudad"];

            respuesta.IdConv = idConv;
            Resultado res = app.ProcesarEspecialidadesCiudad(identificacion, tipoDoc,ciudad, token, idConv,identificacionChat, numeroCelular);
            respuesta.Token = res.Token;
            List<Especialidad> espes = (List<Especialidad>)res.Result;
            if (espes[0].Nombre != "error_parametros" && espes[0].Nombre != "error_desconocido")
            {
                respuesta.Status = "OK";
                respuesta.Info.Add("data", espes);
            }
            else
            {
                respuesta.Status = "error";
                respuesta.Info.Add("data", espes[0].Nombre);
            }
            return Json(respuesta);
        }*/
        
        [HttpPost]
        [Route("procesarBeneficiariosCiudades")]
        public void ProcesarBeneficiariosCiudades([FromBody]dynamic request)
        {
            SchedulingPetitionsApp app = new SchedulingPetitionsApp();
            string[] sessionId = request["sessionId"].ToString().Split('*');
            string numeroCelular = utilidad.GetNumero(sessionId[1]);
            string identificacion = request["tipoDoc"]+ request["numDoc"];
            string idConv = sessionId[0];
            string token = request["token"];
            app.ProcesarBeneficiariosCiudades(identificacion,token,idConv,numeroCelular);
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
            string identificacionChat = request["tipoDoc"] + request["numDoc"];
            int ciudad = (int)request["ciudad"];
            SchedulingPetitionsApp app = new SchedulingPetitionsApp();
            app.ProcesarEspecialidadesCiudad(identificacion, tipoDoc, ciudad, token, idConv, identificacionChat, numeroCelular);
        }
        [HttpPost]
        [Route("procesarCitas")]
        public void ProcesarCitas([FromBody]dynamic request)
        {
            string[] sessionId = request["sessionId"].ToString().Split('*');
            string numeroCelular = utilidad.GetNumero(sessionId[1]);
            string identificacion = request["tipoDoc"] + request["numDoc"];
            string idConv = sessionId[0];
            string token = request["token"];
            int ciudad = (int)request["ciudad"];
            int especialidad = (int)request["especialidad"];
            SchedulingPetitionsApp app = new SchedulingPetitionsApp();
            app.ProcesarCitas(ciudad, especialidad, token, idConv, numeroCelular, identificacion);
        }
    }
}
