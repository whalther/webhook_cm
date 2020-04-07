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
           
            ResponseDto respuesta = new ResponseDto();
           
            
            try
             {
                 var bodyStream = new StreamReader(HttpContext.Request.InputStream);
                 string s = bodyStream.ReadToEnd();

                 RequestDto request = JsonConvert.DeserializeObject<RequestDto>(s);
                 string action = request.queryResult.action;
              
                
                switch (action)
                 {
                     case "1":
                         respuesta.Source = "webhook_boleta";
                        respuesta.FulfillmentText = "prueba";
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
                 return new JsonResult { Data = new ResponseDto() { FulfillmentText = ex.Message, Source = "ERROR" } };
                throw;
             }
        }

        


    }

    
}