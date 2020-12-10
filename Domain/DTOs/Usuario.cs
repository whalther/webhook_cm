using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
   public class Usuario
    {
        public int? IdMensaje { get; set; }
        public string Mensaje { get; set; }
        public string TipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Sexo { get; set; }
        public string Ciudad { get; set; }
        public string DireccionCasa { get; set; }
        public string Celular { get; set; }

    }
}
