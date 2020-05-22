using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
   public interface IAuthenticationSaveRepository
    {
        Task SaveAuthentication (string numDoc, string tipoDoc, string token, string idConv);
        Task SaveValidacionOtp (string resOtp, string idConv);
        dynamic GetAuthentication(string idConv);
        Boolean DeleteAuthentication(string idConv);
    }
}
