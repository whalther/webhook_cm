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
        public Usuario ValidarUsuario(ISchedulingPetitionsRepository petitionsRepository, string identificacion, string token) {
            Cifrador cf = new Cifrador();
            string iv = cf.generarIv();
            Dictionary<string, string> param = new Dictionary<string, string>() {
                {"numID",identificacion }
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
            string iv = cf.generarIv();
            
            Dictionary<string, string> param = new Dictionary<string, string>() {
                {"numID",identificacion }
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

       /* public List<Ciudad> GetCiudades(ISchedulingPetitionsRepository petitionsRepository, ISchedulingSaveRepository saveRepository, string identificacion, string tipoId, string token, string idConv)
        {
            Cifrador cf = new Cifrador();
            string iv = cf.generarIv();
            Dictionary<string, string> param= new Dictionary<string, string>() {
                {"tipIdeBeneficiario",tipoId },
                {"numIdeBeneficiario",identificacion}
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
                bool save = saveRepository.SaveCiudades(jsonResp, idConv, identificacion);
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
        */
        public string ProcesarEspecialidadesCiudad(ISchedulingPetitionsRepository petitionsRepository, ISchedulingSaveRepository saveRepository, string identificacion, string tipoId, int ciudad, string token, string idConv)
        {
            Cifrador cf = new Cifrador();
            string iv = cf.generarIv();
            Dictionary<string, string> param = new Dictionary<string, string>() {
                {"tipIdeBeneficiario",tipoId },
                {"numIdeBeneficiario",identificacion},
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
            string resultado = petitionsRepository.ProcesarEspecialidadesCiudad(hd, parametros);

            if (resultado != "error_parametros" && resultado != "error_desconocido" && resultado != "error_token")
            {
                string ivE = resultado.Substring(0, 16);
                string content = resultado.Substring(16);
                string textoPlano = cf.Descifrar(content, ivE);
                List<Especialidad> jsonResp = JsonConvert.DeserializeObject<List<Especialidad>>(textoPlano);
                bool save = saveRepository.SaveEspecialidadesCiudad(jsonResp, idConv);
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
        public string ProcesarCitas(ISchedulingPetitionsRepository petitionsRepository, ISchedulingSaveRepository saveRepository, int ciudad, int especialidad, string token, string idConv)
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
            string resultado = petitionsRepository.ProcesarCitas(hd, parametros);

            if (resultado != "error_parametros" && resultado != "error_desconocido" && resultado != "error_token")
            {
                string ivE = resultado.Substring(0, 16);
                string content = resultado.Substring(16);
                string textoPlano = cf.Descifrar(content, ivE);
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
                return textoPlano;
            }
            else
            {
                return resultado;
            }
        }

        public string ProcesarCiudadesBeneficiarioBd(ISchedulingPetitionsRepository petitionsRepository, ISchedulingSaveRepository saveRepository, string numDoc, string tipoDoc, string token, string idConv,int idUsuario) {
            Cifrador cf = new Cifrador();
            string iv = cf.generarIv();
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
            string resultado = petitionsRepository.GetCiudadesUsuario(hd, parametros);

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

    }
}
