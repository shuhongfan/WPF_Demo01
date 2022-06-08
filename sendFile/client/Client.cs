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

namespace client
{
    public partial class client : Form
    {
        public Socket ClientSocket;
        public client()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false; //从不是创建控件的线程访问它
        }
		
        //客户端连接服务器端
        private void btnConn_Click(object sender, EventArgs e)
        {
            ClientSocket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp
            );

            try
            {
                ClientSocket.Connect(
                    IPAddress.Parse(txtIP.Text),
                    int.Parse(txtPort.Text)
                );
            }
            catch (Exception exception)
            {
                MessageBox.Show("连接失败,重新连接");
                return;
            }

            Thread thread = new Thread(
                new ParameterizedThreadStart(ReceiveData));
            thread.IsBackground = true;
            thread.Start(ClientSocket);
        }
        public void ReceiveData(object socket)
        {
            var proxySocket = socket as Socket;
            byte[] data = new byte[1024*1024];
            while (true)
            {
                int len = 0;
                try
                {
                    len = proxySocket.Receive(data, 0, data.Length, SocketFlags.None);
                }
                catch (Exception e)
                {
                    AppendTextToTxtLog(string.Format("服务端:{0} 非正常退出",proxySocket.RemoteEndPoint.ToString()));
                    StopConnect();
                    return;
                }

                if (len<=0)
                {
                    AppendTextToTxtLog(string.Format("服务端:{0}正常退出",proxySocket.RemoteEndPoint.ToString()));
                    StopConnect();
                    return;
                }

                int  type = data[0];
                if (type==1)
                {
                    string str = Encoding.Default.GetString(data,1,len-1);
                    AppendTextToTxtLog(string.Format("收到服务器端:{0}的消息是:{1}",proxySocket.RemoteEndPoint.ToString(),str));
                }
                else if (type == 2)
                {
                    Flash();
                } 
                else if (type==3)
                {
                    saveFile(data,len);
                }

            }
        }
        public void Flash()
        {
            Point curLocation = this.Location;
            Random r = new Random();
            for (int i = 0; i < 50; i++)
            {
                this.Location = new Point(
                    r.Next(curLocation.X - 10, curLocation.X + 10));
                r.Next(curLocation.Y - 10, curLocation.Y + 10);
                Thread.Sleep(30);
                this.Location = curLocation;
            }
        }

        public void saveFile(byte[] data, int length)
        {
            using (SaveFileDialog sFile = new SaveFileDialog())
            {
                sFile.Filter = "text file(*.txt)|*.txt|picture(*jpg)|*.jpg|word(*docx)|*docx|all file(*.*)|*.*";
                if (sFile.ShowDialog(this) != DialogResult.OK)
                    return;

                byte[] storedData = new byte[length - 1];
                Buffer.BlockCopy(data, 1, storedData, 0, length - 1);
                File.WriteAllBytes(sFile.FileName, storedData);

            }
        }
        //往日志文本框上追加数据
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
                this.txtLog.Text = string.Format("{0}\r\n{1}", txt, txtLog.Text);
            }

        }
        private void StopConnect()
        {
            try
            {
                if (ClientSocket.Connected)
                {
                    ClientSocket.Shutdown(SocketShutdown.Both);
                    ClientSocket.Close(100);
                }

            }
            catch (Exception ex)
            {

            }
        }
		
        //发送消息到服务器端
        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            if (ClientSocket.Connected)
            {
                byte[] data = Encoding.Default.GetBytes(txtMsg.Text);
                ClientSocket.Send(data, 0, data.Length, SocketFlags.None);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }
    }
}
