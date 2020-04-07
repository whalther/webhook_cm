using System;
using System.Threading.Tasks;
using DataAccess.Repositories;
using Domain.DTOs;
using Domain.Repositories;
using Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccess.Tests
{
    [TestClass]
    public class LogpetitionTest
    {
        [TestMethod]
        public async void LogPetition()
        {
            //Arrange
            LogPeticion log = new LogPeticion();
            ILogRepository logRepository = new LogRepository();
            LogService logService = new LogService();
            //Act
             await logService.GuardarErrorLogPeticion(logRepository,"prueba","prueba_error","test").ConfigureAwait(false);
            //Accert
          // Assert.IsNotNull(seInserto);
        }
    }
}
