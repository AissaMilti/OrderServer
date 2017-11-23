using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace OrderServer.Host.Helpers
{
    public static class SocketHelper
    {
        public static List<Socket> Connections = new List<Socket>();

        private static EndPoint GetEndPoint()
        {
            var address = IPAddress.Parse(GetLocalIPAddress());

            var endPoint = new IPEndPoint(address, 8080);

            return endPoint;
        }

        public static string GetLocalIPAddress()
        {
            string ipAddress = null;

            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddress = ip.ToString();
                }
            }

            return ipAddress;
        }

        public static Socket GetSocket()
        {
            var socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(GetEndPoint());
            
            return socket;
        }
    }
}
