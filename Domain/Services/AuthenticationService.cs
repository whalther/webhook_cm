using Domain.Repositories;
using Domain.Utilities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;

namespace Domain.Services
{
   public class AuthenticationService
    {
        public string ValidarCliente(IAuthenticationRepository authRepository,string numeroCelular, string documento, string idConv)
        {
            Cifrador cf = new Cifrador();
            string usuarioPwdToken = ConfigurationManager.AppSettings.Get("usuarioBot") +":"+ ConfigurationManager.AppSettings.Get("pwdBot");
            string ivToken = cf.GenerarIv();
            string usuarioPwdCifrado = cf.Cifrar(usuarioPwdToken, ivToken);
            string resultadoValidar;
            Dictionary<string, string> param = new Dictionary<string, string>() {
                {"numeroCelular",numeroCelular },
                {"identificacion",documento}
            };
            string ivCf = cf.GenerarIv();
            string paramCifrado = cf.Cifrar(JsonConvert.SerializeObject(param), ivCf);
            Dictionary<string, string> hd = new Dictionary<string, string>() {
                {"Authorization",usuarioPwdCifrado },
                {"iv",ivToken}
            };
            Dictionary<string, string> parametros = new Dictionary<string, string>() {
                {"mensaje",paramCifrado},
                {"iv",ivCf}
            };
            resultadoValidar = authRepository.ValidarCliente(hd, parametros, idConv); 
            if (resultadoValidar != "error_credenciales" && resultadoValidar != "error_desconocido" && resultadoValidar != "error_prohibido" && resultadoValidar != "error_no_encontrado")
            {
                string iv = resultadoValidar.Substring(0, 16);
                string content = resultadoValidar.Substring(16);
                string textoPlano = cf.Descifrar(content, iv);
                return textoPlano;
            }
            else
            {
                return resultadoValidar;
            }
            
        }

        public string RefreshToken(IAuthenticationRepository authRepository, string numeroCelular, string documento, string idConv)
        {
            Cifrador cf = new Cifrador();
            string usuarioPwdRefresh = ConfigurationManager.AppSettings.Get("usuarioBot") + ":" + ConfigurationManager.AppSettings.Get("pwdBot");
            string ivRefresh = cf.GenerarIv();
            string usuarioPwdCifrado = cf.Cifrar(usuarioPwdRefresh, ivRefresh);
            Dictionary<string, string> param = new Dictionary<string, string>() {
                {"numeroCelular",numeroCelular },
                {"identificacion",documento}
            };
            string ivParam = cf.GenerarIv();
            string paramCifrado = cf.Cifrar(JsonConvert.SerializeObject(param), ivParam);
            Dictionary<string, string> hd = new Dictionary<string, string>() {
                {"Authorization",usuarioPwdCifrado },
                {"iv",ivRefresh}
            };
            Dictionary<string, string> parametros = new Dictionary<string, string>() {
                {"mensaje",paramCifrado},
                {"iv",ivParam}
            };

            return authRepository.RefreshToken(hd, parametros, idConv);
        }

        public string ValidarOtp(IAuthenticationRepository authRepository, string numDoc,string tipoDoc, string otp, string idConv)
        {
            Cifrador cf = new Cifrador();
            string usuarioPwdToken = ConfigurationManager.AppSettings.Get("usuarioBot") + ":" + ConfigurationManager.AppSettings.Get("pwdBot");
            string ivOtp = cf.GenerarIv();
            string usuarioPwdCifrado = cf.Cifrar(usuarioPwdToken, ivOtp);

            Dictionary<string, string> param = new Dictionary<string, string>() {
                {"otp",otp},
                {"identificacion",tipoDoc+numDoc }
            };
            string iv2 = cf.GenerarIv();
            string paramCifrado = cf.Cifrar(JsonConvert.SerializeObject(param), iv2);
            Dictionary<string, string> hd = new Dictionary<string, string>() {
                {"Authorization",usuarioPwdCifrado },
                {"iv",ivOtp}
            };
            Dictionary<string, string> parametros = new Dictionary<string, string>() {
                {"mensaje",paramCifrado},
                {"iv",iv2}
            };
            string resultado =  authRepository.ValidaOtp(hd, parametros, idConv);
            
                return resultado;
        }
    }
}
