using Domain.Repositories;
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
        public string GenerarLink(Dictionary<string, string> headers, Dictionary<string, string> parametros) 
        {
            string url = ConfigurationManager.AppSettings["linkPagos"];
            RestClient rc = new RestClient();
            var resp = rc.HacerPeticion(url, "Payment/GenerateMedicalAppointmentsPaymentLink", parametros, "POST", headers, false);
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
