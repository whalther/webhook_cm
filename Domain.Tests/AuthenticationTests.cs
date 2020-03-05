using System;
using System.Collections.Generic;
using Domain.Repositories;
using Domain.Services;
using Domain.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Tests
{
    [TestClass]
    public class AuthenticationTests
    {
        [TestMethod]
        public void Gettoken()
        {
            
            IAuthenticationRepository authRepository = new AuthenticationRepositoryMock();
            AuthenticationService authService = new AuthenticationService();
            /*Dictionary<string, string> hd = new Dictionary<string, string>() {
                {"Authentication","AZDlINpFqKuJpN0gipOZfq4wXjGB3uOirssg0YO7ep0=" },
                {"iv","fD8/Fj8HL0caPxl/"}
            };
            Dictionary<string, string> parametros = new Dictionary<string, string>() {
                {"mensaje","iV8Piaxt63U8BcKMcFVD0+qNdPfdMaodrcb9QxwGD2stvZ8ZvoVzCZLypV54G9MCL1xygssu/CWN2+f69yBJDQ=="},
                {"iv","fD8/Fj8HL0caPxl/"}
            };*/
            //Act
            string token = authService.GetToken(authRepository, "3194198375", "CC79880800");
            Assert.IsNotNull(token);
            //Accert
           
        }
    }
}
