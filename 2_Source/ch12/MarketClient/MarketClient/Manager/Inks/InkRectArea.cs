using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class InkRectArea : InkObject
    {
        private static Typeface typeface = new Typeface("楷体");

        public InkRectArea(MyInkCanvas myInkCanvas)
            : base(myInkCanvas)
        {
            this.inkType = InkType.文字块;
        }

        public override void CreateNewStroke(InkCanvasStrokeCollectedEventArgs e)
        {
            InkStroke = new InkRectAreaStroke(this, e.Stroke.StylusPoints);
        }

        public override Point Draw(Point first, MyInkData tool, DrawingContext dc, StylusPointCollection points)
        {
            Point pt = (Point)points.Last();
            Vector v = Point.Subtract(pt, first);
            if (double.IsNegativeInfinity(first.X)) return first;
            if (v.Length > 6)
            {
                Rect rect = new Rect(first, v);
                dc.DrawRectangle(null, tool.inkPen, rect);
                if (tool.inkText.Length > 0)
                {
                    double size = rect.Width / tool.inkText.Length;
                    if (size > rect.Height) size = Math.Max(1.0, rect.Height - 2);
                    if (size < 1) size = 1.0;
                    FormattedText ft = new FormattedText(
                        tool.inkText,
                        CultureInfo.CurrentCulture,
                        FlowDirection.LeftToRight,
                        typeface,
                        size,
                        tool.inkBrush);
                    Point p = new Point(rect.X, rect.Y + (rect.Height - ft.LineHeight) / 6);
                    dc.DrawText(ft, p);
                }
            }
            return first;
        }

        protected override void OnStylusDown(RawStylusInput rawStylusInput)
        {
            base.OnStylusDown(rawStylusInput);
            inkTool.inkText = Manager.InkPage.Text;
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

    public class InkRectAreaStroke : InkObjectStroke
    {
        public InkRectAreaStroke(InkObject ink, StylusPointCollection stylusPoints)
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
