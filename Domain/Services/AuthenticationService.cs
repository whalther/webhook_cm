using Domain.Repositories;
using Domain.Utilities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;

namespace Domain.Services
{
   public class AuthenticationService
    {
        public string GetToken(IAuthenticationRepository authRepository,string numeroCelular, string documento, string idConv)
        {
            Cifrador cf = new Cifrador();
            string usuarioPwdToken = ConfigurationManager.AppSettings.Get("usuarioBot") +":"+ ConfigurationManager.AppSettings.Get("pwdBot");
            string ivToken = cf.GenerarIv();
            string usuarioPwdCifrado = cf.Cifrar(usuarioPwdToken, ivToken);
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

            return authRepository.GetToken(hd,parametros,idConv);
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

        public string ValidarOtp(IAuthenticationRepository authRepository, string token, string otp, string idConv)
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
            string resultado =  authRepository.ValidaOtp(hd, parametros, idConv);
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
