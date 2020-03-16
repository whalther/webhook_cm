﻿using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ILocalQueriesRepository
    {
        List<TipoDocumento> GetTiposDocumento();
        List<Contrato> GetContratos(string idConv);
        List<BeneficiarioContratante> GetBeneficiariosContrato(int idContrato, string idConv);
        Ciudad GetCiudadBeneficiario(int idUsuario, string idConv);
        List<Ciudad> GetCiudadesBeneficiario(int idUsuario, string idConv);
        List<Especialidad> GetEspecialidades(string idConv);
        List<Global> GetMedicos(string idConv);
        List<Global> GetCentroMedicos(string idConv);
        List<Cita> GetCitasProximas(string fecha, string idConv);
        List<Cita> GetCitasMedico(int idMedico, string idConv);
        List<Cita> GetCitasCentroMedico(int idCentroMedico, string idConv);
    }
}
