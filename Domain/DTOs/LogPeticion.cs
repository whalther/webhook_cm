using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class LogPeticion
    {
        public string Action { get; set; }
        public string Parameters { get; set; }
        public DateTime FechaHora { get; set; }
        
    }
}
