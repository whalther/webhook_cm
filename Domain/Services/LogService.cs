using Domain.DTOs;
using Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class LogService
    {
        public async Task GuardarErrorLogPeticion(ILogRepository logRepository, string tipo, string param,string detalle,string metodo,string idConv)
        {
           await logRepository.GuardarErrorLogPeticion(tipo,param,detalle,metodo,idConv).ConfigureAwait(false);
        }
        public async Task GuardarLogCitaAgendada(ILogRepository logRepository, Dictionary<string,object> param )
        {
            await logRepository.GuardarLogCitaAgendada(param).ConfigureAwait(false);
        }
    }
}
