using CrossCutting.Repositories;
using DataAccess.Repositories;
using Domain.DTOs;
using Domain.Repositories;
using Domain.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Application
{
    public class LocalQueriesApp
    {
        public List<TipoDocumento> GetTiposDocumento() 
        {
            ILocalQueriesRepository repo = new LocalQueriesRepository();
            LocalQueriesService serv = new LocalQueriesService();
            return serv.GetTiposDocumentos(repo);
        }
        public List<Contrato> GetContratos(string idConv)
        {
            ILocalQueriesRepository repo = new LocalQueriesRepository();
            LocalQueriesService serv = new LocalQueriesService();
            return serv.GetContratos(repo,idConv);
        }
        public ResultBeneficiarios GetBeneficiariosContrato(int idContrato,string idConv)
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
            string identificacion = tipoDoc + numDoc;
            dynamic infoCita = serv.GetInfoAsignarCita(repo,idConv);
            string telefono = String.IsNullOrEmpty(infoCita.telefono) ?"": infoCita.telefono;
            string celular = String.IsNullOrEmpty(infoCita.celular)?"": infoCita.celular;
            Dictionary<string, string> values = new Dictionary<string, string>() {
                {"espacioCita",infoCita.numEspacioCita.ToString()},
                {"tipoId",infoCita.tipoIdBeneficiario},
                {"numId",infoCita.numIdBeneficiario.ToString()},
                {"centroMedico",infoCita.centroMedico.ToString()},
                {"medico",infoCita.idMedico.ToString()},
                {"especialidad",infoCita.especialidad.ToString()},
                {"telefono",telefono},
                {"correo",""},
                {"celular",celular},
                {"token",token}
            };
            string resultadoAsig = sServ.AsignarCita(sRepo,values);
            if (resultadoAsig == "error_token")
            {
                LogApp log = new LogApp();
                Dictionary<string, string> param = new Dictionary<string, string>() {
                {"numeroCelular",numeroCelular },
                {"identificacion",identificacion},
                {"idConv", idConv }
              };
                log.GuardarErrorLogPeticion(resultadoAsig, JsonConvert.SerializeObject(param), "AsignarCita");
                string nToken = aApp.RefreshToken(numeroCelular, identificacion);
                if (nToken != "error_credenciales" && nToken != "error_parametros" && nToken != "error_desconocido")
                {
                    values.Remove("token");
                    values.Add("token",nToken);
                    string res = sServ.AsignarCita(sRepo,values);
                    serv.UpdateCitaBd(repo, idConv, "agendamiento", res);

                }
                else
                {
                    serv.UpdateCitaBd(repo, idConv, "agendamiento", nToken);
                }
            }
            else
            {
                serv.UpdateCitaBd(repo,idConv,"agendamiento", resultadoAsig);
            } 
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
    }
}
