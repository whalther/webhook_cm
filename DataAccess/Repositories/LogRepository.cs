using DataAccess.ColmedicaModel;
using Domain.DTOs;
using Domain.Repositories;
using System;
using System.Diagnostics;

namespace DataAccess.Repositories
{
   public class LogRepository : ILogRepository
    {
        public Boolean GuardarLogPeticion(LogPeticion logPeticion)
        {
            using (ColmedicaContext contexto = new ColmedicaContext())
            {
                try
                {

                    log_petitions log = new log_petitions()
                    {
                        date = DateTime.Now,
                        action = logPeticion.Action,
                        parameters = logPeticion.Parameters
                        
                    };

                    contexto.log_petitions.Add(log);
                    contexto.SaveChanges();
                    return true;
                }
                catch (Exception E)
                {
                    Trace.WriteLine(E.Message);
                    return false;
                }


            }

        }
    }
}
