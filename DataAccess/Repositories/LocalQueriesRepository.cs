using DataAccess.ColmedicaModel;
using Domain.DTOs;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
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
                                 IdContrato = tp.idContrato,
                                 Nombre = tp.nombre,
                                 IdConv = idConv
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
        public ResultBeneficiarios GetBeneficiariosContrato(string idContrato,string idConv)
        {
            ResultBeneficiarios resultado = new ResultBeneficiarios();
            try
            {
                using (ColmedicaContext contexto = new ColmedicaContext())
                {
                    resultado.Beneficiarios = (from tb in contexto.tempBeneficiarios
                                 where (tb.idConv == idConv && tb.numeroContrato==idContrato)
                                 select new BeneficiarioContratante() {
                                    CodigoCiudadResidencia = (int)tb.ciudadResidencia,
                                    Colectivo = tb.colectivo.ToString(),
                                    Plan = tb.descripcionPlan,
                                    IdUsuario =(int) tb.idUsuario,
                                    Nombres = tb.nombre,
                                    NumeroContrato = tb.numeroContrato.ToString(),
                                    NumeroIdentificacion = tb.numeroIdentificacion,
                                    Parentesco = tb.parentesco,
                                    Genero = tb.sexo,
                                    TelefonoCelular = tb.telefonoCelular,
                                    TelefonoResidencia = tb.telefonoResidencia,
                                    TipoIdentificacion = tb.tipoIdentificacion,
                                    CorreoElectronico = tb.correo
                                 }
                                 ).ToList();
                    resultado.Cotizante = (from tb in contexto.tempBeneficiarios
                                               where (tb.idConv == idConv && tb.parentesco=="Cotizante")
                                               select new BeneficiarioContratante()
                                               {
                                                   CodigoCiudadResidencia = (int)tb.ciudadResidencia,
                                                   Colectivo = tb.colectivo.ToString(),
                                                   Plan = tb.descripcionPlan,
                                                   IdUsuario = (int)tb.idUsuario,
                                                   Nombres = tb.nombre,
                                                   NumeroContrato = tb.numeroContrato.ToString(),
                                                   NumeroIdentificacion = tb.numeroIdentificacion,
                                                   Parentesco = tb.parentesco,
                                                   Genero = tb.sexo,
                                                   TelefonoCelular = tb.telefonoCelular,
                                                   TelefonoResidencia = tb.telefonoResidencia,
                                                   TipoIdentificacion = tb.tipoIdentificacion,
                                                   CorreoElectronico = tb.correo
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
                                     Cantidad = cius.cantidad
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
                                     Cantidad = tc.cantidad
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
                                     TipoEspecialidad = te.tipoEspecialidad,
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
                                     orderby tia.fechaHoraInicio ascending,tia.horaInicio descending
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
                                     ).Take(15).ToList();
                    }
                    else {
                        DateTime nFecha = DateTime.Parse(fecha);
                        resultado = (from tia in contexto.tempInfoAgendamiento
                                     orderby tia.fechaHoraInicio ascending, tia.horaInicio descending
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
                                     orderby tia.fechaHoraInicio ascending, tia.horaInicio descending
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
                                     ).Take(15).ToList();
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
                                 orderby tia.fechaHoraInicio ascending, tia.horaInicio descending
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
                                 ).Take(15).ToList();
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
                                             select new { tb.telefonoResidencia, tb.telefonoCelular, tb.correo }
                                             ).FirstOrDefault();
                        contexto.cleanTablesFlujo(0,idConv,"tempCita");
                        contexto.updateCita(idConv, "tipoIdBeneficiario", tipoDoc);
                        contexto.updateCita(idConv, "numIdBeneficiario", numDoc);
                        contexto.updateCita(idConv, "telefono", resultado.telefonoResidencia);
                        contexto.updateCita(idConv, "celular", resultado.telefonoCelular);
                        contexto.updateCita(idConv, "correo", resultado.correo);
                        contexto.updateCita(idConv, "estado", "0");
                        contexto.updateCita(idConv, "result", "");
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
                                 where (tct.idConv == idConv && tb.idConv == idConv)
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
                                     tia.nombreMedico,
                                     tia.dia,
                                     tct.numEspacioCita
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
                var s = contexto.Database.SqlQuery<string>("SELECT @@VERSION as V;").FirstOrDefault();
                var s2 = contexto.Database.SqlQuery<string>("SELECT top 1 * from tempAuth;").FirstOrDefault();
                var s3 = contexto.Database.SqlQuery<string>("SELECT top 1 * from tempBeneficiarios;").FirstOrDefault();
                var s4 = contexto.Database.SqlQuery<string>("SELECT top 1 * from tempCitasBeneficiario;").FirstOrDefault();
                var s5 = contexto.Database.SqlQuery<string>("SELECT top 1 * from tempCiudades;").FirstOrDefault();
                var s6 = contexto.Database.SqlQuery<string>("SELECT top 1 * from tempContratos;").FirstOrDefault();
                var s7 = contexto.Database.SqlQuery<string>("SELECT top 1 * from tempEspecialidades;").FirstOrDefault();
                var s8 = contexto.Database.SqlQuery<string>("SELECT top 1 * from tempInfoAgendamiento;").FirstOrDefault();

                    if (s.Length >= 0 || s2.Length >= 0 || s3.Length >= 0 || s4.Length >= 0 || s5.Length >= 0 || s6.Length >= 0 || s7.Length >= 0 || s8.Length >= 0)
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
        public List<CitaBeneficiario> GetCitasBeneficiario(string idConv)
        {
            List<CitaBeneficiario> resultado = null;
            try
            {

                using (ColmedicaContext contexto = new ColmedicaContext())
                {

                    resultado = (from tcb in contexto.tempCitasBeneficiario
                                 where tcb.idConv == idConv
                                 select new CitaBeneficiario()
                                 {
                                    AsignoCita = tcb.asignoCita,
                                    ConQR = tcb.conQr,
                                    Especialidad = tcb.especialidad,
                                    Estado = tcb.estado,
                                    EstadoQR = (int)tcb.estadoQr,
                                    Fecha = tcb.fecha.ToString(),
                                    FechaHoraCita = (DateTime)tcb.fechaHora,
                                    HoraFin = tcb.horaFin,
                                    HoraInicio = tcb.horaInicio,
                                    IdCentroMedico = (int)tcb.idCentroMedico,
                                    IdCita = (int)tcb.idCita,
                                    IdEstado = (int)tcb.idEstado,
                                    Nombre = tcb.nombre,
                                    NombreAgente = tcb.nombreAgente,
                                    NombreCentroMedico = tcb.nombreCentroMedico,
                                    NombreMedico = tcb.nombreMedico,
                                    NumeroIdentificacion = tcb.numeroIdentificacion,
                                    Observaciones = tcb.observaciones,
                                    TelefonoContacto = tcb.telefonoContacto,
                                    TipoIdentificacion = tcb.tipoIdentificacion,
                                    ValorPagar = tcb.valorPagar
                                 }
                                 ).ToList();
                }
                return resultado;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                return new List<CitaBeneficiario>();
                throw;
            }
        }
        public CitaBeneficiario GetInfoCitaBeneficiario(string idConv, int idCita)
        {
            CitaBeneficiario resultado = null;
            try
            {

                using (ColmedicaContext contexto = new ColmedicaContext())
                {

                    resultado = (from tcb in contexto.tempCitasBeneficiario
                                 where (tcb.idConv == idConv && tcb.idCita == idCita)
                                 select new CitaBeneficiario()
                                 {
                                     AsignoCita = tcb.asignoCita,
                                     ConQR = tcb.conQr,
                                     Especialidad = tcb.especialidad,
                                     Estado = tcb.estado,
                                     EstadoQR = (int)tcb.estadoQr,
                                     Fecha = tcb.fecha.ToString(),
                                     FechaHoraCita = (DateTime)tcb.fechaHora,
                                     HoraFin = tcb.horaFin,
                                     HoraInicio = tcb.horaInicio,
                                     IdCentroMedico = (int)tcb.idCentroMedico,
                                     IdCita = (int)tcb.idCita,
                                     IdEstado = (int)tcb.idEstado,
                                     Nombre = tcb.nombre,
                                     NombreAgente = tcb.nombreAgente,
                                     NombreCentroMedico = tcb.nombreCentroMedico,
                                     NombreMedico = tcb.nombreMedico,
                                     NumeroIdentificacion = tcb.numeroIdentificacion,
                                     Observaciones = tcb.observaciones,
                                     TelefonoContacto = tcb.telefonoContacto,
                                     TipoIdentificacion = tcb.tipoIdentificacion,
                                     ValorPagar = tcb.valorPagar
                                 }
                                 ).FirstOrDefault();
                }
                return resultado;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                return new CitaBeneficiario();
                throw;
            }
        }
        public Boolean UpdateCancelacionCita(string idConv, int idCita, string resultado) 
        {
            using (ColmedicaContext contexto = new ColmedicaContext())
            {
                try
                {

                    var up = (from cit in contexto.tempCitasBeneficiario
                              where (cit.idConv == idConv && cit.idCita==idCita)
                              select cit).FirstOrDefault();
                    up.resultCancelacion = resultado;
                    contexto.SaveChanges();
                    return true;
                }
                catch (Exception E)
                {
                    Trace.WriteLine(E.Message);
                    return false;
                    throw;
                }
            }
        }
        public string GetEstadoCancelacion(string idConv, int idCita)
        {
            using (ColmedicaContext contexto = new ColmedicaContext())
            {
                try
                {

                    string estado = (from cit in contexto.tempCitasBeneficiario
                              where (cit.idConv == idConv && cit.idCita == idCita)
                              select cit.resultCancelacion).FirstOrDefault();
                    return estado;
                }
                catch (Exception E)
                {
                    Trace.WriteLine(E.Message);
                    return "error_bd";
                    throw;
                }
            }
        }
       
    }
}