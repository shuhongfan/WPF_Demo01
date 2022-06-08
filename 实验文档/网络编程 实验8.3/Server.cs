using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sendFile
{
    public partial class Server : Form
    {
        List<Socket> ClientSocketList = new List<Socket>();
        public Server()
        {
            InitializeComponent();
        }
      //启动：服务器端开启侦听
      private void btnStart_Click(object sender, EventArgs e)
      {
       
      }

        //接受客户端的连接请求
        public void AcceptClientConnect(object socket)
        {
            var serverSocket = socket as Socket;
            this.AppendTextToTxtLog("服务器段开始接收客户端的连接。");
            while (true)
            {
                var proxSocket = serverSocket.Accept();
                this.AppendTextToTxtLog(string.Format("客户端：{0}连接上了", proxSocket.RemoteEndPoint.ToString()));
                ClientSocketList.Add(proxSocket);
                //不停地接收当前连接的客户断发送来的消息
                ThreadPool.QueueUserWorkItem(new WaitCallback(ReceiveData), proxSocket);
            }
        }

        //接受客户端的消息
        public void ReceiveData(object socket)
        {
            var proxSocket = socket as Socket;
            byte[] data = new byte[1024 * 1024];
            while (true)
            {

                int len = 0;
                try
                {
                    len = proxSocket.Receive(data, 0, data.Length, SocketFlags.None);
                }
                catch (Exception ex)
                {
                    AppendTextToTxtLog(string.Format("客户端：{0} 非正常退出", proxSocket.RemoteEndPoint.ToString()));
                    ClientSocketList.Remove(proxSocket);
                    StopConnect(proxSocket);
                    return;
                }
                if (len <= 0)
                {
                    //客户端正常退出
                    AppendTextToTxtLog(string.Format("客户端：{0} 正常退出", proxSocket.RemoteEndPoint.ToString()));
                    ClientSocketList.Remove(proxSocket);
                    StopConnect(proxSocket);
                    return;//让方法结束，终结当前接收客户端数据的异步线程
                }
                //把接收到的数据放到文本框上去
                string str = Encoding.Default.GetString(data, 0, len);                
                AppendTextToTxtLog(string.Format("接收到客户端：{0}的消息是：{1}" ,proxSocket.RemoteEndPoint.ToString(), str));                
            }
        }
        private void StopConnect(Socket proxSocket)
        {
            try
            {
                if (proxSocket.Connected)
                {
                    proxSocket.Shutdown(SocketShutdown.Both);
                    proxSocket.Close(100);
                }
            }
            catch (Exception ex)
            {
                //不需处理断开连接时的异常
            }
        }
        //往日志的文本框上追加数据
        public void AppendTextToTxtLog(string txt)
        {
            if (txtLog.InvokeRequired)
            {

                txtLog.BeginInvoke(new Action<string>(s => {
                    this.txtLog.Text = string.Format("{0}\r\n{1}", txtLog.Text, s);
                }), txt);
            }
            else
            {
                this.txtLog.Text = string.Format("{0}\r\n{1}", txtLog.Text, txt);
            }

        }
        //发送消息
        private void btnSendMsg_Click(object sender, EventArgs e)
        {
           
        }
         //向客户端发送抖动命令
        private void btnFlash_Click(object sender, EventArgs e)
        {
           
        }
        //选择一个小文件发送到客户端
        private void btnSendFile_Click(object sender, EventArgs e)
        {

        }
    }
}
