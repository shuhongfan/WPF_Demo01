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
            
        }
        public void ReceiveData(object socket)
        {
           
        }
        public void Flash()
        {
            
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
            
           
        }
    }
}
