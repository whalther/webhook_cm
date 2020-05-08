using Domain.DTOs;
using Domain.Repositories;
using System;
using System.Collections.Generic;

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
        public ResultBeneficiarios GetBeneficiatiosContrato(ILocalQueriesRepository repo,string contrato, string idConv)
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
        public Boolean QueryDummy(ILocalQueriesRepository repo)
        {
            return repo.QueryDummy();
        }
        public List<CitaBeneficiario> GetCitasBeneficiario(ILocalQueriesRepository repo, string idConv) {
            return repo.GetCitasBeneficiario(idConv);
        }
        public CitaBeneficiario GetInfoCitaBeneficiario(ILocalQueriesRepository repo, string idConv, int idCita)
        {
            return repo.GetInfoCitaBeneficiario(idConv,idCita);
        }
        public Boolean UpdateCancelacionCita(ILocalQueriesRepository repo, string idConv, int idCita, string resultado)
        {
            return repo.UpdateCancelacionCita(idConv, idCita, resultado);
        }
        public string GetEstadoCancelacion(ILocalQueriesRepository repo, string idConv, int idCita)
        {
            return repo.GetEstadoCancelacion(idConv, idCita);
        }
        public dynamic GetInfoLinkPagos(ILocalQueriesRepository repo, string idConv, int idCita) 
        {
            return repo.GetInfoLinkPagos(idConv,idCita);
        }
        public Boolean UpdateLinkCita(ILocalQueriesRepository repo, string idConv, int idCita, string result)
        {
            return repo.UpdateLinkCita(idConv,idCita,result);
        }
    }
}
