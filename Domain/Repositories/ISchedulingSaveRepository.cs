using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
   public interface ISchedulingSaveRepository
    {
        Boolean SaveBeneficiarios(List<BeneficiarioContratante> beneficiarios, string idConv);
        Boolean SaveCiudades(List<Ciudad> ciudades, string idConv, int idUsuario);
        Boolean SaveEspecialidadesCiudad(List<Especialidad> especialidades, string idConv);
        Boolean SaveCitasCiudad(List<Cita> citas, string idConv);
    }
}
