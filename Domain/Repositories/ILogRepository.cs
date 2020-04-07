using Domain.DTOs;
using System;
using System.Threading.Tasks;

namespace Domain.Repositories
{
   public interface ILogRepository
    {
        Boolean GuardarLogPeticion(LogPeticion logPeticion);
        Task GuardarErrorLogPeticion(string tipo, string param, string metodo);
    }
}
