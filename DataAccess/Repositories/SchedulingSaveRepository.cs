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
                        tempBeneficiarios ben = new tempBeneficiarios()
                        {
                            ciudadResidencia = beneficiario.CiudadResidencia,
                            colectivo = beneficiario.Colectivo==""?0:Int32.Parse(beneficiario.Colectivo),
                            descripcionPlan = beneficiario.DescripcionPlan,
                            idUsuario = beneficiario.IdUsuario,
                            nombre = beneficiario.Nombre,
                            numeroContrato = beneficiario.NumeroContrato,
                            numeroIdentificacion = beneficiario.NumeroIdentificacion,
                            parentesco = beneficiario.Parentesco,
                            sexo = beneficiario.Sexo,
                            telefonoCelular = beneficiario.TelefonoCelular,
                            telefonoResidencia = beneficiario.TelefonoResidencia,
                            tipoIdentificacion = beneficiario.TipoIdentificacion,
                            idConv = idConv
                        };
                        contexto.tempBeneficiarios.Add(ben);
                    }

                    dynamic contratos = beneficiarios.Select(o =>  o.DescripcionPlan ).Distinct().ToList();
                    foreach (string contrato in contratos) {
                        tempContratos c = new tempContratos()
                        {
                          nombre = contrato,
                          idConv = idConv
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
                }
            }
        }
       
       public Boolean SaveCiudades(List<Ciudad> ciudades, string idConv)
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
                           idConv = idConv
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
                            idConv = idConv
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
                }
            }
        }
       public Boolean SaveCitasCiudad(List<CitaCiudad> citas, string idConv) 
        {
            using (ColmedicaContext contexto = new ColmedicaContext())
            {
                try
                {
                    foreach (CitaCiudad cita in citas)
                    {
                        tempCitas cit = new tempCitas()
                        {
                            dia = cita.Dia,
                            direccionCentroMedico = cita.DireccionCentroMedico,
                            fecha = cita.Fecha,
                            fechaHoraInicio = cita.FechaHoraInicio,
                            horaFin = cita.HoraFin,
                            horaInicio = cita.HoraInicio,
                            idCentroMedico = cita.IdCentroMedico,
                            idEspacioCita = cita.IdEspacioCita,
                            idMedico = cita.IdMedico,
                            nombreCentroMedico = cita.NombreCentroMedico,
                            nombreEspacioFisico = cita.NombreEspacioFisico,
                            nombreEspecialidad = cita.NombreEspecialidad,
                            nombreMedico = cita.NombreMedico,
                            idConv = idConv
                        };
                        contexto.tempCitas.Add(cit);
                    }
                    contexto.SaveChanges();
                    return true;
                }
                catch (Exception E)
                {
                    Trace.WriteLine(E.Message);
                    return false;
                }
            }
        }
    }
}
