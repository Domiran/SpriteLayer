namespace SpriteLayer.Forms
{
    partial class LayeredSpriteEditor
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileNewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileOpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSaveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportFinalSheetMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sheetMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layerMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.layerMenuAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.layerMenuSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.layerMenuRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.layerMenuSetShade = new System.Windows.Forms.ToolStripMenuItem();
            this.layerMenuSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.layerMenuMoveUp = new System.Windows.Forms.ToolStripMenuItem();
            this.layerMenuMoveDown = new System.Windows.Forms.ToolStripMenuItem();
            this.viewsTabs = new System.Windows.Forms.TabControl();
            this.tabLayers = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.layerList = new System.Windows.Forms.ListBox();
            this.layerView = new System.Windows.Forms.PictureBox();
            this.tabFinalSheet = new System.Windows.Forms.TabPage();
            this.finalSheetView = new SpriteLayer.Forms.NearestNeighorPictureBox();
            this.tabFrame = new System.Windows.Forms.TabPage();
            this.frameView = new SpriteLayer.Forms.NearestNeighorPictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.frameSizeApply = new System.Windows.Forms.Button();
            this.frameSizeHeightField = new System.Windows.Forms.NumericUpDown();
            this.frameSizeWidthField = new System.Windows.Forms.NumericUpDown();
            this.frameSizeLabel = new System.Windows.Forms.Label();
            this.frameNumberLabel = new System.Windows.Forms.Label();
            this.frameNumberField = new System.Windows.Forms.NumericUpDown();
            this.menuStrip1.SuspendLayout();
            this.layerMenu.SuspendLayout();
            this.viewsTabs.SuspendLayout();
            this.tabLayers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layerView)).BeginInit();
            this.tabFinalSheet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.finalSheetView)).BeginInit();
            this.tabFrame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.frameView)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.frameSizeHeightField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.frameSizeWidthField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.frameNumberField)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.sheetMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileNewMenuItem,
            this.fileOpenMenuItem,
            this.fileSaveMenuItem,
            this.fileSaveAsMenuItem,
            this.exportFinalSheetMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileMenuItem.Text = "&File";
            // 
            // fileNewMenuItem
            // 
            this.fileNewMenuItem.Name = "fileNewMenuItem";
            this.fileNewMenuItem.Size = new System.Drawing.Size(177, 22);
            this.fileNewMenuItem.Text = "&New";
            this.fileNewMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // fileOpenMenuItem
            // 
            this.fileOpenMenuItem.Name = "fileOpenMenuItem";
            this.fileOpenMenuItem.Size = new System.Drawing.Size(177, 22);
            this.fileOpenMenuItem.Text = "&Open...";
            this.fileOpenMenuItem.Visible = false;
            // 
            // fileSaveMenuItem
            // 
            this.fileSaveMenuItem.Name = "fileSaveMenuItem";
            this.fileSaveMenuItem.Size = new System.Drawing.Size(177, 22);
            this.fileSaveMenuItem.Text = "&Save";
            this.fileSaveMenuItem.Visible = false;
            // 
            // fileSaveAsMenuItem
            // 
            this.fileSaveAsMenuItem.Name = "fileSaveAsMenuItem";
            this.fileSaveAsMenuItem.Size = new System.Drawing.Size(177, 22);
            this.fileSaveAsMenuItem.Text = "Save &As...";
            this.fileSaveAsMenuItem.Visible = false;
            // 
            // exportFinalSheetMenuItem
            // 
            this.exportFinalSheetMenuItem.Name = "exportFinalSheetMenuItem";
            this.exportFinalSheetMenuItem.Size = new System.Drawing.Size(177, 22);
            this.exportFinalSheetMenuItem.Text = "Export Final Sheet...";
            this.exportFinalSheetMenuItem.Click += new System.EventHandler(this.exportFinalSheetMenuItem_Click);
            // 
            // sheetMenuItem
            // 
            this.sheetMenuItem.DropDown = this.layerMenu;
            this.sheetMenuItem.Name = "sheetMenuItem";
            this.sheetMenuItem.Size = new System.Drawing.Size(47, 20);
            this.sheetMenuItem.Text = "Layer";
            // 
            // layerMenu
            // 
            this.layerMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.layerMenuAdd,
            this.layerMenuSep1,
            this.layerMenuRemove,
            this.layerMenuSetShade,
            this.layerMenuSep2,
            this.layerMenuMoveUp,
            this.layerMenuMoveDown});
            this.layerMenu.Name = "layerMenu";
            this.layerMenu.Size = new System.Drawing.Size(139, 126);
            // 
            // layerMenuAdd
            // 
            this.layerMenuAdd.Name = "layerMenuAdd";
            this.layerMenuAdd.Size = new System.Drawing.Size(138, 22);
            this.layerMenuAdd.Text = "Add Layer...";
            // 
            // layerMenuSep1
            // 
            this.layerMenuSep1.Name = "layerMenuSep1";
            this.layerMenuSep1.Size = new System.Drawing.Size(135, 6);
            // 
            // layerMenuRemove
            // 
            this.layerMenuRemove.Name = "layerMenuRemove";
            this.layerMenuRemove.Size = new System.Drawing.Size(138, 22);
            this.layerMenuRemove.Text = "Remove";
            // 
            // layerMenuSetShade
            // 
            this.layerMenuSetShade.Name = "layerMenuSetShade";
            this.layerMenuSetShade.Size = new System.Drawing.Size(138, 22);
            this.layerMenuSetShade.Text = "Set Shade...";
            // 
            // layerMenuSep2
            // 
            this.layerMenuSep2.Name = "layerMenuSep2";
            this.layerMenuSep2.Size = new System.Drawing.Size(135, 6);
            // 
            // layerMenuMoveUp
            // 
            this.layerMenuMoveUp.Name = "layerMenuMoveUp";
            this.layerMenuMoveUp.Size = new System.Drawing.Size(138, 22);
            this.layerMenuMoveUp.Text = "Move Up";
            this.layerMenuMoveUp.ToolTipText = "Move the global ordering of the selected  layer up.\r\n\r\nThis resets custom order o" +
    "n every pixel.";
            // 
            // layerMenuMoveDown
            // 
            this.layerMenuMoveDown.Name = "layerMenuMoveDown";
            this.layerMenuMoveDown.Size = new System.Drawing.Size(138, 22);
            this.layerMenuMoveDown.Text = "Move Down";
            this.layerMenuMoveDown.ToolTipText = "Move the global ordering of the selected  layer down.\r\n\r\nThis resets custom order" +
    " on every pixel.\r\n";
            // 
            // viewsTabs
            // 
            this.viewsTabs.Controls.Add(this.tabLayers);
            this.viewsTabs.Controls.Add(this.tabFinalSheet);
            this.viewsTabs.Controls.Add(this.tabFrame);
            this.viewsTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewsTabs.Location = new System.Drawing.Point(0, 24);
            this.viewsTabs.Name = "viewsTabs";
            this.viewsTabs.SelectedIndex = 0;
            this.viewsTabs.Size = new System.Drawing.Size(800, 426);
            this.viewsTabs.TabIndex = 1;
            // 
            // tabLayers
            // 
            this.tabLayers.Controls.Add(this.splitContainer1);
            this.tabLayers.Location = new System.Drawing.Point(4, 24);
            this.tabLayers.Name = "tabLayers";
            this.tabLayers.Padding = new System.Windows.Forms.Padding(3);
            this.tabLayers.Size = new System.Drawing.Size(792, 398);
            this.tabLayers.TabIndex = 0;
            this.tabLayers.Text = "Layers";
            this.tabLayers.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.layerList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.layerView);
            this.splitContainer1.Size = new System.Drawing.Size(786, 392);
            this.splitContainer1.SplitterDistance = 168;
            this.splitContainer1.TabIndex = 2;
            // 
            // layerList
            // 
            this.layerList.ContextMenuStrip = this.layerMenu;
            this.layerList.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.layerList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layerList.FormattingEnabled = true;
            this.layerList.ItemHeight = 15;
            this.layerList.Location = new System.Drawing.Point(0, 0);
            this.layerList.Name = "layerList";
            this.layerList.Size = new System.Drawing.Size(168, 392);
            this.layerList.TabIndex = 0;
            // 
            // layerView
            // 
            this.layerView.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.layerView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layerView.Location = new System.Drawing.Point(0, 0);
            this.layerView.Name = "layerView";
            this.layerView.Size = new System.Drawing.Size(614, 392);
            this.layerView.TabIndex = 1;
            this.layerView.TabStop = false;
            // 
            // tabFinalSheet
            // 
            this.tabFinalSheet.Controls.Add(this.finalSheetView);
            this.tabFinalSheet.Location = new System.Drawing.Point(4, 24);
            this.tabFinalSheet.Name = "tabFinalSheet";
            this.tabFinalSheet.Padding = new System.Windows.Forms.Padding(3);
            this.tabFinalSheet.Size = new System.Drawing.Size(792, 398);
            this.tabFinalSheet.TabIndex = 1;
            this.tabFinalSheet.Text = "Final Sheet";
            this.tabFinalSheet.UseVisualStyleBackColor = true;
            // 
            // finalSheetView
            // 
            this.finalSheetView.AllowMove = true;
            this.finalSheetView.AllowScale = true;
            this.finalSheetView.AllowSelectPixel = true;
            this.finalSheetView.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.finalSheetView.Location = new System.Drawing.Point(3, 3);
            this.finalSheetView.Name = "finalSheetView";
            this.finalSheetView.RenderScale = 1F;
            this.finalSheetView.Size = new System.Drawing.Size(67, 57);
            this.finalSheetView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.finalSheetView.TabIndex = 0;
            this.finalSheetView.TabStop = false;
            // 
            // tabFrame
            // 
            this.tabFrame.Controls.Add(this.frameView);
            this.tabFrame.Controls.Add(this.panel1);
            this.tabFrame.Location = new System.Drawing.Point(4, 24);
            this.tabFrame.Name = "tabFrame";
            this.tabFrame.Padding = new System.Windows.Forms.Padding(3);
            this.tabFrame.Size = new System.Drawing.Size(792, 398);
            this.tabFrame.TabIndex = 2;
            this.tabFrame.Text = "Frames";
            this.tabFrame.UseVisualStyleBackColor = true;
            // 
            // frameView
            // 
            this.frameView.AllowMove = false;
            this.frameView.AllowScale = true;
            this.frameView.AllowSelectPixel = false;
            this.frameView.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.frameView.Location = new System.Drawing.Point(3, 66);
            this.frameView.Name = "frameView";
            this.frameView.RenderScale = 1F;
            this.frameView.Size = new System.Drawing.Size(43, 40);
            this.frameView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.frameView.TabIndex = 0;
            this.frameView.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.frameSizeApply);
            this.panel1.Controls.Add(this.frameSizeHeightField);
            this.panel1.Controls.Add(this.frameSizeWidthField);
            this.panel1.Controls.Add(this.frameSizeLabel);
            this.panel1.Controls.Add(this.frameNumberLabel);
            this.panel1.Controls.Add(this.frameNumberField);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(786, 57);
            this.panel1.TabIndex = 1;
            // 
            // frameSizeApply
            // 
            this.frameSizeApply.Location = new System.Drawing.Point(209, 28);
            this.frameSizeApply.Name = "frameSizeApply";
            this.frameSizeApply.Size = new System.Drawing.Size(75, 23);
            this.frameSizeApply.TabIndex = 5;
            this.frameSizeApply.Text = "Apply";
            this.frameSizeApply.UseVisualStyleBackColor = true;
            this.frameSizeApply.Click += new System.EventHandler(this.frameSizeApply_Click);
            // 
            // frameSizeHeightField
            // 
            this.frameSizeHeightField.Location = new System.Drawing.Point(143, 28);
            this.frameSizeHeightField.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.frameSizeHeightField.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.frameSizeHeightField.Name = "frameSizeHeightField";
            this.frameSizeHeightField.Size = new System.Drawing.Size(60, 23);
            this.frameSizeHeightField.TabIndex = 4;
            this.frameSizeHeightField.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // frameSizeWidthField
            // 
            this.frameSizeWidthField.Location = new System.Drawing.Point(77, 28);
            this.frameSizeWidthField.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.frameSizeWidthField.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.frameSizeWidthField.Name = "frameSizeWidthField";
            this.frameSizeWidthField.Size = new System.Drawing.Size(60, 23);
            this.frameSizeWidthField.TabIndex = 3;
            this.frameSizeWidthField.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // frameSizeLabel
            // 
            this.frameSizeLabel.AutoSize = true;
            this.frameSizeLabel.Location = new System.Drawing.Point(5, 28);
            this.frameSizeLabel.Name = "frameSizeLabel";
            this.frameSizeLabel.Size = new System.Drawing.Size(66, 15);
            this.frameSizeLabel.TabIndex = 2;
            this.frameSizeLabel.Text = "Frame Size:";
            // 
            // frameNumberLabel
            // 
            this.frameNumberLabel.AutoSize = true;
            this.frameNumberLabel.Location = new System.Drawing.Point(5, 3);
            this.frameNumberLabel.Name = "frameNumberLabel";
            this.frameNumberLabel.Size = new System.Drawing.Size(43, 15);
            this.frameNumberLabel.TabIndex = 1;
            this.frameNumberLabel.Text = "Frame:";
            // 
            // frameNumberField
            // 
            this.frameNumberField.Location = new System.Drawing.Point(77, 1);
            this.frameNumberField.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.frameNumberField.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.frameNumberField.Name = "frameNumberField";
            this.frameNumberField.Size = new System.Drawing.Size(60, 23);
            this.frameNumberField.TabIndex = 0;
            this.frameNumberField.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.frameNumberField.ValueChanged += new System.EventHandler(this.frameNumberField_ValueChanged);
            // 
            // LayeredSpriteEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.viewsTabs);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "LayeredSpriteEditor";
            this.Text = "Pixel Layer";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.layerMenu.ResumeLayout(false);
            this.viewsTabs.ResumeLayout(false);
            this.tabLayers.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layerView)).EndInit();
            this.tabFinalSheet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.finalSheetView)).EndInit();
            this.tabFrame.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.frameView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.frameSizeHeightField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.frameSizeWidthField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.frameNumberField)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileMenuItem;
        private ToolStripMenuItem fileNewMenuItem;
        private ToolStripMenuItem fileOpenMenuItem;
        private ToolStripMenuItem fileSaveMenuItem;
        private ToolStripMenuItem fileSaveAsMenuItem;
        private TabControl viewsTabs;
        private TabPage tabLayers;
        private TabPage tabFinalSheet;
        private ListBox layerList;
        private Forms.NearestNeighorPictureBox finalSheetView;
        private SplitContainer splitContainer1;
        private TabPage tabFrame;
        private Forms.NearestNeighorPictureBox frameView;
        private ToolStripMenuItem sheetMenuItem;
        private PictureBox layerView;
        private ToolStripMenuItem exportFinalSheetMenuItem;
        private Panel panel1;
        private NumericUpDown frameSizeHeightField;
        private NumericUpDown frameSizeWidthField;
        private Label frameSizeLabel;
        private Label frameNumberLabel;
        private NumericUpDown frameNumberField;
        private Button frameSizeApply;
        private ContextMenuStrip layerMenu;
        private ToolStripMenuItem layerMenuAdd;
        private ToolStripSeparator layerMenuSep1;
        private ToolStripMenuItem layerMenuRemove;
        private ToolStripMenuItem layerMenuSetShade;
        private ToolStripSeparator layerMenuSep2;
        private ToolStripMenuItem layerMenuMoveUp;
        private ToolStripMenuItem layerMenuMoveDown;
    }
}