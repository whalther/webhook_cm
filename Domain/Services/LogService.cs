using Domain.DTOs;
using Domain.Repositories;

namespace Domain.Services
{
    public class LogService
    {
        public bool GuardarLogPeticion(ILogRepository logRepository, LogPeticion logPeticion)
        {
            return logRepository.GuardarLogPeticion(logPeticion);
        }
        public bool GuardarErrorLogPeticion(ILogRepository logRepository, string tipo, string param,string metodo)
        {
            return logRepository.GuardarErrorLogPeticion(tipo,param,metodo);
        }
    }
}
