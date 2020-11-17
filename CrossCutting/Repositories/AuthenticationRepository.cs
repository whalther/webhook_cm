using Domain.Repositories;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;

namespace CrossCutting.Repositories
{
    public class AuthenticationRepository:IAuthenticationRepository
    {
        public string ValidarCliente(Dictionary<string, string> headers, Dictionary<string, string> parametros,string idConv) {
            string url= ConfigurationManager.AppSettings["baseUrlApiToken"];
            LogRepository log = new LogRepository();
            RestClient rc = new RestClient();
            var resp = rc.HacerPeticion(url, "ValidarCliente", parametros, "POST", headers, true);
            string status = resp.StatusCode.ToString();
            switch (status){
                case "OK":
                    return resp.Content;
                case "Unauthorized":
                    log.GuardarErrorLogPeticion("error_credenciales", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "ValidarCliente", idConv);
                    return "error_credenciales";
                case "Forbidden":
                    log.GuardarErrorLogPeticion("error_prohibido", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "ValidarCliente", idConv);
                    return "error_prohibido";
                case "NotFound":
                    log.GuardarErrorLogPeticion("error_no_encontrado", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "ValidarCliente", idConv);
                    return "error_no_encontrado";
                default:
                    log.GuardarErrorLogPeticion("error_desconocido", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "ValidarCliente", idConv);
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
            string url = ConfigurationManager.AppSettings["baseUrlApiToken"];
            RestClient rc = new RestClient();
            LogRepository log = new LogRepository();
            var resp = rc.HacerPeticion(url, "ValidarOtp", parametros, "POST", headers, true);
            string status = resp.StatusCode.ToString();
            switch (status)
            {
                case "OK":
                    dynamic jsonResp = JsonConvert.DeserializeObject<dynamic>(resp.Content);
                    string token = jsonResp.ValidarOtpResult;
                    return token; 
                case "NotAcceptable":
                    log.GuardarErrorLogPeticion("otp_utilizado", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "ValidaOtp", idConv);
                    return "otp_utilizado";
                case "Forbidden":
                    log.GuardarErrorLogPeticion("otp_invalido", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "ValidaOtp", idConv);
                    return "otp_invalido";
                case "RequestTimeout":
                    log.GuardarErrorLogPeticion("otp_vencido", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "ValidaOtp", idConv);
                    return "otp_vencido";
                default:
                    log.GuardarErrorLogPeticion("error_desconocido", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "ValidaOtp", idConv);
                    return "error_desconocido";
            }
        }
    }
}
