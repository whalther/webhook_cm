using Domain.DTOs;
using Domain.Repositories;
using Domain.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
  public class InvoiceService
    {
        public List<ContratoFactura> GetContratosFactura(IInvoiceRepository invoiceRepository, IInvoiceSaveRepository saveRepository, string token, string idConv)
        {
            Cifrador cf = new Cifrador();
            
            Dictionary<string, string> hd = new Dictionary<string, string>() {
                {"token",token }
            };
           
            string resultado = invoiceRepository.GetContratosFactura(hd, idConv);

            if (resultado != "error_parametros" && resultado != "error_desconocido" && resultado != "error_token")
            {
                string ivBensPeticion = resultado.Substring(0, 16);
                string content = resultado.Substring(16);
                string textoPlano = cf.Descifrar(content, ivBensPeticion);
                List<ContratoFactura> jsonResp = JsonConvert.DeserializeObject<List<ContratoFactura>>(textoPlano);
                bool save = saveRepository.SaveContratosFactura(jsonResp, idConv);
                if (save)
                {
                    return jsonResp;
                }
                else
                {
                    List<ContratoFactura> c = new List<ContratoFactura>() { new ContratoFactura() { PlanContrato = "error_bd" } };
                    return c;
                }

            }
            else
            {
                List<ContratoFactura> c = new List<ContratoFactura>() { new ContratoFactura() { PlanContrato = resultado } };
                return c;
            }
        }
        public List<ContratoFactura> GetContratoFacturasBd(IInvoiceSaveRepository saveRepository, string idConv)
        {
            return saveRepository.GetContratosFacturaBd(idConv);
        }
        public dynamic SendEmailRetefuente(IInvoiceRepository invoiceRepository, IInvoiceSaveRepository saveRepository, string tipoDoc, string numDoc,string correo, string token, string idConv)
        {
            Cifrador cf = new Cifrador();
            string iv= cf.GenerarIv();

            Dictionary<string, string> param = new Dictionary<string, string>() {
                {"tipoIdentificacion",tipoDoc },
                {"numeroIdentificacion",numDoc },
                {"email",correo }
            };
            string paramCifrado = cf.Cifrar(JsonConvert.SerializeObject(param), iv);
            Dictionary<string, string> hd = new Dictionary<string, string>() {
                {"token",token }
            };
            Dictionary<string, string> parametrosBens = new Dictionary<string, string>() {
                {"mensaje",paramCifrado},
                {"iv",iv}
            };
            string resultado = invoiceRepository.SendEmailRetefuente(hd, parametrosBens, idConv);

            if (resultado != "error_parametros" && resultado != "error_desconocido" && resultado != "error_token")
            {
                string ivBensPeticion = resultado.Substring(0, 16);
                string content = resultado.Substring(16);
                string textoPlano = cf.Descifrar(content, ivBensPeticion);
                dynamic jsonResp = JsonConvert.DeserializeObject<ExpandoObject>(textoPlano);
                return jsonResp;
            }
            else
            {
                return resultado;
            }
        }
        public string GetCorreoTitular(IInvoiceSaveRepository saveRepository, string idConv)
        {
            return saveRepository.GetCorreoTitular(idConv);
        }
        public string GetResultEmailRetefuente(IInvoiceSaveRepository saveRepository, string idConv)
        {
            return saveRepository.GetResultEmailRetefuente(idConv);
        }
        public bool SaveResultEmailRetefuente(IInvoiceSaveRepository saveRepository,string tipoDoc, string numDoc, string correo, dynamic jsonResp, string idConv)
        {
        return saveRepository.SaveResultEmailRetefuente(tipoDoc, numDoc, correo, jsonResp, idConv);
        }
    }
}
