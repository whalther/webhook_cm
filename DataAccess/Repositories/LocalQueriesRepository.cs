using DataAccess.ColmedicaModel;
using Domain.DTOs;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class LocalQueriesRepository: ILocalQueriesRepository 
    {
        public List<TipoDocumento> GetTiposDocumento() {
            List<TipoDocumento> resultado = null;
            try
            {
                using (ColmedicaContext contexto = new ColmedicaContext())
                {
                    resultado = (from tp in contexto.cmTipoDocumento
                                 where tp.estado == 1
                                 select new TipoDocumento()
                                 {
                                     Id = tp.id,
                                     TipoDoc = tp.tipo_doc,
                                     LabelDocumento = tp.doc_label,
                                     Orden = (int) tp.orden
                                 }
                                 ).ToList();
                }
                return resultado;
            }
            catch (Exception)
            {
                return new List<TipoDocumento>();
            }
        }
        public List<Contrato> GetContratos(string idConv)
        {
            List<Contrato> resultado = null;
            try
            {
                using (ColmedicaContext contexto = new ColmedicaContext())
                {
                    resultado = (from tp in contexto.tempContratos
                                 where tp.idConv == idConv
                                 select new Contrato() {
                                 idContrato =(int) tp.idContrato,
                                 nombre = tp.nombre,
                                 idConv = idConv
                                 }
                                 ).ToList();
                }
                return resultado;
            }
            catch (Exception)
            {
                return new List<Contrato>();
            }
        }
        public List<BeneficiarioContratante> GetBeneficiariosContrato(int contrato,string idConv)
        {
            List<BeneficiarioContratante> resultado = null;
            try
            {
                using (ColmedicaContext contexto = new ColmedicaContext())
                {
                    resultado = (from tb in contexto.tempBeneficiarios
                                 where (tb.idConv == idConv && tb.numeroContrato==contrato)
                                 select new BeneficiarioContratante() {
                                    CiudadResidencia = (int)tb.ciudadResidencia,
                                    Colectivo = tb.colectivo.ToString(),
                                    DescripcionPlan = tb.descripcionPlan,
                                    IdUsuario =(int) tb.idUsuario,
                                    Nombre = tb.nombre,
                                    NumeroContrato =(int) tb.numeroContrato,
                                    NumeroIdentificacion = tb.numeroIdentificacion,
                                    Parentesco = tb.parentesco,
                                    Sexo = tb.sexo,
                                    TelefonoCelular = tb.telefonoCelular,
                                    TelefonoResidencia = tb.telefonoResidencia,
                                    TipoIdentificacion = tb.tipoIdentificacion
                                 }
                                 ).ToList();
                }
                return resultado;
            }
            catch (Exception)
            {
                return new List<BeneficiarioContratante>();
            }
        }
        public Ciudad GetCiudadBeneficiario(int idUsuario, string idConv)
        {
            Ciudad resultado = null;
            try
            {

                using (ColmedicaContext contexto = new ColmedicaContext())
                {
                    resultado = (from tb in contexto.tempBeneficiarios
                                 join cius in contexto.tempCiudades on tb.ciudadResidencia equals cius.ciuCod
                                 where (tb.idConv == idConv && tb.idUsuario == idUsuario)
                                 select new Ciudad()
                                 {
                                     CiuCod = (int)cius.ciuCod,
                                     CiuNombre = cius.ciuNombre,
                                 }
                                 ).FirstOrDefault();
                }
                return resultado;
            }
            catch (Exception)
            {
                return new Ciudad();
            }
        }
        public List<Ciudad> GetCiudadesBeneficiario(int idUsuario, string idConv)
        {
            List<Ciudad> resultado = null;
            try
            {

                using (ColmedicaContext contexto = new ColmedicaContext())
                {
                    resultado = (from tc in contexto.tempCiudades
                                
                                 where (tc.idConv == idConv && tc.idUsuario == idUsuario)
                                 select new Ciudad()
                                 {
                                     CiuCod = (int)tc.ciuCod,
                                     CiuNombre = tc.ciuNombre,
                                 }
                                 ).ToList();
                }
                return resultado;
            }
            catch (Exception)
            {
                return new List<Ciudad>();
            }
        }
        public List<Especialidad> GetEspecialidades(string idConv)
        {
            List<Especialidad> resultado = null;
            try
            {

                using (ColmedicaContext contexto = new ColmedicaContext())
                {
                    
                    resultado = (from te in contexto.tempEspecialidades

                                 where te.idConv == idConv
                                 select new Especialidad()
                                 {
                                     Nombre = te.nombre,
                                     TipoEspecialidad =(int) te.tipoEspecialidad,
                                 }
                                 ).ToList();
                }
                return resultado;
            }
            catch (Exception)
            {
                return new List<Especialidad>();
            }
        }
        public List<Global> GetMedicos(string idConv)
        {
            List<Global> resultado = null;
            try
            {

                using (ColmedicaContext contexto = new ColmedicaContext())
                {

                    resultado = (from tia in contexto.tempInfoAgendamiento
                                 where tia.idConv == idConv
                                 select new Global()
                                 {
                                     Id = (int) tia.idMedico,
                                     Nombre = tia.nombreMedico
                                 }
                                 ).Distinct().ToList();
                }
                return resultado;
            }
            catch (Exception)
            {
                return new List<Global>();
            }
        }
        public List<Global> GetCentroMedicos(string idConv)
        {
            List<Global> resultado = null;
            try
            {

                using (ColmedicaContext contexto = new ColmedicaContext())
                {

                    resultado = (from tia in contexto.tempInfoAgendamiento
                                 where tia.idConv == idConv
                                 select new Global()
                                 {
                                     Id = (int)tia.idCentroMedico,
                                     Nombre = tia.nombreCentroMedico
                                 }
                                 ).Distinct().ToList();
                }
                return resultado;
            }
            catch (Exception)
            {
                return new List<Global>();
            }
        }
        public List<Cita> GetCitasProximas(string fecha, string idConv)
        {
            List<Cita> resultado = null;
            try
            {

                using (ColmedicaContext contexto = new ColmedicaContext())
                {
                    if (fecha == "")
                    {
                        resultado = (from tia in contexto.tempInfoAgendamiento
                                     orderby tia.fecha
                                     where tia.idConv == idConv
                                     select new Cita()
                                     {
                                         Dia = tia.dia,
                                         DireccionCentroMedico = tia.direccionCentroMedico,
                                         Fecha = (DateTime)tia.fecha,
                                         FechaHoraInicio = (DateTime)tia.fechaHoraInicio,
                                         HoraFin = tia.horaFin,
                                         HoraInicio = tia.horaInicio,
                                         IdCentroMedico = (int)tia.idCentroMedico,
                                         IdEspacioCita = (int)tia.idEspacioCita,
                                         IdMedico = (int)tia.idMedico,
                                         NombreCentroMedico = tia.nombreCentroMedico,
                                         NombreEspacioFisico = tia.nombreEspacioFisico,
                                         NombreEspecialidad = tia.nombreEspecialidad,
                                         NombreMedico = tia.nombreMedico
                                     }
                                     ).Take(10).ToList();
                    }
                    else {
                       DateTime nFecha = DateTime.Parse(fecha);
                        resultado = (from tia in contexto.tempInfoAgendamiento
                                     orderby tia.fecha
                                     where (tia.idConv == idConv && tia.fecha >= nFecha)
                                     select new Cita()
                                     {
                                         Dia = tia.dia,
                                         DireccionCentroMedico = tia.direccionCentroMedico,
                                         Fecha = (DateTime)tia.fecha,
                                         FechaHoraInicio = (DateTime)tia.fechaHoraInicio,
                                         HoraFin = tia.horaFin,
                                         HoraInicio = tia.horaInicio,
                                         IdCentroMedico = (int)tia.idCentroMedico,
                                         IdEspacioCita = (int)tia.idEspacioCita,
                                         IdMedico = (int)tia.idMedico,
                                         NombreCentroMedico = tia.nombreCentroMedico,
                                         NombreEspacioFisico = tia.nombreEspacioFisico,
                                         NombreEspecialidad = tia.nombreEspecialidad,
                                         NombreMedico = tia.nombreMedico
                                     }
                                     ).Take(10).ToList();
                    }
                    
                }
                return resultado;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                return new List<Cita>();
            }
        }
        public List<Cita> GetCitasMedico(int idMedico, string idConv)
        {
            List<Cita> resultado = null;
            try
            {

                using (ColmedicaContext contexto = new ColmedicaContext())
                {
                    
                        resultado = (from tia in contexto.tempInfoAgendamiento
                                     orderby tia.fecha
                                     where (tia.idConv == idConv && tia.idMedico==idMedico)
                                     select new Cita()
                                     {
                                         Dia = tia.dia,
                                         DireccionCentroMedico = tia.direccionCentroMedico,
                                         Fecha = (DateTime)tia.fecha,
                                         FechaHoraInicio = (DateTime)tia.fechaHoraInicio,
                                         HoraFin = tia.horaFin,
                                         HoraInicio = tia.horaInicio,
                                         IdCentroMedico = (int)tia.idCentroMedico,
                                         IdEspacioCita = (int)tia.idEspacioCita,
                                         IdMedico = (int)tia.idMedico,
                                         NombreCentroMedico = tia.nombreCentroMedico,
                                         NombreEspacioFisico = tia.nombreEspacioFisico,
                                         NombreEspecialidad = tia.nombreEspecialidad,
                                         NombreMedico = tia.nombreMedico
                                     }
                                     ).Take(10).ToList();
                }
                return resultado;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                return new List<Cita>();
            }
        }
        public List<Cita> GetCitasCentroMedico(int idCentroMedico, string idConv)
        {
            List<Cita> resultado = null;
            try
            {

                using (ColmedicaContext contexto = new ColmedicaContext())
                {

                    resultado = (from tia in contexto.tempInfoAgendamiento
                                 orderby tia.fecha
                                 where (tia.idConv == idConv && tia.idCentroMedico == idCentroMedico)
                                 select new Cita()
                                 {
                                     Dia = tia.dia,
                                     DireccionCentroMedico = tia.direccionCentroMedico,
                                     Fecha = (DateTime)tia.fecha,
                                     FechaHoraInicio = (DateTime)tia.fechaHoraInicio,
                                     HoraFin = tia.horaFin,
                                     HoraInicio = tia.horaInicio,
                                     IdCentroMedico = (int)tia.idCentroMedico,
                                     IdEspacioCita = (int)tia.idEspacioCita,
                                     IdMedico = (int)tia.idMedico,
                                     NombreCentroMedico = tia.nombreCentroMedico,
                                     NombreEspacioFisico = tia.nombreEspacioFisico,
                                     NombreEspecialidad = tia.nombreEspecialidad,
                                     NombreMedico = tia.nombreMedico
                                 }
                                 ).Take(10).ToList();
                }
                return resultado;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                return new List<Cita>();
            }
        }
    }
}
