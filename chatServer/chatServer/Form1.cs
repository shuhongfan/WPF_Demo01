using System.Net;
using System.Net.Sockets;
using System.Text;

namespace chatServer
{
    public partial class Form1 : Form
    {
        private List<Socket> ClientProxySocketList = new List<Socket>();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // 1. 创建Socket
            Socket socket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp
            );
            
            // 2.绑定端口
            socket.Bind(
                new IPEndPoint(
                    IPAddress.Parse(txtIP.Text),
                    int.Parse(txtPort.Text)
                    ));

            // 3.开启侦听
            socket.Listen(10);

            // 4.开始接收客户端的连接，该方法将一个任务项排列到线程池中，实现多线程
            ThreadPool.QueueUserWorkItem(
                new WaitCallback(AcceptClientConnect),
                socket
            );
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


        /**
         * 接收客户端的连接请求
         */
        public void AcceptClientConnect(Object socket)
        {
            Socket serverSocket = socket as Socket;
            this.AppendTextToTxtLog("服务器段开始接收客户端的连接。");
            while (true)
            {
                Socket proxySocket = serverSocket.Accept();
                this.AppendTextToTxtLog(string.Format("客户端：{0}连接上了", proxySocket.RemoteEndPoint.ToString()));
                ClientProxySocketList.Add(proxySocket);
                ThreadPool.QueueUserWorkItem(
                    new WaitCallback(ReceiveData), 
                    proxySocket);
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
                    ClientProxySocketList.Remove(proxSocket);
                    StopConnect(proxSocket);
                    return;
                }
                if (len <= 0)
                {
                    //客户端正常退出
                    AppendTextToTxtLog(string.Format("客户端：{0} 正常退出", proxSocket.RemoteEndPoint.ToString()));
                    ClientProxySocketList.Remove(proxSocket);
                    StopConnect(proxSocket);
                    return;//让方法结束，终结当前接收客户端数据的异步线程
                }
                //把接收到的数据放到文本框上去
                string str = Encoding.Default.GetString(data, 0, len);
                AppendTextToTxtLog(string.Format("接收到客户端：{0}的消息是：{1}", proxSocket.RemoteEndPoint.ToString(), str));
            }
        }

        /**
         *  //客户端正常或者非常退出时断开连接
         */
        private void StopConnect(Socket proxySocket)
        {
            try
            {
                if (proxySocket.Connected)
                {
                    proxySocket.Shutdown(SocketShutdown.Both);
                    proxySocket.Close(100);
                }
            }
            catch (Exception e)
            {
                

            }
        }

        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            foreach (Socket socket in ClientProxySocketList)
            {
                if (socket.Connected)
                {
                    byte[] bytes = Encoding.Default.GetBytes(txtMsg.Text);
                    socket.Send(bytes, 0, bytes.Length, SocketFlags.None);
                }
            }
        }
    }
}