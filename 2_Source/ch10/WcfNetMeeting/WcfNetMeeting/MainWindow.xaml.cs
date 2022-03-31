using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

namespace WcfNetMeeting
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, IMeetingService
    {
        public static List<string> users = new List<string>();
        private static MeetingRoom[] rooms;
        private ServiceHost host;
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.Closing += MainWindow_Closing;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                host = new ServiceHost(typeof(MainWindow));
                host.Open();
                textBlock1.Text = "监听的Uri为：";
                foreach (var v in host.Description.Endpoints)
                {
                    textBlock1.Text += v.ListenUri.ToString() + "\n";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Application.Current.Shutdown();
            }
        }

        void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            host.Close();
            Application.Current.Shutdown();
        }

        private void btn_click(object sender, RoutedEventArgs e)
        {
            //说明：此例子仅为学习用，实际应用中一个应用程序应该只有一个客户端
            rooms = new MeetingRoom[3];
            rooms[0] = CreateRoom("客户端1", "张三易", -420, -150);
            rooms[1] = CreateRoom("客户端2", "李四耳", 360, -150);
            rooms[2] = CreateRoom("客户端3", "王五伞", -20, 160);
            foreach (var room in rooms) { room.Show(); }
        }

        private MeetingRoom CreateRoom(string head, string name, double dx, double dy)
        {
            MeetingRoom w = new MeetingRoom()
            {
                Title = head,  Left = this.Left + dx, Top = this.Top + dy,
                Owner = this,
            };
            w.UserName = name;
            return w;
        }

        #region 实现IMeetingService接口

        public void EnterRoom(string userName)
        {
            users.Add(userName);
            foreach (var v in rooms)
            {
                if (userName == v.UserName)
                {
                    v.isInRoom = true;
                    v.btnExitRoom.IsEnabled = true;
                }
                if (v.isInRoom) v.AddUser(userName);
            }
        }

        public void Say(string userName, string message)
        {
            foreach (var v in rooms)
            {
                if (v.isInRoom) v.AddTalk("{0}：{1}", userName, message);
            }
        }

        public void ExitRoom(string userName)
        {
            users.Remove(userName);
            foreach (var v in rooms)
            {
                if (userName != v.UserName) v.RemoveUser(userName);
            }
        }
        #endregion
    }
}
