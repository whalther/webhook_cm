using Domain.DTOs;
using System.Collections.Generic;

namespace Domain.Repositories
{
   public interface IInvoiceSaveRepository
    {
        bool SaveContratosFactura(List<ContratoFactura> contratos, string idConv);
        List<ContratoFactura> GetContratosFacturaBd(string idConv);
        bool SaveResultEmailRetefuente(string tipoDoc, string numDoc, string correo, dynamic jsonResp, string idConv);
        string GetCorreoTitular(string idConv);
        dynamic GetResultEmailRetefuente(string idConv);
    }
}
