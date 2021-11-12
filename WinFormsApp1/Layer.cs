using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpriteLayer
{
    internal class Layer
    {
        public Bitmap SpriteSheet { get; private set; }
        public string? SheetFile { get; private set; } = null;
        public string? Name { get; set; } = null;
        public Color Shade { get; set; }
        public Size Size
        {
            get
            {
                return SpriteSheet?.Size ?? Size.Empty;
            }
        }

        public Layer(string file)
        {
            SpriteSheet = new Bitmap(file);
            Name = System.IO.Path.GetFileName(file);
            SheetFile = file;
            Shade = Color.White;
        }

        public Color this[Point p]
        {
            get
            {
                var origColor = SpriteSheet.GetPixel(p.X, p.Y);
                float r = origColor.R / 255.0F;
                float g = origColor.G / 255.0F;
                float b = origColor.B / 255.0F;
                float a = origColor.A / 255.0F;

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
    }
}
