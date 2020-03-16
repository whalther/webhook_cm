using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
   public class Cita
    {
        public string Dia { get; set; }
        public string DireccionCentroMedico { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public string HoraFin { get; set; }
        public string HoraInicio { get; set; }
        public int IdCentroMedico { get; set; }
        public int IdEspacioCita { get; set; }
        public int IdMedico { get; set; }
        public string NombreCentroMedico { get; set; }
        public string NombreEspacioFisico { get; set; }
        public string NombreEspecialidad { get; set; }
        public string NombreMedico { get; set; }
    }
}
