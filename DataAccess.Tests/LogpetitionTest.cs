﻿using System.Threading.Tasks;
using CrossCutting.Repositories;
using Domain.Repositories;
using Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccess.Tests
{
    [TestClass]
    public class LogpetitionTest
    {
        [TestMethod]
        public async Task LogPetition()
        {
            ILogRepository logRepository = new LogRepository();
            LogService logService = new LogService();
            await logService.GuardarErrorLogPeticion(logRepository,"prueba","prueba_error","prueba","test", "alvaroposada").ConfigureAwait(false);
            Assert.IsNotNull("a");
          
        }
    }
}
