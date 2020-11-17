using CrossCutting.Repositories;
using DataAccess.Repositories;
using Domain.DTOs;
using Domain.Repositories;
using Domain.Services;
using System.Collections.Generic;
using System.Linq;

namespace Application
{
    public class SchedulingPetitionsApp
    {
        public Resultado ValidarUsuario(string numDoc, string tipoDoc,string numeroCelular, string identificacionBene, string token, string idConv)
        {
            ISchedulingPetitionsRepository petitionsRepository = new SchedulingPetitionsRepository();
            SchedulingPetitionsService serv = new SchedulingPetitionsService();
            AuthenticationApp aApp = new AuthenticationApp();
            Usuario us = serv.ValidarUsuario(petitionsRepository, identificacionBene, token,idConv);
            Resultado res = new Resultado();
            string identificacion = tipoDoc + numDoc;
            if (us.Mensaje == "error_token")
            {
                string nToken = aApp.RefreshToken(numeroCelular, identificacion,idConv);
                if (nToken != "error_credenciales" && nToken != "error_parametros" && nToken != "error_desconocido")
                {
                    Usuario nUs = serv.ValidarUsuario(petitionsRepository, identificacionBene, nToken,idConv);
                    res.Result = nUs;
                }
                else {
                    Usuario nUs = new Usuario() {Mensaje = nToken };
                    res.Result = nUs;
                }
                res.Token = nToken;
            }
            else 
            {
                res.Result = us;
                res.Token = token;
            }
            return res;
        }
        public Resultado ProcesarEspecialidadesCiudad(string numDoc, string tipoDoc, string ciudad, string token, string idConv,string numDocChat,string tipoDocChat, string numeroCelular)
        {
            ISchedulingPetitionsRepository petitionsRepository = new SchedulingPetitionsRepository();
            ISchedulingSaveRepository saveRepository = new SchedulingSaveRepository();
            SchedulingPetitionsService serv = new SchedulingPetitionsService();
            AuthenticationApp aApp = new AuthenticationApp();
            serv.LimpiarTablasFlujo(saveRepository, 0, idConv, "tempEspecialidades");
            List<Especialidad> espe = serv.ProcesarEspecialidadesCiudad(petitionsRepository, saveRepository, numDoc, tipoDoc,ciudad, token, idConv);
            Resultado res = new Resultado();
            string identificacionChat = tipoDocChat + numDocChat;
            if (espe.Count == 0)
            {
                res.Result = new List<Especialidad>();
                res.Token = token;
            }
            else
            {
                if (espe[0].Nombre == "error_token")
                {
                    string nToken = aApp.RefreshToken(numeroCelular, identificacionChat, idConv);
                    if (nToken != "error_credenciales" && nToken != "error_parametros" && nToken != "error_desconocido")
                    {
                        res.Result = serv.ProcesarEspecialidadesCiudad(petitionsRepository, saveRepository, numDoc, tipoDoc, ciudad, nToken, idConv);
                    }
                    else
                    {
                        res.Result = nToken;
                    }
                    res.Token = nToken;
                }
                else
                {
                    res.Result = espe;
                    res.Token = token;
                }
            }
            return res;
        }
        public Resultado ProcesarCitas(string ciudad, string especialidad, string token, string idConv,string numeroCelular,string numDoc, string tipoDoc) 
        {
            ISchedulingPetitionsRepository petitionsRepository = new SchedulingPetitionsRepository();
            ISchedulingSaveRepository saveRepository = new SchedulingSaveRepository();
            SchedulingPetitionsService serv = new SchedulingPetitionsService();
            AuthenticationApp aApp = new AuthenticationApp();
            serv.LimpiarTablasFlujo(saveRepository, 0, idConv, "tempInfoAgendamiento");
            string cc = serv.ProcesarCitas(petitionsRepository, saveRepository, ciudad, especialidad, token, idConv);
            Resultado res = new Resultado();
            string identificacion = tipoDoc + numDoc;
            if (cc == "error_token")
            {
                string nToken = aApp.RefreshToken(numeroCelular, identificacion,idConv);
                if (nToken != "error_credenciales" && nToken != "error_parametros" && nToken != "error_desconocido")
                {
                    res.Result = serv.ProcesarCitas(petitionsRepository, saveRepository, ciudad, especialidad, nToken, idConv);
                }
                else {
                    res.Result = nToken;
                }
                res.Token = nToken;
            }
            else
            {
                res.Result = cc;
                res.Token = token;
            }
            return res;
        }
        public void ProcesarBeneficiariosCiudades(string numDoc,string tipoDoc, string token, string idConv, string numeroCelular) {
            ISchedulingPetitionsRepository petitionsRepository = new SchedulingPetitionsRepository();
            ISchedulingSaveRepository saveRepository = new SchedulingSaveRepository();
            SchedulingPetitionsService serv = new SchedulingPetitionsService();
            AuthenticationApp aApp = new AuthenticationApp();
            LocalQueriesApp lApp = new LocalQueriesApp();
            string identificacion = tipoDoc + numDoc;
            _ = lApp.LimpiarTablas(idConv);
                List<BeneficiarioContratante> bens = serv.GetBeneficiariosContratante(petitionsRepository, saveRepository, identificacion, token, idConv);
                if (bens[0].Parentesco == "error_token")
                {
                    token = aApp.RefreshToken(numeroCelular, identificacion,idConv);
                    if (token != "error_credenciales" && token != "error_parametros" && token != "error_desconocido")
                    {
                        bens = serv.GetBeneficiariosContratante(petitionsRepository, saveRepository, identificacion, token, idConv);
                    }
                }
                if (bens.Count > 0)
                {
                    dynamic lBen = bens.GroupBy(elem => elem.IdUsuario).Select(group => group.First());
                foreach (BeneficiarioContratante ben in lBen)
                    {
                        string rC = serv.ProcesarCiudadesBeneficiarioBd(petitionsRepository, saveRepository, ben.NumeroIdentificacion, ben.TipoIdentificacion, token, idConv, ben.IdUsuario);
                        if (rC == "error_token")
                        {
                            token = aApp.RefreshToken(numeroCelular, identificacion,idConv);
                            serv.ProcesarCiudadesBeneficiarioBd(petitionsRepository, saveRepository, ben.NumeroIdentificacion, ben.TipoIdentificacion, token, idConv, ben.IdUsuario);
                        }
                    }
            }
        }
        public void DummyPetition() {
            ISchedulingPetitionsRepository petitionsRepository = new SchedulingPetitionsRepository();
            SchedulingPetitionsService serv = new SchedulingPetitionsService();
            serv.DummyPetition(petitionsRepository);
        }
        public void ProcesarCitasBeneficiario(string numDoc,string tipoDoc, string token, string idConv, string numeroCelular, string idUsuario)
        {
            ISchedulingPetitionsRepository petitionsRepository = new SchedulingPetitionsRepository();
            ISchedulingSaveRepository saveRepository = new SchedulingSaveRepository();
            SchedulingPetitionsService serv = new SchedulingPetitionsService();
            AuthenticationApp aApp = new AuthenticationApp();
            serv.LimpiarTablasFlujo(saveRepository, 0, idConv, "tempCitasBeneficiario");
            string identificacion = tipoDoc + numDoc;
            string resp = serv.ConsultarCitasBeneficiario(petitionsRepository, saveRepository, idConv,token, idUsuario);
            if (resp == "error_token")
            {
                token = aApp.RefreshToken(numeroCelular, identificacion,idConv);
                if (token != "error_credenciales" && token != "error_parametros" && token != "error_desconocido")
                {
                    _ = serv.ConsultarCitasBeneficiario(petitionsRepository, saveRepository, idConv, token, idUsuario);
                }
            }
        }
    }
}