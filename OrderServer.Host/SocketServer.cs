using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using OrderServer.Host.Helpers;
using OrderServer.Host.Data;
using Newtonsoft.Json;
using OrderServer.Host.Models;

namespace OrderServer.Host
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
                var c = new MyClient(client);
                var dishesJson = JsonConvert.SerializeObject(Context.Dishes);
                var toBytes = Encoding.ASCII.GetBytes(dishesJson);
                client.Send(toBytes);
            }
        }

    }
}
