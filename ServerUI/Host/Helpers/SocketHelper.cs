﻿using Host.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;

namespace Host.Helpers
{
    public static class SocketHelper
    {
        public static List<SocketClient> Connections = new List<SocketClient>();
        public static ObservableCollection<SocketClient> ConnectionsObsverableCollection = new ObservableCollection<SocketClient>();

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
