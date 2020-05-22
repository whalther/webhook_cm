using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webhook
{
    public class Utilities
    {
        public string GetNumero(string numero) 
        {
            if (numero.Substring(0, 2) == "57")
            {
                return numero.Substring(2);
            }
            else if (numero.Substring(0, 3) == "503")
            {
                return numero.Substring(3);
            }
            else {
                return numero;
            }
        }
    }
}