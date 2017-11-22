using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using OrderServer.Host.Helpers;

namespace Console.UI
{
    public class Program
    {
        static void Main(string[] args)
        {
            SocketHelper.MyEndPoint();
        }
    }
}
