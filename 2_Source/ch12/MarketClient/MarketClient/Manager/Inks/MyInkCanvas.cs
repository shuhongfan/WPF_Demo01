using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Input.StylusPlugIns;
using System.Windows.Media;

namespace MarketClient.Inks
{
    public class MyInkCanvas : InkCanvas
    {
        private InkObject ink;
        private string fileName = string.Empty;
        public MyInkData myData { get; set; }
        public DrawingAttributes inkDA { get; set; }

        /// <summary>
        /// 背景曲线填充用的颜色。默认为透明色
        /// </summary>
        public Color fillColor { get; set; }

        /// <summary>
        /// 要显示的墨迹是否来自墨迹文件
        /// </summary>
        public bool isFromFile { get; set; }

        /// <summary>
        /// 初始化墨迹类型和笔尖信息，其初值必须与InkPage中的默认选项相同
        /// </summary>
        public MyInkCanvas()
        {
            this.isFromFile = false;
            this.fillColor = Colors.Transparent;
            this.myData = new MyInkData()
            {
                inkRadius = 4,
                inkColor = Colors.Blue
            };
            UpdateInkParams();
            SetInkAttributes(InkType.文字块.ToString());
        }

        /// <summary>
        /// 根据墨迹类型和笔尖信息，设置MyInkCanvas中的相关参数
        /// </summary>
        private void UpdateInkParams()
        {
            if (isFromFile) return;
            this.ClearSelected();
            inkDA = new DrawingAttributes()
            {
                Color = myData.inkColor,
                Width = 2 * myData.inkRadius,
                Height = 2 * myData.inkRadius,
                StylusTip = StylusTip.Ellipse,
                IgnorePressure = true,
                FitToCurve = false
            };
            this.DefaultDrawingAttributes = inkDA;
            this.DynamicRenderer = ink;
        }

        /// <summary>当从文件中加载墨迹时，会自动调用此方法</summary>
        protected override void OnStrokesReplaced(InkCanvasStrokesReplacedEventArgs e)
        {
            isFromFile = true;
            StrokeCollection strokes = e.NewStrokes.Clone();
            this.Strokes.Remove(e.NewStrokes);
            base.OnStrokesReplaced(e);

            foreach (var v in strokes)
            {
                this.Strokes.Remove(v);
                Guid id = v.GetPropertyDataIds()[0];
                string[] s = ((string)v.GetPropertyData(id)).Split('#');
                MyInkData data = new MyInkData();
                for (int i = 0; i < s.Length; i++)
                {
                    string[] property = s[i].Split(':');
                    switch (property[0])
                    {
                        case "inkName":
                            this.SetInkAttributes(property[1]);
                            break;
                        case "inkRadius":
                            data.inkRadius = double.Parse(property[1]);
                            break;
                        case "inkColor":
                            string[] c = property[1].Split(',');
                            data.inkColor = Color.FromArgb(byte.Parse(c[0]), byte.Parse(c[1]), byte.Parse(c[2]), byte.Parse(c[3]));
                            break;
                        case "inkText":
                            data.inkText = property[1];
                            break;
                    }
                }
                ink.inkTool = ink.CreateFromInkData(data);
                InkCanvasStrokeCollectedEventArgs args = new InkCanvasStrokeCollectedEventArgs(v);
                this.OnStrokeCollected(args);
            }
            this.isFromFile = false;
        }

        /// <summary>当收集墨迹时，会自动调用此方法</summary>
        protected override void OnStrokeCollected(InkCanvasStrokeCollectedEventArgs e)
        {
            this.Strokes.Remove(e.Stroke);
            ink.CreateNewStroke(e);
            MyInkData data = ink.InkStroke.inkTool;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("inkName:{0}#", ink.inkType);
            sb.AppendFormat("inkRadius:{0}#", data.inkRadius);
            Color c = data.inkColor;
            sb.AppendFormat("inkColor:{0},{1},{2},{3}#", c.A, c.R, c.G, c.B);
            sb.AppendFormat("inkText:{0}#", data.inkText);
            ink.InkStroke.AddPropertyData(Guid.NewGuid(), sb.ToString());
            this.Strokes.Add(ink.InkStroke);
            InkCanvasStrokeCollectedEventArgs args = new InkCanvasStrokeCollectedEventArgs(ink.InkStroke);
            base.OnStrokeCollected(args);
        }

        /// <summary>当套索选择笔画时，会自动调用此方法</summary>
        protected override void OnSelectionChanging(InkCanvasSelectionChangingEventArgs e)
        {
            StrokeCollection selection = e.GetSelectedStrokes();
            foreach (Stroke v in Strokes)
            {
                if (selection.Contains(v) == true)
                {
                    v.DrawingAttributes.Color = Colors.RoyalBlue;
                }
                else
                {
                    v.DrawingAttributes.Color = fillColor;
                }
            }
            base.OnSelectionChanging(e);
        }

        /// <summary>清除笔画的选择状态，将所有笔画都变为不选择</summary>
        private void ClearSelected()
        {
            foreach (Stroke aStroke in Strokes)
            {
                aStroke.DrawingAttributes.Color = fillColor;
            }
        }
        
        /// <summary>初始化墨迹绘制时需要的信息</summary>
        public void SetInkAttributes(string name)
        {
            switch (name)
            {
                //---------------文件存取---------------------
                case "加载":
                    LoadInkFromFile();
                    break;
                case "保存":
                    SaveInkToFile();
                    break;
                case "另存为":
                    SaveInkToNewFile();
                    break;
                //---------------墨迹类型---------------------
                case "文字块":
                    ink = new InkRectArea(this);
                    UpdateInkParams();
                    this.EditingMode = InkCanvasEditingMode.Ink;
                    break;
                case "箭头":
                    ink = new InkArrow(this);
                    UpdateInkParams();
                    this.EditingMode = InkCanvasEditingMode.Ink;
                    break;
                case "通道":
                    ink = new InkDoubleLine(this);
                    UpdateInkParams();
                    this.EditingMode = InkCanvasEditingMode.Ink;
                    break;
                case "入口":
                    ink = new InkRectIn(this);
                    UpdateInkParams();
                    this.EditingMode = InkCanvasEditingMode.Ink;
                    break;
                case "出口":
                    ink = new InkRectOut(this);
                    UpdateInkParams();
                    this.EditingMode = InkCanvasEditingMode.Ink;
                    break;
                case "视频":
                    ink = new InkVideo(this);
                    UpdateInkParams();
                    this.EditingMode = InkCanvasEditingMode.Ink;
                    break;
                //---------------笔尖颜色---------------------
                case "蓝色":
                    isFromFile = false;
                    myData.inkColor = Colors.Blue;
                    UpdateInkParams();
                    this.EditingMode = InkCanvasEditingMode.Ink;
                    break;
                case "红色":
                    isFromFile = false;
                    myData.inkColor = Colors.Red;
                    UpdateInkParams();
                    this.EditingMode = InkCanvasEditingMode.Ink;
                    break;
                case "绿色":
                    isFromFile = false;
                    myData.inkColor = Colors.Green;
                    UpdateInkParams();
                    this.EditingMode = InkCanvasEditingMode.Ink;
                    break;
                case "浅灰":
                    isFromFile = false;
                    myData.inkColor = Colors.LightGray;
                    UpdateInkParams();
                    this.EditingMode = InkCanvasEditingMode.Ink;
                    break;
                case "黑色":
                    isFromFile = false;
                    myData.inkColor = Colors.Black;
                    UpdateInkParams();
                    this.EditingMode = InkCanvasEditingMode.Ink;
                    break;
                case "天蓝":
                    isFromFile = false;
                    myData.inkColor = Colors.SkyBlue;
                    UpdateInkParams();
                    this.EditingMode = InkCanvasEditingMode.Ink;
                    break;
                //---------------笔尖大小------------------------
                case "2":
                case "4":
                case "6":
                case "10":
                case "16":
                case "22":
                    isFromFile = false;
                    myData.inkRadius = double.Parse(name);
                    UpdateInkParams();
                    this.EditingMode = InkCanvasEditingMode.Ink;
                    break;
                //---------------操作选择---------------------
                case "套索":
                    isFromFile = false;
                    this.UseCustomCursor = false;
                    this.EditingMode = InkCanvasEditingMode.Select;
                    break;
                case "全选":
                    isFromFile = false;
                    this.UseCustomCursor = false;
                    this.Select(Strokes);
                    this.EditingMode = InkCanvasEditingMode.Select;
                    break;
                case "全不选":
                    isFromFile = false;
                    this.UseCustomCursor = false;
                    ClearSelected();
                    this.EditingMode = InkCanvasEditingMode.None;
                    break;
                case "墨迹擦除":
                    isFromFile = false;
                    this.UseCustomCursor = false;
                    foreach (Stroke v in Strokes)
                    {
                        v.DrawingAttributes.Color = Colors.RoyalBlue;
                    }
                    this.EditingMode = InkCanvasEditingMode.EraseByPoint;
                    break;
                case "笔画擦除":
                    isFromFile = false;
                    this.UseCustomCursor = false;
                    foreach (Stroke v in Strokes)
                    {
                        v.DrawingAttributes.Color = Colors.RoyalBlue;
                    }
                    this.EditingMode = InkCanvasEditingMode.EraseByStroke;
                    break;
                case "全部删除":
                    isFromFile = false;
                    this.UseCustomCursor = false;
                    this.Strokes.Clear();
                    this.EditingMode = InkCanvasEditingMode.None;
                    break;
            }
        }

        /// <summary>从墨迹文件中加载墨迹笔画</summary>
        public void LoadInkFromFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.CurrentDirectory;
            ofd.Filter = "墨迹文件|*.ink|所有文件|*.*";
            if (ofd.ShowDialog() == false)
            {
                return;
            }
            this.fileName = ofd.FileName;
            LoadInkFromFile(this.fileName);
            SetInkAttributes(Manager.InkPage.RadiusName);
            SetInkAttributes(Manager.InkPage.ColorName);
            SetInkAttributes(Manager.InkPage.ToolName);
        }

        public void LoadInkFromFile(string fileName)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                StrokeCollection strokes = new StrokeCollection(fs);
                this.Strokes = strokes;
                this.isFromFile = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载失败：" + ex.StackTrace);
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }

        /// <summary>将墨迹笔画保存到墨迹文件中</summary>
        public void SaveInkToFile()
        {
            if (this.fileName == string.Empty)
            {
                this.fileName = Environment.CurrentDirectory + "\\MyTest.ink";
            }
            FileStream fs = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Create);
                this.Strokes.Save(fs);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败：" + ex.StackTrace);
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }

        public void SaveInkToNewFile()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = Environment.CurrentDirectory;
            sfd.Filter = "墨迹文件|*.ink|所有文件|*.*";
            if (sfd.ShowDialog() == true)
            {
                this.fileName = sfd.FileName;
                SaveInkToFile();
            }
        }
    }
}
