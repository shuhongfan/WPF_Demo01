using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace clientSocket
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string host = "127.0.0.1";
            int port = 1000;
            Socket clientSocket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp
            );
            clientSocket.Connect(host, port);
            string sendMsg = "hello,server! This is client";
            byte[] data = Encoding.ASCII.GetBytes(sendMsg);
            clientSocket.Send(data, 0, data.Length, SocketFlags.None);
            Console.WriteLine("成功向服务器发送消息：{0}\n", sendMsg);
            Console.ReadLine();
        }
    }
}
