using System.Collections.Generic;

namespace Domain.Repositories
{
   public interface ISchedulingPetitionsRepository
    {
        string ValidarUsuario(Dictionary<string, string> headers, Dictionary<string, string> parametros, string idConv);
        string GetBeneficiariosContratante(Dictionary<string, string> headers, Dictionary<string, string> parametros, string idConv);
        string GetCiudadesUsuario(Dictionary<string, string> headers, Dictionary<string, string> parametros,string idConv);
        string ProcesarEspecialidadesCiudad(Dictionary<string, string> headers, Dictionary<string, string> parametros,string idConv);
        string ProcesarCitas(Dictionary<string, string> headers, Dictionary<string, string> parametros,string idConv);
        string AsignarCita(Dictionary<string, string> headers, Dictionary<string, string> parametros, string idConv);
        string ConsultarCitasBeneficiario(Dictionary<string, string> headers, Dictionary<string, string> parametros, string idConv);
        string CancelarCitaBeneficiario(Dictionary<string, string> headers, Dictionary<string, string> parametros, string idConv);
        void DummyPetition();

    }
}
