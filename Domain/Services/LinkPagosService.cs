using Domain.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class LinkPagosService
    {
        public string GenerarLink(ILinkPagosRepository repo,Dictionary<string,object> paramEnvio, string idConv)
        {
            Dictionary<string, string> hd = new Dictionary<string, string>() {
                {"Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings["appIdLinkPagos"]}
            };
            Dictionary<string, string> parametros = new Dictionary<string, string>() {
                {"clientInfo",JsonConvert.SerializeObject(paramEnvio)}
            };
            string resultado = repo.GenerarLink(hd, parametros,idConv);
                return resultado;
        }
        public dynamic GetInfoLinkPagos(ILinkPagosQueriesRepository repo, string idConv, int idCita, string flag)
        {
            return repo.GetInfoLinkPagos(idConv, idCita, flag);
        }
        public Boolean UpdateLinkCita(ILinkPagosQueriesRepository repo, string idConv, int idCita, string result, string flag)
        {
            return repo.UpdateLinkCita(idConv, idCita, result, flag);
        }
        public string GetLinkPago(ILinkPagosQueriesRepository repo, string idConv, int idCita, string flag)
        {
            return repo.GetLinkPago(idConv, idCita, flag);
        }
    }
}
