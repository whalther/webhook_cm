using DataAccess.Repositories;
using Domain.DTOs;
using Domain.Repositories;
using Domain.Services;


namespace Application
{
   public class LogApp
    {
        public bool GuardarLogPeticion(LogPeticion logPeticion)
        {
            ILogRepository logRepository = new LogRepository();
            return new LogService().GuardarLogPeticion(logRepository, logPeticion);

        }
        public bool GuardarErrorLogPeticion(string tipo, string param,string metodo)
        {
            ILogRepository logRepository = new LogRepository();
            return new LogService().GuardarErrorLogPeticion(logRepository,tipo,param,metodo);

        }
    }
}
