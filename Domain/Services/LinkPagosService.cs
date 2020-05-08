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
        public string GenerarLink(ILinkPagosRepository repo,Dictionary<string,object> paramEnvio)
        {
            Dictionary<string, string> hd = new Dictionary<string, string>() {
                {"Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings["appIdLinkPagos"]}
            };
            Dictionary<string, string> parametros = new Dictionary<string, string>() {
                {"clientInfo",JsonConvert.SerializeObject(paramEnvio)}
            };
            string resultado = repo.GenerarLink(hd, parametros);
                return resultado;
        }
    }
}
