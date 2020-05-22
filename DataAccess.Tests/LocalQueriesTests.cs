using DataAccess.Repositories;
using Domain.Repositories;
using Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccess.Tests
{
    [TestClass]
    public class LocalQueriesTests
    {
        [TestMethod]
        public void UpdateCita()
        {
           
            ILocalQueriesRepository logRepository = new LocalQueriesRepository();
            LocalQueriesService logService = new LocalQueriesService();
            
            //Act
            bool seInserto = logService.UpdateCitaBd(logRepository,"32432klkl-234","tipoIdBeneficiario","DUI");
            //Accert
            Assert.IsTrue(seInserto);
        }
    }
}
