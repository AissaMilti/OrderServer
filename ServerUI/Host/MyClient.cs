using Host.Helpers;
using Host.Models;
using Newtonsoft.Json;
using ServerUI.Host.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;


namespace Host
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
                var socketClient = SocketHelper.Connections.FirstOrDefault(c => c.Socket == _client);
                
                var id = Counter.IdCounter.Count() + 1;
                Counter.IdCounter.Add(id);
                order.CustomerId = id;

                socketClient.CustomerId.Add(id);
                Application.Current.Dispatcher.Invoke(new Action(() => { OrderData.OrdersObsverableCollection.Add(order); }));
                
                _client.Send(Encoding.UTF8.GetBytes("Order Id: " + id.ToString()));

            }

            var socketClientToRemove = SocketHelper.Connections.FirstOrDefault(c => c.Socket == _client);
            SocketHelper.Connections.Remove(socketClientToRemove);
            _client.Close();
        }

    
    }
    
    


}
