using OrderServer.Host;
using OrderServer.Host.Helpers;
using System;

namespace Console.UI
{
    public class Program
    {
        static void Main(string[] args)
        {
            var serviceStarter = new ServiceStarter();
            serviceStarter.Start();
            System.Console.WriteLine("Starting server......");

            var ip = SocketHelper.GetLocalIPAddress();
            System.Console.WriteLine($"Server ip: {ip}");
            System.Console.WriteLine($"Server ip: 8080");
            System.Console.ReadLine();
        }
    }
}
