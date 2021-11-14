using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpriteLayer
{
    /// <summary>
    /// Enforces the constraint that Pixel should not need to modify the contents of a Layer.
    /// </summary>
    public interface IReadOnlyLayer
    {
        Color this[Point p] { get; }
        string Name { get; }
        Color Shade { get; }
        Bitmap SpriteSheet { get; }
    }
}
