using Domain.DTOs;
using Domain.Repositories;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class LogService
    {
        public bool GuardarLogPeticion(ILogRepository logRepository, LogPeticion logPeticion)
        {
            return logRepository.GuardarLogPeticion(logPeticion);
        }
        public async Task GuardarErrorLogPeticion(ILogRepository logRepository, string tipo, string param,string metodo)
        {
           await logRepository.GuardarErrorLogPeticion(tipo,param,metodo).ConfigureAwait(false);
        }
    }
}
