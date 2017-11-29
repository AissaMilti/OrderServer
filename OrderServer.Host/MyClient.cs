using Newtonsoft.Json;
using OrderServer.Host.Helpers;
using OrderServer.Host.Models;
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
                var order = JsonConvert.DeserializeObject<Order>(request);
                OrderData.Orders.Add(order);


                //var date = DateTime.Now.ToLongDateString();
                //var dateBytes = Encoding.ASCII.GetBytes(date);
                //_client.Send(dateBytes);
            }
            var socketClientToRemove = SocketHelper.Connections.FirstOrDefault(c => c.Socket == _client);
            SocketHelper.Connections.Remove(socketClientToRemove);
            _client.Close();
        }
    }
}
