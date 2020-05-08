using System;
namespace Domain.DTOs
{
    public class LogPeticion
    {
        public string Tipo { get; set; }
        public string Params { get; set; }
        public string Detalle { get; set; }
        public DateTime Fecha { get; set; }
        public string Metodo { get; set; }
        public string Origen { get; set; }
        public string IdConv { get; set; }
        public string Traza { get; set; }
    }
}
