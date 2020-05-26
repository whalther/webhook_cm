using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ILocalQueriesRepository
    {
        List<Contrato> GetContratos(string idConv);
        ResultBeneficiarios GetBeneficiariosContrato(string idContrato, string idConv);
        Ciudad GetCiudadBeneficiario(int idUsuario, string idConv);
        List<Ciudad> GetCiudadesBeneficiario(int idUsuario, string idConv);
        List<Especialidad> GetEspecialidades(string idConv);
        List<GlobalResp> GetMedicos(string idConv);
        List<GlobalResp> GetCentroMedicos(string idConv);
        List<Cita> GetCitasProximas(string fecha, string idConv);
        List<Cita> GetCitasMedico(int idMedico, string idConv);
        List<Cita> GetCitasCentroMedico(int idCentroMedico, string idConv);
        Boolean UpdateCitaBd(string idConv, string campo, string valor);
        Boolean LimpiarTablas(string idConv);
        dynamic GetInfoCita(string idConv);
        dynamic GetInfoAsignarCita(string idConv);
        Boolean QueryDummy();
        List<CitaBeneficiario> GetCitasBeneficiario(string idConv);
        CitaBeneficiario GetInfoCitaBeneficiario(string idConv, int idCita);
        Boolean UpdateCancelacionCita(string idConv, int idCita, string resultado);
        String GetEstadoCancelacion(string idConv, int idCita);
        Task<int> SaveCitaNoTemp(string idConv, int idCita, string flag, string estado);

    }
}
