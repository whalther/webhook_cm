using CrossCutting.Repositories;
using Domain.Repositories;
using Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
             string token = authService.ValidarCliente(authRepository, "3194198375", "CC79880800","alvaroprueba");
            Assert.IsNotNull(token);
        }

        [TestMethod]
        public void RefreshToken()
        {
            IAuthenticationRepository authRepository = new AuthenticationRepository();
            AuthenticationService authService = new AuthenticationService();
            string token = authService.RefreshToken(authRepository, "3194198375", "CC79880800","alvaroprueba");
            Assert.IsNotNull(token);
        }

        [TestMethod]
        public void ValidarOtp()
        {
            IAuthenticationRepository authRepository = new AuthenticationRepository();
            AuthenticationService authService = new AuthenticationService();
            
            string res = authService.ValidarOtp(authRepository, "","", "635933","alvaroprueba");
            Assert.IsNotNull(res);
        }
    }
}
