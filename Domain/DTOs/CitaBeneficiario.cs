using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class CitaBeneficiario
    {
        public string AsignoCita { get; set; }
        public int? ConQR { get; set; }
        public string Especialidad { get; set; }
        public string Estado { get; set; }
        public int EstadoQR { get; set; }
        public string Fecha { get; set; }
        public DateTime FechaHoraCita { get; set; }
        public string HoraFin { get; set; }
        public string HoraInicio { get; set; }
        public int IdCentroMedico { get; set; }
        public int IdCita { get; set; }
        public int IdEstado { get; set; }
        public string Nombre { get; set; }
        public string NombreAgente { get; set; }
        public string NombreCentroMedico { get; set; }
        public string NombreMedico { get; set; }
        public string NumeroIdentificacion { get; set; }
        public string Observaciones { get; set; }
        public string TelefonoContacto { get; set; }
        public string TipoIdentificacion { get; set; }
        public string ValorPagar { get; set; }
    }
}
