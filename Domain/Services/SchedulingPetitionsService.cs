using Domain.DTOs;
using Domain.Repositories;
using Domain.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
   public class SchedulingPetitionsService
    {
        public Usuario ValidarUsuario(ISchedulingPetitionsRepository petitionsRepository, string idetificacion, string token) {
            Cifrador cf = new Cifrador();
            string iv = cf.generarIv();
            Dictionary<string, string> param = new Dictionary<string, string>() {
                {"numID",idetificacion }
            };
            string paramCifrado = cf.Cifrar(JsonConvert.SerializeObject(param),iv);
            Dictionary<string, string> hd = new Dictionary<string, string>() {
                {"token",token }
            };
            Dictionary<string, string> parametros = new Dictionary<string, string>() {
                {"mensaje",paramCifrado},
                {"iv",iv}
            };
            string resultado =  petitionsRepository.ValidarUsuario(hd, parametros);
            
            if (resultado != "error_parametros" && resultado != "error_desconocido" && resultado != "error_token")
            {
                string ivE = resultado.Substring(0, 16);
                string content = resultado.Substring(16);
                string textoPlano = cf.Descifrar(content, ivE);
                Usuario jsonResp = JsonConvert.DeserializeObject<Usuario>(textoPlano);
                return jsonResp;
            }
            else
            {
                Usuario u = new Usuario() { Mensaje = resultado };
                return u;
            }
        }
        public List<BeneficiarioContratante> GetBeneficiariosContratante(ISchedulingPetitionsRepository petitionsRepository,ISchedulingSaveRepository saveRepository,  string idetificacion, string token, string idConv)
        {
            Cifrador cf = new Cifrador();
            string iv = cf.generarIv();
            
            Dictionary<string, string> param = new Dictionary<string, string>() {
                {"numID",idetificacion }
            };
            string paramCifrado = cf.Cifrar(JsonConvert.SerializeObject(param), iv);
            Dictionary<string, string> hd = new Dictionary<string, string>() {
                {"token",token }
            };
            Dictionary<string, string> parametros = new Dictionary<string, string>() {
                {"mensaje",paramCifrado},
                {"iv",iv}
            };
            string resultado = petitionsRepository.GetBeneficiariosContratante(hd, parametros);

            if (resultado != "error_parametros" && resultado != "error_desconocido" && resultado != "error_token")
            {
                string ivE = resultado.Substring(0, 16);
                string content = resultado.Substring(16);
                string textoPlano = cf.Descifrar(content, ivE);
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

        public List<Ciudad> GetCiudades(ISchedulingPetitionsRepository petitionsRepository, ISchedulingSaveRepository saveRepository, string idetificacion, string tipoId, string token, string idConv)
        {
            Cifrador cf = new Cifrador();
            string iv = cf.generarIv();
            Dictionary<string, string> param= new Dictionary<string, string>() {
                {"tipIdeBeneficiario",tipoId },
                {"numIdeBeneficiario",idetificacion}
            };
            string paramCifrado = cf.Cifrar(JsonConvert.SerializeObject(param), iv);
            Dictionary<string, string> hd = new Dictionary<string, string>() {
                {"token",token }
            };
            Dictionary<string, string> parametros = new Dictionary<string, string>() {
                {"mensaje",paramCifrado},
                {"iv",iv}
            };
            string resultado = petitionsRepository.GetCiudadesUsuario(hd, parametros);

            if (resultado != "error_parametros" && resultado != "error_desconocido" && resultado != "error_token")
            {
                string ivE = resultado.Substring(0, 16);
                string content = resultado.Substring(16);
                string textoPlano = cf.Descifrar(content, ivE);
                List<Ciudad> jsonResp = JsonConvert.DeserializeObject<List<Ciudad>>(textoPlano);
                bool save = saveRepository.SaveCiudades(jsonResp, idConv);
                if (save)
                {
                    return jsonResp;
                }
                else
                {
                    List<Ciudad> u = new List<Ciudad>() { new Ciudad() { CiuNombre = "error_bd" } };
                    return u;
                }
            }
            else
            {
                List<Ciudad> c = new List<Ciudad>() { new Ciudad { CiuNombre = resultado } };
                return c;
            }

        }

        public List<Especialidad> GetEspecialidadesCiudad(ISchedulingPetitionsRepository petitionsRepository, ISchedulingSaveRepository saveRepository, string idetificacion, string tipoId, int ciudad, string token, string idConv)
        {
            Cifrador cf = new Cifrador();
            string iv = cf.generarIv();
            Dictionary<string, string> param = new Dictionary<string, string>() {
                {"tipIdeBeneficiario",tipoId },
                {"numIdeBeneficiario",idetificacion},
                {"ciudad", ciudad.ToString() }
            };
            string paramCifrado = cf.Cifrar(JsonConvert.SerializeObject(param), iv);
            Dictionary<string, string> hd = new Dictionary<string, string>() {
                {"token",token }
            };
            Dictionary<string, string> parametros = new Dictionary<string, string>() {
                {"mensaje",paramCifrado},
                {"iv",iv}
            };
            string resultado = petitionsRepository.GetEspecialidadesCiudad(hd, parametros);

            if (resultado != "error_parametros" && resultado != "error_desconocido" && resultado != "error_token")
            {
                string ivE = resultado.Substring(0, 16);
                string content = resultado.Substring(16);
                string textoPlano = cf.Descifrar(content, ivE);
                List<Especialidad> jsonResp = JsonConvert.DeserializeObject<List<Especialidad>>(textoPlano);
                bool save = saveRepository.SaveEspecialidadesCiudad(jsonResp, idConv);
                if (save)
                {
                    return jsonResp;
                }
                else
                {
                    List<Especialidad> e = new List<Especialidad>() { new Especialidad() { Nombre = "error_bd" } };
                    return e;
                }
            }
            else
            {
                List<Especialidad> e = new List<Especialidad>() { new Especialidad { Nombre = resultado } };
                return e;
            }

        }
        public List<CitaCiudad> GetCitasCiudad(ISchedulingPetitionsRepository petitionsRepository, ISchedulingSaveRepository saveRepository, int ciudad, int especialidad, string token, string idConv)
        {
            Cifrador cf = new Cifrador();
            string iv = cf.generarIv();
            Dictionary<string, string> param = new Dictionary<string, string>() {
                {"ciudad",ciudad.ToString() },
                {"especialidad",especialidad.ToString()}
            };
            string paramCifrado = cf.Cifrar(JsonConvert.SerializeObject(param), iv);
            Dictionary<string, string> hd = new Dictionary<string, string>() {
                {"token",token }
            };
            Dictionary<string, string> parametros = new Dictionary<string, string>() {
                {"mensaje",paramCifrado},
                {"iv",iv}
            };
            string resultado = petitionsRepository.GetCitasCiudad(hd, parametros);

            if (resultado != "error_parametros" && resultado != "error_desconocido" && resultado != "error_token")
            {
                string ivE = resultado.Substring(0, 16);
                string content = resultado.Substring(16);
                string textoPlano = cf.Descifrar(content, ivE);
                List<CitaCiudad> jsonResp = JsonConvert.DeserializeObject<List<CitaCiudad>>(textoPlano);
                
                bool save = saveRepository.SaveCitasCiudad(jsonResp, idConv);
                if (save)
                {
                    return jsonResp;
                }
                else
                {
                    List<CitaCiudad> c = new List<CitaCiudad>() { new CitaCiudad() {  Dia = "error_bd" } };
                    return c;
                }
            }
            else
            {
                List<CitaCiudad> c = new List<CitaCiudad>() { new CitaCiudad { Dia = resultado } };
                return c;
            }
        }

        public string AsignarCita(ISchedulingPetitionsRepository petitionsRepository, int espacioCita, string tipoId, string numId, int centroMedico,int medico, int especialidad, string telefono, string correo, string celular, string token)
        {
            Cifrador cf = new Cifrador();
            string iv = cf.generarIv();
            Dictionary<string, string> param = new Dictionary<string, string>() {
                {"numEspacioCita", espacioCita.ToString() },
                { "tipoIdBeneficiario",tipoId},
                {"numIdBeneficiario",numId},
                {"centroMedico",centroMedico.ToString() }, 
                {"IdMedico", medico.ToString()},
                {"especialidad",especialidad.ToString() }, 
                {"telefono",telefono }, 
                {"correo",correo }, 
                {"celular",celular}
                    };
            string paramCifrado = cf.Cifrar(JsonConvert.SerializeObject(param), iv);
            Dictionary<string, string> hd = new Dictionary<string, string>() {
                {"token",token }
            };
            Dictionary<string, string> parametros = new Dictionary<string, string>() {
                {"mensaje",paramCifrado},
                {"iv",iv}
            };
            string resultado = petitionsRepository.AsignarCita(hd, parametros);

            if (resultado != "error_parametros" && resultado != "error_desconocido" && resultado != "error_token")
            {
                string ivE = resultado.Substring(0, 16);
                string content = resultado.Substring(16);
                string textoPlano = cf.Descifrar(content, ivE);
               // Usuario jsonResp = JsonConvert.DeserializeObject<Usuario>(textoPlano);
                return textoPlano;
            }
            else
            {
                return resultado;
            }
        }

    }
}
