using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace SpriteLayer.Forms
{
    public class NearestNeighorPictureBox : PictureBox
    {
        [Browsable(false)]
        public List<Point> SelectedPoints = new List<Point>();

        [Browsable(true)]
        [Category("Custom")]
        [Description("Allow the contents to be scaled with the mouse wheel.")]
        public bool AllowScale { get; set; } = true;

        [Category("Custom")]

        [Description("Allow the user to select pixels.")]
        [Browsable(true)]
        public bool AllowSelectPixel { get; set; } = true;

        [Category("Custom")]
        [Browsable(true)]
        [Description("Allow the contents to be moved by holding the middle mouse button.")]
        public bool AllowMove { get; set; } = true;

        [Category("Custom")]
        [Browsable(true)]
        [Description("Specify the interpolation mode for rendering.")]
        public System.Drawing.Drawing2D.InterpolationMode InterpolationMode { get; set; } = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

        [Browsable(true)]
        [Category("Custom")]
        [Description("Specify the amount the picture is scaled.")]
        public float RenderScale { get; set; } = 1.0f;

        private Point _lastMouseDown = new Point(0, 0);
        private Point _lastMyPosition = new Point(0, 0);
        const float RenderScaleDelta = 0.0005f;

        protected override void OnPaint(PaintEventArgs paintEventArgs)
        {
            paintEventArgs.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            paintEventArgs.Graphics.InterpolationMode = InterpolationMode;
            paintEventArgs.Graphics.Clear(Color.White);

            base.OnPaint(paintEventArgs);
            if (!AllowSelectPixel || (this.Image == null))
            {

                return;
            }

            var sz = this.Size;
            var gridPen = new Pen(Color.Red, 0.125f);
            var selectedPixelPen = new Pen(Color.Blue, 2.0f);
            var scalex = (float)sz.Width / this.Image.Width;
            var scaley = (float)sz.Height / this.Image.Height;

            // if the scale doesn't allow lines to be drawn by at least 1 pixel apart, we're done
            if ((scalex <= 1.0f) || (scaley <= 1.0f))
                return;

            for(float y = 0; y < sz.Height; y+=scaley)
            {
                paintEventArgs.Graphics.DrawLine(gridPen, 0.0f, y, (float)sz.Width, y);
            }

            for (float x = 0; x < sz.Width; x+=scalex)
            {
                paintEventArgs.Graphics.DrawLine(gridPen, x, 0.0f, x, (float)sz.Height);
            }

            foreach(var point in SelectedPoints)
            {
                paintEventArgs.Graphics.DrawRectangle(selectedPixelPen, point.X * scalex, point.Y * scaley, scalex, scaley);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (AllowMove && (e.Button == MouseButtons.Middle))
            {
                _lastMouseDown = this.PointToScreen(e.Location);
                _lastMyPosition = this.Location;
                Cursor = Cursors.Cross;
                System.Diagnostics.Debug.WriteLine("OnMouseDown");
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if(AllowMove && (e.Button == MouseButtons.Middle))
            {
                Cursor = Cursors.Default;
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (!AllowSelectPixel)
                return;
            if((e.Button == MouseButtons.Left) && (Control.ModifierKeys == Keys.None))
            {
                SelectedPoints.Clear();
                var sz = this.Size;
                var scalex = (float)sz.Width / this.Image.Width;
                var scaley = (float)sz.Height / this.Image.Height;
                var p = new Point((int)(e.X / scalex), (int)(e.Y / scaley));
                SelectedPoints.Add(p);
                this.Invalidate();
            }
            else if ((e.Button == MouseButtons.Left) && (Control.ModifierKeys == Keys.Control))
            {
                var sz = this.Size;
                var scalex = (float)sz.Width / this.Image.Width;
                var scaley = (float)sz.Height / this.Image.Height;
                var p = new Point((int)(e.X / scalex), (int)(e.Y / scaley));
                var found = SelectedPoints.Contains(p);
                if (!found)
                {
                    SelectedPoints.Add(p);
                }
                else
                {
                    SelectedPoints.Remove(p);
                }
                this.Invalidate();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if(AllowMove && (e.Button == MouseButtons.Middle))
            {
                var screenPoint = this.PointToScreen(e.Location);
                var diff = new Point(_lastMouseDown.X - screenPoint.X, _lastMouseDown.Y - screenPoint.Y);
                this.Location = new Point(_lastMyPosition.X - diff.X, _lastMyPosition.Y - diff.Y);
                this.Invalidate();
            }
            else if (AllowSelectPixel && (e.Button == MouseButtons.Left) && (Control.ModifierKeys == Keys.Control))
            {
                var sz = this.Size;
                var scalex = (float)sz.Width / this.Image.Width;
                var scaley = (float)sz.Height / this.Image.Height;
                var p = new Point((int)(e.X / scalex), (int)(e.Y / scaley));
                var found = SelectedPoints.Contains(p);
                if (!found)
                {
                    SelectedPoints.Add(p);
                    this.Invalidate();
                }
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            RenderScale += (e.Delta * RenderScaleDelta);
            UpdateRenderViewScale();
            ((HandledMouseEventArgs)e).Handled = true;
        }

        public void UpdateRenderViewScale()
        {
            var sz = this.Image.Size;
            this.Width = (int)(RenderScale * sz.Width);
            this.Height = (int)(RenderScale * sz.Height);
        }

        public void ExternalWheel(MouseEventArgs e)
        {
            OnMouseWheel(e);
        }
    }
}
