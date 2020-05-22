using Domain.DTOs;
using Domain.Repositories;
using Domain.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Domain.Services
{
   public class SchedulingPetitionsService
    {
        public Usuario ValidarUsuario(ISchedulingPetitionsRepository petitionsRepository, string identificacion, string token, string idConv) {
            Cifrador cf = new Cifrador();
            string ivUsuario = cf.GenerarIv();
            Dictionary<string, string> param = new Dictionary<string, string>() {
                {"numID",identificacion }
            };
            string paramCifradoUsuario = cf.Cifrar(JsonConvert.SerializeObject(param), ivUsuario);
            Dictionary<string, string> hd = new Dictionary<string, string>() {
                {"token",token }
            };
            Dictionary<string, string> parametros = new Dictionary<string, string>() {
                {"mensaje",paramCifradoUsuario},
                {"iv",ivUsuario}
            };
            string resultado =  petitionsRepository.ValidarUsuario(hd, parametros, idConv);
            
            if (resultado != "error_parametros" && resultado != "error_desconocido" && resultado != "error_token")
            {
                string ivUsuarioPeticion = resultado.Substring(0, 16);
                string contentUsuario = resultado.Substring(16);
                string textoPlano = cf.Descifrar(contentUsuario, ivUsuarioPeticion);
                Usuario jsonResp = JsonConvert.DeserializeObject<Usuario>(textoPlano.ToString());
                return jsonResp;
            }
            else
            {
                Usuario u = new Usuario() { Mensaje = resultado };
                return u;
            }
        }
        public List<BeneficiarioContratante> GetBeneficiariosContratante(ISchedulingPetitionsRepository petitionsRepository,ISchedulingSaveRepository saveRepository,  string identificacion, string token, string idConv)
        {
            Cifrador cf = new Cifrador();
            string ivBens = cf.GenerarIv();
            
            Dictionary<string, string> paramBens = new Dictionary<string, string>() {
                {"numID",identificacion }
            };
            string paramCifradoBens = cf.Cifrar(JsonConvert.SerializeObject(paramBens), ivBens);
            Dictionary<string, string> hd = new Dictionary<string, string>() {
                {"token",token }
            };
            Dictionary<string, string> parametrosBens = new Dictionary<string, string>() {
                {"mensaje",paramCifradoBens},
                {"iv",ivBens}
            };
            string resultado = petitionsRepository.GetBeneficiariosContratante(hd, parametrosBens,idConv);

            if (resultado != "error_parametros" && resultado != "error_desconocido" && resultado != "error_token")
            {
                string ivBensPeticion = resultado.Substring(0, 16);
                string content = resultado.Substring(16);
                string textoPlano = cf.Descifrar(content, ivBensPeticion);
                List<BeneficiarioContratante> jsonResp = JsonConvert.DeserializeObject<List<BeneficiarioContratante>>(textoPlano);
                bool save = saveRepository.SaveBeneficiarios(jsonResp,idConv);
                if (save)
                {
                    return jsonResp;
                }
                else {
                    List<BeneficiarioContratante> u = new List<BeneficiarioContratante>() { new BeneficiarioContratante() { Parentesco = "error_bd" } };
                    return u;
                }
                
            }
            else
            {
                List<BeneficiarioContratante> u = new List<BeneficiarioContratante>() { new BeneficiarioContratante() { Parentesco = resultado } };
                return u;
            }
        }
        public List<Especialidad> ProcesarEspecialidadesCiudad(ISchedulingPetitionsRepository petitionsRepository, ISchedulingSaveRepository saveRepository, string identificacion, string tipoId, int ciudad, string token, string idConv)
        {
            Cifrador cf = new Cifrador();
            string ivEspe = cf.GenerarIv();
            Dictionary<string, string> param = new Dictionary<string, string>() {
                {"tipIdeBeneficiario",tipoId },
                {"numIdeBeneficiario",identificacion},
                {"ciudad", ciudad.ToString() }
            };
            string paramCifradoEspe = cf.Cifrar(JsonConvert.SerializeObject(param), ivEspe);
            Dictionary<string, string> hd = new Dictionary<string, string>() {
                {"token",token }
            };
            Dictionary<string, string> parametros = new Dictionary<string, string>() {
                {"mensaje",paramCifradoEspe},
                {"iv",ivEspe}
            };
            string resultado = petitionsRepository.ProcesarEspecialidadesCiudad(hd, parametros,idConv);

            if (resultado != "error_parametros" && resultado != "error_desconocido" && resultado != "error_token")
            {
                string ivEspePeticion = resultado.Substring(0, 16);
                string content = resultado.Substring(16);
                string textoPlano = cf.Descifrar(content, ivEspePeticion);
                List<Especialidad> jsonResp = JsonConvert.DeserializeObject<List<Especialidad>>(textoPlano);
                bool save = saveRepository.SaveEspecialidadesCiudad(jsonResp, idConv);
                if (save)
                {
                    return jsonResp;
                }
                else
                {
                    List <Especialidad> e = new List<Especialidad>() { new Especialidad() { Nombre = "error_bd" } };
                    return e;
                }
            }
            else
            {
                List<Especialidad> e = new List<Especialidad>() { new Especialidad { Nombre = resultado } };
                return e;
            }

        }
        public string ProcesarCitas(ISchedulingPetitionsRepository petitionsRepository, ISchedulingSaveRepository saveRepository, int ciudad, string especialidad, string token, string idConv)
        {
            Cifrador cf = new Cifrador();
            string ivCita = cf.GenerarIv();
            Dictionary<string, string> param = new Dictionary<string, string>() {
                {"ciudad",ciudad.ToString() },
                {"especialidad",especialidad.ToString()}
            };
            string paramCifradoCita = cf.Cifrar(JsonConvert.SerializeObject(param), ivCita);
            Dictionary<string, string> hd = new Dictionary<string, string>() {
                {"token",token }
            };
            Dictionary<string, string> parametros = new Dictionary<string, string>() {
                {"mensaje",paramCifradoCita},
                {"iv",ivCita}
            };
            string resultado = petitionsRepository.ProcesarCitas(hd, parametros,idConv);

            if (resultado != "error_parametros" && resultado != "error_desconocido" && resultado != "error_token")
            {
                string ivCitapeticion = resultado.Substring(0, 16);
                string content = resultado.Substring(16);
                string textoPlano = cf.Descifrar(content, ivCitapeticion);
                List<Cita> jsonResp = JsonConvert.DeserializeObject<List<Cita>>(textoPlano);
                
                bool save = saveRepository.SaveCitasCiudad(jsonResp, idConv);
                if (save)
                {
                    return "ok";
                }
                else
                {
                    return "error_bd";
                }
            }
            else
            {
                return resultado;
            }
        }

        public string AsignarCita(ISchedulingPetitionsRepository petitionsRepository, Dictionary<string,string> values,string idConv)
        {
            Cifrador cf = new Cifrador();
            string iv = cf.GenerarIv();
            Dictionary<string, string> param = new Dictionary<string, string>() {
                {"numEspacioCita", values["espacioCita"].ToString() },
                { "tipoIdBeneficiario",values["tipoId"].ToString()},
                {"numIdBeneficiario",values["numId"].ToString()},
                {"centroMedico",values["centroMedico"].ToString() }, 
                {"IdMedico", values["medico"].ToString()},
                {"especialidad",values["especialidad"].ToString()}, 
                {"telefono",values["telefono"].ToString() }, 
                {"correo",values["correo"].ToString() }, 
                {"celular",values["celular"].ToString()}
                    };
            string paramCifradoCita = cf.Cifrar(JsonConvert.SerializeObject(param), iv);
            Dictionary<string, string> hd = new Dictionary<string, string>() {
                {"token",values["token"].ToString() }
            };
            Dictionary<string, string> parametros = new Dictionary<string, string>() {
                {"mensaje",paramCifradoCita},
                {"iv",iv}
            };
            string resultado = petitionsRepository.AsignarCita(hd, parametros,idConv);

            if (resultado != "error_parametros" && resultado != "error_desconocido" && resultado != "error_token")
            {
                string ivE = resultado.Substring(0, 16);
                string content = resultado.Substring(16);
                string textoPlano = cf.Descifrar(content, ivE);
                return textoPlano;
            }
            else
            {
                return resultado;
            }
        }

        public string ProcesarCiudadesBeneficiarioBd(ISchedulingPetitionsRepository petitionsRepository, ISchedulingSaveRepository saveRepository, string numDoc, string tipoDoc, string token, string idConv,int idUsuario) {
            Cifrador cf = new Cifrador();
            string iv = cf.GenerarIv();
            Dictionary<string, string> param = new Dictionary<string, string>() {
                {"tipIdeBeneficiario",tipoDoc },
                {"numIdeBeneficiario",numDoc}
            };
            string paramCifrado = cf.Cifrar(JsonConvert.SerializeObject(param), iv);
            Dictionary<string, string> hd = new Dictionary<string, string>() {
                {"token",token }
            };
            Dictionary<string, string> parametros = new Dictionary<string, string>() {
                {"mensaje",paramCifrado},
                {"iv",iv}
            };
            string resultado = petitionsRepository.GetCiudadesUsuario(hd, parametros,idConv);

            if (resultado != "error_parametros" && resultado != "error_desconocido" && resultado != "error_token")
            {
                string ivE = resultado.Substring(0, 16);
                string content = resultado.Substring(16);
                string textoPlano = cf.Descifrar(content, ivE);
                List<Ciudad> jsonResp = JsonConvert.DeserializeObject<List<Ciudad>>(textoPlano);
                saveRepository.SaveCiudades(jsonResp, idConv, idUsuario);
                return "ok";
            }
            else
            {
                return resultado;
            }

        }
        public string ConsultarCitasBeneficiario(ISchedulingPetitionsRepository petitionsRepository, ISchedulingSaveRepository saveRepository,string idConv, string token, string idUsuario)
        {
            Cifrador cf = new Cifrador();
            string iv = cf.GenerarIv();
            Dictionary<string, string> param = new Dictionary<string, string>() {
                {"idUsuario",idUsuario }
            };
            string paramCifrado = cf.Cifrar(JsonConvert.SerializeObject(param), iv);
            Dictionary<string, string> hd = new Dictionary<string, string>() {
                {"token",token }
            };
            Dictionary<string, string> parametros = new Dictionary<string, string>() {
                {"mensaje",paramCifrado},
                {"iv",iv}
            };
            string resultado = petitionsRepository.ConsultarCitasBeneficiario(hd, parametros,idConv);

            if (resultado != "error_parametros" && resultado != "error_desconocido" && resultado != "error_token")
            {
                string ivE = resultado.Substring(0, 16);
                string content = resultado.Substring(16);
                string textoPlano = cf.Descifrar(content, ivE);
                List<CitaBeneficiario> jsonResp = JsonConvert.DeserializeObject<List<CitaBeneficiario>>(textoPlano);
                saveRepository.SaveCitasBeneficiario(jsonResp, idConv);
                return "ok";
            }
            else
            {
                return resultado;
            }

        }
        public string CancelarCitaBeneficiario(ISchedulingPetitionsRepository petitionsRepository, string token, string identificacionCotizante, string identificacionBeneficiario, string idCita,string idConv)
        {
            Cifrador cf = new Cifrador();
            string iv = cf.GenerarIv();
            Dictionary<string, string> param = new Dictionary<string, string>() {
                {"identificacionCotizante",identificacionCotizante },
                {"identificacionBeneficiario",identificacionBeneficiario },
                {"id_Cita",idCita }
            };
            string paramCifrado = cf.Cifrar(JsonConvert.SerializeObject(param), iv);
            Dictionary<string, string> hd = new Dictionary<string, string>() {
                {"token",token }
            };
            Dictionary<string, string> parametros = new Dictionary<string, string>() {
                {"mensaje",paramCifrado},
                {"iv",iv}
            };
            string resultado = petitionsRepository.CancelarCitaBeneficiario(hd, parametros,idConv);

            if (resultado != "error_parametros" && resultado != "error_desconocido" && resultado != "error_token")
            {
                string ivE = resultado.Substring(0, 16);
                string content = resultado.Substring(16);
                string textoPlano = cf.Descifrar(content, ivE);
                return textoPlano;
            }
            else
            {
                return resultado;
            }

        }
        public Boolean LimpiarTablasFlujo(ISchedulingSaveRepository repo, int proceso, string idConv, string tabla) 
        {
            return repo.LimpiarTablasFlujo(proceso,idConv,tabla);
        }
        public void DummyPetition(ISchedulingPetitionsRepository repo)
        {
             repo.DummyPetition();
        }
    }
}
