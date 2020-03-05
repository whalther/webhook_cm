using System;
using System.Collections.Generic;
using CrossCutting.Repositories;
using Domain.Repositories;
using Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CrossCuttingTests
{
    [TestClass]
    public class AuthTests
    {
        [TestMethod]
        public void GetToken()
        {
             IAuthenticationRepository authRepository = new AuthenticationRepository();
             AuthenticationService authService = new AuthenticationService();
             string token = authService.GetToken(authRepository, "3194198375", "CC79880800");
            Assert.IsNotNull(token);
        }

        [TestMethod]
        public void RefreshToken()
        {
            IAuthenticationRepository authRepository = new AuthenticationRepository();
            AuthenticationService authService = new AuthenticationService();
            string token = authService.RefreshToken(authRepository, "3194198375", "CC79880800");
            Assert.IsNotNull(token);
        }

        [TestMethod]
        public void ValidarOtp()
        {
            IAuthenticationRepository authRepository = new AuthenticationRepository();
            AuthenticationService authService = new AuthenticationService();
            string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c3VhcmlvIjoiTkk5MDA4MDE0NTkiLCJjbGllbnRlIjoiQ0M3OTg4MDgwMCIsImV4cCI6MTU4MzM1NDY3Ni4wfQ.A870IM9HF2kxY-BGoKwheS8dtgiSvZEdHO6fP3M2Hn0";
            string res = authService.ValidarOtp(authRepository, token, "635933");
            Assert.IsNotNull(res);
        }
    }
}
