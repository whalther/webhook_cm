using DataAccess.ColmedicaModel;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
   public class AuthenticationSaveRepository: IAuthenticationSaveRepository
    {
        public async Task SaveValidaCliente(string numDoc, string tipoDoc, string valida, string idConv) {
            using (ColmedicaContext contexto = new ColmedicaContext())
            {
                try
                {
                    tempAuth auth = new tempAuth()
                    {
                        idConv = idConv,
                        resultValida = valida,
                        token = "",
                        numDoc = numDoc,
                        tipoDoc = tipoDoc,
                        date = DateTime.Now
                    };
                   contexto.tempAuth.Add(auth);
                  await Task.FromResult( contexto.SaveChanges()).ConfigureAwait(false);
                }
                catch (Exception E)
                {
                    Trace.WriteLine(E.Message);
                    throw;
                }
            }

        }
        public async Task SaveValidacionOtp(string resOtp,string otp, string idConv)
        {
            using (ColmedicaContext contexto = new ColmedicaContext())
            {
                try
                {

                    var up = (from ta in contexto.tempAuth
                              where ta.idConv == idConv
                              select ta).FirstOrDefault();
                    up.otp = otp;
                    up.token = resOtp;
                    await Task.FromResult(contexto.SaveChanges()).ConfigureAwait(false);
                }
                catch (Exception E)
                {
                    Trace.WriteLine(E.Message);
                    throw;
                }
            }
        }
        public dynamic GetValidacion(string idConv)
        {
            using (ColmedicaContext contexto = new ColmedicaContext())
            {
                try
                {

                    dynamic up = (from ta in contexto.tempAuth
                              where ta.idConv == idConv
                              select ta).FirstOrDefault();
                    return up;
                }
                catch (Exception E)
                {
                    Trace.WriteLine(E.Message);
                    throw;
                }
            }
        }
        public Boolean DeleteAuthentication(string idConv)
        {
            using (ColmedicaContext contexto = new ColmedicaContext())
            {
                try
                {
                    var rBorrar = (from ta in contexto.tempAuth
                                   where ta.idConv == idConv
                                   select ta).FirstOrDefault();
                    if (rBorrar != null) { 
                        contexto.tempAuth.Remove(rBorrar);
                        contexto.SaveChanges();
                    }

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
