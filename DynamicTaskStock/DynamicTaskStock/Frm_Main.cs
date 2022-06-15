using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Net;

namespace DynamicTaskStock
{
    public partial class Frm_Main : Form
    {
        public Frm_Main()
        {
            InitializeComponent();
        }

        private Thread td; //����һ���߳�
        private TcpListener tcpListener; //ʵ������������
        string message = ""; //��¼���������

        private void StartListen()
        {
            tcpListener = new TcpListener(888); //����TcpListenerʵ��
            tcpListener.Start(); //��ʼ����
            while (true)
            {
                TcpClient tclient = tcpListener.AcceptTcpClient(); //������������
                NetworkStream nstream = tclient.GetStream(); //��ȡ������
                byte[] mbyte = new byte[1024]; //��������
                int i = nstream.Read(mbyte, 0, mbyte.Length); //��������д�뻺��
                message = Encoding.Default.GetString(mbyte, 0, i); //��ȡ���������
            }
        }

        private void Frm_Main_Load(object sender, EventArgs e)
        {
            td = new Thread(new ThreadStart(this.StartListen)); //ͨ���̵߳���StartListen����
            td.Start(); //��ʼ�����߳�
        }

        private void Frm_Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.tcpListener != null)
            {
                tcpListener.Stop(); //ֹͣ��������
            }

            if (td != null)
            {
                if (td.ThreadState == ThreadState.Running) //�ж��߳��Ƿ�����
                {
                    td.Abort(); //��ֹ�߳�
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                IPAddress[] ip = Dns.GetHostAddresses(Dns.GetHostName()); //��ȡ������ַ
                string message = "����ֵ�"; //���������
                TcpClient client = new TcpClient(txtAdd.Text, 888); //����TcpClientʵ��
                NetworkStream netstream = client.GetStream(); //����NetworkStreamʵ��
                StreamWriter wstream = new StreamWriter(netstream, Encoding.Default); //����StreamWriterʵ��
                wstream.Write(message); //����Ϣд����
                wstream.Flush();
                wstream.Close(); //�ر���
                client.Close(); //�ر�TcpClient����
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        bool k = true; //һ����ǣ����ڿ���ͼ������

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (message.Length > 0) //��������д���������
            {
                if (k) //kΪtrueʱ
                {
                    notifyIcon1.Icon = Properties.Resources._1; //����ͼ��Ϊ1
                    k = false; //��kΪfalse
                }
                else //kΪfalseʱ
                {
                    notifyIcon1.Icon = Properties.Resources._2; //ͼ��ͼ��Ϊ2��͸����ͼ��
                    k = true; //kΪtrue
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            message = ""; //��մ�������
            notifyIcon1.Icon = Properties.Resources._1; //��ʼ����ʾ��ͼ��
        }
    }
}