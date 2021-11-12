namespace SpriteLayer.Forms
{
    partial class PixelLayerView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.layerList = new System.Windows.Forms.ListBox();
            this.acceptPanel = new System.Windows.Forms.Panel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.editPanel = new System.Windows.Forms.Panel();
            this.layerDown = new System.Windows.Forms.Button();
            this.layerUp = new System.Windows.Forms.Button();
            this.acceptPanel.SuspendLayout();
            this.editPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // layerList
            // 
            this.layerList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layerList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.layerList.FormattingEnabled = true;
            this.layerList.ItemHeight = 15;
            this.layerList.Location = new System.Drawing.Point(0, 38);
            this.layerList.Name = "layerList";
            this.layerList.Size = new System.Drawing.Size(384, 682);
            this.layerList.TabIndex = 0;
            // 
            // acceptPanel
            // 
            this.acceptPanel.Controls.Add(this.cancelButton);
            this.acceptPanel.Controls.Add(this.okButton);
            this.acceptPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.acceptPanel.Location = new System.Drawing.Point(0, 720);
            this.acceptPanel.Name = "acceptPanel";
            this.acceptPanel.Size = new System.Drawing.Size(384, 41);
            this.acceptPanel.TabIndex = 1;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cancelButton.Location = new System.Drawing.Point(195, 8);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(71, 25);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.okButton.Location = new System.Drawing.Point(118, 8);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(71, 25);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // editPanel
            // 
            this.editPanel.Controls.Add(this.layerDown);
            this.editPanel.Controls.Add(this.layerUp);
            this.editPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.editPanel.Location = new System.Drawing.Point(0, 0);
            this.editPanel.Name = "editPanel";
            this.editPanel.Size = new System.Drawing.Size(384, 38);
            this.editPanel.TabIndex = 2;
            // 
            // layerDown
            // 
            this.layerDown.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.layerDown.Location = new System.Drawing.Point(197, 9);
            this.layerDown.Name = "layerDown";
            this.layerDown.Size = new System.Drawing.Size(71, 23);
            this.layerDown.TabIndex = 1;
            this.layerDown.Text = "Down";
            this.layerDown.UseVisualStyleBackColor = true;
            this.layerDown.Click += new System.EventHandler(this.layerDown_Click);
            // 
            // layerUp
            // 
            this.layerUp.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.layerUp.Location = new System.Drawing.Point(116, 9);
            this.layerUp.Name = "layerUp";
            this.layerUp.Size = new System.Drawing.Size(75, 23);
            this.layerUp.TabIndex = 0;
            this.layerUp.Text = "Up";
            this.layerUp.UseVisualStyleBackColor = true;
            this.layerUp.Click += new System.EventHandler(this.layerUp_Click);
            // 
            // PixelLayerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 761);
            this.ControlBox = false;
            this.Controls.Add(this.layerList);
            this.Controls.Add(this.editPanel);
            this.Controls.Add(this.acceptPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "PixelLayerView";
            this.Text = "Selected Pixel(s) Layers";
            this.acceptPanel.ResumeLayout(false);
            this.editPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ListBox layerList;
        private Panel acceptPanel;
        private Button cancelButton;
        private Button okButton;
        private Panel editPanel;
        private Button layerDown;
        private Button layerUp;
    }
}