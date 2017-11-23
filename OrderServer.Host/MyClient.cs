using OrderServer.Host.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OrderServer.Host
{
    class MyClient
    {
        private Socket _client;
        private Task _task;

        public MyClient(Socket client)
        {
            _client = client;
            _task = new Task(Listen);
            _task.Start();
        }

        public void Listen()
        {
            while (true)
            {
                var bytes = new byte[1024];
                var recvBytes = _client.Receive(bytes);
                if (recvBytes == 0)
                {
                    break;
                }
                
                var request = Encoding.UTF8.GetString(bytes, 0, recvBytes);

                //vad gör vi med requesten?
                
                //var date = DateTime.Now.ToLongDateString();
                //var dateBytes = Encoding.ASCII.GetBytes(date);
                //_client.Send(dateBytes);
            }
            SocketHelper.Connections.Remove(_client);
            _client.Close();
        }
    }
}
