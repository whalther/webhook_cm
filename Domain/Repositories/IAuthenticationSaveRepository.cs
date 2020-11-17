using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
   public interface IAuthenticationSaveRepository
    {
        Task SaveValidaCliente(string numDoc, string tipoDoc, string valida, string idConv);
        Task SaveValidacionOtp (string resOtp,string otp, string idConv);
        dynamic GetValidacion(string idConv);
        Boolean DeleteAuthentication(string idConv);
    }
}
