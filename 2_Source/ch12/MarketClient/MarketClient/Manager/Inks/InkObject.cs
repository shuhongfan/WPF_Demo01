using System;
using System.Collections.Generic;
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
    public abstract class InkObject : DynamicRenderer
    {
        public MyInkCanvas myInkCanvas { get; private set; }
        public InkObjectStroke InkStroke { protected set; get; }
        public InkType inkType { get; protected set; }
        public MyInkData inkTool;
        protected Point previousPoint;
        public DrawingAttributes inkDA;

        public abstract void CreateNewStroke(InkCanvasStrokeCollectedEventArgs e);
        public abstract Point Draw(Point first, MyInkData tool, DrawingContext dc, StylusPointCollection points);

        [ThreadStatic]
        protected Brush brush = new SolidColorBrush(Colors.Gray);

        public InkObject(MyInkCanvas myInkCanvas)
        {
            this.myInkCanvas = myInkCanvas;
            brush.Opacity = 0.09;
            inkTool = CreateFromInkData(myInkCanvas.myData);
            inkDA = myInkCanvas.inkDA.Clone();
            this.DrawingAttributes = inkDA;
        }

        public MyInkData CreateFromInkData(MyInkData data)
        {
            MyInkData tool = data.Clone();
            tool.inkBrush = null;
            tool.inkBrush = new SolidColorBrush(data.inkColor);
            tool.inkPen = new Pen(tool.inkBrush, tool.inkRadius * 0.5);
            return tool;
        }

        protected double GetStylusInkDistance(MyInkData data, out Size size)
        {
            double width = data.inkRadius; ;
            double height = data.inkRadius;
            size = new Size(width, height);
            double distance = Math.Max(size.Width, size.Height) * 1.5 + 10;
            return distance;
        }

        protected override void OnStylusDown(RawStylusInput rawStylusInput)
        {
            myInkCanvas.isFromFile = false;
            inkTool = CreateFromInkData(myInkCanvas.myData);
            inkDA = myInkCanvas.inkDA.Clone();
            this.DrawingAttributes = inkDA;
            previousPoint = new Point(double.NegativeInfinity, double.NegativeInfinity);
            base.OnStylusDown(rawStylusInput);
        }

        protected override void OnStylusUp(RawStylusInput rawStylusInput)
        {
            base.OnStylusUp(rawStylusInput);
            this.InkStroke = null;
        }
    }

    public class InkObjectStroke : Stroke
    {
        protected InkObject ink;
        public MyInkData inkTool;

        public InkObjectStroke(InkObject ink, StylusPointCollection stylusPoints)
            : base(stylusPoints)
        {
            this.ink = ink;
            inkTool = ink.CreateFromInkData(ink.inkTool);
            this.DrawingAttributes = ink.inkDA.Clone();
            this.DrawingAttributes.Color = ink.myInkCanvas.fillColor;
        }

        protected virtual void RemoveDirtyStylusPoints()
        {
            if (StylusPoints.Count > 2)
            {
                for (int i = StylusPoints.Count - 2; i > 0; i--)
                {
                    StylusPoints.RemoveAt(i);
                }
            }
        }
    }

}
