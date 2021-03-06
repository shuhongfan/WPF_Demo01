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
    public class InkVideo : InkObject
    {
        public InkVideo(MyInkCanvas myInkCanvas)
            : base(myInkCanvas)
        {
            this.inkType = InkType.视频;
        }

        public override void CreateNewStroke(InkCanvasStrokeCollectedEventArgs e)
        {
            InkStroke = new InkVideoStroke(this, e.Stroke.StylusPoints);
        }

        public static MediaPlayer CreateVideoPlayer(string uriString)
        {
            MediaPlayer p = new MediaPlayer();
            p.Open(new Uri(uriString, UriKind.Relative));
            p.IsMuted = true;
            p.MediaEnded += (s, e) =>{ p.Position = TimeSpan.Zero; };
            p.Play();
            return p;
        }

        public override Point Draw(Point first, MyInkData tool, DrawingContext dc, StylusPointCollection points)
        {
            Point pt = (Point)points.Last();
            Vector v = Point.Subtract(pt, first);
            if (double.IsNegativeInfinity(first.X)) return first;
            if (v.Length > 6)
            {
                Rect rect = new Rect(first, v);
                dc.DrawVideo(tool.player, rect);
            }
            return first;
        }

        protected override void OnStylusDown(RawStylusInput rawStylusInput)
        {
            base.OnStylusDown(rawStylusInput);
            previousPoint = (Point)rawStylusInput.GetStylusPoints().First();
            inkTool.inkText = @".\..\..\Manager\Videos\" + Manager.InkPage.VideoFileName;
            inkTool.player = CreateVideoPlayer(inkTool.inkText);
        }

        protected override void OnStylusUp(RawStylusInput rawStylusInput)
        {
            inkTool.player.Stop();
            inkTool.player.Close();
            base.OnStylusUp(rawStylusInput);
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

    public class InkVideoStroke : InkObjectStroke
    {
        public InkVideoStroke(InkObject ink, StylusPointCollection stylusPoints)
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
            inkTool.player = InkVideo.CreateVideoPlayer(inkTool.inkText);
            ink.Draw(pt1, inkTool, drawingContext, StylusPoints);
        }
    }

}
