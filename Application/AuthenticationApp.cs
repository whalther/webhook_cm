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
        public string GetToken(string numeroCelular, string numDoc, string tipoDoc,string idConv)
        {
            IAuthenticationRepository authRepository = new AuthenticationRepository();
            IAuthenticationSaveRepository saveRepository = new AuthenticationSaveRepository();
            AuthenticationSaveService saveService = new AuthenticationSaveService();
            saveService.DeleteAuthentication(saveRepository, idConv);
            string documento = tipoDoc + numDoc;
            string token = new AuthenticationService().GetToken(authRepository, numeroCelular, documento,idConv);
            saveService.SaveAuthentication(saveRepository, numDoc, tipoDoc, token, idConv);
            return token;
        }
        public string RefreshToken(string numeroCelular, string documento, string idConv) 
        {
            IAuthenticationRepository authRepository = new AuthenticationRepository();
            return new AuthenticationService().RefreshToken(authRepository, numeroCelular, documento, idConv);
        }
        public Resultado ValidarOtp(string token, string otp,string numDoc,string tipoDoc, string numeroCelular,string idConv)
        {
            IAuthenticationRepository authRepository = new AuthenticationRepository();
            IAuthenticationSaveRepository saveRepository = new AuthenticationSaveRepository();
            AuthenticationSaveService saveService = new AuthenticationSaveService();
            Resultado res = new Resultado();
            AuthenticationService serv = new AuthenticationService();
            string resp = serv.ValidarOtp(authRepository, token, otp,idConv);
            string identificacion = tipoDoc + numDoc;
            if (resp == "error_token")
            {
                string nToken = serv.RefreshToken(authRepository, numeroCelular, identificacion,idConv);
                if (nToken != "error_credenciales" && nToken != "error_parametros" && nToken != "error_desconocido")
                {
                    res.Result = serv.ValidarOtp(authRepository, nToken, otp,idConv);
                }
                else
                {
                    res.Result =nToken;
                }
                res.Token = nToken;
            }
            else {
                res.Result = resp;
                res.Token = token;
            }
            saveService.SaveValidacionOtp(saveRepository, res.Result.ToString(), idConv);
            return res;
        }
        public dynamic GetAuthentication(string idConv)
        {
            IAuthenticationSaveRepository saveRepository = new AuthenticationSaveRepository();
            AuthenticationSaveService saveService = new AuthenticationSaveService();
            return saveService.GetAuthentication(saveRepository, idConv);
        }
    }
}
