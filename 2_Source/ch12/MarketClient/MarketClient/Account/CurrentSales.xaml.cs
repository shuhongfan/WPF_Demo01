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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MarketClient.MarketServiceReference;

namespace MarketClient.Account
{
    /// <summary>
    /// SaleAccount.xaml 的交互逻辑
    /// </summary>
    public partial class CurrentSales : Page
    {
        private static int id = 1;
        private string saleId;
        private Random r = new Random();
        public CurrentSales()
        {
            InitializeComponent();
            labelStatus.Content = "操作员：" + MainWindow.UserName;
            grid1.Visibility = System.Windows.Visibility.Hidden;
            MarketServiceClient client = new MarketServiceClient();
            try
            {
                id = client.GetNewId();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                client.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;
            string s = btn.Content.ToString();
            switch (s)
            {
                case "新建":
                    {
                        saleId = DateTime.Now.ToString("yyMMdd") + id.ToString("d4");
                        grid1.RowDefinitions.Clear();
                        grid1.RowDefinitions.Add(new RowDefinition());
                        AddColumn(0, 0, 4,
                            string.Format("销售号：{0}", saleId),
                            false, false, true);
                        grid1.RowDefinitions.Add(new RowDefinition());
                        AddColumn(1, 0, 1, "名称", false, false, true);
                        AddColumn(1, 1, 1, "数量", false, false, true);
                        AddColumn(1, 2, 1, "单价", false, false, true);
                        AddColumn(1, 3, 1, "金额", false, false, true); 
                        grid1.IsEnabled = true;
                        btnNew.IsEnabled = false;
                        btnAdd.IsEnabled = true;
                        btnResult.IsEnabled = false;
                        grid1.Visibility = System.Windows.Visibility.Visible;
                        break;
                    }
                case "添加":
                    {
                        int n = r.Next(1, 3);
                        double d = r.NextDouble() * 20;
                        double result = n * d;
                        int row = grid1.RowDefinitions.Count;
                        grid1.RowDefinitions.Add(new RowDefinition());
                        AddColumn(row, 0, 1, "商品" + (row - 1).ToString(), false, false, false);
                        AddColumn(row, 1, 1, n.ToString(), false, true, false);
                        AddColumn(row, 2, 1, d.ToString(), true, true, false);
                        AddColumn(row, 3, 1, result.ToString(), true, false, true);
                        btnResult.IsEnabled = true;
                        break;
                    }
                case "结算":
                    {
                        int row = grid1.RowDefinitions.Count;
                        double sum = 0;
                        for (int i = 2; i < row; i++)
                        {
                            TextBox t3 = GetTextBox(i, 3);
                            double result = 0;
                            if (double.TryParse(t3.Text, out result) == true)
                            {
                                sum += result;
                            }
                            else
                            {
                                MessageBox.Show("请先更正错误");
                                return;
                            }
                        }
                        grid1.RowDefinitions.Add(new RowDefinition());
                        AddColumn(row, 0, 4,
                            string.Format("合计金额：{0:0.00} 元", sum),
                            false, false, true);

                        MarketServiceClient client = new MarketServiceClient();
                        for (int i = 2; i < row; i++)
                        {
                            TextBox t0 = GetTextBox(i, 0);
                            TextBox t1 = GetTextBox(i, 1);
                            TextBox t2 = GetTextBox(i, 2); 
                            TextBox t3 = GetTextBox(i, 3);
                            client.SaveCurrentSale(
                                saleId,
                                t0.Text,
                                int.Parse(t1.Text),
                                double.Parse(t2.Text),
                                double.Parse(t3.Text),
                                MainWindow.UserName);
                        }
                        id++;
                        client.Close();

                        grid1.IsEnabled = false;
                        btnNew.IsEnabled = true;
                        btnAdd.IsEnabled = false;
                        btnResult.IsEnabled = false;
                        MessageBox.Show("结算完毕。");
                        break;
                    }
            }
        }

        private void AddColumn(int row, int col, int colSpan, string text, bool isDouble, bool isAddEvent,bool isReadOnly)
        {
            TextBox t = new TextBox();
            t.Text = text;
            t.IsReadOnly = isReadOnly;
            if (isDouble)
            {
                t.Text = string.Format("{0:0.00}", double.Parse(text));
            }
            else
            {
                t.Text = text;
            }
            if (isAddEvent) t.TextChanged += t_TextChanged;
            Grid.SetRow(t, row);
            Grid.SetColumn(t, col);
            if (colSpan > 1)
            {
                Grid.SetColumnSpan(t, colSpan);
            }
            grid1.Children.Add(t);
        }

        void t_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = e.Source as TextBox;
            int row = Grid.GetRow(t);
            TextBox t1 = GetTextBox(row, 1);
            TextBox t2 = GetTextBox(row, 2);
            TextBox t3 = GetTextBox(row, 3);
            int n = 0;
            int.TryParse(t1.Text, out n);
            double d = 0;
            double.TryParse(t2.Text, out d);
            double result = n * d;
            if (result == 0)
            {
                t3.Text = "出错";
                t3.Background = Brushes.Red;
                t3.Foreground = Brushes.White;
            }
            else
            {
                t3.Text = string.Format("{0:0.00}", n * d);
                t3.Background = Brushes.White;
                t3.Foreground = Brushes.Black;
            }
        }

        public TextBox GetTextBox(int row, int col)
        {
            int count = grid1.Children.Count;
            for (int i = 0; i < count; i++)
            {
                TextBox t = grid1.Children[i] as TextBox;
                if (t != null)
                {
                    if (Grid.GetRow(t) == row && Grid.GetColumn(t) == col)
                    {
                        return t;
                    }
                }
            }
            return null;
        }
    }
}
