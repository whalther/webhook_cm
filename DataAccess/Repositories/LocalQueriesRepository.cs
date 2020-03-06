using DataAccess.ColmedicaModel;
using Domain.DTOs;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class LocalQueriesRepository: ILocalQueriesRepository 
    {
        public List<TipoDocumento> GetTiposDocumento() {
            List<TipoDocumento> resultado = null;
            try
            {
                using (ColmedicaContext contexto = new ColmedicaContext())
                {
                    resultado = (from tp in contexto.cmTipoDocumento
                                 where tp.estado == 1
                                 select new TipoDocumento()
                                 {
                                     Id = tp.id,
                                     TipoDoc = tp.tipo_doc,
                                     LabelDocumento = tp.doc_label,
                                     Orden = (int) tp.orden
                                 }
                                 ).ToList();
                }
                return resultado;
            }
            catch (Exception)
            {
                return new List<TipoDocumento>();
            }
        }
    }
}
