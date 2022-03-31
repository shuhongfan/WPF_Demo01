using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WcfNetMeeting.MeetingServiceReference;
namespace WcfNetMeeting
{
    /// <summary>
    /// MeetingRoom.xaml 的交互逻辑
    /// </summary>
    public partial class MeetingRoom : Window
    {
        private MeetingServiceClient client = new MeetingServiceClient();
        public bool isInRoom { get; set; }
        public string UserName
        {
            get { return textBoxUserName.Text; }
            set { textBoxUserName.Text = value; }
        }

        public MeetingRoom()
        {
            InitializeComponent();
            this.Loaded += ClientWindow_Loaded;
            this.Closing += ClientWindow_Closing;
        }

        void ClientWindow_Loaded(object sender, RoutedEventArgs e)
        {
            btnExitRoom.IsEnabled = false;
        }

        void ClientWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            client.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if(MainWindow.users.Contains(UserName))
            {
                MessageBox.Show("已经有人用此姓名");
            }
            else
            {
                foreach (var v in MainWindow.users)
                {
                    AddUser(v);
                }
                btnEnterRoom.IsEnabled = false;
                client.EnterRoom(UserName);
            }
        }

        private void btnExitRoom_Click(object sender, RoutedEventArgs e)
        {
            client.ExitRoom(UserName);
            btnEnterRoom.IsEnabled = true;
            btnExitRoom.IsEnabled = false;
            isInRoom = false;
        }

        private void btnSay_Click(object sender, RoutedEventArgs e)
        {
            SendSay();
        }

        private void textBoxTalk_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) SendSay();
        }

        private void SendSay()
        {
            if (isInRoom)
            {
                client.Say(UserName, textBoxTalk.Text);
                textBoxTalk.Text = "";
            }
        }

        public void AddUser(string userName)
        {
            TextBlock t = new TextBlock();
            t.Text = userName;
            listBoxMember.Items.Add(t);
        }

        public void RemoveUser(string userName)
        {
            for (int i = 0; i < listBoxMember.Items.Count; i++)
            {
                TextBlock t = listBoxMember.Items[i] as TextBlock;
                if (t.Text == userName)
                {
                    listBoxMember.Items.Remove(t); break;
                }
            }
        }

        public void AddTalk(string format, params object[] args)
        {
            TextBlock t = new TextBlock();
            t.Text = string.Format(format, args);
            listBoxTalk.Items.Add(t);
        }
    }
}
