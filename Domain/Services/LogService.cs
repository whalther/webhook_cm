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
    }
}
