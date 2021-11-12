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

            layerList.SelectedIndexChanged += SheetList_SelectedIndexChanged;
            layerMoveLayerUpMenuItem.Click += MoveUpToolStripMenuItem_Click;
            layerMoveLayerDownMenuItem.Click += MoveDownToolStripMenuItem_Click;
            layerRemoveMenuItem.Click += layerRemoveMenuItem_Click;

            _layeredSprite = new Sprite(_layeredSprite_RenderUpdate);
            layerList.DataSource = _layeredSprite.Layers;
            finalSheetView.MouseClick += FinalSheetView_MouseClick;
            tabFinalSheet.MouseWheel += finalSheetView_MouseWheel;
        }

        private void FinalSheetView_MouseClick(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var pos = e.Location;
                var pixels = new List<Pixel>(finalSheetView.SelectedPoints.Count);
                foreach (var point in finalSheetView.SelectedPoints)
                {
                    pixels.Add(_layeredSprite.GetPixel(point));
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
            var layer = (Layer)layerList.SelectedItem;
            _layeredSprite.RemoveLayer(layer);
        }

        private void MoveDownToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            var index = layerList.SelectedIndex;
            _suspendSheetChange = true;
            bool ok = _layeredSprite.MoveLayerDown(index);
            _suspendSheetChange = false;
            if (ok) layerList.SelectedIndex = index + 1;
        }

        private void MoveUpToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            var index = layerList.SelectedIndex;
            _suspendSheetChange = true;
            bool ok = _layeredSprite.MoveLayerUp(index);
            _suspendSheetChange = false;
            if(ok) layerList.SelectedIndex = index - 1;
        }

        private void _layeredSprite_RenderUpdate(Bitmap render)
        {
            finalSheetView.Image = render;
        }

        private void SheetList_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_suspendSheetChange) return;
            var sheet = (Layer)layerList.SelectedItem;
            layerView.Image = sheet?.SpriteSheet;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _layeredSprite = new Sprite(_layeredSprite_RenderUpdate);
        }

        private void layerAddMenuItem_Click(object sender, EventArgs e)
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

                var sheet = new Layer(x.FileName);
                try
                {
                    _layeredSprite.AddLayer(sheet);
                    UpdateRenderViewScale();
                    layerList.SelectedItem = sheet;
                    frameNumberField.Maximum = _layeredSprite.TotalFrames; // could have changed if we added first sheet
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
                _layeredSprite.SpriteFrameSize = new Size(((int)frameSizeWidthField.Value), ((int)frameSizeHeightField.Value));
                frameView.Image = _layeredSprite.GetFrame(((int)frameNumberField.Value) - 1); // -1 because the UI shows 1 to max, but internally it's 0-based
                frameNumberField.Maximum = _layeredSprite.TotalFrames;
            }
        }
    }
}
