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
   public class LinkPagosRepository : ILinkPagosRepository
    {
        public string GenerarLink(Dictionary<string, string> headers, Dictionary<string, string> parametros, string idConv) 
        {
            string url = ConfigurationManager.AppSettings["linkPagos"];
            RestClient rc = new RestClient();
            LogRepository log = new LogRepository();
            var resp = rc.HacerPeticion(url, "Payment/GenerateMedicalAppointmentsPaymentLink", parametros, "POST", headers, false);
            string status = resp.StatusCode.ToString();
            switch (status)
            {
                case "OK":
                    return resp.Content;
                default:
                    log.GuardarErrorLogPeticion("error_link", JsonConvert.SerializeObject(parametros), resp.StatusDescription, "GenerarLink", idConv);
                    return "error_interno_fenix";
            }
        }
    }
}
