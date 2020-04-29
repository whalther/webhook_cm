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
        public async Task SaveAuthentication(string numDoc, string tipoDoc, string token, string idConv) {
            using (ColmedicaContext contexto = new ColmedicaContext())
            {
                try
                {
                    var rBorrar = (from ta in contexto.tempAuth
                                  where ta.idConv == idConv
                                  select ta).FirstOrDefault();
                    if (rBorrar != null) { contexto.tempAuth.Remove(rBorrar); }
                    tempAuth auth = new tempAuth()
                    {
                        idConv = idConv,
                        token = token,
                        numDoc = numDoc,
                        tipoDoc = tipoDoc
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
        public async Task SaveValidacionOtp(string resOtp, string idConv)
        {
            using (ColmedicaContext contexto = new ColmedicaContext())
            {
                try
                {

                    var up = (from ta in contexto.tempAuth
                              where ta.idConv == idConv
                              select ta).FirstOrDefault();
                    up.otp = resOtp; 
                    await Task.FromResult(contexto.SaveChanges()).ConfigureAwait(false);
                }
                catch (Exception E)
                {
                    Trace.WriteLine(E.Message);
                    throw;
                }
            }
        }
        public dynamic GetAuthentication(string idConv)
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
    }
}
