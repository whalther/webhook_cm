using CrossCutting.Repositories;
using DataAccess.Repositories;
using Domain.DTOs;
using Domain.Repositories;
using Domain.Services;

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
        public Resultado ValidarOtp(string token, string otp,string identificacion, string numeroCelular)
        {
            IAuthenticationRepository authRepository = new AuthenticationRepository();
            Resultado res = new Resultado();
            AuthenticationService serv = new AuthenticationService();
            string resp = serv.ValidarOtp(authRepository, token, otp);
            if (resp == "error_token")
            {
                string nToken = serv.RefreshToken(authRepository, numeroCelular, identificacion);
                string nResp = serv.ValidarOtp(authRepository, nToken, otp);
                res.Result = nResp;
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
