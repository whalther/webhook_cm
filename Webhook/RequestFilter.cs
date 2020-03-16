using Application;
using Domain.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Webhook
{
    public class RequestFilter : AuthorizeAttribute
    {
        public override void OnAuthorization(
              System.Web.Http.Controllers.HttpActionContext actionContext)
        {
                var ip = GetClientIpAddress(actionContext.Request);
                string path = actionContext.Request.RequestUri.AbsolutePath;
                LogApp logApp = new LogApp();
                logApp.GuardarLogPeticion(new LogPeticion()
                {
                    //   ContenidoPeticion = JsonConvert.SerializeObject(actionContext.Request.RequestUri.Query.Trim(new Char[]{'?'}).Split('&')),
                    ContenidoPeticion = JsonConvert.SerializeObject(actionContext.Request.Content.ReadAsStringAsync().Result.Split('&')),
                    //ContenidoPeticion = JsonConvert.SerializeObject(actionContext.Request.RequestUri.ToString().Split('?')[1]),
                    Ip = ip,
                    Path = path
                });
           
        }

        private string GetClientIpAddress(HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return IPAddress.Parse(((HttpContextBase)request.Properties["MS_HttpContext"]).Request.UserHostAddress).ToString();
            }

            return String.Empty;
        }
    }
}