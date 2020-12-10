using CrossCutting.Repositories;
using DataAccess.Repositories;
using Domain.DTOs;
using Domain.Repositories;
using Domain.Services;
using System.Collections.Generic;
using System.Dynamic;

namespace Application
{
   public class InvoiceApp
    {
        public Resultado GetContratosFactura(string token,string numeroCelular, string numDoc,string tipoDoc, string idConv)
        {
            IInvoiceRepository invoiceRepository = new InvoiceRepository();
            IInvoiceSaveRepository saveRepository = new InvoiceSaveRepository();
            InvoiceService serv = new InvoiceService();
            AuthenticationApp aApp = new AuthenticationApp();
            ISchedulingSaveRepository saveRepository2 = new SchedulingSaveRepository();
            SchedulingPetitionsService serv2 = new SchedulingPetitionsService();
            serv2.LimpiarTablasFlujo(saveRepository2, 0, idConv, "tempContratosFactura");
            List<ContratoFactura> cf = serv.GetContratosFactura(invoiceRepository, saveRepository, token, idConv);
            Resultado res = new Resultado();
            string identificacion = tipoDoc + numDoc;
            if (cf.Count == 0)
            {
                res.Result = new List<ContratoFactura>();
                res.Token = token;
            }
            else
            {
                if (cf[0].PlanContrato == "error_token")
                {
                    string nToken = aApp.RefreshToken(numeroCelular, identificacion, idConv);
                    if (nToken != "error_credenciales" && nToken != "error_parametros" && nToken != "error_desconocido")
                    {
                        res.Result = serv.GetContratosFactura(invoiceRepository, saveRepository, nToken, idConv);
                    }
                    else
                    {
                        res.Result = nToken;
                    }
                    res.Token = nToken;
                }
                else
                {
                    res.Result = cf;
                    res.Token = token;
                }
            }
            return res;
        }
        public List<ContratoFactura> GetContratoFacturasBd(string idConv) 
        {
            IInvoiceSaveRepository saveRepository = new InvoiceSaveRepository();
            InvoiceService serv = new InvoiceService();
            return serv.GetContratoFacturasBd(saveRepository,idConv);
        }
        public void SendEmailRetefuente(string tipoDoc, string numDoc,string idConv, string token,string numeroCelular)
        {
            IInvoiceRepository ivRepository = new InvoiceRepository();
            InvoiceService iServ = new InvoiceService();
            IInvoiceSaveRepository saveRepository = new InvoiceSaveRepository();
            InvoiceService serv = new InvoiceService();
            AuthenticationApp aApp = new AuthenticationApp();
            string correo = serv.GetCorreoTitular(saveRepository, idConv);
            string identificacion = tipoDoc + numDoc;
            if (!string.IsNullOrEmpty(correo))
            {
              string res =  iServ.SendEmailRetefuente(ivRepository, saveRepository, tipoDoc, numDoc, correo, token, idConv);
                if (res.ToString() == "error_token")
                {
                    string nToken = aApp.RefreshToken(numeroCelular, identificacion, idConv);
                    if (nToken != "error_credenciales" && nToken != "error_parametros" && nToken != "error_desconocido")
                    {
                       dynamic res2 =  iServ.SendEmailRetefuente(ivRepository, saveRepository, tipoDoc, numDoc, correo, nToken, idConv);
                        if (res2.ToString() != "error_credenciales" && res2.ToString() != "error_parametros" && res2.ToString() != "error_desconocido")
                        {
                            serv.SaveResultEmailRetefuente(saveRepository, tipoDoc, numDoc, correo, res2, idConv);
                        }
                        else
                        {
                            dynamic r = new ExpandoObject();
                            r.Resultado = 0;
                            r.Mensaje = res2;
                            serv.SaveResultEmailRetefuente(saveRepository, tipoDoc, numDoc, correo, r, idConv);
                        }
                    }
                    else
                    {
                        dynamic r = new ExpandoObject();
                        r.Resultado = 0;
                        r.Mensaje = nToken;
                        serv.SaveResultEmailRetefuente(saveRepository, tipoDoc, numDoc, correo, r, idConv);
                    }
                }
                else
                {
                    dynamic r = new ExpandoObject();
                    r.Resultado = 0;
                    r.Mensaje = res;
                    serv.SaveResultEmailRetefuente(saveRepository, tipoDoc, numDoc, correo, r, idConv);
                }
            }
            else
            {
                dynamic r = new ExpandoObject();
                r.Resultado = 0;
                r.Mensaje = "No se encotro correo en base de datos";
                serv.SaveResultEmailRetefuente(saveRepository, tipoDoc, numDoc, correo, r, idConv);
            }
        }
        public dynamic GetResultEmailRetefuente(string idConv)
        {
            IInvoiceSaveRepository saveRepository = new InvoiceSaveRepository();
            InvoiceService serv = new InvoiceService();
            return serv.GetResultEmailRetefuente(saveRepository, idConv);
        }
    }
}
