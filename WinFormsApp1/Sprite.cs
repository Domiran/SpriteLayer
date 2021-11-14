using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpriteLayer
{
    /// <summary>
    /// A sprite is a collection of layers, which can be ordered to form a final sprite sheet.
    /// </summary>
    public class Sprite : IDisposable
    {
        private Bitmap _lastRender; // cached representation of the final image
        public System.ComponentModel.BindingList<Layer> Layers { get; private set; } // all sheets

        /// <summary>
        /// All pixels in the final image.
        /// </summary>
        private Pixel[] _pixels; 
        public Size SpriteFrameSize { get; set; }

        public delegate void RenderUpdateDelegate(Bitmap render);

        private event RenderUpdateDelegate? RenderUpdate;

        private delegate void PixelActionDelegate(ref Pixel p);

        public Size SpriteSheetSize
        {
            get
            {
                if (Layers.Count == 0)
                {
                    return Size.Empty;
                }
                else
                {
                    return Layers[0].Size;
                }
            }
        }

        // returns true if we moved
        public bool MoveLayerDown(int index)
        {
            var nextIndex = index + 1;
            if (nextIndex < Layers.Count)
            {
                // this resets all custom pixel ordering

                var temp = Layers[nextIndex];
                Layers[nextIndex] = Layers[index];
                Layers[index] = temp;
                ResetPixelLayering();
                Render();
                return true;
            }
            return false;
        }

        // returns true if we moved
        public bool MoveLayerUp(int index)
        {
            var prevIndex = index - 1;
            if (prevIndex >= 0)
            {
                // this resets all custom pixel ordering

                var temp = Layers[prevIndex];
                Layers[prevIndex] = Layers[index];
                Layers[index] = temp;
                ResetPixelLayering();
                Render();
                return true;
            }
            return false;
        }

        public bool MovePixellayerUp(int index, Pixel pixel)
        {
            if(pixel.MoveLayerUp(index))
            {
                Render();
                return true;
            }
            return false;
        }
        
        public bool MovePixellayerDown(int index, Pixel pixel)
        {
            if(pixel.MoveLayerDown(index))
            {
                Render();
                return true;
            }
            return false;
        }

        public Sprite(RenderUpdateDelegate renderUpdate)
        {
            RenderUpdate += renderUpdate;
            Layers = new System.ComponentModel.BindingList<Layer>();
            _pixels = new Pixel[1]; // mostly to shut the compiler up
            SpriteFrameSize = new Size(256, 256);
            _lastRender = new Bitmap(256, 256);
            using Graphics g = Graphics.FromImage(_lastRender);
            g.FillRectangle(Brushes.Black, 0, 0, 256, 256);
            g.DrawString("No sheet loaded yet", new Font("Consolas", 12), Brushes.White, 4, 4);
            Render();
        }

        public void AddLayer(Layer layer)
        {
            if (Layers.Count > 1)
            {
                // verify it's the same size
                if(SpriteSheetSize != layer.Size)
                {
                    throw new ArgumentException($"Size of {layer.Name}, {layer.Size}, must match the size of all other layers, {SpriteSheetSize}");
                }
            }

            Layers.Insert(0, layer);
            if (Layers.Count == 1)
            {
                SetupLayeringArray();
            }

            AddLayerInternal(layer);

            Render();
        }

        public void RemoveLayer(Layer layer)
        {
            // remove the sheet
            // we don't care what this does to any custom pixel layering

            Layers.Remove(layer);
            PixelActionDelegate removeLayerFn = void (ref Pixel p) => p.RemoveLayer(layer);
            ForEachPixelObject(removeLayerFn);
            Render();
        }

        private void AddLayerInternal(Layer layer)
        {
            // new layers go on top

            PixelActionDelegate addLayerFn = (ref Pixel p) => p.AddLayer(layer);
            ForEachPixelObject(addLayerFn);
        }

        public void SetLayerShade(Layer layer, Color shade)
        {
            layer.Shade = shade;

            PixelActionDelegate calcLayerFn = (ref Pixel p) => p.CalculateColor();
            ForEachPixelObject(calcLayerFn);
            Render();
        }

        private void ForEachPixelObject(PixelActionDelegate pred)
        {
            var sz = SpriteSheetSize;
            var totalPixels = sz.Width * sz.Height;
            Parallel.For(0, _pixels.Length, index =>
            {
                pred(ref _pixels[index]);
            });
        }

        private void SetupLayeringArray()
        {
            // reset the _pixels
            // ensure they have a position
            // ensure the render image is the correct size

            var sz = SpriteSheetSize;
            var totalPixels = sz.Width * sz.Height;
            Point p = new Point(0, 0);

            _pixels = new Pixel[totalPixels];
            int x = 0;
            int y = 0;
            var width = sz.Width;
            for(int i = 0; i < totalPixels; i++)
            {

                _pixels[i] = new Pixel(x, y);
                x++;
                if (x == sz.Width)
                {
                    x = 0;
                    y++;
                }
            }

            _lastRender = new Bitmap(sz.Width, sz.Height);
        }

        private void ResetPixelLayering()
        {
            PixelActionDelegate reset = (ref Pixel p) =>
            {
                p.SetLayers(Layers);
            };

            ForEachPixelObject(reset);
        }
        
        public void Refresh()
        {
            Render();
        }

        public Image GetFrame(int i)
        {
            var across = SpriteSheetSize.Width / SpriteFrameSize.Width;
            var down = SpriteSheetSize.Height / SpriteFrameSize.Height;
            var total = across * down;
            if (i < 0 || i >= total)
                return new Bitmap(1, 1);

            var row = i / across;
            var col = i % across;
            var topLeft = new Point(col * SpriteFrameSize.Width, row * SpriteFrameSize.Height);

            // assume the last render is up-to-date
            Bitmap frame = new Bitmap(SpriteFrameSize.Width,SpriteFrameSize.Height);
            using Graphics g = Graphics.FromImage(frame);
            var destRect = new Rectangle(new Point(0, 0), SpriteFrameSize);
            var srcRect = new Rectangle(topLeft, SpriteFrameSize);
            g.DrawImage(_lastRender, destRect, srcRect, GraphicsUnit.Pixel);
            return frame;
        }

        public int TotalFrames
        {
            get
            {
                var across = SpriteSheetSize.Width / SpriteFrameSize.Width;
                var down = SpriteSheetSize.Height / SpriteFrameSize.Height;
                var total = across * down;
                return total;
            }
        }

        public Pixel this[Point p]
        {
            get
            {
                var index = (p.Y * SpriteSheetSize.Width) + p.X;
                return _pixels[index];
            }
        }

        private void Render()
        {
            var sz = SpriteSheetSize;
            if((sz.Width == 0) || (sz.Height == 0))
                return;

            // render the final image
            using Graphics g = Graphics.FromImage(_lastRender);
            g.FillRectangle(Brushes.Black, 0, 0, _lastRender.Width, _lastRender.Height);
            var gdata = _lastRender.LockBits(new Rectangle(0, 0, sz.Width, sz.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            var bytes = Math.Abs(gdata.Stride) * gdata.Height;
            byte[] rgbValues = new byte[bytes];
            var dataPtr = gdata.Scan0;
            System.Runtime.InteropServices.Marshal.Copy(dataPtr, rgbValues, 0, bytes);

            Parallel.For(0, bytes / 4, i =>
            {
                var colorIndex = i * 4;
                var color = _pixels[i].Color;
                rgbValues[colorIndex+0] = color.B;
                rgbValues[colorIndex+1] = color.G;
                rgbValues[colorIndex+2] = color.R;
                rgbValues[colorIndex+3] = color.A;
                i++;
            });

            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, dataPtr, bytes);

            _lastRender.UnlockBits(gdata);

            if (RenderUpdate != null)
            {
                RenderUpdate(_lastRender);
            }
        }

        public void Dispose()
        {
            foreach(var layer in Layers)
            {
                ((IDisposable)layer).Dispose();
            }
            ((IDisposable)_lastRender).Dispose();
        }
    }
}
