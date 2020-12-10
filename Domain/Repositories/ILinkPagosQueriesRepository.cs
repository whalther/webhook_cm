using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
   public interface ILinkPagosQueriesRepository
    {
        dynamic GetInfoLinkPagos(string idConv, int idCita, string flag);
        Boolean UpdateLinkCita(string idConv, int idCita, string result, string flag);
        string GetLinkPago(string idConv, int idCita, string flag);
        dynamic GetInfoLinkPagoFactura(string idConv);
        Boolean LogPagoFactura(string idConv, string estado, string numDoc,string tipoDoc, string flag, string numeroContrato,string saldo,string link);
        dynamic GetLinkPagoFactura(string idConv, string contrato);
    }
}
