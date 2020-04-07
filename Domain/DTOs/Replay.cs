using System.Collections.Generic;
namespace Domain.DTOs
{
   public class Replay
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string IdConv { get; set; }
        public string Token { get; set; }
        public Dictionary<string, object> Info { get; set; }
        public Replay()
        {
            Info = new Dictionary<string, object>();
        }
    }
}
