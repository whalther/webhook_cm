using System;
using System.Security.Cryptography;
using System.Text;
using CrossCutting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Domain.Utilities;
using System.Configuration;
using Domain.Repositories;
using CrossCutting.Repositories;
using Domain.Services;
using Domain.DTOs;
using Newtonsoft.Json;
using DataAccess.Repositories;

namespace CrossCuttingTests
{
    [TestClass]
    public class PeticionesTests
    {
       

        [TestMethod]
        public void ValidarUsuario()
        {
            ISchedulingPetitionsRepository petRepository = new SchedulingPetitionsRepository();
            SchedulingPetitionsService petService = new SchedulingPetitionsService();
            string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c3VhcmlvIjoiTkk5MDA4MDE0NTkiLCJjbGllbnRlIjoiQ0M3OTg4MDgwMCIsImV4cCI6MTU4MzM1NzcxMy4wfQ.U8yW-XYCp72oeVb-m55xoe3-QnJaZV4Y3KnzgH4bssU";
            Usuario res = petService.ValidarUsuario(petRepository, "CC79880800", token);
            Assert.IsNotNull(res);

        }

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
        [TestMethod]
        public void GetCiudadesUsuario()
        {
            ISchedulingPetitionsRepository petRepository = new SchedulingPetitionsRepository();
            SchedulingPetitionsService petService = new SchedulingPetitionsService();
            IAuthenticationRepository authRepository = new AuthenticationRepository();
            ISchedulingSaveRepository saveRepository = new SchedulingSaveRepository();
            AuthenticationService authService = new AuthenticationService();
            string token = authService.RefreshToken(authRepository, "3194198375", "CC79880800");
            List<Ciudad> res = petService.GetCiudades(petRepository,saveRepository, "79880800","CC", token, "98fddusfh89udf-sf98df-9");
            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void GetEspecialidadesCiudad()
        {
            ISchedulingPetitionsRepository petRepository = new SchedulingPetitionsRepository();
            SchedulingPetitionsService petService = new SchedulingPetitionsService();
            IAuthenticationRepository authRepository = new AuthenticationRepository();
            ISchedulingSaveRepository saveRepository = new SchedulingSaveRepository();
            AuthenticationService authService = new AuthenticationService();
            string token = authService.RefreshToken(authRepository, "3194198375", "CC79880800");
            List<Especialidad> res = petService.GetEspecialidadesCiudad(petRepository, saveRepository, "1123440768", "TI", 50001, token, "98fddusfh89udf-sf98df-9");
            Assert.IsNotNull(res);
        }
        [TestMethod]
        public void GetCitasCiudad()
        {
            ISchedulingPetitionsRepository petRepository = new SchedulingPetitionsRepository();
            SchedulingPetitionsService petService = new SchedulingPetitionsService();
            IAuthenticationRepository authRepository = new AuthenticationRepository();
            ISchedulingSaveRepository saveRepository = new SchedulingSaveRepository();
            AuthenticationService authService = new AuthenticationService();
            string token = authService.RefreshToken(authRepository, "3194198375", "CC79880800");
            List<CitaCiudad> res = petService.GetCitasCiudad(petRepository, saveRepository, 50001, 172, token, "98fddusfh89udf-sf98df-9");
            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void AsignarCita()
        {
            ISchedulingPetitionsRepository petRepository = new SchedulingPetitionsRepository();
            SchedulingPetitionsService petService = new SchedulingPetitionsService();
            IAuthenticationRepository authRepository = new AuthenticationRepository();
            ISchedulingSaveRepository saveRepository = new SchedulingSaveRepository();
            AuthenticationService authService = new AuthenticationService();
            string token = authService.RefreshToken(authRepository, "3194198375", "CC79880800");
            List<CitaCiudad> res = petService.GetCitasCiudad(petRepository, saveRepository, 50001, 172, token, "98fddusfh89udf-sf98df-9");
            Assert.IsNotNull(res);
        }




    }
}
