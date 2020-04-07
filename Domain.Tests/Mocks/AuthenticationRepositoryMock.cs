using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tests.Mocks
{
   public class AuthenticationRepositoryMock: IAuthenticationRepository
    {
        public string GetToken(Dictionary<string, string> headers, Dictionary<string, string> parametros) {

            try
            {
                return "exito";
            }
            catch {
                return "error_desconocido";
                throw;
            }
        }

        public string RefreshToken(Dictionary<string, string> headers, Dictionary<string, string> parametros)
        {
            return "prueba";
        }
        public string ValidaOtp(Dictionary<string, string> headers, Dictionary<string, string> parametros)
        {
            return "prueba";
        }
    }
}
