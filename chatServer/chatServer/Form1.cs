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
            // 1. ����Socket
            Socket socket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp
            );
            
            // 2.�󶨶˿�
            socket.Bind(
                new IPEndPoint(
                    IPAddress.Parse(txtIP.Text),
                    int.Parse(txtPort.Text)
                    ));

            // 3.��������
            socket.Listen(10);

            // 4.��ʼ���տͻ��˵����ӣ��÷�����һ�����������е��̳߳��У�ʵ�ֶ��߳�
            ThreadPool.QueueUserWorkItem(
                new WaitCallback(AcceptClientConnect),
                socket
            );
        }

        //����־���ı�����׷������
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
         * ���տͻ��˵���������
         */
        public void AcceptClientConnect(Object socket)
        {
            Socket serverSocket = socket as Socket;
            this.AppendTextToTxtLog("�������ο�ʼ���տͻ��˵����ӡ�");
            while (true)
            {
                Socket proxySocket = serverSocket.Accept();
                this.AppendTextToTxtLog(string.Format("�ͻ��ˣ�{0}��������", proxySocket.RemoteEndPoint.ToString()));
                ClientProxySocketList.Add(proxySocket);
                ThreadPool.QueueUserWorkItem(
                    new WaitCallback(ReceiveData), 
                    proxySocket);
            }
        }

        //���ܿͻ��˵���Ϣ
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
                    AppendTextToTxtLog(string.Format("�ͻ��ˣ�{0} �������˳�", proxSocket.RemoteEndPoint.ToString()));
                    ClientProxySocketList.Remove(proxSocket);
                    StopConnect(proxSocket);
                    return;
                }
                if (len <= 0)
                {
                    //�ͻ��������˳�
                    AppendTextToTxtLog(string.Format("�ͻ��ˣ�{0} �����˳�", proxSocket.RemoteEndPoint.ToString()));
                    ClientProxySocketList.Remove(proxSocket);
                    StopConnect(proxSocket);
                    return;//�÷����������սᵱǰ���տͻ������ݵ��첽�߳�
                }
                //�ѽ��յ������ݷŵ��ı�����ȥ
                string str = Encoding.Default.GetString(data, 0, len);
                AppendTextToTxtLog(string.Format("���յ��ͻ��ˣ�{0}����Ϣ�ǣ�{1}", proxSocket.RemoteEndPoint.ToString(), str));
            }
        }

        /**
         *  //�ͻ����������߷ǳ��˳�ʱ�Ͽ�����
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