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
    
    public partial class tempCitasBeneficiario
    {
        public int id { get; set; }
        public string asignoCita { get; set; }
        public Nullable<int> conQr { get; set; }
        public string especialidad { get; set; }
        public string estado { get; set; }
        public Nullable<int> estadoQr { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public Nullable<System.DateTime> fechaHora { get; set; }
        public string horaFin { get; set; }
        public string horaInicio { get; set; }
        public Nullable<int> idCentroMedico { get; set; }
        public Nullable<int> idCita { get; set; }
        public Nullable<int> idEstado { get; set; }
        public string nombre { get; set; }
        public string nombreAgente { get; set; }
        public string nombreCentroMedico { get; set; }
        public string nombreMedico { get; set; }
        public string numeroIdentificacion { get; set; }
        public string observaciones { get; set; }
        public string telefonoContacto { get; set; }
        public string tipoIdentificacion { get; set; }
        public string valorPagar { get; set; }
        public string idConv { get; set; }
        public string resultCancelacion { get; set; }
        public string linkPago { get; set; }
        public Nullable<System.DateTime> fechaRegistro { get; set; }
    }
}
