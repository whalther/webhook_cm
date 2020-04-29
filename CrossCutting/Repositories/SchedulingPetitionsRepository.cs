using Domain.Repositories;
using System.Collections.Generic;
using System.Configuration;


namespace CrossCutting.Repositories
{
    public class SchedulingPetitionsRepository:ISchedulingPetitionsRepository
    {
        public string ValidarUsuario(Dictionary<string, string> headers, Dictionary<string, string> parametros) {
            string url = ConfigurationManager.AppSettings["baseUrlApi"];
            RestClient rc = new RestClient();
            var resp = rc.HacerPeticion(url, "VerificaUsuario", parametros, "POST", headers, true);
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

        public string GetBeneficiariosContratante(Dictionary<string, string> headers, Dictionary<string, string> parametros) 
        {
            string url = ConfigurationManager.AppSettings["baseUrlApi"];
            RestClient rc = new RestClient();
            var resp = rc.HacerPeticion(url, "BeneficiariosContratante", parametros, "POST", headers, true);
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
        public string GetCiudadesUsuario(Dictionary<string, string> headers, Dictionary<string, string> parametros)
        {
            string url = ConfigurationManager.AppSettings["baseUrlApi"];
            RestClient rc = new RestClient();
            var resp = rc.HacerPeticion(url, "getCiudades", parametros, "POST", headers, true);
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
        public string ProcesarEspecialidadesCiudad(Dictionary<string, string> headers, Dictionary<string, string> parametros)
        {
            string url = ConfigurationManager.AppSettings["baseUrlApi"];
            RestClient rc = new RestClient();
            var resp = rc.HacerPeticion(url, "especialidadesCiudad", parametros, "POST", headers, true);
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
        public string ProcesarCitas(Dictionary<string, string> headers, Dictionary<string, string> parametros)
        {
            string url = ConfigurationManager.AppSettings["baseUrlApi"];
            RestClient rc = new RestClient();
            var resp = rc.HacerPeticion(url, "consultarCitasCiudad", parametros, "POST", headers, true);
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
        public string AsignarCita(Dictionary<string, string> headers, Dictionary<string, string> parametros)
        {
            string url = ConfigurationManager.AppSettings["baseUrlApi"];
            RestClient rc = new RestClient();
            var resp = rc.HacerPeticion(url, "asignarCita", parametros, "POST", headers, true);
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
        public void DummyPetition()
        {
            string url = ConfigurationManager.AppSettings["baseUrlApi"];
            RestClient rc = new RestClient();
            rc.HacerPeticion(url, "VerificaUsuario", null, "POST", null, true);
        }
        public string ConsultarCitasBeneficiario(Dictionary<string, string> headers, Dictionary<string, string> parametros)
        {
            string url = ConfigurationManager.AppSettings["baseUrlApi"];
            RestClient rc = new RestClient();
            var resp = rc.HacerPeticion(url, "consultarCitasBeneficiario", parametros, "POST", headers, true);
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
        public string CancelarCitaBeneficiario(Dictionary<string, string> headers, Dictionary<string, string> parametros)
        {
            string url = ConfigurationManager.AppSettings["baseUrlApi"];
            RestClient rc = new RestClient();
            var resp = rc.HacerPeticion(url, "cancelarCitaBeneficiario", parametros, "POST", headers, true);
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
