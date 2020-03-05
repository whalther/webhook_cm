using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
   public class Replay
    {
        public string status { get; set; }
        public string message { get; set; }
        public string idConv { get; set; }
        public Dictionary<string, object> info { get; set; }
        public Replay()
        {
            info = new Dictionary<string, object>();
        }
    }
}
