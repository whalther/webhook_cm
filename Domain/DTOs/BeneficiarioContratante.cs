using System;


namespace Domain.DTOs
{
    public class BeneficiarioContratante
    {
        public int CiudadResidencia { get; set; }
        public string Colectivo { get; set; }
        public string DescripcionPlan { get; set; }
        public int Edad { get; set; }
        public DateTime FechaFinVigencia { get; set; }
        public DateTime FechaInicioVigencia { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int IdUsuario { get; set; }
        public string Mora { get; set; }
        public string Nombre { get; set; }
        public int NumeroContrato { get; set; }
        public string NumeroIdentificacion { get; set; }
        public string Parentesco { get; set; }
        public string Sexo { get; set; }
        public string TelefonoCelular { get; set; }
        public string TelefonoResidencia { get; set; }
        public string TipoIdentificacion { get; set; }

    }
}
