using DataAccess.ColmedicaModel;
using Domain.DTOs;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                return new List<TipoDocumento>();
                throw;
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
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                return new List<Contrato>();
                throw;
            }
        }
        public ResultBeneficiarios GetBeneficiariosContrato(int idContrato,string idConv)
        {
            ResultBeneficiarios resultado = new ResultBeneficiarios();
            try
            {
                using (ColmedicaContext contexto = new ColmedicaContext())
                {
                    resultado.Beneficiarios = (from tb in contexto.tempBeneficiarios
                                 where (tb.idConv == idConv && tb.numeroContrato==idContrato)
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
                    resultado.Cotizante = (from tb in contexto.tempBeneficiarios
                                               where (tb.idConv == idConv && tb.parentesco=="Cotizante")
                                               select new BeneficiarioContratante()
                                               {
                                                   CiudadResidencia = (int)tb.ciudadResidencia,
                                                   Colectivo = tb.colectivo.ToString(),
                                                   DescripcionPlan = tb.descripcionPlan,
                                                   IdUsuario = (int)tb.idUsuario,
                                                   Nombre = tb.nombre,
                                                   NumeroContrato = (int)tb.numeroContrato,
                                                   NumeroIdentificacion = tb.numeroIdentificacion,
                                                   Parentesco = tb.parentesco,
                                                   Sexo = tb.sexo,
                                                   TelefonoCelular = tb.telefonoCelular,
                                                   TelefonoResidencia = tb.telefonoResidencia,
                                                   TipoIdentificacion = tb.tipoIdentificacion
                                               }
                                 ).FirstOrDefault();
                }
                return resultado;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                return new ResultBeneficiarios();
                throw;
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
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                return new Ciudad();
                throw;
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
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                return new List<Ciudad>();
                throw;
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
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                return new List<Especialidad>();
                throw;
            }
        }
        public List<GlobalResp> GetMedicos(string idConv)
        {
            List<GlobalResp> resultado = null;
            try
            {

                using (ColmedicaContext contexto = new ColmedicaContext())
                {

                    resultado = (from tia in contexto.tempInfoAgendamiento
                                 where tia.idConv == idConv
                                 select new GlobalResp()
                                 {
                                     Id = (int) tia.idMedico,
                                     Nombre = tia.nombreMedico
                                 }
                                 ).Distinct().ToList();
                }
                return resultado;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                return new List<GlobalResp>();
                throw;
            }
        }
        public List<GlobalResp> GetCentroMedicos(string idConv)
        {
            List<GlobalResp> resultado = null;
            try
            {

                using (ColmedicaContext contexto = new ColmedicaContext())
                {

                    resultado = (from tia in contexto.tempInfoAgendamiento
                                 where tia.idConv == idConv
                                 select new GlobalResp()
                                 {
                                     Id = (int)tia.idCentroMedico,
                                     Nombre = tia.nombreCentroMedico
                                 }
                                 ).Distinct().ToList();
                }
                return resultado;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                return new List<GlobalResp>();
                throw;
            }
        }
        public List<Cita> GetCitasProximas(string fecha, string idConv)
        {
            List<Cita> resultado = null;
            try
            {

                using (ColmedicaContext contexto = new ColmedicaContext())
                {
                    if (String.IsNullOrEmpty(fecha))
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
                throw;
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
                throw;
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
                throw;
            }
        }
        public Boolean UpdateCitaBd(string idConv,string campo, string valor) {
            try
            {
                using (ColmedicaContext contexto = new ColmedicaContext())
                {
                    if (campo == "documento")
                    {
                        string[] doc = valor.Split('*');
                        string tipoDoc = doc[0];
                        string numDoc = doc[1];
                        dynamic resultado = (from tb in contexto.tempBeneficiarios
                                             where (tb.idConv == idConv && tb.tipoIdentificacion == tipoDoc && tb.numeroIdentificacion == numDoc)
                                             select new { tb.telefonoResidencia, tb.telefonoCelular }
                                             ).FirstOrDefault();
                        contexto.updateCita(idConv, "tipoIdBeneficiario", tipoDoc);
                        contexto.updateCita(idConv, "numIdBeneficiario", numDoc);
                        contexto.updateCita(idConv, "telefono", resultado.telefonoResidencia);
                        contexto.updateCita(idConv, "celular", resultado.telefonoCelular);
                        contexto.updateCita(idConv, "estado", "0");
                    }
                    else if (campo == "cita")
                    {
                        int nValor = int.Parse(valor);
                        dynamic resultado = (from tia in contexto.tempInfoAgendamiento
                                             where (tia.idConv == idConv && tia.idEspacioCita == nValor)
                                             select new { tia.idMedico, tia.idCentroMedico }
                                                 ).FirstOrDefault();
                      
                        contexto.updateCita(idConv, "numEspacioCita", valor);
                        contexto.updateCita(idConv, "idMedico", resultado.idMedico.ToString());
                        contexto.updateCita(idConv, "centroMedico", resultado.idCentroMedico.ToString());
                    }
                    else if (campo == "agendamiento")
                    {
                        contexto.updateCita(idConv, "estado", "1");
                        contexto.updateCita(idConv, "result", valor);
                    }
                    else
                    {
                        contexto.updateCita(idConv, campo, valor);
                    }
                    
                }
                return true;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                return false;
                throw;
            }

        }
        public Boolean LimpiarTablas(string idConv)
        {
            try
            {
                using (ColmedicaContext contexto = new ColmedicaContext())
                {
                        contexto.cleanTablesConversation(idConv);
                }
                return true;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                return false;
                throw;
            }
        }
        public dynamic GetInfoCita(string idConv)
        {
            dynamic resultado;
            try
            {
                using (ColmedicaContext contexto = new ColmedicaContext())
                {
                    resultado = (from tct in contexto.tempCita
                                 join tb in contexto.tempBeneficiarios on new { x1 = tct.tipoIdBeneficiario, x2 = tct.numIdBeneficiario } equals new { x1 = tb.tipoIdentificacion, x2 = tb.numeroIdentificacion }
                                 join tciu in contexto.tempCiudades on tb.ciudadResidencia equals tciu.ciuCod
                                 join tia in contexto.tempInfoAgendamiento on tct.numEspacioCita equals tia.idEspacioCita
                                 where (tct.idConv == idConv)
                                 select new 
                                 {
                                     tb.nombre,
                                     tb.tipoIdentificacion,
                                     tb.numeroIdentificacion,
                                     tciu.ciuNombre,
                                     tia.direccionCentroMedico,
                                     tia.fecha,
                                     tia.nombreEspecialidad,
                                     tia.horaInicio,
                                     tia.horaFin,
                                     tia.nombreEspacioFisico,
                                     tia.nombreMedico
                                 }
                                 ).FirstOrDefault();
                   
                }
                return resultado;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                return new { };
                throw;
            }

        }
        public dynamic GetInfoAsignarCita(string idConv)
        {
            dynamic resultado;
            try
            {
                using (ColmedicaContext contexto = new ColmedicaContext())
                {
                    resultado = (from tct in contexto.tempCita
                                 where tct.idConv == idConv
                                 select tct
                                 ).FirstOrDefault();

                }
                return resultado;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                return new { };
                throw;
            }
        }
        public Boolean QueryDummy()
        {
            try
            {
                using (ColmedicaContext contexto = new ColmedicaContext())
                {
                   var s = contexto.Database.SqlQuery<string>("SELECT @@VERSION as V").FirstOrDefault();
                    if (s.Length >= 0)
                    {
                        return true;
                    }
                    else return false;
                }
               
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                return false;
                throw;
            }
        }
    }
}
