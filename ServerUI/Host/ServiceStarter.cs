using Host.Data;
using Host.Helpers;

namespace Host
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
