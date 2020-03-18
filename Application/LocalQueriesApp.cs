using CrossCutting.Repositories;
using DataAccess.Repositories;
using Domain.DTOs;
using Domain.Repositories;
using Domain.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public List<BeneficiarioContratante> GetBeneficiariosContrato(int idContrato,string idConv)
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
        public List<Global> GetMedicos(string idConv)
        {
            ILocalQueriesRepository repo = new LocalQueriesRepository();
            LocalQueriesService serv = new LocalQueriesService();
            return serv.GetMedicos(repo, idConv);
        }
        public List<Global> GetCentrosMedicos(string idConv)
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
        public string AsignarCita(string idConv,string numDoc, string tipoDoc,string numeroCelular,string token)
        {
            ILocalQueriesRepository repo = new LocalQueriesRepository();
            ISchedulingPetitionsRepository sRepo = new SchedulingPetitionsRepository();
            LocalQueriesService serv = new LocalQueriesService();
            SchedulingPetitionsService sServ = new SchedulingPetitionsService();
            AuthenticationApp aApp = new AuthenticationApp();
            string identificacion = tipoDoc + numDoc;
            dynamic infoCita = serv.GetInfoAsignarCita(repo,idConv);
            string telefono = infoCita.telefono;
            if (telefono == null) { telefono = ""; }
            string celular = infoCita.celular;
            if (celular == null) { celular = ""; };
            string resultadoAsig = sServ.AsignarCita(sRepo,infoCita.numEspacioCita,infoCita.tipoIdBeneficiario,infoCita.numIdBeneficiario,infoCita.centroMedico,infoCita.idMedico,infoCita.especialidad,telefono,"",celular,token);
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
                if (nToken != "error_credenciales" & nToken != "error_parametros" & nToken != "error_desconocido")
                {
                    return sServ.AsignarCita(sRepo, infoCita.numEspacioCita, infoCita.tipoIdBeneficiario, infoCita.numIdBeneficiario, infoCita.centroMedico, infoCita.idMedico, infoCita.especialidad, telefono, "", celular, nToken);

                }
                else
                {
                    log.GuardarErrorLogPeticion(nToken, JsonConvert.SerializeObject(param), "AsignarCita");
                    return nToken;
                }
                
            }
            else
            {
                return resultadoAsig;
            } 
        }

    }
}
