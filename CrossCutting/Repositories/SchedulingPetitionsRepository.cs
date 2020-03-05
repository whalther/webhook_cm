using Domain.DTOs;
using Domain.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string GetEspecialidadesCiudad(Dictionary<string, string> headers, Dictionary<string, string> parametros)
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
        public string GetCitasCiudad(Dictionary<string, string> headers, Dictionary<string, string> parametros)
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
        public string GetCitasBeneficiario(Dictionary<string, string> headers, Dictionary<string, string> parametros)
        {
            return "";
        }
        public string CancelarCitaBeneficiario(Dictionary<string, string> headers, Dictionary<string, string> parametros)
        {
            return "";
        }
    }
}
