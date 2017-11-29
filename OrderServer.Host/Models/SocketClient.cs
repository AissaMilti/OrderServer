using System.Net.Sockets;

namespace OrderServer.Host.Models
{
    public class SocketClient
    {
        public string CustomerId { get; set; }
        public Socket Socket { get; set; }
    }
}
