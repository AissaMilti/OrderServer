using OrderServer.Host.Data;

namespace OrderServer.Host
{
    public class ServiceStarter
    {
        public void Start()
        {
            DataIntializer.Seed();
            var socketServer = new SocketServer();    

        }
    }
}
