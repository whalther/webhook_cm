//using Application.Services;
using Application;
using Domain.DTOs;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Web.Mvc;



namespace Webhook.Controllers
{

    public class MainController : Controller
    {
        [HttpPost]
        public JsonResult Index()
        {
           
            ResponseDTO respuesta = new ResponseDTO();
           
            
            try
             {
                 var bodyStream = new StreamReader(HttpContext.Request.InputStream);
                 string s = bodyStream.ReadToEnd();

                 RequestDTO request = JsonConvert.DeserializeObject<RequestDTO>(s);
                 string[] sessionString = request.session.Split('/');
                 int lastIndex = sessionString.Length - 1;
                 string action = request.queryResult.action;
                LogApp logApp = new LogApp();
                logApp.GuardarLogPeticion(new LogPeticion()
                {
                    Action = action,
                    Parameters = JsonConvert.SerializeObject(request.queryResult.parameters)
                }) ;
                switch (action)
                 {
                     case "1":
                         respuesta.Source = "webhook_boleta";
                        respuesta.FulfillmentText = "prueba";
                        /* 
                         GetBoletaService getBoleta = new GetBoletaService();
                         respuesta.fulfillmentText = getBoleta.GetBoleta(sessionDto.empleadoId, sessionDto.vhur, sessionDto.fromDb,
                                                                             Convert.ToInt32(request.queryResult.parameters["time"].Substring(0, 4)),
                                                                             Convert.ToInt32(Convert.ToDouble(request.queryResult.parameters["partner_mes"])));                        
                         */
                        break;

                     case "lifemiles_action":
                         respuesta.Source = "Webhook Lifemiles";
                         respuesta.FulfillmentText = action;
                         break;
                     default:
                         respuesta.Source = "No action";
                         respuesta.FulfillmentText = "No se pudo detectar action: "+action;
                         break;
                 }

                 var result = new JsonResult { Data = respuesta };
                return result;
             }
             catch (Exception ex)
             {
                 return new JsonResult { Data = new ResponseDTO() { FulfillmentText = ex.Message, Source = "ERROR" } };                
             }
        }

        


    }

    
}