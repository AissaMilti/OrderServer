using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OrderServer.Host.Helpers
{
    public static class SocketHelper
    {
        private static EndPoint GetEndPoint()
        {
            var address = IPAddress.Parse(GetLocalIPAddress());

            var endPoint = new IPEndPoint(address, 8080);

            return endPoint;
        }

        private static string GetLocalIPAddress()
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

        public static Socket MySocket()
        {
            var socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(GetEndPoint());

            return socket;
        }
    }
}
