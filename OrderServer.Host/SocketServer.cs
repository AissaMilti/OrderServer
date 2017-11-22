using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using OrderServer.Host.Helpers;

namespace OrderServer.Host
{
    public class SocketServer
    {
        Socket socket = SocketHelper.MySocket();

        public SocketServer()
        {
            var task = new Task(Listen);
            task.Start();
            socket.Close();
        }

        public void Listen()
        {
            socket.Listen(10);
            while (true)
            {
                var client = socket.Accept();//blocking
                var c = new MyClient(client);
                var welcome = "Välkommen";
                var toBytes = Encoding.ASCII.GetBytes(welcome);
                client.Send(toBytes);
            }
        }

    }
}
