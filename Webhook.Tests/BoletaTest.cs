using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web.Mvc;
using Webhook.Controllers;

namespace Webhook.Tests
{
    [TestClass]
    public class BoletaTest
    {
        [TestMethod]
        public void ObtenerBoletasTest()
        {
            //Arrange
            MainController mainController = new MainController();
            int empleadoId = 10260;
            int mes = 1;
            int anio = 2018;

            //Act
            JsonResult boleta = mainController.Index();

            //Assert

            Assert.IsNotNull(boleta);
        }
    }
}
