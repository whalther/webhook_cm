using Domain.DTOs;
using System;
using System.Collections.Generic;

namespace Domain.Repositories
{
    public interface ILocalQueriesRepository
    {
        List<TipoDocumento> GetTiposDocumento();
        List<Contrato> GetContratos(string idConv);
        ResultBeneficiarios GetBeneficiariosContrato(int idContrato, string idConv);
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
       }
}
