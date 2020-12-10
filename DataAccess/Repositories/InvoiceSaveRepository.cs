using DataAccess.ColmedicaModel;
using Domain.DTOs;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class InvoiceSaveRepository : IInvoiceSaveRepository
    {
        public bool SaveContratosFactura(List<ContratoFactura> contratos, string idConv) 
        {
            using (ColmedicaContext contexto = new ColmedicaContext())
            {
                try
                {
                    foreach (ContratoFactura contrato in contratos)
                    {
                        tempContratosFactura con = new tempContratosFactura()
                        {
                            contrato = contrato.PlanContrato,
                            numeroContrato = contrato.NumeroContrato,
                            saldo = contrato.Saldo,
                            idConv = idConv,
                            fechaRegistro = DateTime.Now
                        };
                        contexto.tempContratosFactura.Add(con);
                    }
                    contexto.SaveChanges();
                    return true;
                }
                catch (Exception E)
                {
                    Trace.WriteLine(E.Message);
                    return false;
                    throw;
                }
            }
        }
        public List<ContratoFactura> GetContratosFacturaBd(string idConv)
        {
           
                List<ContratoFactura> resultado;
                try
                {
                    using (ColmedicaContext contexto = new ColmedicaContext())
                    {
                        resultado = (from tc in contexto.tempContratosFactura
                                     where tc.idConv == idConv
                                     select new ContratoFactura()
                                     {
                                         PlanContrato = tc.contrato,
                                         NumeroContrato = tc.numeroContrato,
                                         Saldo = tc.saldo
                                     }
                                     ).ToList();
                    }
                    return resultado;
                }
                catch (Exception E)
                {
                    Trace.WriteLine(E.Message);
                    return new List<ContratoFactura>();
                    throw;
                }
        }
      public  bool SaveResultEmailRetefuente(string tipoDoc, string numDoc, string correo, dynamic jsonResp, string idConv)
        {
            using (ColmedicaContext contexto = new ColmedicaContext())
            {
                try
                {
                    LogsEmailRetefuente ler = new LogsEmailRetefuente()
                    {
                        enviado = (int)jsonResp.Resultado,
                        mensaje = jsonResp.Mensaje,
                        tipoDoc = tipoDoc,
                        numDoc = numDoc,
                        correo = correo,
                        idConv = idConv
                        };
                        contexto.LogsEmailRetefuente.Add(ler);
                    contexto.SaveChanges();
                    return true;
                }
                catch (Exception E)
                {
                    Trace.WriteLine(E.Message);
                    return false;
                    throw;
                }
            }
        }
       public string GetCorreoTitular( string idConv)
        {
            using (ColmedicaContext contexto = new ColmedicaContext())
            {
                try
                {
                    string correo = (from tt in contexto.tempTitular
                                     where tt.idConv == idConv
                                     select tt.email
                                     ).FirstOrDefault();
                    return correo;
                }
                catch (Exception E)
                {
                    Trace.WriteLine(E.Message);
                    return "";
                    throw;
                }
            }
        }
        public dynamic GetResultEmailRetefuente(string idConv)
        {
            using (ColmedicaContext contexto = new ColmedicaContext())
            {
                try
                {
                    dynamic res = (from ler in contexto.LogsEmailRetefuente
                                     where ler.idConv == idConv 
                                     select new {
                                     Id = ler.id,
                                     Enviado = ler.enviado,
                                     Mensaje = ler.mensaje
                                     }
                                     ).OrderByDescending(u => u.Id).FirstOrDefault();
                    dynamic r = new ExpandoObject();
                    r.Enviado = res.Enviado;
                    r.Mensaje = res.Mensaje;
                    return r;
                }
                catch (Exception E)
                {
                    Trace.WriteLine(E.Message);
                    return null;
                    throw;
                }
            }
        }
    }
}
