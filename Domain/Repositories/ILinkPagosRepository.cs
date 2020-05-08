using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
   public interface ILinkPagosRepository
    {
         string GenerarLink(Dictionary<string, string> headers, Dictionary<string, string> parametros);
    }
}
