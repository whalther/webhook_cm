using CrossCutting.Repositories;
using DataAccess.Repositories;
using Domain.Repositories;
using Domain.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class LinkPagosApp
    {
        public void GenerarLink(string idConv, int idCita)
        {
            ILinkPagosRepository repo = new LinkPagosRepository();
            LinkPagosService serv = new LinkPagosService();
            IAuthenticationRepository auth = new AuthenticationRepository();
            AuthenticationService authServ = new AuthenticationService();
            ILocalQueriesRepository local = new LocalQueriesRepository();
            LocalQueriesService localServ = new LocalQueriesService();
            dynamic info = (object)localServ.GetInfoLinkPagos(local, idConv, idCita);
            string identificacion = info.TipoIdentificacion + info.NumeroIdentificacion;
            string token = authServ.RefreshToken(auth,info.TelefonoCelular, identificacion,idConv);
            
            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                 {"customerName",info.Nombre},
                 {"phoneNumber", info.TelefonoCelular},
                 {"contractNumber",info.NumeroContrato },
                 {"idNum",info.NumeroIdentificacion},
                 {"typeId",info.TipoIdentificacion},
                 {"source","CHATBOT" },
                 {"paymentInfo",new Dictionary<string,string>(){
                     {"amount",Convert.ToInt64(Math.Floor(Convert.ToDouble(info.ValorPagar))).ToString() },
                     {"currency", "COP" } } },
                 {"paymentType",new Dictionary<string,int>(){
                     {"idPaymentType",2 } } },
                 {"token", token}
            };
          string resp =  serv.GenerarLink(repo,param);
            if (resp != "error") 
            {
                dynamic respObj = JsonConvert.DeserializeObject<dynamic>(resp);
                string link = respObj.paymentInfo.paymentLink;
                localServ.UpdateLinkCita(local,idConv,idCita,link);
            }
            else
            {
                localServ.UpdateLinkCita(local, idConv, idCita, resp);
            }
        }
    }
}
