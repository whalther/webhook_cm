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
    
    public partial class tempCita
    {
        public int id { get; set; }
        public Nullable<int> numEspacioCita { get; set; }
        public string tipoIdBeneficiario { get; set; }
        public string numIdBeneficiario { get; set; }
        public Nullable<int> idMedico { get; set; }
        public Nullable<int> centroMedico { get; set; }
        public string especialidad { get; set; }
        public string telefono { get; set; }
        public string celular { get; set; }
        public string correo { get; set; }
        public string idConv { get; set; }
        public Nullable<int> estado { get; set; }
        public string result { get; set; }
        public string linkPago { get; set; }
        public string valorPagar { get; set; }
        public Nullable<int> idCita { get; set; }
        public Nullable<System.DateTime> fechaRegistro { get; set; }
    }
}
