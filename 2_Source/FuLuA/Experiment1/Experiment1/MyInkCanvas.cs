using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Experiment1
{
    public class MyInkCanvas : InkCanvas
    {
        private InkVideo ink = new InkVideo();
        public MyInkCanvas()
        {
            this.DynamicRenderer = ink;
            this.EditingMode = InkCanvasEditingMode.Ink;
        }

        /// <summary>
        /// 当动态绘制完成后抬起鼠标时，会自动调用此方法收集墨迹
        /// </summary>
        protected override void OnStrokeCollected(InkCanvasStrokeCollectedEventArgs e)
        {
            this.Strokes.Remove(e.Stroke);
            ink.CreateNewStroke(e);
            this.Strokes.Add(ink.InkStroke);
            InkCanvasStrokeCollectedEventArgs args = new InkCanvasStrokeCollectedEventArgs(ink.InkStroke);
            base.OnStrokeCollected(args);
        }
    }
}
