using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpriteLayer
{
    /// <summary>
    /// Pixel is a representation of a pixel in the final rendered image.
    /// 
    /// Each Pixel can have its own own Layer order.
    /// </summary>
    public struct Pixel
    {
        /// <summary>
        /// Location of this pixel.
        /// </summary>
        private Point _position;

        /// <summary>
        /// Layers of this pixel, with implied ordering (index 0 is first layer).
        /// </summary>
        private List<IReadOnlyLayer> _layers;
        public Color Color { get; private set; }

        public Pixel(Point pos)
        {
            Color = Color.White;
            _position = pos;
            _layers = new List<IReadOnlyLayer>();
        }
        
        public Pixel(int x, int y)
        {
            Color = Color.White;
            _position = new Point(x, y);
            _layers = new List<IReadOnlyLayer>();
        }

        public void CalculateColor()
        {
            if (_layers.Count == 0)
            {
                Color = Color.Transparent;
            }

            else
            {
                foreach (var layer in _layers)
                {
                    var color = layer[_position];
                    if (color.A > 0)
                    {
                        Color = color;
                        return;
                    }
                }
               Color = Color.Transparent;
            }
        }

        public void AddLayer(IReadOnlyLayer layer)
        {
            // new layers go on top
            _layers.Insert(0, layer);
            CalculateColor();
        }

        // returns true if we moved
        public bool MoveLayerDown(int index)
        {
            var nextIndex = index + 1;
            if (nextIndex < _layers.Count)
            {
                // this resets all custom pixel ordering
 
                var temp = _layers[nextIndex];
                _layers[nextIndex] = _layers[index];
                _layers[index] = temp;
                CalculateColor();
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

                var temp = _layers[prevIndex];
                _layers[prevIndex] = _layers[index];
                _layers[index] = temp;
                CalculateColor();
                return true;
            }
            return false;
        }

        public void SetLayers(List<IReadOnlyLayer> layers)
        {
            _layers.Clear();
            _layers.AddRange(layers);
            CalculateColor();
        }
        
        public void SetLayers(List<Layer> layers)
        {
            _layers.Clear();
            _layers.AddRange(layers);
            CalculateColor();
        }
        
        public void SetLayers(System.ComponentModel.BindingList<Layer> layers)
        {
            _layers.Clear();
            _layers.AddRange(layers);
            CalculateColor();
        }

        public void RemoveLayer(IReadOnlyLayer layer)
        {
            _layers.Remove(layer);
            CalculateColor();
        }

        public IReadOnlyList<IReadOnlyLayer> Layers
        {
            get
            {
                return _layers;
            }
        }
    }
}
