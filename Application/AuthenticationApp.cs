using CrossCutting.Repositories;
using DataAccess.Repositories;
using Domain.DTOs;
using Domain.Repositories;
using Domain.Services;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Application
{
    public class AuthenticationApp
    {
        public string GetToken(string numeroCelular, string documento)
        {
            IAuthenticationRepository authRepository = new AuthenticationRepository();
            return new AuthenticationService().GetToken(authRepository, numeroCelular, documento);
        }
        public string RefreshToken(string numeroCelular, string documento) 
        {
            IAuthenticationRepository authRepository = new AuthenticationRepository();
            return new AuthenticationService().RefreshToken(authRepository, numeroCelular, documento);
        }
        public Resultado ValidarOtp(string token, string otp,string identificacion, string numeroCelular,string idConv)
        {
            IAuthenticationRepository authRepository = new AuthenticationRepository();
            Resultado res = new Resultado();
            AuthenticationService serv = new AuthenticationService();
            string resp = serv.ValidarOtp(authRepository, token, otp);
            if (resp == "error_token")
            {
                LogApp log = new LogApp();
                Dictionary<string, string> param = new Dictionary<string, string>() {
                {"numeroCelular",numeroCelular },
                {"identificacion",identificacion},
                {"idConv", idConv }
              };
                log.GuardarErrorLogPeticion(resp, JsonConvert.SerializeObject(param), "ValidarOtp");
                string nToken = serv.RefreshToken(authRepository, numeroCelular, identificacion);
                if (nToken != "error_credenciales" & nToken != "error_parametros" & nToken != "error_desconocido")
                {
                    res.Result = serv.ValidarOtp(authRepository, nToken, otp);
                }
                else
                {
                    log.GuardarErrorLogPeticion(nToken, JsonConvert.SerializeObject(param), "ValidarOtp");
                    res.Result =nToken;
                }
                res.Token = nToken;
            }
            else {
                res.Result = resp;
                res.Token = token;
            }
            return res;
        }
            
    }
}
