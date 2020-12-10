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
        public dynamic GetInfoLinkPagoFactura(ILinkPagosQueriesRepository repo, string idConv)
        {
            return repo.GetInfoLinkPagoFactura(idConv);
        }
        public string GenerarLinkPagoFactura(ILinkPagosRepository repo, Dictionary<string, object> paramEnvio, string idConv)
        {
            Dictionary<string, string> hd = new Dictionary<string, string>() {
                {"Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings["appIdLinkPagos"]}
            };
            Dictionary<string, string> parametros = new Dictionary<string, string>() {
                {"clientInfo",JsonConvert.SerializeObject(paramEnvio)}
            };
            string resultado = repo.GenerarLinkPagoFactura(hd, parametros, idConv);
            return resultado;
        }
       public Boolean LogPagoFactura(ILinkPagosQueriesRepository repo,string idConv, string estado, string numDoc, string tipoDoc, string flag, string numeroContrato, string saldo, string link)
        {
            return repo.LogPagoFactura(idConv, estado, numDoc, tipoDoc, flag, numeroContrato, saldo, link);
        }
        public dynamic GetLinkPagoFactura(ILinkPagosQueriesRepository repo, string idConv, string contrato)
        {
            return repo.GetLinkPagoFactura(idConv, contrato);
        }
    }
}
