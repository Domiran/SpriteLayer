using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpriteLayer
{
    internal class Pixel
    {
        public Point Position { get; private set; } // location of this pixel
        public List<Layer> Layers { get; private set; } // layers of this pixel, with implied ordering

        public Pixel(Point pos)
        {
            Position = pos;
            Layers = new List<Layer>();
        }

        public void AddLayer(Layer layer)
        {
            // new layers go on top
            Layers.Insert(0, layer);
        }

        public Color Color
        {
            get
            {
                // 0 is top-most layer
                if(Layers.Count == 0)
                    return Color.White;

                foreach (var layer in Layers)
                {
                    var color = layer[Position];
                    if (color.A > 0)
                        return color;
                }
                return Color.Transparent;
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
                return true;
            }
            return false;
        }
    }
}
