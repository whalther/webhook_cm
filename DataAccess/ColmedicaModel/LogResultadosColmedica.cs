//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess.ColmedicaModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class LogResultadosColmedica
    {
        public int id { get; set; }
        public string tipoTransaccion { get; set; }
        public bool exitoso { get; set; }
        public string detalle { get; set; }
        public System.DateTime fechaTransaccion { get; set; }
        public string sessionId { get; set; }
        public string celular { get; set; }
    }
}
