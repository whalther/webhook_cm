using Domain.Repositories;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;


namespace CrossCutting.Repositories
{
    public class SchedulingPetitionsRepository:ISchedulingPetitionsRepository
    {
        public string ValidarUsuario(Dictionary<string, string> headers, Dictionary<string, string> parametros,string idConv) {
            string url = ConfigurationManager.AppSettings["baseUrlApi"];
            RestClient rc = new RestClient();
            LogRepository log = new LogRepository();
            var resp = rc.HacerPeticion(url, "VerificaUsuario", parametros, "POST", headers, true);
            string status = resp.StatusCode.ToString();
            switch (status)
            {
                case "OK":
                    return resp.Content;
                case "Unauthorized":
                    log.GuardarErrorLogPeticion("error_token", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "ValidarUsuario", idConv);
                    return "error_token";
                case "Forbidden":
                    log.GuardarErrorLogPeticion("error_parametros", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "ValidarUsuario", idConv);
                    return "error_parametros";
                default:
                    log.GuardarErrorLogPeticion("error_desconocido", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "ValidarUsuario", idConv);
                    return "error_desconocido";
            }
        }

        public string GetBeneficiariosContratante(Dictionary<string, string> headers, Dictionary<string, string> parametros,string idConv) 
        {
            string url = ConfigurationManager.AppSettings["baseUrlApi"];
            RestClient rc = new RestClient();
            LogRepository log = new LogRepository();
            var resp = rc.HacerPeticion(url, "BeneficiariosContratante", parametros, "POST", headers, true);
            string status = resp.StatusCode.ToString();
            switch (status)
            {
                case "OK":
                    return resp.Content;
                case "Unauthorized":
                    log.GuardarErrorLogPeticion("error_token", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "GetBeneficiariosContratante", idConv);
                    return "error_token";
                case "Forbidden":
                    log.GuardarErrorLogPeticion("error_parametros", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "GetBeneficiariosContratante", idConv);
                    return "error_parametros";
                default:
                    log.GuardarErrorLogPeticion("error_desconocido", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "GetBeneficiariosContratante", idConv);
                    return "error_desconocido";
            }
        }
        public string GetCiudadesUsuario(Dictionary<string, string> headers, Dictionary<string, string> parametros,string idConv)
        {
            string url = ConfigurationManager.AppSettings["baseUrlApi"];
            RestClient rc = new RestClient();
            LogRepository log = new LogRepository();
            var resp = rc.HacerPeticion(url, "getCiudades", parametros, "POST", headers, true);
            string status = resp.StatusCode.ToString();
            switch (status)
            {
                case "OK":
                    return resp.Content;
                case "Unauthorized":
                    log.GuardarErrorLogPeticion("error_token", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "GetCiudadesUsuario", idConv);
                    return "error_token";
                case "Forbidden":
                    log.GuardarErrorLogPeticion("error_parametros", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "GetCiudadesUsuario", idConv);
                    return "error_parametros";
                default:
                    log.GuardarErrorLogPeticion("error_desconocido", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "GetCiudadesUsuario", idConv);
                    return "error_desconocido";
            }
        }
        public string ProcesarEspecialidadesCiudad(Dictionary<string, string> headers, Dictionary<string, string> parametros,string idConv)
        {
            string url = ConfigurationManager.AppSettings["baseUrlApi"];
            RestClient rc = new RestClient();
            LogRepository log = new LogRepository();
            var resp = rc.HacerPeticion(url, "especialidadesCiudad", parametros, "POST", headers, true);
            string status = resp.StatusCode.ToString();
            switch (status)
            {
                case "OK":
                    return resp.Content;
                case "Unauthorized":
                    log.GuardarErrorLogPeticion("error_token", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "ProcesarEspecialidadesCiudad", idConv);
                    return "error_token";
                case "Forbidden":
                    log.GuardarErrorLogPeticion("error_parametros", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "ProcesarEspecialidadesCiudad", idConv);
                    return "error_parametros";
                default:
                    log.GuardarErrorLogPeticion("error_desconocido", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "ProcesarEspecialidadesCiudad", idConv);
                    return "error_desconocido";
            }
        }
        public string ProcesarCitas(Dictionary<string, string> headers, Dictionary<string, string> parametros,string idConv)
        {
            string url = ConfigurationManager.AppSettings["baseUrlApi"];
            RestClient rc = new RestClient();
            LogRepository log = new LogRepository();
            var resp = rc.HacerPeticion(url, "consultarCitasCiudad", parametros, "POST", headers, true);
            string status = resp.StatusCode.ToString();
            switch (status)
            {
                case "OK":
                    return resp.Content;
                case "Unauthorized":
                    log.GuardarErrorLogPeticion("error_token", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "ProcesarCitas", idConv);
                    return "error_token";
                case "Forbidden":
                    log.GuardarErrorLogPeticion("error_parametros", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "ProcesarCitas", idConv);
                    return "error_parametros";
                default:
                    log.GuardarErrorLogPeticion("error_desconocido", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "ProcesarCitas", idConv);
                    return "error_desconocido";
            }
        }
        public string AsignarCita(Dictionary<string, string> headers, Dictionary<string, string> parametros, string idConv)
        {
            string url = ConfigurationManager.AppSettings["baseUrlApi"];
            RestClient rc = new RestClient();
            LogRepository log = new LogRepository();
            var resp = rc.HacerPeticion(url, "asignarCita", parametros, "POST", headers, true);
            string status = resp.StatusCode.ToString();
            switch (status)
            {
                case "OK":
                    return resp.Content;
                case "Unauthorized":
                    log.GuardarErrorLogPeticion("error_token", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "AsignarCita", idConv);
                    return "error_token";
                case "Forbidden":
                    log.GuardarErrorLogPeticion("error_parametros", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "AsignarCita", idConv);
                    return "error_parametros";
                default:
                    log.GuardarErrorLogPeticion("error_desconocido", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "AsignarCita", idConv);
                    return "error_desconocido";
            }
        }
        public void DummyPetition()
        {
            string url = ConfigurationManager.AppSettings["baseUrlApi"];
            RestClient rc = new RestClient();
            rc.HacerPeticion(url, "VerificaUsuario", null, "POST", null, true);
        }
        public string ConsultarCitasBeneficiario(Dictionary<string, string> headers, Dictionary<string, string> parametros, string idConv)
        {
            string url = ConfigurationManager.AppSettings["baseUrlApi"];
            RestClient rc = new RestClient();
            LogRepository log = new LogRepository();
            var resp = rc.HacerPeticion(url, "consultarCitasBeneficiario", parametros, "POST", headers, true);
            string status = resp.StatusCode.ToString();
            switch (status)
            {
                case "OK":
                    return resp.Content;
                case "Unauthorized":
                    log.GuardarErrorLogPeticion("error_token", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "ConsultarCitasBeneficiario", idConv);
                    return "error_token";
                case "Forbidden":
                    log.GuardarErrorLogPeticion("error_parametros", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "ConsultarCitasBeneficiario", idConv);
                    return "error_parametros";
                default:
                    log.GuardarErrorLogPeticion("error_desconocido", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "ConsultarCitasBeneficiario", idConv);
                    return "error_desconocido";
            }
        }
        public string CancelarCitaBeneficiario(Dictionary<string, string> headers, Dictionary<string, string> parametros, string idConv)
        {
            string url = ConfigurationManager.AppSettings["baseUrlApi"];
            RestClient rc = new RestClient();
            LogRepository log = new LogRepository();
            var resp = rc.HacerPeticion(url, "cancelarCitaBeneficiario", parametros, "POST", headers, true);
            string status = resp.StatusCode.ToString();
            switch (status)
            {
                case "OK":
                    return resp.Content;
                case "Unauthorized":
                    log.GuardarErrorLogPeticion("error_token", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "CancelarCitaBeneficiario", idConv);
                    return "error_token";
                case "Forbidden":
                    log.GuardarErrorLogPeticion("error_parametros", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "CancelarCitaBeneficiario", idConv);
                    return "error_parametros";
                default:
                    log.GuardarErrorLogPeticion("error_desconocido", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "CancelarCitaBeneficiario", idConv);
                    return "error_desconocido";
            }
        }
    }
}
