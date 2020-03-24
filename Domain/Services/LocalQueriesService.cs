using Domain.DTOs;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
   public class LocalQueriesService
    {
        public List<TipoDocumento> GetTiposDocumentos(ILocalQueriesRepository repo) 
        {
            return repo.GetTiposDocumento();
        }
        public List<Contrato> GetContratos(ILocalQueriesRepository repo,string idConv)
        {
            return repo.GetContratos(idConv);
        }
        public List<BeneficiarioContratante> GetBeneficiatiosContrato(ILocalQueriesRepository repo,int contrato, string idConv)
        {
            return repo.GetBeneficiariosContrato(contrato,idConv);
        }
        public Ciudad GetCiudadBeneficiario(ILocalQueriesRepository repo, int idUsuario, string idConv)
        {
            return repo.GetCiudadBeneficiario(idUsuario, idConv);
        }
        public List<Ciudad> GetCiudadesBeneficiario(ILocalQueriesRepository repo, int idUsuario, string idConv)
        {
            return repo.GetCiudadesBeneficiario(idUsuario, idConv);
        }
        public List<Especialidad> GetEspecialidades(ILocalQueriesRepository repo, string idConv)
        {
            return repo.GetEspecialidades( idConv);
        }
        public List<GlobalResp> GetMedicos(ILocalQueriesRepository repo, string idConv) 
        {
            return repo.GetMedicos(idConv);
        }
        public List<GlobalResp> GetCentroMedicos(ILocalQueriesRepository repo, string idConv) 
        {
            return repo.GetCentroMedicos(idConv);
        }
        public List<Cita> GetCitasProximas(ILocalQueriesRepository repo, string fecha, string idConv)
        {
            return repo.GetCitasProximas(fecha, idConv);
        }
        public List<Cita> GetCitasMedico(ILocalQueriesRepository repo, int idMedico, string idConv)
        {
            return repo.GetCitasMedico(idMedico, idConv);
        }
        public List<Cita> GetCitasCentroMedico(ILocalQueriesRepository repo, int idCentroMedico, string idConv)
        {
            return repo.GetCitasCentroMedico(idCentroMedico, idConv);
        }
        public Boolean UpdateCitaBd(ILocalQueriesRepository repo, string idConv,string campo, string valor)
        {
            return repo.UpdateCitaBd(idConv,campo,valor);
        }
        public Boolean LimpiarTablas(ILocalQueriesRepository repo, string idConv)
        {
            return repo.LimpiarTablas(idConv);
        }
        public dynamic GetInfoCita(ILocalQueriesRepository repo, string idConv)
        {
            return repo.GetInfoCita(idConv);
        }
        public dynamic GetInfoAsignarCita(ILocalQueriesRepository repo, string idConv)
        {
            return repo.GetInfoAsignarCita(idConv);
        }
    }
}
