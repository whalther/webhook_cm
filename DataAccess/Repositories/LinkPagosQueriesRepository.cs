using DataAccess.ColmedicaModel;
using Domain.Repositories;
using System;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
   public class LinkPagosQueriesRepository : ILinkPagosQueriesRepository
    {
        public dynamic GetInfoLinkPagos(string idConv, int idCita, string flag)
        {
            dynamic resultado;
            try
            {
                using (ColmedicaContext contexto = new ColmedicaContext())
                {
                    
                    if (flag == "agendamiento")
                    {
                        resultado = (from tc in contexto.tempCita
                                     join tb in contexto.tempBeneficiarios on new { x1 = tc.numIdBeneficiario, x2 = tc.tipoIdBeneficiario } equals new { x1 = tb.numeroIdentificacion, x2 = tb.tipoIdentificacion }
                                     where (tc.idConv == idConv && tc.numEspacioCita == idCita && tb.idConv==idConv)
                                     select new
                                     {
                                         NumeroIdentificacion = tc.numIdBeneficiario,
                                         TipoIdentificacion = tc.tipoIdBeneficiario,
                                         ValorPagar = tc.valorPagar,
                                         NumeroContrato = tb.numeroContrato,
                                         TelefonoCelular = tb.telefonoCelular,
                                         Nombre = tb.nombre
                                     }
                                ).FirstOrDefault();
                    }
                    else
                    {
                        resultado = (from tcb in contexto.tempCitasBeneficiario
                                     join tb in contexto.tempBeneficiarios on new { x1 = tcb.numeroIdentificacion, x2 = tcb.tipoIdentificacion } equals new { x1 = tb.numeroIdentificacion, x2 = tb.tipoIdentificacion }
                                     where (tcb.idConv == idConv && tcb.idCita == idCita && tb.idConv == idConv)
                                     select new
                                     {
                                         NumeroIdentificacion = tcb.numeroIdentificacion,
                                         TipoIdentificacion = tcb.tipoIdentificacion,
                                         ValorPagar = tcb.valorPagar,
                                         NumeroContrato = tb.numeroContrato,
                                         TelefonoCelular = tb.telefonoCelular,
                                         Nombre = tb.nombre
                                     }
                                  ).FirstOrDefault();
                    }
                    
                }
                dynamic r = new ExpandoObject();
                r.Nombre = resultado.Nombre;
                r.NumeroIdentificacion = resultado.NumeroIdentificacion;
                r.TipoIdentificacion = resultado.TipoIdentificacion;
                r.ValorPagar = resultado.ValorPagar;
                r.NumeroContrato = resultado.NumeroContrato;
                r.TelefonoCelular = resultado.TelefonoCelular;

                return r;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                return new { };
                throw;
            }
        }
        public Boolean UpdateLinkCita(string idConv, int idCita, string result, string flag)
        {
            using (ColmedicaContext contexto = new ColmedicaContext())
            {
                try
                {
                    if (flag == "agendamiento")
                    {
                        var up = (from cit in contexto.tempCita
                                  where (cit.idConv == idConv && cit.numEspacioCita == idCita)
                                  select cit).FirstOrDefault();
                        up.linkPago = result;
                    }
                    else
                    {
                        var up = (from cit in contexto.tempCitasBeneficiario
                                  where (cit.idConv == idConv && cit.idCita == idCita)
                                  select cit).FirstOrDefault();
                        up.linkPago = result;
                    }
                    contexto.SaveChanges();
                    SaveLinkCitaNoTemp(idConv, idCita, flag).ConfigureAwait(false);
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
        public string GetLinkPago(string idConv, int idCita, string flag)
        {
            string link = "";
            using (ColmedicaContext contexto = new ColmedicaContext())
            {
                try
                {

                    if (flag == "agendamiento")
                    {
                        link = (from cit in contexto.tempCita
                                       where (cit.idConv == idConv && cit.numEspacioCita == idCita)
                                       select cit.linkPago).FirstOrDefault();
                    }
                    else
                    {
                        link = (from cit in contexto.tempCitasBeneficiario
                                       where (cit.idConv == idConv && cit.idCita == idCita)
                                       select cit.linkPago).FirstOrDefault();
                    }
                    
                    return link;
                }
                catch (Exception E)
                {
                    Trace.WriteLine(E.Message);
                    return "error_bd";
                    throw;
                }
            }
        }
        private async Task SaveLinkCitaNoTemp(string idConv, int idCita, string flag)
        {
            using (ColmedicaContext contexto = new ColmedicaContext())
            {
                try
                {
                    await Task.Run(() => contexto.saveLinkCitaNoTemp(idConv, idCita, flag)).ConfigureAwait(false);
                }
                catch (Exception E)
                {
                    Trace.WriteLine(E.Message);
                    throw;
                }
            }
        }
    }
}
