using System.Collections.Generic;
using System.Net.Sockets;

namespace Host.Models
{
    public class SocketClient
    {
        public List<int> CustomerId { get; set; } = new List<int>();
        public Socket Socket { get; set; }
    }
}
