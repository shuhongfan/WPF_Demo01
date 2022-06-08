using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SocketFile
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Socket server = null;
        string picPath = "";

        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            //初始化
            var ip = AddressHelper.GetLocalhostIPv4Addresses().First();
            var port = AddressHelper.GetOneAvailablePortInLocalhost();
            txtIP.Text = ip.ToString();
            txtPORT.Text = port.ToString();

            IPEndPoint iep = new IPEndPoint(ip, port);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(iep);
            server.Listen(1);
            //启动线程开始接收数据
            DoWork();
            tbinfo.Text = iep.ToString() + "已启动监听";
        }

        private Task DoWork()
        {
            return Task.Run(() =>
            {
                //创建委托，更新图片控件
                Action<byte[]> action = buf =>
                {
                    image.Source = ByteToBitmapImage(buf);
                };
                while (true)
                {
                    var client = server.Accept();
                    var data = SocketHelper.ReceiveVarData(client);

                    this.Dispatcher.BeginInvoke(action, data);
                    client.Shutdown(SocketShutdown.Both);
                    client.Close();
                }
            });

        }
        //二进制转图片
        public BitmapImage ByteToBitmapImage(byte[] buf)
        {
            BitmapImage bmp = new BitmapImage();
            MemoryStream ms = new MemoryStream(buf);
            ms.Position = 0;
            bmp.BeginInit();
            bmp.StreamSource = ms;
            bmp.EndInit();
            return bmp;
        }



        private void selectImg_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                picPath = ofd.FileName;
                image.Source = new BitmapImage(new Uri(picPath));
            }

        }

        //取远程终端的IPEndPoint
        private IPEndPoint getRemIEP()
        {
            return new IPEndPoint(IPAddress.Parse(txtIP.Text), int.Parse(txtPORT.Text));
        }
        //发送
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var newSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            using (newSocket)
            {
                IPEndPoint remIep = getRemIEP();
                newSocket.Connect(remIep);
                var buf = File.ReadAllBytes(picPath);
                SocketHelper.SendVarData(newSocket, buf);
            }
        }

        private void senImg_Click(object sender, RoutedEventArgs e)
        {
            var newSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            using (newSocket)
            {
                IPEndPoint remIep = getRemIEP();
                newSocket.Connect(remIep);
                var buf = File.ReadAllBytes(picPath);
                SocketHelper.SendVarData(newSocket, buf);
            }

        }
    }
}
