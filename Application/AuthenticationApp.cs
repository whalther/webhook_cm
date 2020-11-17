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
        public string ValidarCliente(string numeroCelular, string numDoc, string tipoDoc,string idConv)
        {
            IAuthenticationRepository authRepository = new AuthenticationRepository();
            IAuthenticationSaveRepository saveRepository = new AuthenticationSaveRepository();
            AuthenticationSaveService saveService = new AuthenticationSaveService();
            saveService.DeleteAuthentication(saveRepository, idConv);
            string documento = tipoDoc + numDoc;
            string token = new AuthenticationService().ValidarCliente(authRepository, numeroCelular, documento,idConv);
            saveService.SaveValidaCliente(saveRepository, numDoc, tipoDoc, token, idConv);
            return token;
        }
        public string RefreshToken(string numeroCelular, string documento, string idConv) 
        {
            IAuthenticationRepository authRepository = new AuthenticationRepository();
            return new AuthenticationService().RefreshToken(authRepository, numeroCelular, documento, idConv);
        }
        public string ValidarOtp(string otp,string numDoc,string tipoDoc, string idConv)
        {
            IAuthenticationRepository authRepository = new AuthenticationRepository();
            IAuthenticationSaveRepository saveRepository = new AuthenticationSaveRepository();
            AuthenticationSaveService saveService = new AuthenticationSaveService();
            AuthenticationService serv = new AuthenticationService();
            string resp = serv.ValidarOtp(authRepository, numDoc,tipoDoc, otp,idConv);
            saveService.SaveValidacionOtp(saveRepository, resp,otp, idConv);
            return resp;
        }
        public dynamic GetValidacion(string idConv)
        {
            IAuthenticationSaveRepository saveRepository = new AuthenticationSaveRepository();
            AuthenticationSaveService saveService = new AuthenticationSaveService();
            return saveService.GetValidacion(saveRepository, idConv);
        }
    }
}
