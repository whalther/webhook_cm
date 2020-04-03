using DataAccess.ColmedicaModel;
using DataAccess.Services;
using Domain.DTOs;
using Domain.Repositories;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
   public class LogRepository : ILogRepository
    {
        private ServiceBusClient ServiceClient;
        public LogRepository()
        {
            ServiceClient = new ServiceBusClient(ConfigurationManager.AppSettings["logicAppConec"], ConfigurationManager.AppSettings["nameCola"] );
        }
        public Boolean GuardarLogPeticion(LogPeticion logPeticion)
        {
            using (ColmedicaContext contexto = new ColmedicaContext())
            {
                try
                {

                    log_petitions log = new log_petitions()
                    {
                        date = DateTime.Now,
                        ip = logPeticion.Ip,
                        path = logPeticion.Path,
                        requestContent = logPeticion.ContenidoPeticion

                    };
                    contexto.log_petitions.Add(log);
                    contexto.SaveChanges();
                    return true;
                }
                catch (Exception E)
                {
                    Trace.WriteLine(E.Message);
                    return false;
                    throw;
                }
            }

        }

        public async Task GuardarErrorLogPeticion(string tipo,string param,string metodo)
        {
            using (ColmedicaContext contexto = new ColmedicaContext())
            {
              try
                {

                   logErrorPeticion log = new logErrorPeticion()
                    {
                        tipo = tipo,
                        @params = param,
                        fecha = DateTime.Now,
                        metodo = metodo,
                        origen = "webhook"
                    };

                  //    contexto.logErrorPeticion.Add(log);
                    await SendChatbotData(log).ConfigureAwait(false);
                    contexto.SaveChanges();
                   // return true;
                }
                catch (Exception E)
                {
                    Trace.WriteLine(E.Message);
                   // return false;
                    throw;
                }
            }

        }
        private async Task<bool> SendChatbotData(logErrorPeticion RequestPNR)
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
