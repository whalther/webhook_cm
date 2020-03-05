using System;
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
        public void LogPetition()
        {
            //Arrange
            LogPeticion log = new LogPeticion();
            ILogRepository logRepository = new LogRepository();
            LogService logService = new LogService();
            log.FechaHora = new DateTime();
            log.Action = "prueba";
            log.Parameters = "['id_empleado=16309','membresia=14']";
            //Act
            bool seInserto = logService.GuardarLogPeticion(logRepository, log);
            //Accert
           Assert.IsTrue(seInserto);
        }
    }
}
