using System;


namespace Domain.DTOs
{
    public class BeneficiarioContratante
    {
        public string CodigoCiudadResidencia { get; set; }
        public string Colectivo { get; set; }
        public string Plan { get; set; }
        public int Edad { get; set; }
        public DateTime FechaFinVigencia { get; set; }
        public DateTime FechaInicioVigencia { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int IdUsuario { get; set; }
        public string Mora { get; set; }
        public string Nombres { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string NumeroContrato { get; set; }
        public string NumeroIdentificacion { get; set; }
        public string Parentesco { get; set; }
        public string Genero { get; set; }
        public string TelefonoCelular { get; set; }
        public string TelefonoResidencia { get; set; }
        public string TipoIdentificacion { get; set; }
        public string CorreoElectronico { get; set; }

    }
}
