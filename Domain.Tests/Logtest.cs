using System;
using Domain.DTOs;
using Domain.Repositories;
using Domain.Services;
using Domain.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Tests
{
    [TestClass]
    public class Logtest
    {
        [TestMethod]
        public void log()
        {
            LogPeticion log = new LogPeticion();
            ILogRepository logRepository = new LogRepositoryMock();
            LogService logService = new LogService();
            log.FechaHora = new DateTime();
            log.Action = "Prueba";
            log.Parameters = "['id_empleado=16309','membresia=14']";
            //Act
            bool seInserto = logService.GuardarLogPeticion(logRepository, log);
            //Accert
            Assert.IsTrue(seInserto);
        }
    }
}
