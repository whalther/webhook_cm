using Domain.DTOs;
using System;
using System.Collections.Generic;

namespace Domain.Repositories
{
   public interface ISchedulingSaveRepository
    {
        Boolean SaveBeneficiarios(List<BeneficiarioContratante> beneficiarios, string idConv);
        Boolean SaveCiudades(List<Ciudad> ciudades, string idConv, int idUsuario);
        Boolean SaveEspecialidadesCiudad(List<Especialidad> especialidades, string idConv);
        Boolean SaveCitasCiudad(List<Cita> citas, string idConv);
        Boolean LimpiarTablasFlujo(int proceso, string idConv, string tabla);
        Boolean SaveCitasBeneficiario(List<CitaBeneficiario> citas, string idConv);
    }
}
