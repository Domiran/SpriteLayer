namespace SpriteLayer.Forms
{
    public partial class LayeredSpriteEditor : Form
    {
        Sprite _layeredSprite;
        bool _suspendSheetChange;
        float _renderViewScale = 1.0f;
        const float RenderScaleDelta = 0.0005f;

        public LayeredSpriteEditor()
        {
            InitializeComponent();

            _suspendSheetChange = false;

            layerList.SelectedIndexChanged += layerList_SelectedIndexChanged;

            layerMenu.Opening += LayerMenu_Opening;
            layerMenuAdd.Click += layerAddMenuItem_Click;
            layerMenuMoveUp.Click += layerMenuMoveUp_Click;
            layerMenuMoveDown.Click += layerMenuMoveDown_Click;
            layerMenuRemove.Click += layerRemoveMenuItem_Click;
            layerMenuSetShade.Click += LayerMenuSetShade_Click;

            _layeredSprite = new Sprite(_layeredSprite_RenderUpdate);
            layerList.DataSource = _layeredSprite.Layers;
            finalSheetView.MouseClick += finalSheetView_MouseClick;
            tabFinalSheet.MouseWheel += finalSheetView_MouseWheel;
        }

        private Layer? SelectedLayer
        {
            get
            {
                return (layerList.SelectedItem as Layer);
            }
        }

        private int SelectedLayerIndex
        {
            get
            {
                return layerList.SelectedIndex;
            }
            set
            {
                if ((value >= 0) && (value < _layeredSprite.Layers.Count))
                {
                    var layer = _layeredSprite.Layers[value];
                    layerList.SelectedIndex = value;
                    layerView.Image = layer.SpriteSheet;
                }
                else
                {
                    layerList.SelectedIndex = -1;
                    layerView.Image = null;
                }
            }
        }

        private int ViewingFrameIndex
        {
            get
            {
                // -1 because the UI shows 1 to max, but internally it's 0-based
                return ((int)frameNumberField.Value) - 1;
            }
        }

        private Size ViewingFrameSize
        {
            get
            {
                return new Size((int)frameSizeWidthField.Value, (int)frameSizeHeightField.Value);
            }
        }

        private void LayerMenu_Opening(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            // determine what actions can be done by the control selection
            var hasSelection = layerList.SelectedItem != null;

            // add layer can always be done
            layerMenuMoveUp.Enabled = hasSelection;
            layerMenuMoveDown.Enabled = hasSelection;
            layerMenuRemove.Enabled = hasSelection;
            layerMenuSetShade.Enabled = hasSelection;
        }

        private void LayerMenuSetShade_Click(object? sender, EventArgs e)
        {
            var layer = SelectedLayer;
            var dlg = new ColorDialog();
            dlg.Color = layer!.Shade;
            var res = dlg.ShowDialog(); 
            if(res == DialogResult.OK)
            {
                _layeredSprite.SetLayerShade(layer!, dlg.Color);
            }
        }

        private void finalSheetView_MouseClick(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var pos = e.Location;
                var pixels = new List<Pixel>(finalSheetView.SelectedPoints.Count);
                foreach (var point in finalSheetView.SelectedPoints)
                {
                    pixels.Add(_layeredSprite[point]);
                }
                using var frm = new PixelLayerView(pixels, _layeredSprite.Refresh);
                frm.Location = pos;
                frm.ShowDialog(this);
            }
        }

        private void UpdateRenderViewScale()
        {
            finalSheetView.Width = (int)(_renderViewScale * _layeredSprite.SpriteSheetSize.Width);
            finalSheetView.Height = (int)(_renderViewScale * _layeredSprite.SpriteSheetSize.Height);
        }

        private void finalSheetView_MouseWheel(object? sender, MouseEventArgs e)
        {
            finalSheetView.ExternalWheel(e);
        }

        private void layerRemoveMenuItem_Click(object? sender, EventArgs e)
        {
            var index = SelectedLayerIndex;
            var res = MessageBox.Show(this, $"Are you sure you want to remove the selected layer, \"{SelectedLayer!.Name}\"?", "Remove Layer", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                _layeredSprite.RemoveLayer(SelectedLayer!); // menu won't show if it's null
                if (index < _layeredSprite.Layers.Count)
                {
                    SelectedLayerIndex = index;
                }
            }
        }

        private void layerMenuMoveDown_Click(object? sender, EventArgs e)
        {
            var index = SelectedLayerIndex;
            _suspendSheetChange = true;
            bool ok = _layeredSprite.MoveLayerDown(index);
            _suspendSheetChange = false;
            if (ok) SelectedLayerIndex = index + 1;
        }

        private void layerMenuMoveUp_Click(object? sender, EventArgs e)
        {
            var index = SelectedLayerIndex;
            _suspendSheetChange = true;
            bool ok = _layeredSprite.MoveLayerUp(index);
            _suspendSheetChange = false;
            if(ok) SelectedLayerIndex = index - 1;
        }

        private void _layeredSprite_RenderUpdate(Bitmap render)
        {
            finalSheetView.Image = render;
        }

        private void layerList_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_suspendSheetChange) return;
            var sheet = SelectedLayer;
            layerView.Image = sheet?.SpriteSheet;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _layeredSprite?.Dispose();
            _layeredSprite = new Sprite(_layeredSprite_RenderUpdate);
        }

        private void layerAddMenuItem_Click(object? sender, EventArgs e)
        {
            var x = new OpenFileDialog();
            x.Filter = "Image Files (*.png; *.bmp; *.jpg; *.tga)|*.png; *.bmp; *.jpg; *.tga|All Files (*.*)|*.*";
            var res = x.ShowDialog();
            if(res == DialogResult.OK)
            {

                var found = _layeredSprite.Layers.Any(s => s.SheetFile == x.FileName);

                if (found)
                {
                    res = MessageBox.Show(this, $"A sheet for {x.FileName} already exists. Are you sure you want to add it again?", "Add Sheet", MessageBoxButtons.YesNo);
                    if (res == DialogResult.No)
                        return;
                }

                var layer = new Layer(x.FileName);
                try
                {
                    _layeredSprite.AddLayer(layer);
                    UpdateRenderViewScale();
                    SelectedLayerIndex = 0;
                    frameNumberField.Maximum = _layeredSprite.TotalFrames; // could have changed if we added first sheet
                    layerView.Image = layer.SpriteSheet;
                }
                catch (ArgumentException aex)
                {
                    MessageBox.Show(this, aex.Message, "Add Sheet");
                }
            }
        }

        private void exportFinalSheetMenuItem_Click(object sender, EventArgs e)
        {
            var x = new SaveFileDialog();
            x.Filter = "Image Files (*.png; *.bmp; *.jpg; *.tga)|*.png; *.bmp; *.jpg; *.tga|All Files (*.*)|*.*";
            var res = x.ShowDialog();
            if (res == DialogResult.OK)
            {
                try
                {
                    // we only need to assume the sheet view we're looking at is up-to-date, and it should be
                    finalSheetView.Image.Save(x.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, $"Couldn't save \"{x.FileName}\":\n\n{ex.Message}");
                }
            }
        }

        private void frameNumberField_ValueChanged(object sender, EventArgs e)
        {
            if (_layeredSprite != null)
            {
                frameView.Image = _layeredSprite.GetFrame(((int)frameNumberField.Value));
            }
        }

        private void frameSizeApply_Click(object sender, EventArgs e)
        {
            if (_layeredSprite != null)
            {
                var newSize = ViewingFrameSize;
                _layeredSprite.SpriteFrameSize = newSize;
                frameView.Image = _layeredSprite.GetFrame(ViewingFrameIndex);
                frameView.Size = newSize;
                frameView.RenderScale = 1.0f;
                frameNumberField.Maximum = _layeredSprite.TotalFrames;
            }
        }
    }
}
