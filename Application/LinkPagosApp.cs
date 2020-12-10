using CrossCutting.Repositories;
using DataAccess.Repositories;
using Domain.Repositories;
using Domain.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Application
{
    public class LinkPagosApp
    {
        public void GenerarLink(string idConv, int idCita,string flag)
        {
            ILinkPagosRepository repo = new LinkPagosRepository();
            LinkPagosService serv = new LinkPagosService();
            IAuthenticationRepository auth = new AuthenticationRepository();
            AuthenticationService authServ = new AuthenticationService();
            ILinkPagosQueriesRepository linkRepo = new LinkPagosQueriesRepository();
            LinkPagosService linkServ = new LinkPagosService();
            dynamic info = (object)linkServ.GetInfoLinkPagos(linkRepo, idConv, idCita, flag);
            string identificacion = info.TipoIdentificacion + info.NumeroIdentificacion;
            string token = authServ.RefreshToken(auth,info.TelefonoCelular, identificacion,idConv);
            
             if (info.ValorPagar.ToString() == "0" && ConfigurationManager.AppSettings["env"] == "prod")
            {
                linkServ.UpdateLinkCita(linkRepo, idConv, idCita, "error_valor", flag);
            }
            else
            {
                if (Convert.ToInt64(Math.Floor(Convert.ToDouble(info.ValorPagar))) == 0 && ConfigurationManager.AppSettings["env"] == "dev")
                {
                    info.ValorPagar = "100";
                }
                Dictionary<string, object> param = new Dictionary<string, object>()
            {
                 {"customerName",info.Nombre},
                 {"phoneNumber", info.TelefonoCelular},
                 {"contractNumber",""},
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
                string resp = serv.GenerarLink(repo, param, idConv);
                if (resp != "error_interno_fenix")
                {
                    dynamic respObj = JsonConvert.DeserializeObject<dynamic>(resp);
                    bool status = respObj.Success;
                    if (status)
                    {
                        string link = respObj.PaymentLink;
                        linkServ.UpdateLinkCita(linkRepo, idConv, idCita, link, flag);
                    }
                    else
                    {
                        linkServ.UpdateLinkCita(linkRepo, idConv, idCita, "error|" + respObj.Message, flag);
                    }
                }
                else
                {
                    linkServ.UpdateLinkCita(linkRepo, idConv, idCita, resp, flag);
                }
            }
        }
        public string GetLinkPago(string idConv, int idCita, string flag)
        {
            ILinkPagosQueriesRepository linkRepo = new LinkPagosQueriesRepository();
            LinkPagosService linkServ = new LinkPagosService();
            return linkServ.GetLinkPago(linkRepo,idConv,idCita, flag);
        }
        public void GenerarLinkPagoFactura(string idConv, string tipoDoc, string numDoc, string contrato, string saldo)
        {
            ILinkPagosRepository repo = new LinkPagosRepository();
            LinkPagosService serv = new LinkPagosService();
            ILinkPagosQueriesRepository linkRepo = new LinkPagosQueriesRepository();
            LinkPagosService linkServ = new LinkPagosService();
            dynamic info = (object)linkServ.GetInfoLinkPagoFactura(linkRepo, idConv);
            _ = linkServ.LogPagoFactura(linkRepo, idConv,"",numDoc,tipoDoc,"insert",contrato,saldo,"");

            if (saldo == "0" && ConfigurationManager.AppSettings["env"] == "prod")
            {
                _ = linkServ.LogPagoFactura(linkRepo, idConv, "error", numDoc, tipoDoc, "insert", contrato, saldo, "error_valor");
            }
            else if (info==null) 
            {
                _ = linkServ.LogPagoFactura(linkRepo, idConv, "error", numDoc, tipoDoc, "insert", contrato, saldo, "error_correo");
            }
            else
            {
                if (Convert.ToInt64(Math.Floor(Convert.ToDouble(saldo))) == 0 && ConfigurationManager.AppSettings["env"] == "dev")
                {
                    saldo = "100";
                }
                Dictionary<string, object> param = new Dictionary<string, object>()
            {
                 {"customerName",info.Nombre},
                 {"contractNumber",contrato},
                 {"idNum",info.NumeroIdentificacion},
                 {"typeId",info.TipoIdentificacion},
                 {"source","CHATBOT" },
                 {"paymentInfo",new Dictionary<string,string>(){
                     {"amount",Convert.ToInt64(Math.Floor(Convert.ToDouble(saldo))).ToString() },
                     {"currency", "COP" } } },
                 {"channel","3" },
                 {"userCreated",info.Correo },
                 {"paymentType",new Dictionary<string,int>(){
                     {"idPaymentType",1 } } }
            };

                string resp = serv.GenerarLinkPagoFactura(repo, param, idConv);
                if (resp != "error_interno_fenix")
                {
                    dynamic respObj = JsonConvert.DeserializeObject<dynamic>(resp);
                    bool status = respObj.Success;
                    if (status)
                    {
                        string link = respObj.PaymentLink;
                        _ = linkServ.LogPagoFactura(linkRepo, idConv, "completado", numDoc, tipoDoc, "update", contrato, saldo, link);
                    }
                    else
                    {
                        _ = linkServ.LogPagoFactura(linkRepo, idConv, "error", numDoc, tipoDoc, "update", contrato, saldo, respObj.Message);
                    }
                }
                else
                {
                    _ = linkServ.LogPagoFactura(linkRepo, idConv, "error", numDoc, tipoDoc, "update", contrato, saldo, resp);
                }
            }

        }
        public dynamic GetLinkPagoFactura(string idConv, string contrato)
        {
            ILinkPagosQueriesRepository linkRepo = new LinkPagosQueriesRepository();
            LinkPagosService linkServ = new LinkPagosService();
            return linkServ.GetLinkPagoFactura(linkRepo, idConv, contrato);
        }
    }
}
