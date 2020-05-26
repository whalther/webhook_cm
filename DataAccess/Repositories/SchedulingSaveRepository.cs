using DataAccess.ColmedicaModel;
using Domain.DTOs;
using Domain.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DataAccess.Repositories
{
    public class SchedulingSaveRepository : ISchedulingSaveRepository
    {
       public Boolean SaveBeneficiarios(List<BeneficiarioContratante> beneficiarios, string idConv) 
        {
            using (ColmedicaContext contexto = new ColmedicaContext())
            {
                try
                {
                    foreach (BeneficiarioContratante beneficiario in beneficiarios)
                    {
                        string nombre = beneficiario.Nombres.TrimEnd(' ') + " " + beneficiario.PrimerApellido.TrimEnd(' ') + " " + beneficiario.SegundoApellido.TrimEnd(' ');
                        tempBeneficiarios ben = new tempBeneficiarios()
                        {
                            ciudadResidencia = beneficiario.CodigoCiudadResidencia,
                            colectivo = beneficiario.Colectivo.Length==0?0:Int32.Parse(beneficiario.Colectivo),
                            descripcionPlan = beneficiario.Plan,
                            idUsuario = beneficiario.IdUsuario,
                            nombre = nombre,
                            numeroContrato = beneficiario.NumeroContrato,
                            numeroIdentificacion = beneficiario.NumeroIdentificacion,
                            parentesco = beneficiario.Parentesco,
                            sexo = beneficiario.Genero,
                            telefonoCelular = beneficiario.TelefonoCelular,
                            telefonoResidencia = beneficiario.TelefonoResidencia,
                            tipoIdentificacion = beneficiario.TipoIdentificacion,
                            correo = beneficiario.CorreoElectronico,
                            idConv = idConv,
                            fechaRegistro = DateTime.Now
                        };
                        contexto.tempBeneficiarios.Add(ben);
                    }

                    dynamic contratos = beneficiarios.Select(o =>  new {o.Plan,o.NumeroContrato }).Distinct().ToList();
                    foreach (dynamic contrato in contratos) {
                        tempContratos c = new tempContratos()
                        {
                          idContrato = contrato.NumeroContrato,
                          nombre = contrato.Plan,
                          idConv = idConv,
                          fechaRegistro = DateTime.Now
                        };

                        contexto.tempContratos.Add(c);
                    }
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
       
       public Boolean SaveCiudades(List<Ciudad> ciudades, string idConv, int idUsuario)
        {
            using (ColmedicaContext contexto = new ColmedicaContext())
            {
                try
                {
                    foreach (Ciudad ciudad in ciudades)
                    {
                        tempCiudades ciu = new tempCiudades()
                        {
                           cantidad = ciudad.Cantidad,
                           ciuCod = ciudad.CiuCod,
                           ciuNombre = ciudad.CiuNombre,
                           idConv = idConv,
                           idUsuario = idUsuario,
                           fechaRegistro = DateTime.Now
                        };
                        contexto.tempCiudades.Add(ciu);
                    }

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
       public Boolean SaveEspecialidadesCiudad(List<Especialidad> especialidades, string idConv)
        {
            using (ColmedicaContext contexto = new ColmedicaContext())
            {
                try
                {
                    foreach (Especialidad especialidad in especialidades)
                    {
                        tempEspecialidades esp = new tempEspecialidades()
                        {
                            nombre = especialidad.Nombre,
                            tipoEspecialidad = especialidad.TipoEspecialidad,
                            idConv = idConv,
                            fechaRegistro = DateTime.Now
                        };
                        contexto.tempEspecialidades.Add(esp);
                    }
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
       public Boolean SaveCitasCiudad(List<Cita> citas, string idConv) 
        {
            using (ColmedicaContext contexto = new ColmedicaContext())
            {
                try
                {
                    contexto.insertCitas(JsonConvert.SerializeObject(citas),idConv);
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
        public Boolean LimpiarTablasFlujo(int proceso, string idConv, string tabla) {
            try
            {
                using (ColmedicaContext contexto = new ColmedicaContext())
                {
                    contexto.cleanTablesFlujo(proceso,idConv,tabla);
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
        public Boolean SaveCitasBeneficiario(List<CitaBeneficiario> citas, string idConv)
        {
            using (ColmedicaContext contexto = new ColmedicaContext())
            {
                try
                {
                    foreach (CitaBeneficiario cita in citas)
                    {
                        DateTime nFecha = DateTime.Parse(cita.Fecha);
                        tempCitasBeneficiario cit = new tempCitasBeneficiario()
                        {
                            asignoCita = cita.AsignoCita,
                            conQr = cita.ConQR,
                            especialidad = cita.Especialidad,
                            estado = cita.Estado,
                            estadoQr = cita.EstadoQR,
                            fecha = nFecha,
                            fechaHora = cita.FechaHoraCita,
                            horaFin = cita.HoraFin,
                            horaInicio = cita.HoraInicio,
                            idCentroMedico = cita.IdCentroMedico,
                            idCita = cita.IdCita,
                            idEstado = cita.IdEstado,
                            nombre = cita.Nombre,
                            nombreAgente = cita.NombreAgente,
                            nombreCentroMedico = cita.NombreCentroMedico,
                            nombreMedico = cita.NombreMedico,
                            numeroIdentificacion = cita.NumeroIdentificacion,
                            observaciones = cita.Observaciones,
                            telefonoContacto = cita.TelefonoContacto,
                            tipoIdentificacion = cita.TipoIdentificacion,
                            valorPagar = cita.ValorPagar,
                            idConv = idConv,
                            fechaRegistro = DateTime.Now
                        };
                        contexto.tempCitasBeneficiario.Add(cit);
                    }
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
    }
}
