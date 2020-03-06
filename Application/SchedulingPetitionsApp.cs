using CrossCutting.Repositories;
using DataAccess.Repositories;
using Domain.DTOs;
using Domain.Repositories;
using Domain.Services;
using System.Collections.Generic;

namespace Application
{
    public class SchedulingPetitionsApp
    {
        public Resultado ValidarUsuario(string identificacion,string numeroCelular, string token)
        {
            ISchedulingPetitionsRepository petitionsRepository = new SchedulingPetitionsRepository();
            IAuthenticationRepository authRepository = new AuthenticationRepository();
            SchedulingPetitionsService serv = new SchedulingPetitionsService();
            AuthenticationService authService = new AuthenticationService();
            Usuario us = serv.ValidarUsuario(petitionsRepository,identificacion, token);
            Resultado res = new Resultado();
            if (us.Mensaje == "error_token")
            {
                string nToken = authService.RefreshToken(authRepository, numeroCelular, identificacion);
                if (nToken != "error_credenciales" & nToken != "error_parametros" & nToken != "error_desconocido")
                {
                    Usuario nUs = serv.ValidarUsuario(petitionsRepository, identificacion, nToken);
                    res.Result = nUs;
                }
                else {
                    Usuario nUs = new Usuario() {Mensaje = nToken };
                    res.Result = nUs;
                }
                res.Token = nToken;
            }
            else 
            {
                res.Result = us;
                res.Token = token;
            }
            return res;
        }
        public Resultado GetBeneficiariosContratante(string identificacion, string token, string idConv, string numeroCelular)
        {
            ISchedulingPetitionsRepository petitionsRepository = new SchedulingPetitionsRepository();
            IAuthenticationRepository authRepository = new AuthenticationRepository();
            ISchedulingSaveRepository saveRepository = new SchedulingSaveRepository();
            SchedulingPetitionsService serv = new SchedulingPetitionsService();
            AuthenticationService authService = new AuthenticationService();
            List<BeneficiarioContratante> bens = serv.GetBeneficiariosContratante(petitionsRepository, saveRepository,identificacion, token,idConv);
            Resultado res = new Resultado();
            if (bens[0].Parentesco == "error_token")
            {
                string nToken = authService.RefreshToken(authRepository, numeroCelular, identificacion);
                res.Result = serv.GetBeneficiariosContratante(petitionsRepository, saveRepository, identificacion, nToken, idConv);
                res.Token = nToken;
            }
            else
            {
                res.Result = bens;
                res.Token = token;
            }
            return res;
        }
        public Resultado GetCiudades(string identificacion, string tipoId, string token, string idConv, string numeroCelular)
        {
            ISchedulingPetitionsRepository petitionsRepository = new SchedulingPetitionsRepository();
            IAuthenticationRepository authRepository = new AuthenticationRepository();
            ISchedulingSaveRepository saveRepository = new SchedulingSaveRepository();
            SchedulingPetitionsService serv = new SchedulingPetitionsService();
            AuthenticationService authService = new AuthenticationService();
            List<Ciudad> cius = serv.GetCiudades(petitionsRepository, saveRepository, identificacion,tipoId, token, idConv);
            Resultado res = new Resultado();
            if (cius[0].CiuNombre == "error_token")
            {
                string nToken = authService.RefreshToken(authRepository, numeroCelular, identificacion);
                res.Result = serv.GetCiudades(petitionsRepository, saveRepository, identificacion, tipoId, nToken, idConv);
                res.Token = nToken;
            }
            else
            {
                res.Result = cius;
                res.Token = token;
            }
            return res;
        }
        public Resultado GetEspecialidadesCiudad(string identificacion, string tipoId, int ciudad, string token, string idConv, string numeroCelular)
        {
            ISchedulingPetitionsRepository petitionsRepository = new SchedulingPetitionsRepository();
            IAuthenticationRepository authRepository = new AuthenticationRepository();
            ISchedulingSaveRepository saveRepository = new SchedulingSaveRepository();
            SchedulingPetitionsService serv = new SchedulingPetitionsService();
            AuthenticationService authService = new AuthenticationService();
            List<Especialidad> espe = serv.GetEspecialidadesCiudad(petitionsRepository, saveRepository, identificacion, tipoId,ciudad, token, idConv);
            Resultado res = new Resultado();
            if (espe[0].Nombre == "error_token")
            {
                string nToken = authService.RefreshToken(authRepository, numeroCelular, identificacion);
                res.Result = serv.GetEspecialidadesCiudad(petitionsRepository, saveRepository, identificacion, tipoId, ciudad, nToken, idConv);
                res.Token = nToken;
            }
            else
            {
                res.Result = espe;
                res.Token = token;
            }
            return res;
        }
        public Resultado GetCitasCiudad(int ciudad, int especialidad, string token, string idConv,string numeroCelular,string identificacion) 
        {
            ISchedulingPetitionsRepository petitionsRepository = new SchedulingPetitionsRepository();
            IAuthenticationRepository authRepository = new AuthenticationRepository();
            ISchedulingSaveRepository saveRepository = new SchedulingSaveRepository();
            SchedulingPetitionsService serv = new SchedulingPetitionsService();
            AuthenticationService authService = new AuthenticationService();
            List<CitaCiudad> cc = serv.GetCitasCiudad(petitionsRepository, saveRepository, ciudad, especialidad, token, idConv);
            Resultado res = new Resultado();
            if (cc[0].Dia == "error_token")
            {
                string nToken = authService.RefreshToken(authRepository, numeroCelular, identificacion);
                res.Result = serv.GetCitasCiudad(petitionsRepository, saveRepository, ciudad, especialidad, nToken, idConv);
                res.Token = nToken;
            }
            else
            {
                res.Result = cc;
                res.Token = token;
            }
            return res;
        }
        public Resultado AsignarCita(int espacioCita, string tipoId, string numId, int centroMedico, int medico, int especialidad, string telefono, string correo, string celular, string token, string numeroCelularConv,string identificacion)
        {
            ISchedulingPetitionsRepository petitionsRepository = new SchedulingPetitionsRepository();
            IAuthenticationRepository authRepository = new AuthenticationRepository();
            SchedulingPetitionsService serv = new SchedulingPetitionsService();
            AuthenticationService authService = new AuthenticationService();
            string result = serv.AsignarCita(petitionsRepository, espacioCita, tipoId, numId, centroMedico, medico, especialidad, telefono, correo, celular, token);
            Resultado res = new Resultado();
            if (result == "error_token")
            {
                string nToken = authService.RefreshToken(authRepository, numeroCelularConv, identificacion);
                res.Result = serv.AsignarCita(petitionsRepository, espacioCita, tipoId, numId, centroMedico, medico, especialidad, telefono, correo, celular, nToken);
                res.Token = nToken;
            }
            else
            {
                res.Result = result;
                res.Token = token;
            }
            return res;
        }
    }
}