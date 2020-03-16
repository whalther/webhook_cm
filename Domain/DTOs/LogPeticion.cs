using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class LogPeticion
    {
        public string Ip { get; set; }
        public string ContenidoPeticion { get; set; }
        public DateTime FechaHora { get; set; }
        public string Path { get; set; }

    }
}
