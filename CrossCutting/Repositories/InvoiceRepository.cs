using Domain.Repositories;
using System.Collections.Generic;
using System.Configuration;


namespace CrossCutting.Repositories
{
   public class InvoiceRepository : IInvoiceRepository
    {
        public string GetContratosFactura(Dictionary<string, string> headers, string idConv) 
        {
            string url = ConfigurationManager.AppSettings["baseUrlApi"];
            RestClient rc = new RestClient();
            LogRepository log = new LogRepository();
            var resp = rc.HacerPeticion(url, "ConsultaSaldosContrato ", null, "POST", headers, true);
            string status = resp.StatusCode.ToString();
            switch (status)
            {
                case "OK":
                    return resp.Content;
                case "Unauthorized":
                    log.GuardarErrorLogPeticion("error_token", "", resp.StatusDescription, "GetContratosFactura", idConv);
                    return "error_token";
                case "Forbidden":
                    log.GuardarErrorLogPeticion("error_parametros", "", resp.StatusDescription, "GetContratosFactura", idConv);
                    return "error_parametros";
                default:
                    log.GuardarErrorLogPeticion("error_desconocido", "", resp.StatusDescription, "GetContratosFactura", idConv);
                    return "error_desconocido";
            }
        }
        public string SendEmailRetefuente(Dictionary<string, string> headers, Dictionary<string, string> param, string idConv)
        {
            string url = ConfigurationManager.AppSettings["baseUrlApi"];
            RestClient rc = new RestClient();
            LogRepository log = new LogRepository();
            var resp = rc.HacerPeticion(url, "EnviarEmailCertificacionRetefuente", param, "POST", headers, true);
            string status = resp.StatusCode.ToString();
            switch (status)
            {
                case "OK":
                    return resp.Content;
                case "Unauthorized":
                    log.GuardarErrorLogPeticion("error_token", "", resp.StatusDescription, "SendEmailRetefuente", idConv);
                    return "error_token";
                case "Forbidden":
                    log.GuardarErrorLogPeticion("error_parametros", "", resp.StatusDescription, "SendEmailRetefuente", idConv);
                    return "error_parametros";
                default:
                    log.GuardarErrorLogPeticion("error_desconocido", "", resp.StatusDescription, "SendEmailRetefuente", idConv);
                    return "error_desconocido";
            }
        }
    }
}
