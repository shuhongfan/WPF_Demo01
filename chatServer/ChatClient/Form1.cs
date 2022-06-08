using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatClient
{
    public partial class Form1 : Form
    {
        public Socket ClientSocket;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            // 1. 创建socket对象，客户端socket实例不需要固定的端口
            ClientSocket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp
            );

            // 2.连接服务端
            try
            {
                ClientSocket.Connect(IPAddress.Parse(txtIP.Text),int.Parse(txtPort.Text));
            }
            catch (Exception exception)
            {
                MessageBox.Show("连接失败，重新连接");
                return;
            }

            // 3. 发送消息，接收消息
            Thread thread = new Thread(
                new ParameterizedThreadStart(ReceiveData));
            thread.IsBackground = true;
            thread.Start(ClientSocket);
        }

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
                catch
                {
                    AppendTextToTxtLog(string.Format("服务器端：{0} 非正常退出", proxSocket.RemoteEndPoint.ToString()));
                    StopConnect();//关闭连接
                    return;
                }
                if (len <= 0)
                {
                    //客户端正常退出
                    AppendTextToTxtLog(string.Format("服务器端：{0} 正常退出", proxSocket.RemoteEndPoint.ToString()));
                    StopConnect();//关闭连接
                    return;//让方法结束，终结当前接收客户端数据的异步线程
                }
                //把接收到的数据放到文本框上去
                string str = Encoding.Default.GetString(data, 0, len);
                AppendTextToTxtLog(string.Format("接收到客户端：{0}的消息是：{1}", proxSocket.RemoteEndPoint.ToString(), str));

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

        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            if (ClientSocket.Connected)
            {
                byte[] data = Encoding.Default.GetBytes(txtMsg.Text);
                ClientSocket.Send(data, 0, data.Length, SocketFlags.None);
            }
        }
    }
}