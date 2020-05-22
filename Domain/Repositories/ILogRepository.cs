using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
   public interface ILogRepository
    {
        Task GuardarErrorLogPeticion(string tipo, string param,string detalle, string metodo, string idConv);
        Task GuardarLogCitaAgendada(Dictionary<string,object> param);
    }
}
