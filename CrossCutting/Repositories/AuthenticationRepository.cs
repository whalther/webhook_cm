using Domain.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace CrossCutting.Repositories
{
    public class AuthenticationRepository:IAuthenticationRepository
    {
        public string GetToken(Dictionary<string, string> headers, Dictionary<string, string> parametros) {
            string url= ConfigurationManager.AppSettings["baseUrlApiToken"];
            RestClient rc = new RestClient();
            var resp = rc.HacerPeticion(url, "GenerarToken", parametros, "POST", headers, true);
            string status = resp.StatusCode.ToString();
            switch (status){
                case "OK":
                    dynamic jsonResp = JsonConvert.DeserializeObject<dynamic>(resp.Content);
                    string token = jsonResp.GenerarTokenResult;
                    return token;
                case "Unauthorized":
                    return "error_credenciales";
                case "Forbidden":
                    return "error_parametros";
                default:
                    return "error_desconocido";
            }
           
        }
        public string RefreshToken(Dictionary<string, string> headers, Dictionary<string, string> parametros)
        {
            string url = ConfigurationManager.AppSettings["baseUrlApiToken"];
            RestClient rc = new RestClient();
            var resp = rc.HacerPeticion(url, "RefrescarToken", parametros, "POST", headers, true);
            string status = resp.StatusCode.ToString();
            switch (status)
            {
                case "OK":
                    dynamic jsonResp = JsonConvert.DeserializeObject<dynamic>(resp.Content);
                    string token = jsonResp.RefrescarTokenResult;
                    return token;
                case "Unauthorized":
                    return "error_credenciales";
                case "Forbidden":
                    return "error_parametros";
                default:
                    return "error_desconocido";
            }
        }

        public string ValidaOtp(Dictionary<string, string> headers, Dictionary<string, string> parametros) {
            string url = ConfigurationManager.AppSettings["baseUrlApi"];
            RestClient rc = new RestClient();
            var resp = rc.HacerPeticion(url, "ValidarOtp", parametros, "POST", headers, true);
            string status = resp.StatusCode.ToString();
            switch (status)
            {
                case "OK":
                    return resp.Content;
                case "Unauthorized":
                    return "error_token";
                case "Forbidden":
                    return "error_parametros";
                default:
                    return "error_desconocido";
            }
        }
    }
}
