using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Host.Helpers;
using Host.Data;
using Host.Models;
using Newtonsoft.Json;
using ServerUI;
using System.Windows.Threading;
using System;
using System.Windows;

namespace Host
{
    public class SocketServer
    {
        Socket socket = SocketHelper.GetSocket();

        public SocketServer()
        {
            var task = new Task(() => Listen());
            task.Start();            
        }

        private void Listen()
        {
            socket.Listen(1);
            while (true)
            {
                var client = socket.Accept();//blocking

                var socketClient = new SocketClient()
                {
                    Socket = client
                };
                
                SocketHelper.Connections.Add(socketClient);
                //add to obsverable collection
                Application.Current.Dispatcher.Invoke(() => { SocketHelper.ConnectionsObsverableCollection.Add(socketClient); });
                var c = new MyClient(client);
                var dishesJson = JsonConvert.SerializeObject(Context.Dishes);
                var toBytes = Encoding.ASCII.GetBytes(dishesJson);
                client.Send(toBytes);
            }
        }

    }
}
