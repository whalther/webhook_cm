using DataAccess.Repositories;
using Domain.DTOs;
using Domain.Repositories;
using Domain.Services;
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

    }
}
