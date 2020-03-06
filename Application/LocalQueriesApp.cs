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
    }
}
