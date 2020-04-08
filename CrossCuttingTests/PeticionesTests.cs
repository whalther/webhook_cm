using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Domain.Repositories;
using CrossCutting.Repositories;
using Domain.Services;
using Domain.DTOs;
using DataAccess.Repositories;

namespace CrossCuttingTests
{
    [TestClass]
    public class PeticionesTests
    {

        [TestCategory("UnitTests")]
        [TestMethod]
        public void ValidarUsuario()
        {
            ISchedulingPetitionsRepository petRepository = new SchedulingPetitionsRepository();
            SchedulingPetitionsService petService = new SchedulingPetitionsService();
            string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c3VhcmlvIjoiTkk5MDA4MDE0NTkiLCJjbGllbnRlIjoiQ0M3OTg4MDgwMCIsImV4cCI6MTU4MzM1NzcxMy4wfQ.U8yW-XYCp72oeVb-m55xoe3-QnJaZV4Y3KnzgH4bssU";
            Usuario res = petService.ValidarUsuario(petRepository, "CC79880800", token);
            Assert.IsNotNull(res);

        }
        [TestCategory("UnitTests")]
        [TestMethod]
        public void GetBeneficiariosContratante()
        {
            ISchedulingPetitionsRepository petRepository = new SchedulingPetitionsRepository();
            SchedulingPetitionsService petService = new SchedulingPetitionsService();
            IAuthenticationRepository authRepository = new AuthenticationRepository();
            ISchedulingSaveRepository saveRepository = new SchedulingSaveRepository();
            AuthenticationService authService = new AuthenticationService();
            string token = authService.RefreshToken(authRepository, "3194198375", "CC79880800");
            List<BeneficiarioContratante> res = petService.GetBeneficiariosContratante(petRepository,saveRepository, "CC79880800", token,"98fddusfh89udf-sf98df-9");
            Assert.IsNotNull(res);

        }
        [TestCategory("UnitTests")]
        [TestMethod]
        public void GetEspecialidadesCiudad()
        {
            ISchedulingPetitionsRepository petRepository = new SchedulingPetitionsRepository();
            SchedulingPetitionsService petService = new SchedulingPetitionsService();
            IAuthenticationRepository authRepository = new AuthenticationRepository();
            ISchedulingSaveRepository saveRepository = new SchedulingSaveRepository();
            AuthenticationService authService = new AuthenticationService();
            string token = authService.RefreshToken(authRepository, "3194198375", "CC79880800");
            string res = petService.ProcesarEspecialidadesCiudad(petRepository, saveRepository, "1123440768", "TI", 50001, token, "98fddusfh89udf-sf98df-9");
            Assert.IsNotNull(res);
        }
        [TestCategory("UnitTests")]
        [TestMethod]
        public void GetCitasCiudad()
        {
            ISchedulingPetitionsRepository petRepository = new SchedulingPetitionsRepository();
            SchedulingPetitionsService petService = new SchedulingPetitionsService();
            IAuthenticationRepository authRepository = new AuthenticationRepository();
            ISchedulingSaveRepository saveRepository = new SchedulingSaveRepository();
            AuthenticationService authService = new AuthenticationService();
            string token = authService.RefreshToken(authRepository, "3194198375", "CC79880800");
            string res = petService.ProcesarCitas(petRepository, saveRepository, 50001, 172, token, "98fddusfh89udf-sf98df-9");
            Assert.IsNotNull(res);
        }
        [TestCategory("UnitTests")]
        [TestMethod]
        public void AsignarCita()
        {
            ISchedulingPetitionsRepository petRepository = new SchedulingPetitionsRepository();
            SchedulingPetitionsService petService = new SchedulingPetitionsService();
            IAuthenticationRepository authRepository = new AuthenticationRepository();
            AuthenticationService authService = new AuthenticationService();
            string token = authService.RefreshToken(authRepository, "3194198375", "CC79880800");
            Dictionary<string, string> values = new Dictionary<string, string>() {
                {"espacioCita","10782925"},
                {"tipoId","CC"},
                {"numId","79880800"},
                {"centroMedico","55983"},
                {"medico","2355"},
                {"especialidad","172"},
                {"telefono","000"},
                {"correo",""},
                {"celular","3134846707"},
                {"token",token}
            };
            string res = petService.AsignarCita(petRepository, values);
            Assert.IsNotNull(res);
           
        }




    }
}
