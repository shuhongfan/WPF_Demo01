using System;
using System.Windows.Ink;
using System.Windows.Media;

namespace MarketClient.Inks
{
    public enum InkType { 文字块, 箭头, 通道, 入口, 出口, 视频 }
    public enum InkColorType { 蓝色, 红色, 绿色, 浅灰, 黑色, 天蓝 }

    public class MyInkData
    {
        public Brush inkBrush;
        public Pen inkPen;
        public double inkRadius;
        public Color inkColor;
        public string inkText = string.Empty;
        public MediaPlayer player = null;

        public MyInkData Clone()
        {
            MyInkData data = new MyInkData()
            {
                inkRadius = this.inkRadius,
                inkColor = this.inkColor,
                inkText = this.inkText
            };
            return data;
        }
    }
}
