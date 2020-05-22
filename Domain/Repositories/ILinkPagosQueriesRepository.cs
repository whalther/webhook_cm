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
    }
}
