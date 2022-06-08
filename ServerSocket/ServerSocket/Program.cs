using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerSocket
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string host = "127.0.0.1";
            int port = 1000;
            IPAddress ip = IPAddress.Parse(host);
            IPEndPoint endPoint = new IPEndPoint(ip,port);
            Socket socket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp);
            socket.Bind(endPoint);
            socket.Listen(10);
            Console.WriteLine("建立连接：");
            Socket tempSocket = socket.Accept();
            string strReceive = "";
            byte[] receiveBytes = new Byte[1024];
            int iByte = tempSocket.Receive(receiveBytes, receiveBytes.Length, 0);
            strReceive += Encoding.ASCII.GetString(receiveBytes, 0, iByte);
            Console.WriteLine("接收来自客户端的信息为：" + strReceive);
            string strSend = "successful";
            byte[] sendBytes = Encoding.ASCII.GetBytes(strSend);
            tempSocket.Send(sendBytes, 0);
            tempSocket.Close();
            socket.Close();
            Console.ReadLine();
        }
    }
}
