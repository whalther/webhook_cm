using CrossCutting.Repositories;
using DataAccess.Repositories;
using Domain.DTOs;
using Domain.Repositories;
using Domain.Services;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Application
{
    public class SchedulingPetitionsApp
    {
        public Resultado ValidarUsuario(string identificacion,string numeroCelular, string identificacionBene, string token, string idConv)
        {
            ISchedulingPetitionsRepository petitionsRepository = new SchedulingPetitionsRepository();
            SchedulingPetitionsService serv = new SchedulingPetitionsService();
            AuthenticationApp aApp = new AuthenticationApp();
            Usuario us = serv.ValidarUsuario(petitionsRepository, identificacionBene, token);
            Resultado res = new Resultado();
            if (us.Mensaje == "error_token")
            {
                LogApp log = new LogApp();
                Dictionary<string, string> param = new Dictionary<string, string>() {
                {"numeroCelular",numeroCelular },
                {"identificacion",identificacion},
                {"idConv", idConv }
              };
                log.GuardarErrorLogPeticion(us.Mensaje, JsonConvert.SerializeObject(param), "ValidarUsuario");

                string nToken = aApp.RefreshToken(numeroCelular, identificacion);
                if (nToken != "error_credenciales" & nToken != "error_parametros" & nToken != "error_desconocido")
                {
                    Usuario nUs = serv.ValidarUsuario(petitionsRepository, identificacionBene, nToken);
                    res.Result = nUs;
                }
                else {
                    log.GuardarErrorLogPeticion(nToken, JsonConvert.SerializeObject(param), "ValidarUsuario");
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
        public Resultado ProcesarEspecialidadesCiudad(string identificacion, string tipoId, int ciudad, string token, string idConv,string identificacionChat, string numeroCelular)
        {
            ISchedulingPetitionsRepository petitionsRepository = new SchedulingPetitionsRepository();
            ISchedulingSaveRepository saveRepository = new SchedulingSaveRepository();
            SchedulingPetitionsService serv = new SchedulingPetitionsService();
            AuthenticationApp aApp = new AuthenticationApp();
            serv.LimpiarTablasFlujo(saveRepository, 0, idConv, "tempEspecialidades");
            string espe = serv.ProcesarEspecialidadesCiudad(petitionsRepository, saveRepository, identificacion, tipoId,ciudad, token, idConv);
            Resultado res = new Resultado();
            if (espe == "error_token")
            {
                LogApp log = new LogApp();
                Dictionary<string, string> param = new Dictionary<string, string>() {
                {"numeroCelular",numeroCelular },
                {"identificacion",tipoId+identificacion},
                {"identificacionChat",identificacionChat},
                {"ciudad",ciudad.ToString()},
                {"idConv", idConv }
              };
                log.GuardarErrorLogPeticion(espe, JsonConvert.SerializeObject(param), "GetEspecialidadesCiudad");
                string nToken = aApp.RefreshToken(numeroCelular, identificacionChat);
                if (nToken != "error_credenciales" & nToken != "error_parametros" & nToken != "error_desconocido")
                {
                    res.Result = serv.ProcesarEspecialidadesCiudad(petitionsRepository, saveRepository, identificacion, tipoId, ciudad, nToken, idConv);
                }
                else
                {
                    log.GuardarErrorLogPeticion(nToken, JsonConvert.SerializeObject(param), "GetEspecialidadesCiudad");
                    res.Result = nToken;
                }
                res.Token = nToken;
            }
            else
            {
                res.Result = espe;
                res.Token = token;
            }
            return res;
        }
        public Resultado ProcesarCitas(int ciudad, int especialidad, string token, string idConv,string numeroCelular,string identificacion) 
        {
            ISchedulingPetitionsRepository petitionsRepository = new SchedulingPetitionsRepository();
            ISchedulingSaveRepository saveRepository = new SchedulingSaveRepository();
            SchedulingPetitionsService serv = new SchedulingPetitionsService();
            AuthenticationApp aApp = new AuthenticationApp();
            serv.LimpiarTablasFlujo(saveRepository, 0, idConv, "tempInfoAgendamiento");
            string cc = serv.ProcesarCitas(petitionsRepository, saveRepository, ciudad, especialidad, token, idConv);
            Resultado res = new Resultado();
            if (cc == "error_token")
            {
                LogApp log = new LogApp();
                Dictionary<string, string> param = new Dictionary<string, string>() {
                {"numeroCelular",numeroCelular },
                {"identificacion",identificacion},
                {"especialidad",especialidad.ToString()},
                {"ciudad",ciudad.ToString()},
                {"idConv", idConv }
              };
                log.GuardarErrorLogPeticion(cc, JsonConvert.SerializeObject(param), "GetCitasCiudad");
                string nToken = aApp.RefreshToken(numeroCelular, identificacion);
                if (nToken != "error_credenciales" & nToken != "error_parametros" & nToken != "error_desconocido")
                {
                    res.Result = serv.ProcesarCitas(petitionsRepository, saveRepository, ciudad, especialidad, nToken, idConv);
                }
                else {
                    log.GuardarErrorLogPeticion(nToken, JsonConvert.SerializeObject(param), "GetCitasCiudad");
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
        public Resultado AsignarCita(int espacioCita, string tipoId, string numId, int centroMedico, int medico, int especialidad, string telefono, string correo, string celular, string token, string numeroCelularConv,string identificacion)
        {
            ISchedulingPetitionsRepository petitionsRepository = new SchedulingPetitionsRepository();
            SchedulingPetitionsService serv = new SchedulingPetitionsService();
            AuthenticationApp aApp = new AuthenticationApp();
            string result = serv.AsignarCita(petitionsRepository, espacioCita, tipoId, numId, centroMedico, medico, especialidad, telefono, correo, celular, token);
            Resultado res = new Resultado();
            if (result == "error_token")
            {
                LogApp log = new LogApp();
                Dictionary<string, string> param = new Dictionary<string, string>() {
                {"espacioCita",espacioCita.ToString() },
                {"tipoId",tipoId},
                {"numId",numId},
                {"centroMedico",centroMedico.ToString()},
                {"medico", medico.ToString() },
                {"especialidad", especialidad.ToString() },
                {"telefono", telefono },
                {"medico", medico.ToString() },
                {"correo", correo },
                {"celular", celular },
                {"numeroCelularConv", numeroCelularConv },
                {"identificacion", identificacion }
              };
                log.GuardarErrorLogPeticion(result, JsonConvert.SerializeObject(param), "AsignarCita");
                string nToken = aApp.RefreshToken(numeroCelularConv, identificacion);
                if (nToken != "error_credenciales" & nToken != "error_parametros" & nToken != "error_desconocido")
                {
                    res.Result = serv.AsignarCita(petitionsRepository, espacioCita, tipoId, numId, centroMedico, medico, especialidad, telefono, correo, celular, nToken);
                }
                else {
                    log.GuardarErrorLogPeticion(nToken, JsonConvert.SerializeObject(param), "AsignarCita");
                    res.Result = nToken;
                }
                res.Token = nToken;
            }
            else
            {
                res.Result = result;
                res.Token = token;
            }
            return res;
        }
        public void ProcesarBeneficiariosCiudades(string identificacion, string token, string idConv, string numeroCelular) {
            ISchedulingPetitionsRepository petitionsRepository = new SchedulingPetitionsRepository();
            ISchedulingSaveRepository saveRepository = new SchedulingSaveRepository();
            SchedulingPetitionsService serv = new SchedulingPetitionsService();
            AuthenticationApp aApp = new AuthenticationApp();
            LocalQueriesApp lApp = new LocalQueriesApp();
            bool limpia = lApp.LimpiarTablas(idConv);
            if (limpia)
            {
                //serv.LimpiarTablasFlujo(saveRepository, 1, idConv, "");
                List<BeneficiarioContratante> bens = serv.GetBeneficiariosContratante(petitionsRepository, saveRepository, identificacion, token, idConv);

                if (bens[0].Parentesco == "error_token")
                {
                    LogApp log = new LogApp();
                    Dictionary<string, string> param = new Dictionary<string, string>() {
                {"numeroCelular",numeroCelular },
                {"identificacion",identificacion},
                {"idConv", idConv }
              };
                    log.GuardarErrorLogPeticion(bens[0].Parentesco, JsonConvert.SerializeObject(param), "GetBeneficiariosContratante");

                    token = aApp.RefreshToken(numeroCelular, identificacion);
                    if (token != "error_credenciales" & token != "error_parametros" & token != "error_desconocido")
                    {
                        bens = serv.GetBeneficiariosContratante(petitionsRepository, saveRepository, identificacion, token, idConv);
                    }
                    else
                    {
                        log.GuardarErrorLogPeticion(token, JsonConvert.SerializeObject(param), "GetBeneficiariosContratante");
                    }
                }

                if (bens.Count > 0)
                {
                    foreach (BeneficiarioContratante ben in bens)
                    {
                        string rC = serv.ProcesarCiudadesBeneficiarioBd(petitionsRepository, saveRepository, ben.NumeroIdentificacion, ben.TipoIdentificacion, token, idConv, ben.IdUsuario);
                        if (rC == "error_token")
                        {
                            token = aApp.RefreshToken(numeroCelular, identificacion);
                            serv.ProcesarCiudadesBeneficiarioBd(petitionsRepository, saveRepository, ben.NumeroIdentificacion, ben.TipoIdentificacion, token, idConv, ben.IdUsuario);
                        }
                    }
                }
            }
        }
        public void DummyPetition() {
            ISchedulingPetitionsRepository petitionsRepository = new SchedulingPetitionsRepository();
            SchedulingPetitionsService serv = new SchedulingPetitionsService();
            serv.DummyPetition(petitionsRepository);
        }
    }
}