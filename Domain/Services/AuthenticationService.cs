using Domain.Repositories;
using Domain.Utilities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;

namespace Domain.Services
{
   public class AuthenticationService
    {
        public string GetToken(IAuthenticationRepository authRepository,string numeroCelular, string documento)
        {
            Cifrador cf = new Cifrador();
            string usuarioPwd = ConfigurationManager.AppSettings.Get("usuarioBot") +":"+ ConfigurationManager.AppSettings.Get("pwdBot");
            string iv = cf.GenerarIv();
            string usuarioPwdCifrado = cf.Cifrar(usuarioPwd,iv);
            Dictionary<string, string> param = new Dictionary<string, string>() {
                {"numeroCelular",numeroCelular },
                {"identificacion",documento}
            };
            string iv2 = cf.GenerarIv();
            string paramCifrado = cf.Cifrar(JsonConvert.SerializeObject(param), iv2);
            Dictionary<string, string> hd = new Dictionary<string, string>() {
                {"Authorization",usuarioPwdCifrado },
                {"iv",iv}
            };
            Dictionary<string, string> parametros = new Dictionary<string, string>() {
                {"mensaje",paramCifrado},
                {"iv",iv2}
            };

            return authRepository.GetToken(hd,parametros);
        }

        public string RefreshToken(IAuthenticationRepository authRepository, string numeroCelular, string documento)
        {
            Cifrador cf = new Cifrador();
            string usuarioPwd = ConfigurationManager.AppSettings.Get("usuarioBot") + ":" + ConfigurationManager.AppSettings.Get("pwdBot");
            string iv = cf.GenerarIv();
            string usuarioPwdCifrado = cf.Cifrar(usuarioPwd, iv);
            Dictionary<string, string> param = new Dictionary<string, string>() {
                {"numeroCelular",numeroCelular },
                {"identificacion",documento}
            };
            string iv2 = cf.GenerarIv();
            string paramCifrado = cf.Cifrar(JsonConvert.SerializeObject(param), iv2);
            Dictionary<string, string> hd = new Dictionary<string, string>() {
                {"Authorization",usuarioPwdCifrado },
                {"iv",iv}
            };
            Dictionary<string, string> parametros = new Dictionary<string, string>() {
                {"mensaje",paramCifrado},
                {"iv",iv2}
            };

            return authRepository.RefreshToken(hd, parametros);
        }

        public string ValidarOtp(IAuthenticationRepository authRepository, string token, string otp)
        {
            Cifrador cf = new Cifrador();
            Dictionary<string, string> param = new Dictionary<string, string>() {
                {"otp",otp}
            };
            string iv2 = cf.GenerarIv();
            string paramCifrado = cf.Cifrar(JsonConvert.SerializeObject(param), iv2);
            Dictionary<string, string> hd = new Dictionary<string, string>() {
                {"token",token}
            };
            Dictionary<string, string> parametros = new Dictionary<string, string>() {
                {"mensaje",paramCifrado},
                {"iv",iv2}
            };
            string resultado =  authRepository.ValidaOtp(hd, parametros);
            if (resultado != "error_parametros" && resultado != "error_desconocido" && resultado != "error_token")
            {
                string iv = resultado.Substring(0, 16);
                string content = resultado.Substring(16);
                string textoPlano = cf.Descifrar(content, iv);
                dynamic jsonResp = JsonConvert.DeserializeObject<dynamic>(textoPlano);
                string estado = jsonResp.resultado;
                return estado;
            }
            else {
                return resultado;
            }
            
        }
    }
}
