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

namespace Experiment1
{
    public class InkVideo : DynamicRenderer
    {
        public string inkText { private set; get; }
        public InkVideoStroke InkStroke { private set; get; }
        private Point previousPoint;
        private MediaPlayer p1;
        public void CreateNewStroke(InkCanvasStrokeCollectedEventArgs e)
        {
            InkStroke = new InkVideoStroke(this, e.Stroke.StylusPoints);
        }

        public static MediaPlayer CreateVideoPlayer(string uriString)
        {
            MediaPlayer p = new MediaPlayer();
            p.Open(new Uri(uriString, UriKind.Relative));
            p.IsMuted = true;
            p.MediaEnded += (sender, args) => { p.Position = TimeSpan.Zero; };
            p.Play();
            return p;
        }

        public void Draw(Point first, MediaPlayer player, DrawingContext dc, StylusPointCollection points)
        {
            Point pt = (Point)points.Last();
            Vector v = Point.Subtract(pt, first);
            if (double.IsNegativeInfinity(first.X)) return;
            if (v.Length > 6)
            {
                Rect rect = new Rect(first, v);
                dc.DrawVideo(player, rect);
            }
        }

        protected override void OnStylusDown(RawStylusInput rawStylusInput)
        {
            base.OnStylusDown(rawStylusInput);
            previousPoint = (Point)rawStylusInput.GetStylusPoints().First();
            inkText = @".\..\..\Videos\" + MainWindow.VideoFileName;
            p1 = CreateVideoPlayer(inkText);
        }

        protected override void OnStylusUp(RawStylusInput rawStylusInput)
        {
            p1.Stop();
            p1.Close();
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
            this.Draw(previousPoint, p1, drawingContext, stylusPoints);
        }
    }

    public class InkVideoStroke : Stroke
    {
        private InkVideo ink;
        private string inkText;

        public InkVideoStroke(InkVideo ink, StylusPointCollection stylusPoints)
            : base(stylusPoints)
        {
            this.ink = ink;
            this.inkText = ink.inkText;
        }
        
        protected override void DrawCore(DrawingContext drawingContext, DrawingAttributes drawingAttributes)
        {
            MediaPlayer mp = InkVideo.CreateVideoPlayer(inkText);
            Point pt1 = (Point)StylusPoints.First();
            ink.Draw(pt1, mp, drawingContext, StylusPoints);
        }
    }
}
