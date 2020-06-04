using Domain.Repositories;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;

namespace CrossCutting.Repositories
{
    public class AuthenticationRepository:IAuthenticationRepository
    {
        public string GetToken(Dictionary<string, string> headers, Dictionary<string, string> parametros,string idConv) {
            string url= ConfigurationManager.AppSettings["baseUrlApiToken"];
            LogRepository log = new LogRepository();
            RestClient rc = new RestClient();
            var resp = rc.HacerPeticion(url, "GenerarToken", parametros, "POST", headers, true);
            string status = resp.StatusCode.ToString();
            switch (status){
                case "OK":
                    dynamic jsonResp = JsonConvert.DeserializeObject<dynamic>(resp.Content);
                    string token = jsonResp.GenerarTokenResult;
                    return token;
                case "Unauthorized":
                    log.GuardarErrorLogPeticion("error_credenciales", JsonConvert.SerializeObject(parametros), resp.StatusDescription,"GetToken",idConv);
                    return "error_credenciales";
                case "Forbidden":
                    log.GuardarErrorLogPeticion("error_prohibido", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "GetToken", idConv);
                    return "error_prohibido";
                case "NotFound":
                    log.GuardarErrorLogPeticion("error_no_encontrado", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "GetToken", idConv);
                    return "error_no_encontrado";
                default:
                    log.GuardarErrorLogPeticion("error_desconocido", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "GetToken", idConv);
                    return "error_desconocido";
            }
           
        }
        public string RefreshToken(Dictionary<string, string> headers, Dictionary<string, string> parametros,string idConv)
        {
            string url = ConfigurationManager.AppSettings["baseUrlApiToken"];
            RestClient rc = new RestClient();
            LogRepository log = new LogRepository();
            var resp = rc.HacerPeticion(url, "RefrescarToken", parametros, "POST", headers, true);
            string status = resp.StatusCode.ToString();
            switch (status)
            {
                case "OK":
                    dynamic jsonResp = JsonConvert.DeserializeObject<dynamic>(resp.Content);
                    string token = jsonResp.RefrescarTokenResult;
                    return token;
                case "Unauthorized":
                    log.GuardarErrorLogPeticion("error_credenciales", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "RefreshToken", idConv);
                    return "error_credenciales";
                case "Forbidden":
                    log.GuardarErrorLogPeticion("error_parametros", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "RefreshToken", idConv);
                    return "error_parametros";
                default:
                    log.GuardarErrorLogPeticion("error_desconocido", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "RefreshToken", idConv);
                    return "error_desconocido";
            }
        }

        public string ValidaOtp(Dictionary<string, string> headers, Dictionary<string, string> parametros, string idConv) {
            string url = ConfigurationManager.AppSettings["baseUrlApi"];
            RestClient rc = new RestClient();
            LogRepository log = new LogRepository();
            var resp = rc.HacerPeticion(url, "ValidarOtp", parametros, "POST", headers, true);
            string status = resp.StatusCode.ToString();
            switch (status)
            {
                case "OK":
                    return resp.Content;
                case "Unauthorized":
                    log.GuardarErrorLogPeticion("error_token", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "ValidaOtp", idConv);
                    return "error_token";
                case "Forbidden":
                    log.GuardarErrorLogPeticion("error_parametros", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "ValidaOtp", idConv);
                    return "error_parametros";
                default:
                    log.GuardarErrorLogPeticion("error_desconocido", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "ValidaOtp", idConv);
                    return "error_desconocido";
            }
        }
    }
}
