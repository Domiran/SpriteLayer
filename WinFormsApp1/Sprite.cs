using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpriteLayer
{
    internal class Sprite
    {
        private Bitmap _lastRender; // cached representation of the final image
        public System.ComponentModel.BindingList<Layer> Layers { get; private set; } // all sheets
        private List<Pixel> _pixels; // all pixels in the final image
        public Size SpriteFrameSize { get; set; }

        public delegate void RenderUpdateDelegate(Bitmap render);

        private event RenderUpdateDelegate? RenderUpdate;

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
            _pixels = new List<Pixel>();
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
            var removesheet = void (Pixel p) => p.Layers.Remove(layer);
            ForEachPixelObject(removesheet);
            Render();
        }

        private void AddLayerInternal(Layer sheet)
        {
            // new layers go on top

            var addsheet = (Pixel p) => p.AddLayer(sheet);
            ForEachPixelObject(addsheet);
            Render();
        }

        private void ForEachPixelObject(Action<Pixel> pred)
        {
            var sz = SpriteSheetSize;
            var totalPixels = sz.Width * sz.Height;
            foreach (var pixel in _pixels)
            {
                pred(pixel);
            }
        }

        private void SetupLayeringArray()
        {
            // reset the _pixels
            // ensure they have a position
            // ensure the render image is the correct size

            var sz = SpriteSheetSize;
            var totalPixels = sz.Width * sz.Height;
            Point p = new Point(0, 0);

            _pixels = new List<Pixel>(totalPixels);

            for (int i = 0; i < totalPixels; i++)
            {
                var pixel = new Pixel(p);
                _pixels.Add(pixel);
                p.X++;
                if(p.X == sz.Width)
                {
                    p.X = 0;
                    p.Y++;
                }
            }

            _lastRender = new Bitmap(sz.Width, sz.Height);
        }

        private void ResetPixelLayering()
        {
            var reset = (Pixel p) =>
            {
                p.Layers.Clear();
                p.Layers.AddRange(Layers);
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

        public Pixel GetPixel(Point p)
        {
            var index = (p.Y * SpriteSheetSize.Width) + p.X;
            return _pixels[index];
        }

        private void Render()
        {
            var sz = SpriteSheetSize;
            // render the final image
            using Graphics g = Graphics.FromImage(_lastRender);
            g.FillRectangle(Brushes.Black, 0, 0, _lastRender.Width, _lastRender.Height);
            int i = 0;
            for (int y = 0; y < sz.Height; y++)
            {
                for(int x = 0; x < sz.Width; x++)
                {
                    _lastRender.SetPixel(x, y, _pixels[i].Color);
                    i++;
                }
            }

            if(RenderUpdate != null)
            {
                RenderUpdate(_lastRender);
            }
        }

        public List<Layer> PixelLayers(Point p)
        {
            var index = (p.Y * SpriteSheetSize.Width) + p.X;
            return _pixels[index].Layers;
        }
    }
}
