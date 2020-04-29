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
        public async Task SaveAuthentication(IAuthenticationSaveRepository authRepository, string numDoc, string tipoDoc, string token, string idConv)
        {
            await authRepository.SaveAuthentication(numDoc, tipoDoc, token, idConv).ConfigureAwait(false);
        }
        public async Task SaveValidacionOtp(IAuthenticationSaveRepository authRepository, string resOtp, string idConv)
        {
            await authRepository.SaveValidacionOtp(resOtp, idConv).ConfigureAwait(false);
        }
        public dynamic GetAuthentication(IAuthenticationSaveRepository authRepository, string idConv) 
        {
            return authRepository.GetAuthentication(idConv);
        }
        

    }
}
