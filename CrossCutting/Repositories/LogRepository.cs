using DataAccess.Services;
using Domain.DTOs;
using Domain.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CrossCutting.Repositories
{
    public class LogRepository : ILogRepository
    {
        readonly private ServiceBusClient ServiceClient;
        public LogRepository()
        {
            ServiceClient = new ServiceBusClient(ConfigurationManager.AppSettings["logicAppConec"], ConfigurationManager.AppSettings["nameCola"]);
        }
        public async Task GuardarErrorLogPeticion(string tipo, string param, string detalle, string metodo, string idConv)
        {
                try
                {

                    LogPeticion log = new LogPeticion()
                    {
                        Tipo = tipo,
                        Params = param,
                        Detalle = detalle,
                        Fecha = Convert.ToDateTime(string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now)),
                        Metodo = metodo,
                        Origen = "webhook",
                        IdConv = idConv,
                        Traza = "error"
                    };
                    await SendChatbotData(log).ConfigureAwait(false);
                }
                catch (Exception E)
                {
                    Trace.WriteLine(E.Message);
                    throw;
                }
        }
        public async Task GuardarLogCitaAgendada(Dictionary<string, object> param)
        {
                try
                {
                    await SendChatbotData(param).ConfigureAwait(false);
                }
                catch (Exception E)
                {
                    Trace.WriteLine(E.Message);
                    throw;
                }
        }
        private async Task<bool> SendChatbotData(dynamic RequestPNR)
        {
            try
            {
                string json = JsonConvert.SerializeObject(RequestPNR);
                await ServiceClient.SendMessagesAsync(json).ConfigureAwait(false);
                return true;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                return false;
                throw;
            }
        }
    }
}
