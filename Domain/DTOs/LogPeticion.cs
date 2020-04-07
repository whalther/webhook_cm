using System;
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
