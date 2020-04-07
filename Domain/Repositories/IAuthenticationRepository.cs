using System.Collections.Generic;

namespace Domain.Repositories
{
   public interface IAuthenticationRepository
    {
         string GetToken(Dictionary<string, string> headers, Dictionary<string, string> parametros);
         string RefreshToken(Dictionary<string, string> headers, Dictionary<string, string> parametros);
         string ValidaOtp(Dictionary<string, string> headers, Dictionary<string, string> parametros);
    }
}
