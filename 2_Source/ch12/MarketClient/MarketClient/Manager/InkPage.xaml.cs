using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MarketClient.Manager
{
    /// <summary>
    /// InkPage.xaml 的交互逻辑
    /// </summary>
    public partial class InkPage : Page
    {
        /// <summary>将要绘制的文字</summary>
        public static string Text;
        /// <summary>将要绘制的视频</summary>
        public static string VideoFileName;

        //下面的初值必须与默认选项相同
        public static string ToolName = "文字块"; //绘制工具
        public static string ColorName = "蓝色"; //笔尖颜色
        public static string RadiusName = "4"; //笔尖大小

        public InkPage()
        {
            InitializeComponent();
            dockPanelText.Visibility = System.Windows.Visibility.Visible;
            dockPanelVideo.Visibility = System.Windows.Visibility.Hidden;
            this.Unloaded += InkPage_Unloaded;
        }

        void InkPage_Unloaded(object sender, RoutedEventArgs e)
        {
            ink1 = null;
        }

        private void RibbonRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RibbonRadioButton rrb = e.Source as RibbonRadioButton;
            string name = rrb.Label;
            ink1.SetInkAttributes(name);
            string parentName = (rrb.Parent as RibbonGroup).Header.ToString();
            switch (parentName)
            {
                case "文件存取":
                    ClearEditOperation();
                    rrb.IsChecked = false;
                    break;
                case "绘制工具":
                    ClearEditOperation();
                    ToolName = name;
                    if (name == "文字块")
                    {
                        dockPanelText.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                    {
                        dockPanelText.Visibility = System.Windows.Visibility.Hidden;
                    }
                    if (name == "视频")
                    {
                        dockPanelVideo.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                    {
                        dockPanelVideo.Visibility = System.Windows.Visibility.Hidden;
                    }
                    break;
                case "笔尖颜色":
                    ClearEditOperation();
                    ColorName = name;
                    break;
                case "笔尖大小":
                    ClearEditOperation();
                    RadiusName = name;
                    break;
                case "操作选择":
                    foreach (var v in rgTool.Items)
                    {
                        RibbonRadioButton rrb1 = v as RibbonRadioButton;
                        if (rrb1 != null)
                        {
                            rrb1.IsChecked = false;
                        }
                    }
                    break;
            }
        }

        private void ClearEditOperation()
        {
            foreach (var v in rgOperation.Items)
            {
                RibbonRadioButton rrb = v as RibbonRadioButton;
                if (rrb != null)
                {
                    rrb.IsChecked = false;
                }
            }
        }

        private void textBoxWenZi_TextChanged(object sender, TextChangedEventArgs e)
        {
            Text = textBoxWenZi.Text;
        }

        private void RadioButtonVideo_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = e.Source as RadioButton;
            VideoFileName = rb.Content.ToString();
        }
    }
}
