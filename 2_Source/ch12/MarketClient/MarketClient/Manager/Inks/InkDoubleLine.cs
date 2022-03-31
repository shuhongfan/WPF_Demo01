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
    public class InkDoubleLine : InkObject
    {
        public InkDoubleLine(MyInkCanvas myInkCanvas)
            : base(myInkCanvas)
        {
            this.inkType = InkType.通道;
        }

        public override void CreateNewStroke(InkCanvasStrokeCollectedEventArgs e)
        {
            InkStroke = new InkDoubleLineStroke(this, e.Stroke.StylusPoints);
        }

        public override Point Draw(Point first, MyInkData tool, DrawingContext dc, StylusPointCollection points)
        {
            Point pt = (Point)points.Last();
            Vector v = Point.Subtract(pt, first);
            if (double.IsNegativeInfinity(first.X)) return first;
            if (v.Length > 6)
            {
                Rect rect = new Rect(first, v);
                dc.DrawRectangle(Brushes.LightGray, null, rect);
                if (rect.Width >= rect.Height)
                {
                    dc.DrawLine(tool.inkPen, rect.TopLeft, rect.TopRight);
                    dc.DrawLine(tool.inkPen, rect.BottomLeft, rect.BottomRight);
                }
                else
                {
                    dc.DrawLine(tool.inkPen, rect.TopLeft, rect.BottomLeft);
                    dc.DrawLine(tool.inkPen, rect.TopRight, rect.BottomRight);
                }
            }
            return first;
        }

        protected override void OnStylusDown(RawStylusInput rawStylusInput)
        {
            base.OnStylusDown(rawStylusInput);
            previousPoint = (Point)rawStylusInput.GetStylusPoints().First();
        }

        protected override void OnStylusMove(RawStylusInput rawStylusInput)
        {
            StylusPointCollection stylusPoints = rawStylusInput.GetStylusPoints();
            this.Reset(Stylus.CurrentStylusDevice, stylusPoints);
            base.OnStylusMove(rawStylusInput);
        }

        protected override void OnDraw(DrawingContext drawingContext, StylusPointCollection stylusPoints, Geometry geometry, Brush fillBrush)
        {
            Draw(previousPoint, inkTool, drawingContext, stylusPoints);
            base.OnDraw(drawingContext, stylusPoints, geometry, brush);
        }
    }

    public class InkDoubleLineStroke : InkObjectStroke
    {
        public InkDoubleLineStroke(InkObject ink, StylusPointCollection stylusPoints)
            : base(ink, stylusPoints)
        {
            if (ink.myInkCanvas.isFromFile == false)
            {
                this.RemoveDirtyStylusPoints();
            }
        }

        protected override void DrawCore(DrawingContext drawingContext, DrawingAttributes drawingAttributes)
        {
            base.DrawCore(drawingContext, drawingAttributes);
            Point pt1 = (Point)StylusPoints.First();
            ink.Draw(pt1, inkTool, drawingContext, StylusPoints);
        }
    }
}
