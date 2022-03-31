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
using System.Windows.Shapes;
using NetTcpClient.RndGameServiceReference;

namespace NetTcpClient
{
    /// <summary>
    /// ClientWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ClientWindow : Window, IRndGameServiceCallback
    {
        public string UserName
        {
            get { return textBoxUserName.Text; }
            set { textBoxUserName.Text = value; }
        }
        private const int max = 16; //棋盘行列最大值
        private int maxTables;

        private int tableIndex = -1; //房间中所坐的游戏桌号，-1表示未进入房间
        private int tableSide = -1;  //游戏桌座位号,0:黑方，1:白方,-1:未入座
        private bool isGameStart = false;  //是否已开始游戏
        private Border[,] rooms;  //每个房间一桌
        private int[,] grid = new int[max, max]; //保存颜色，用于消点时进行判断
        private bool isFromServer;
        private Image[,] images = new Image[max, max];
        private RndGameServiceClient client;

        public ClientWindow()
        {
            InitializeComponent();
            this.Closing += MainWindow_Closing;
            ChangeRoomsVisible(false);
            ChangePlayerRoomVisible(false);
        }

        void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ChangeState(btnLogin, true, btnLogout, false);
            if (client != null)
            {
                if (tableIndex != -1) //如果在房间内，要求先返回大厅
                {
                    MessageBox.Show("请先返回大厅，然后再退出");
                    e.Cancel = true;
                }
                else
                {
                    client.Logout(UserName); //从大厅退出
                }
            }
        }

        private void ChangeRoomsVisible(bool visible)
        {
            if (visible == false)
            {
                gridRooms.Visibility = System.Windows.Visibility.Collapsed;
                gridMessage.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                gridRooms.Visibility = System.Windows.Visibility.Visible;
                gridMessage.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void ChangePlayerRoomVisible(bool visible)
        {
            if (visible == false)
            {
                gridRoom.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                gridRoom.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void AddMessage(string str)
        {
            TextBlock t = new TextBlock();
            t.Text = str;
            t.Foreground = Brushes.Blue;
            listBoxMessage.Items.Add(t);
        }

        private void AddColorMessage(string str, SolidColorBrush color)
        {
            TextBlock t = new TextBlock();
            t.Text = str;
            t.Foreground = color;
            listBoxMessage.Items.Add(t);
        }

        private static void ChangeState(Button btnStart, bool isStart, Button btnStop, bool isStop)
        {
            btnStart.IsEnabled = isStart;
            btnStop.IsEnabled = isStop;
        }

        //单击登录按钮引发的事件
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            UserName = textBoxUserName.Text;
            InstanceContext context = new InstanceContext(this);
            client = new RndGameServiceReference.RndGameServiceClient(context);

            serviceTextBlock.Text = "服务端地址：" + client.Endpoint.ListenUri.ToString();
            try
            {
                client.Login(textBoxUserName.Text);
                ChangeState(btnLogin, false, btnLogout, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("与服务端连接失败：" + ex.Message);
                return;
            }
            this.Cursor = Cursors.Arrow;
        }

        //单击退出按钮引发的事件
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); //在窗口的Closing事件中处理退出操作
        }

        //在某个座位坐下时引发的事件
        private void RoomSide_MouseDown(object sender, MouseButtonEventArgs e)
        {
            btnLogout.IsEnabled = false;
            Border border = e.Source as Border;
            if (border != null)
            {
                string s = border.Tag.ToString();
                tableIndex = int.Parse(s[0].ToString());
                tableSide = int.Parse(s[1].ToString());
                client.SitDown(UserName, tableIndex, tableSide);
            }
        }

        //单击发送按钮引发的事件
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            client.Talk(tableIndex, UserName, textBoxTalk.Text);
        }

        //在对话文本框中按回车键时引发的事件
        private void textBoxTalk_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                client.Talk(tableIndex, UserName, textBoxTalk.Text);
            }
        }

        /// <summary>当游戏难度级别发生变化时引发的事件</summary>
        private void radioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (client == null) return;
            if (isFromServer == true) return;
            RadioButton r = (RadioButton)e.Source;
            client.SetLevel(tableIndex, int.Parse(r.Name[2].ToString()));
        }

        //单击开始按钮引发的事件
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            client.Start(UserName, tableIndex, tableSide);
            btnStart.IsEnabled = false;
        }

        //单击返回大厅按钮引发的事件
        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            client.GetUp(tableIndex, tableSide);
            if (tableSide == 0) textBlockBlackUserName.Text = "";
            else textBlockWhiteUserName.Text = "";
            this.tableIndex = -1;
            this.tableSide = -1;
            ChangeState(btnStart, true, btnLogout, true);
            listBoxMessage.Items.Clear();
            ChangePlayerRoomVisible(false);
        }

        //在棋盘上单击鼠标左键时引发的事件
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isGameStart == false) return;

            Point point = e.GetPosition(canvas1);
            int x = (int)(point.X + 10) / 20;
            int y = (int)(point.Y + 10) / 20;
            if (!(x < 1 || x > max || y < 1 || y > max))
            {
                if (grid[x - 1, y - 1] != -1)
                {
                    client.UnsetDot(tableIndex, x - 1, y - 1);
                }
            }
        }

        #region 实现IRndGameServiceCallback接口
        /// <summary>有用户登录</summary>
        public void ShowLogin(string loginUserName, int maxTables)
        {
            if (loginUserName == UserName)
            {
                ChangeRoomsVisible(true);
                this.maxTables = maxTables;
                this.CreateTables();
            }
            AddMessage(loginUserName + "进入大厅。");
        }

        /// <summary>其他用户退出</summary>
        public void ShowLogout(string userName)
        {
            AddMessage(userName + "退出大厅。");
        }

        /// <summary>用户入座</summary>
        public void ShowSitDown(string userName, int side)
        {
            if (side == tableSide)
            {
                isGameStart = false;
                btnLogout.IsEnabled = false;
                btnStart.IsEnabled = true;
                listBoxRooms.IsEnabled = false;//返回大厅前不允许再坐到另一个位置
                textBlockRoomNumber.Text = "桌号：" + (tableIndex + 1);
                ChangePlayerRoomVisible(true);
            }
            if (side == 0)
            {
                textBlockBlackUserName.Text = "黑方：" + userName;
                AddMessage(string.Format("{0}在房间{1}黑方入座。", userName, tableIndex + 1));
            }
            else
            {
                textBlockWhiteUserName.Text = "白方：" + userName;
                AddMessage(string.Format("{0}在房间{1}白方入座。", userName, tableIndex + 1));
            }
        }

        /// <summary>用户离座</summary>
        public void ShowGetUp(string userName, int side)
        {
            if (side == tableSide)
            {
                isGameStart = false;
                btnLogout.IsEnabled = true;
                listBoxRooms.IsEnabled = true;//返回大厅后允许再坐到另一个位置
                AddMessage(UserName + "返回大厅。");
                this.tableIndex = -1;
                this.tableSide = -1;
                ChangePlayerRoomVisible(false);
            }
            else
            {
                if (isGameStart)
                {
                    AddMessage(userName + "逃回大厅，游戏终止。");
                    isGameStart = false;
                    btnStart.IsEnabled = true;
                }
                else
                {
                    AddMessage(userName + "返回大厅。");
                }
                if (side == 0) textBlockBlackUserName.Text = "";
                else textBlockWhiteUserName.Text = "";
            }
        }

        public void ShowStart(int side)
        {
            ResetGrid();
            if (side == 0) AddMessage("黑方已开始。");
            else AddMessage("白方已开始。");
        }

        public void ShowTalk(string userName, string message)
        {
            AddColorMessage(string.Format("{0}：{1}", userName, message), Brushes.Black);
        }

        public void GameStart()
        {
            this.isGameStart = true;  //为true时才可以放棋子
        }

        public void ShowWin(string message)
        {
            AddColorMessage("\n" + message + "\n", Brushes.Red);
            btnStart.IsEnabled = true;
            this.isGameStart = false;
        }

        public void UpdateTablesInfo(string tablesInfo, int userCount)
        {
            textBlockMessage.Text = string.Format("在线人数：{0}", userCount);

            for (int i = 0; i < maxTables; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (tableIndex == -1)
                    {
                        if (tablesInfo[2 * i + j] == '0')
                        {
                            rooms[i, j].Child.Visibility = System.Windows.Visibility.Hidden;
                            rooms[i, j].Child.IsEnabled = true;
                        }
                        else
                        {
                            rooms[i, j].Child.Visibility = System.Windows.Visibility.Visible;
                            rooms[i, j].Child.IsEnabled = false;
                        }
                    }
                    else
                    {
                        rooms[i, j].Child.IsEnabled = false;
                        if (tablesInfo[2 * i + j] == '0')
                        {
                            rooms[i, j].Child.Visibility = System.Windows.Visibility.Hidden;
                        }
                        else rooms[i, j].Child.Visibility = System.Windows.Visibility.Visible;
                    }
                }
            }
        }

        public void UpdateLevel(int level)
        {
            isFromServer = true;
            switch (level)
            {
                case 1: rb1.IsChecked = true; break;
                case 2: rb2.IsChecked = true; break;
                case 3: rb3.IsChecked = true; break;
                case 4: rb4.IsChecked = true; break;
                case 5: rb5.IsChecked = true; break;
            }
            isFromServer = false;
        }

        /// <summary>设置棋子状态。参数：行，列，颜色</summary>
        public void GridSetDot(int i, int j, int color)
        {
            grid[i, j] = color;
            if (color == 0) images[i, j] = new Image() { Source = blackImage.Source };
            else images[i, j] = new Image() { Source = whiteImage.Source };
            Canvas.SetLeft(images[i, j], (i + 1) * 20 - 10);
            Canvas.SetTop(images[i, j], (j + 1) * 20 - 10);
            images[i, j].MouseLeftButtonDown += GridImage_MouseLeftButtonDown;
            canvas1.Children.Add(images[i, j]);
        }

        void GridImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(canvas1);
            int x = (int)(point.X + 10) / 20;
            int y = (int)(point.Y + 10) / 20;
            if (!(x < 1 || x > max || y < 1 || y > max))
            {
                if (grid[x - 1, y - 1] != -1)
                {
                    client.UnsetDot(tableIndex, x - 1, y - 1);
                }
            }
        }

        public void GridUnsetDot(int i, int j)
        {
            grid[i, j] = -1;
            canvas1.Children.Remove(images[i, j]);
            images[i, j] = null;
            canvas1.InvalidateVisual();
        }
        #endregion

        #region 接口调用的方法
        /// <summary>创建游戏桌</summary>
        private void CreateTables()
        {
            this.rooms = new Border[maxTables, 2];
            //创建游戏大厅中的房间（每房间一个游戏桌）
            for (int i = 0; i < maxTables; i++)
            {
                int j = i + 1;
                StackPanel sp = new StackPanel()
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(5)
                };
                TextBlock text = new TextBlock()
                {
                    Text = "房间" + (i + 1),
                    VerticalAlignment = System.Windows.VerticalAlignment.Center,
                    Width = 40
                };
                rooms[i, 0] = new Border()
                {
                    Tag = i + "0",
                    Background = Brushes.White,
                    Child = new Image()
                    {
                        Source = ((Image)this.Resources["player"]).Source,
                        Height = 25
                    }
                };
                Image image = new Image()
                {
                    Source = ((Image)this.Resources["smallBoard"]).Source,
                    Height = 25
                };
                rooms[i, 1] = new Border()
                {
                    Tag = i + "1",
                    Background = Brushes.White,
                    Child = new Image()
                    {
                        Source = ((Image)this.Resources["player"]).Source,
                        Height = 25
                    }
                };
                rooms[i, 0].MouseDown += RoomSide_MouseDown;
                rooms[i, 1].MouseDown += RoomSide_MouseDown;
                sp.Children.Add(text);
                sp.Children.Add(rooms[i, 0]);
                sp.Children.Add(image);
                sp.Children.Add(rooms[i, 1]);
                listBoxRooms.Items.Add(sp);
            }
        }

        /// <summary>重置棋盘</summary>
        private void ResetGrid()
        {
            for (int i = 0; i < max; i++)
            {
                for (int j = 0; j < max; j++)
                {
                    if (grid[i, j] != -1)
                    {
                        grid[i, j] = -1;
                        canvas1.Children.Remove(images[i, j]);
                        images[i, j] = null;
                    }
                }
            }
        }
        #endregion //接口调用的方法
    }
}
