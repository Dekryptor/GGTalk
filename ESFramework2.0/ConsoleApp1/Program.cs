using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ESFramework;
using ESFramework.Server.Tcp;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            EsfTcpClient client = new EsfTcpClient("127.0.0.1", 8000);
            NetworkStream stream = client.GetNetworkStream();
            if (stream.CanWrite)
            {
                byte[] buffer = new byte[]{1};
                stream.Write(buffer, 0, 1);
            }
        }
    }
}
