using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpriteLayer
{
    /// <summary>
    /// A layer is a representation of a sprite sheet, optionally with a user-defined shade applied to it.
    /// 
    /// A layer is mostly immutable as long as the application is running.
    /// </summary>
    public class Layer : IDisposable, IReadOnlyLayer
    {
        private Bitmap _spriteSheet;
        public Bitmap SpriteSheet { get { return _spriteSheet; } }
        public string SheetFile { get; private set; }
        public string Name { get; private set; }
        public Color Shade { get; set; }

        private byte[] _imageData; // format is ARGB
        private int _sheetWidth;

        public Size Size
        {
            get
            {
                return SpriteSheet?.Size ?? Size.Empty;
            }
        }

        public Layer(string file)
        {
            _spriteSheet = new Bitmap(file);
            Name = System.IO.Path.GetFileName(file);
            SheetFile = file;
            Shade = Color.White;
            _sheetWidth = SpriteSheet.Width;


            // extract pixel data
            // (C# doesn't let us put this in another function and not violate ctor init, sadly)
            // this takes up more ram, storing the data twice, but massively speeds up pixel operations

            var fullSheetBounds = new Rectangle(0, 0, SpriteSheet.Width, SpriteSheet.Height);
            var bitmapData = _spriteSheet.LockBits(fullSheetBounds, System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            var bytes = Math.Abs(bitmapData.Stride) * bitmapData.Height;
            _imageData = new byte[bytes];
            var dataPtr = bitmapData.Scan0;
            System.Runtime.InteropServices.Marshal.Copy(dataPtr, _imageData, 0, bytes);
            _spriteSheet.UnlockBits(bitmapData);
        }

        public Color this[Point p]
        {
            get
            {
                var pixelIndex = ((p.Y * _sheetWidth) + p.X) * 4;
                float b = _imageData[0 + pixelIndex] / 255.0F;
                float g = _imageData[1 + pixelIndex] / 255.0F;
                float r = _imageData[2 + pixelIndex] / 255.0F;
                float a = _imageData[3 + pixelIndex] / 255.0F;

                r *= (Shade.R / 255.0F);
                g *= (Shade.G / 255.0F);
                b *= (Shade.B / 255.0F);
                a *= (Shade.A / 255.0F);

                Color retColor = Color.FromArgb((int)(a * 255), (int)(r * 255), (int)(g * 255), (int)(b * 255));
                return retColor;
            }
        }
        
        public override string ToString()
        {
            return Name ?? "<no name>";
        }

        public void Dispose()
        {
            ((IDisposable)SpriteSheet).Dispose();
        }
    }
}
