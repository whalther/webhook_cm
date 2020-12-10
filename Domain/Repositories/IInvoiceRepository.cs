using System.Collections.Generic;

namespace Domain.Repositories
{
   public interface IInvoiceRepository
    {
       string GetContratosFactura(Dictionary<string, string> headers, string idConv);
       string SendEmailRetefuente(Dictionary<string, string> headers, Dictionary<string, string> param, string idConv);
    }
}
