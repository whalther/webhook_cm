using System.Collections.Generic;

namespace Domain.Repositories
{
   public interface ISchedulingPetitionsRepository
    {
        string ValidarUsuario(Dictionary<string, string> headers, Dictionary<string, string> parametros);
        string GetBeneficiariosContratante(Dictionary<string, string> headers, Dictionary<string, string> parametros);
        string GetCiudadesUsuario(Dictionary<string, string> headers, Dictionary<string, string> parametros);
        string ProcesarEspecialidadesCiudad(Dictionary<string, string> headers, Dictionary<string, string> parametros);
        string ProcesarCitas(Dictionary<string, string> headers, Dictionary<string, string> parametros);
        string AsignarCita(Dictionary<string, string> headers, Dictionary<string, string> parametros);
        string GetCitasBeneficiario(Dictionary<string, string> headers, Dictionary<string, string> parametros);
        string CancelarCitaBeneficiario(Dictionary<string, string> headers, Dictionary<string, string> parametros);
        void DummyPetition();

    }
}
