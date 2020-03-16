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
                        ip = logPeticion.Ip,
                        path = logPeticion.Path,
                        requestContent = logPeticion.ContenidoPeticion

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

        public Boolean GuardarErrorLogPeticion(string tipo,string param,string metodo)
        {
            using (ColmedicaContext contexto = new ColmedicaContext())
            {
                try
                {

                    logErrorPeticion log = new logErrorPeticion()
                    {
                        tipo = tipo,
                        @params = param,
                        fecha = DateTime.Now,
                        metodo = metodo
                    };

                    contexto.logErrorPeticion.Add(log);
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
