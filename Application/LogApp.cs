using CrossCutting.Repositories;
using Domain.Repositories;
using Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application
{
   public class LogApp
    {
        public async Task GuardarErrorLogPeticion(string tipo, string param,string detalle,string metodo,string idConv)
        {
            ILogRepository logRepository = new LogRepository();
            await new LogService().GuardarErrorLogPeticion(logRepository,tipo,param,detalle,metodo,idConv).ConfigureAwait(false);
        }
        public async Task GuardarLogCitaAgendada(Dictionary<string,object> param)
        {
            ILogRepository logRepository = new LogRepository();
            await new LogService().GuardarLogCitaAgendada(logRepository, param).ConfigureAwait(false);
        }
    }
}
