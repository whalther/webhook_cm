using DataAccess.Repositories;
using Domain.DTOs;
using Domain.Repositories;
using Domain.Services;
using System.Threading.Tasks;

namespace Application
{
   public class LogApp
    {
        public bool GuardarLogPeticion(LogPeticion logPeticion)
        {
            ILogRepository logRepository = new LogRepository();
            return new LogService().GuardarLogPeticion(logRepository, logPeticion);

        }
        public async Task GuardarErrorLogPeticion(string tipo, string param,string metodo)
        {
            ILogRepository logRepository = new LogRepository();
            await new LogService().GuardarErrorLogPeticion(logRepository,tipo,param,metodo).ConfigureAwait(false);
        }
    }
}
