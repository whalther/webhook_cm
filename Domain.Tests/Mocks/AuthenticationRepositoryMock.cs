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
                Dictionary<string, string> hd = new Dictionary<string, string>() {
                {"Authentication","AZDlINpFqKuJpN0gipOZfq4wXjGB3uOirssg0YO7ep0=" },
                {"iv","fD8/Fj8HL0caPxl/"}
            };
                Dictionary<string, string> param2 = new Dictionary<string, string>() {
                {"mensaje","iV8Piaxt63U8BcKMcFVD0+qNdPfdMaodrcb9QxwGD2stvZ8ZvoVzCZLypV54G9MCL1xygssu/CWN2+f69yBJDQ=="},
                {"iv","fD8/Fj8HL0caPxl/"}
            };
                return "exito";
            }
            catch {
                return "";
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
