using Domain.DTOs;
using Domain.Repositories;
using System;
using System.Collections.Generic;


namespace Domain.Tests.Mocks
{
    public class LogRepositoryMock : ILogRepository
    {
        public bool GuardarLogPeticion(LogPeticion logPeticion)
        {
            try
            {
                List<LogPeticion> logPeticiones = new List<LogPeticion>();
                logPeticiones.Add(logPeticion);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
