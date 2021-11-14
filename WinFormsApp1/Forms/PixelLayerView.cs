using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpriteLayer.Forms
{
    public partial class PixelLayerView : Form
    {
        private List<Pixel> _editingPixels;
        private List<List<IReadOnlyLayer>> _originalLayers;
        private List<IReadOnlyLayer> _layers;
        private Action _renderUpdate;

        public PixelLayerView(List<Pixel> editingPixels, Action renderUpdate)
        {
            InitializeComponent();
            layerList.DrawItem += LayerList_DrawItem;
            _renderUpdate = renderUpdate;
            layerList.ItemHeight = 255;

            // preserve all original layer setups because we mangle them during this
            _originalLayers = new List<List<IReadOnlyLayer>>(editingPixels.Count);
            for(int i = 0; i < editingPixels.Count; i++)
            {
                var list = new List<IReadOnlyLayer>();
                list.AddRange(editingPixels[i].Layers);
                _originalLayers.Add(list);
            }

            // set up the layer list we'll work with
            // we'll use the first pixel selected as the list we use
            // we force all pixels to have a uniform ordering, ruining any custom ordering they had, but there's no good solution
            _layers = new List<IReadOnlyLayer>();
            foreach (var layer in editingPixels[0].Layers)
            {
                _layers.Add(layer);
            }

            layerList.DataSource = _layers;
            _editingPixels = editingPixels;
        }

        private void LayerList_DrawItem(object? sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();
            e.Graphics.DrawString(_layers[e.Index].Name, layerList.Font, new SolidBrush(this.ForeColor), e.Bounds.Left, e.Bounds.Top);
            var destRect = e.Bounds;
            destRect.Y += 15;
            destRect.Height -= 15;
            e.Graphics.DrawImage(_layers[e.Index].SpriteSheet, destRect, new Rectangle(new Point(0, 0), _layers[e.Index].SpriteSheet.Size), GraphicsUnit.Pixel);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _editingPixels.Count; i++)
            {
                _editingPixels[i].SetLayers(_originalLayers[i]);
            }
            _renderUpdate();
            this.Close();
        }

        private void layerUp_Click(object sender, EventArgs e)
        {
            var index = layerList.SelectedIndex;
            var prevIndex = index - 1;
            if (prevIndex >= 0)
            {
                var temp = _layers[prevIndex];
                _layers[prevIndex] = _layers[index];
                _layers[index] = temp;
                layerList.SelectedIndex = prevIndex;
                UpdateAllPixelLayers(_layers);
                _renderUpdate();
                layerList.DataSource = null;
                layerList.DataSource = _layers;
            }
        }

        private void layerDown_Click(object sender, EventArgs e)
        {
            var index = layerList.SelectedIndex;
            var nextIndex = index + 1;
            if (nextIndex < _layers.Count)
            {
                var temp = _layers[nextIndex];
                _layers[nextIndex] = _layers[index];
                _layers[index] = temp;
                layerList.SelectedIndex = nextIndex;
                UpdateAllPixelLayers(_layers);
                _renderUpdate();
                layerList.DataSource = null;
                layerList.DataSource = _layers;
            }
        }

        void UpdateAllPixelLayers(List<IReadOnlyLayer> layers)
        {
            foreach (var pixel in _editingPixels)
            {
                pixel.SetLayers(layers);
            }
        }
    }
}
