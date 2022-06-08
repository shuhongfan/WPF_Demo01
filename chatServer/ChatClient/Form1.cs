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
            // 1. ����socket���󣬿ͻ���socketʵ������Ҫ�̶��Ķ˿�
            ClientSocket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp
            );

            // 2.���ӷ����
            try
            {
                ClientSocket.Connect(IPAddress.Parse(txtIP.Text),int.Parse(txtPort.Text));
            }
            catch (Exception exception)
            {
                MessageBox.Show("����ʧ�ܣ���������");
                return;
            }

            // 3. ������Ϣ��������Ϣ
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
                    AppendTextToTxtLog(string.Format("�������ˣ�{0} �������˳�", proxSocket.RemoteEndPoint.ToString()));
                    StopConnect();//�ر�����
                    return;
                }
                if (len <= 0)
                {
                    //�ͻ��������˳�
                    AppendTextToTxtLog(string.Format("�������ˣ�{0} �����˳�", proxSocket.RemoteEndPoint.ToString()));
                    StopConnect();//�ر�����
                    return;//�÷����������սᵱǰ���տͻ������ݵ��첽�߳�
                }
                //�ѽ��յ������ݷŵ��ı�����ȥ
                string str = Encoding.Default.GetString(data, 0, len);
                AppendTextToTxtLog(string.Format("���յ��ͻ��ˣ�{0}����Ϣ�ǣ�{1}", proxSocket.RemoteEndPoint.ToString(), str));

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

        //����־�ı�����׷������
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