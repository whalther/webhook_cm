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
    }
}
