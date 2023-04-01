using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils.SignalR
{
    public class NegotiationResponse
    {
        public int NegotiateVersion { get; set; }
        public string ConnectionId { get; set; }
        public string ConnectionToken { get; set; }

    }
    
}
