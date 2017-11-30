using System.Collections.Generic;
using System.Net.Sockets;

namespace Host.Models
{
    public class SocketClient
    {
        public List<string> CustomerId { get; set; } = new List<string>();
        public Socket Socket { get; set; }
    }
}
