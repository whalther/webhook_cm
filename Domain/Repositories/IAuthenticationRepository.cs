using System.Collections.Generic;

namespace Domain.Repositories
{
   public interface IAuthenticationRepository
    {
         string ValidarCliente(Dictionary<string, string> headers, Dictionary<string, string> parametros,string idConv);
         string RefreshToken(Dictionary<string, string> headers, Dictionary<string, string> parametros, string idConv);
         string ValidaOtp(Dictionary<string, string> headers, Dictionary<string, string> parametros, string idConv);
    }
}
