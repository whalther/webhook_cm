using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Net.Security;
namespace CrossCutting
{
    public class RestClient
    {
        public IRestResponse HacerPeticion(string ruta, string metodo, Dictionary<string, string> parametros, string metodoEnvio, Dictionary<string, string> headers = null, bool peticionBody = false)
        {
            var client = new RestSharp.RestClient(ruta);
            RestRequest request;
            switch (metodoEnvio)
            {
                case "POST":
                    request = new RestRequest(metodo, Method.POST);
                    break;
                case "GET":
                    request = new RestRequest(metodo, Method.GET);
                    break;
                default:
                    request = new RestRequest();
                    break;
            }

            client.RemoteCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => {
                return sslPolicyErrors == SslPolicyErrors.None;
            };
            if (headers != null && headers.Count > 0)
            {
                foreach (var item in headers)
                {
                    client.AddDefaultHeader(item.Key, item.Value);
                }
            }

            if (peticionBody) {
                request.AddJsonBody(parametros);
            }
            else {
                foreach (var p in parametros)
                {
                    request.AddParameter(p.Key, p.Value);
                }
                
            }
            
            return client.Execute(request);
        }
        
    }
}
