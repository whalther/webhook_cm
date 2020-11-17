using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
   public class AuthenticationSaveService
    {
        public async Task SaveValidaCliente(IAuthenticationSaveRepository authRepository, string numDoc, string tipoDoc, string valida, string idConv)
        {
            await authRepository.SaveValidaCliente(numDoc, tipoDoc, valida, idConv).ConfigureAwait(false);
        }
        public async Task SaveValidacionOtp(IAuthenticationSaveRepository authRepository, string resOtp,string otp, string idConv)
        {
            await authRepository.SaveValidacionOtp(resOtp,otp, idConv).ConfigureAwait(false);
        }
        public dynamic GetValidacion(IAuthenticationSaveRepository authRepository, string idConv) 
        {
            return authRepository.GetValidacion(idConv);
        }
        public Boolean DeleteAuthentication(IAuthenticationSaveRepository authRepository, string idConv)
        {
           return authRepository.DeleteAuthentication(idConv);
        }
    }
}
