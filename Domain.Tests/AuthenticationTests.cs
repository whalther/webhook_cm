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
            string token = authService.GetToken(authRepository, "3194198375", "CC79880800");
            Assert.IsNotNull(token);
        }
    }
}
