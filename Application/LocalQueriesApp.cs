﻿using CrossCutting.Repositories;
using DataAccess.Repositories;
using Domain.DTOs;
using Domain.Repositories;
using Domain.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Application
{
    public class LocalQueriesApp
    {
        public List<Contrato> GetContratos(string idConv)
        {
            ILocalQueriesRepository repo = new LocalQueriesRepository();
            LocalQueriesService serv = new LocalQueriesService();
            return serv.GetContratos(repo,idConv);
        }
        public ResultBeneficiarios GetBeneficiariosContrato(string idContrato,string idConv)
        {
            ILocalQueriesRepository repo = new LocalQueriesRepository();
            LocalQueriesService serv = new LocalQueriesService();
            return serv.GetBeneficiatiosContrato(repo,idContrato, idConv);
        }
        public Ciudad GetCiudadBeneficiario(int idUsuario, string idConv)
        {
            ILocalQueriesRepository repo = new LocalQueriesRepository();
            LocalQueriesService serv = new LocalQueriesService();
            return serv.GetCiudadBeneficiario(repo, idUsuario, idConv);
        }
        public List<Ciudad> GetCiudadesBeneficiario(int idUsuario, string idConv)
        {
            ILocalQueriesRepository repo = new LocalQueriesRepository();
            LocalQueriesService serv = new LocalQueriesService();
            return serv.GetCiudadesBeneficiario(repo, idUsuario, idConv);
        }
        public List<Especialidad> GetEspecialidades(string idConv)
        {
            ILocalQueriesRepository repo = new LocalQueriesRepository();
            LocalQueriesService serv = new LocalQueriesService();
            return serv.GetEspecialidades(repo,idConv);
        }
        public List<GlobalResp> GetMedicos(string idConv)
        {
            ILocalQueriesRepository repo = new LocalQueriesRepository();
            LocalQueriesService serv = new LocalQueriesService();
            return serv.GetMedicos(repo, idConv);
        }
        public List<GlobalResp> GetCentrosMedicos(string idConv)
        {
            ILocalQueriesRepository repo = new LocalQueriesRepository();
            LocalQueriesService serv = new LocalQueriesService();
            return serv.GetCentroMedicos(repo, idConv);
        }
        public List<Cita> GetCitasProximas(string fecha,string idConv)
        {
            ILocalQueriesRepository repo = new LocalQueriesRepository();
            LocalQueriesService serv = new LocalQueriesService();
            return serv.GetCitasProximas(repo,fecha, idConv);
        }
        public List<Cita> GetCitasMedico(int idMedico, string idConv)
        {
            ILocalQueriesRepository repo = new LocalQueriesRepository();
            LocalQueriesService serv = new LocalQueriesService();
            return serv.GetCitasMedico(repo, idMedico, idConv);
        }
        public List<Cita> GetCitasCentroMedico(int idCentroMedico, string idConv)
        {
            ILocalQueriesRepository repo = new LocalQueriesRepository();
            LocalQueriesService serv = new LocalQueriesService();
            return serv.GetCitasCentroMedico(repo, idCentroMedico, idConv);
        }
        public Boolean UpdateCitaBd(string idConv,string campo, string valor)
        {
            ILocalQueriesRepository repo = new LocalQueriesRepository();
            LocalQueriesService serv = new LocalQueriesService();
            return serv.UpdateCitaBd(repo,idConv,campo,valor);
        }
        public Boolean LimpiarTablas(string idConv)
        {
            ILocalQueriesRepository repo = new LocalQueriesRepository();
            LocalQueriesService serv = new LocalQueriesService();
            return serv.LimpiarTablas(repo, idConv);
        }
        public dynamic GetInfoCita(string idConv)
        {
            ILocalQueriesRepository repo = new LocalQueriesRepository();
            LocalQueriesService serv = new LocalQueriesService();
            return serv.GetInfoCita(repo, idConv);
        }
        public void AsignarCita(string idConv,string numDoc, string tipoDoc,string numeroCelular,string token)
        {
            ILocalQueriesRepository repo = new LocalQueriesRepository();
            ISchedulingPetitionsRepository sRepo = new SchedulingPetitionsRepository();
            LocalQueriesService serv = new LocalQueriesService();
            SchedulingPetitionsService sServ = new SchedulingPetitionsService();
            AuthenticationApp aApp = new AuthenticationApp();
            LogApp log = new LogApp();
            string identificacion = tipoDoc + numDoc;
            dynamic infoCita = serv.GetInfoAsignarCita(repo,idConv);
            string telefono = String.IsNullOrEmpty(infoCita.telefono) ?"": infoCita.telefono;
            string celular = String.IsNullOrEmpty(infoCita.celular)?"": infoCita.celular;
            bool resultAgendamiento;
            dynamic res;
            string detalle;
            string resultadoAsig;
            string estado;
            Dictionary<string, string> values = new Dictionary<string, string>() {
                {"espacioCita",infoCita.numEspacioCita.ToString()},
                {"tipoId",infoCita.tipoIdBeneficiario},
                {"numId",infoCita.numIdBeneficiario.ToString()},
                {"centroMedico",infoCita.centroMedico.ToString()},
                {"medico",infoCita.idMedico.ToString()},
                {"especialidad",infoCita.especialidad.ToString()},
                {"telefono",telefono},
                {"correo",infoCita.correo},
                {"celular",celular},
                {"token",token}
            };
             resultadoAsig = sServ.AsignarCita(sRepo,values,idConv);
            if (resultadoAsig == "error_token")
            {
                string nToken = aApp.RefreshToken(numeroCelular, identificacion,idConv);
                if (nToken != "error_credenciales" && nToken != "error_parametros" && nToken != "error_desconocido")
                {
                    values.Remove("token");
                    values.Add("token",nToken);
                    resultadoAsig = sServ.AsignarCita(sRepo,values,idConv);
                }
                else
                {
                    resultadoAsig = nToken;
                }
            }
                serv.UpdateCitaBd(repo,idConv,"agendamiento", resultadoAsig);
            if (resultadoAsig != "error_credenciales" && resultadoAsig != "error_parametros" && resultadoAsig != "error_desconocido" && resultadoAsig != "error_token") 
            {
                res = JToken.Parse(resultadoAsig);
                string msj = res.Mensaje;
                string numConfirm = res.Numconfirmacion;
                string valorPagar = res.Valor;
                if (!string.IsNullOrEmpty(numConfirm))
                {
                    resultAgendamiento = true;
                    detalle = "Cita agendada";
                    estado = "agendada";
                    serv.UpdateCitaBd(repo,idConv,"valorPagar",valorPagar);
                    serv.UpdateCitaBd(repo, idConv, "idCita", numConfirm);
                }
                else if (!string.IsNullOrEmpty(msj))
                {
                    resultAgendamiento = false;
                    estado = "error_agendamiento";
                    detalle = res.Mensaje;
                }
                else
                {
                    resultAgendamiento = false;
                    estado = "error_desconocido";
                    detalle = resultadoAsig;
                }
            }
            else
            {
                resultAgendamiento = false;
                estado = resultadoAsig;
                detalle = resultadoAsig;
            }
            Dictionary<string, object> paramLog = new Dictionary<string, object>() {
                {"tipoTransaccion","agendamiento" },
                {"fechaTransaccion", Convert.ToDateTime(string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now)) },
                {"exitoso",resultAgendamiento },
                {"detalle", detalle},
                {"sessionId", idConv },
                {"celular", numeroCelular },
                {"traza" , "log"}
            };
            log.GuardarLogCitaAgendada(paramLog);
            serv.SaveCitaNoTemp(repo,idConv, (int)infoCita.numEspacioCita,"agendamiento",estado);
        }
        public Boolean QueryDummy()
        {
            ILocalQueriesRepository repo = new LocalQueriesRepository();
            LocalQueriesService serv = new LocalQueriesService();
            return serv.QueryDummy(repo);
        }
        public dynamic GetInfoCitaAgendada(string idConv)
        {
            ILocalQueriesRepository repo = new LocalQueriesRepository();
            LocalQueriesService serv = new LocalQueriesService();
            return serv.GetInfoAsignarCita(repo,idConv);
        }
        public List<CitaBeneficiario> GetCitasBeneficiario(string idConv)
        {
            ILocalQueriesRepository repo = new LocalQueriesRepository();
            LocalQueriesService serv = new LocalQueriesService();
            return serv.GetCitasBeneficiario(repo, idConv);
        }
        public CitaBeneficiario GetInfoCitaBeneficiario(string idConv, int idCita)
        {
            ILocalQueriesRepository repo = new LocalQueriesRepository();
            LocalQueriesService serv = new LocalQueriesService();
            return serv.GetInfoCitaBeneficiario(repo, idConv,idCita);
        }
        public void CancelarCitaBeneficiario(string idConv, string numDocConv,string tipoDocConv, string identificacionBeneficiario, string identificacionCotizante,int idCita, string numeroCelular, string token)
        {
            ILocalQueriesRepository repo = new LocalQueriesRepository();
            ISchedulingPetitionsRepository sRepo = new SchedulingPetitionsRepository();
            LocalQueriesService serv = new LocalQueriesService();
            SchedulingPetitionsService sServ = new SchedulingPetitionsService();
            AuthenticationApp aApp = new AuthenticationApp();
            string identificacionConv = tipoDocConv + numDocConv;
            string resultadoCan = sServ.CancelarCitaBeneficiario(sRepo, token,identificacionCotizante,identificacionBeneficiario,idCita.ToString(),idConv);
            string estadoCan;
            if (resultadoCan == "error_token")
            {
                string nToken = aApp.RefreshToken(numeroCelular, identificacionConv,idConv);
                if (nToken != "error_credenciales" && nToken != "error_parametros" && nToken != "error_desconocido")
                {

                    string res = sServ.CancelarCitaBeneficiario(sRepo, nToken, identificacionCotizante, identificacionBeneficiario, idCita.ToString(),idConv);
                    serv.UpdateCancelacionCita(repo, idConv, idCita, res);
                    dynamic resV = JToken.Parse(res);
                    if (resV.Resultado == 1)
                    {
                        estadoCan = "cancelada";
                    }
                    else
                    {
                        estadoCan = "error|" + resV.Mensaje;
                    }
                }
                else
                {
                    serv.UpdateCancelacionCita(repo, idConv, idCita, nToken);
                    estadoCan = nToken;
                }
            }
            else
            {
                dynamic res = JToken.Parse(resultadoCan);
                if (res.Resultado == 1)
                {
                    estadoCan = "cancelada";
                }
                else 
                {
                    estadoCan = "error|" + res.Mensaje;
                }
                serv.UpdateCancelacionCita(repo, idConv, idCita, resultadoCan);
            }
            
            serv.SaveCitaNoTemp(repo, idConv, idCita, "cancelacion", estadoCan);
        }
        public string GetEstadoCancelacion(string idConv, int idCita)
        {
            ILocalQueriesRepository repo = new LocalQueriesRepository();
            LocalQueriesService serv = new LocalQueriesService();
            return serv.GetEstadoCancelacion(repo, idConv, idCita);
        }
    }
}
