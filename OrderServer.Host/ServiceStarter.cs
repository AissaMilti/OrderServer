﻿using OrderServer.Host.Data;
using OrderServer.Host.Helpers;

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
