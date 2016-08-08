namespace SpriteSheetDecomposer
{
    partial class MainWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.scrollImagePangel = new System.Windows.Forms.Panel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.zoomInBtn = new System.Windows.Forms.Button();
            this.zoomOutBtn = new System.Windows.Forms.Button();
            this.restoreBtn = new System.Windows.Forms.Button();
            this.loadXmlBtn = new System.Windows.Forms.Button();
            this.deleteBtn = new System.Windows.Forms.Button();
            this.sliceBtn = new System.Windows.Forms.Button();
            this.openSpriteSheetBtn = new System.Windows.Forms.Button();
            this.splitBtn = new System.Windows.Forms.Button();
            this.mergeBtn = new System.Windows.Forms.Button();
            this.exportBtn = new System.Windows.Forms.Button();
            this.saveXmlBtn = new System.Windows.Forms.Button();
            this.menuTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.exportName = new System.Windows.Forms.TextBox();
            this.groupName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.namePanel = new System.Windows.Forms.Panel();
            this.scrollImagePangel.SuspendLayout();
            this.namePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // scrollImagePangel
            // 
            this.scrollImagePangel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scrollImagePangel.AutoScroll = true;
            this.scrollImagePangel.Controls.Add(this.progressBar);
            this.scrollImagePangel.Location = new System.Drawing.Point(80, 90);
            this.scrollImagePangel.Name = "scrollImagePangel";
            this.scrollImagePangel.Size = new System.Drawing.Size(884, 0);
            this.scrollImagePangel.TabIndex = 9;
            this.scrollImagePangel.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.scrollImagePangel_MouseWheel);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.progressBar.Location = new System.Drawing.Point(221, 464);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(451, 63);
            this.progressBar.TabIndex = 15;
            this.progressBar.UseWaitCursor = true;
            this.progressBar.Visible = false;
            // 
            // zoomInBtn
            // 
            this.zoomInBtn.BackColor = System.Drawing.Color.White;
            this.zoomInBtn.Enabled = false;
            this.zoomInBtn.ForeColor = System.Drawing.Color.Coral;
            this.zoomInBtn.Image = ((System.Drawing.Image)(resources.GetObject("zoomInBtn.Image")));
            this.zoomInBtn.Location = new System.Drawing.Point(878, 12);
            this.zoomInBtn.Name = "zoomInBtn";
            this.zoomInBtn.Size = new System.Drawing.Size(75, 63);
            this.zoomInBtn.TabIndex = 4;
            this.menuTooltip.SetToolTip(this.zoomInBtn, "Zoom In (Shortcut: MouseWheel Up)");
            this.zoomInBtn.UseVisualStyleBackColor = false;
            this.zoomInBtn.Click += new System.EventHandler(this.zoomInBtn_Click);
            // 
            // zoomOutBtn
            // 
            this.zoomOutBtn.BackColor = System.Drawing.Color.White;
            this.zoomOutBtn.Enabled = false;
            this.zoomOutBtn.ForeColor = System.Drawing.Color.Coral;
            this.zoomOutBtn.Image = ((System.Drawing.Image)(resources.GetObject("zoomOutBtn.Image")));
            this.zoomOutBtn.Location = new System.Drawing.Point(959, 12);
            this.zoomOutBtn.Name = "zoomOutBtn";
            this.zoomOutBtn.Size = new System.Drawing.Size(75, 63);
            this.zoomOutBtn.TabIndex = 3;
            this.menuTooltip.SetToolTip(this.zoomOutBtn, "Zoom Out (Shortcut: MouseWheel Out)");
            this.zoomOutBtn.UseVisualStyleBackColor = false;
            this.zoomOutBtn.Click += new System.EventHandler(this.zoomOutBtn_Click);
            // 
            // restoreBtn
            // 
            this.restoreBtn.BackColor = System.Drawing.Color.White;
            this.restoreBtn.Enabled = false;
            this.restoreBtn.ForeColor = System.Drawing.Color.Coral;
            this.restoreBtn.Image = global::SpriteSheetDecomposer.Properties.Resources.old_edit_undo;
            this.restoreBtn.Location = new System.Drawing.Point(566, 12);
            this.restoreBtn.Name = "restoreBtn";
            this.restoreBtn.Size = new System.Drawing.Size(75, 63);
            this.restoreBtn.TabIndex = 13;
            this.menuTooltip.SetToolTip(this.restoreBtn, "Restore Selected Squares (Shortcut: W)");
            this.restoreBtn.UseVisualStyleBackColor = false;
            this.restoreBtn.Visible = false;
            this.restoreBtn.Click += new System.EventHandler(this.restoreBtn_Click);
            // 
            // loadXmlBtn
            // 
            this.loadXmlBtn.BackColor = System.Drawing.Color.White;
            this.loadXmlBtn.Enabled = false;
            this.loadXmlBtn.ForeColor = System.Drawing.Color.Coral;
            this.loadXmlBtn.Image = global::SpriteSheetDecomposer.Properties.Resources.folder_open;
            this.loadXmlBtn.Location = new System.Drawing.Point(123, 12);
            this.loadXmlBtn.Name = "loadXmlBtn";
            this.loadXmlBtn.Size = new System.Drawing.Size(75, 63);
            this.loadXmlBtn.TabIndex = 12;
            this.menuTooltip.SetToolTip(this.loadXmlBtn, "Load Configuration (Shortcut: Ctrl + L)");
            this.loadXmlBtn.UseVisualStyleBackColor = false;
            this.loadXmlBtn.Click += new System.EventHandler(this.loadXmlBtn_Click);
            // 
            // deleteBtn
            // 
            this.deleteBtn.BackColor = System.Drawing.Color.White;
            this.deleteBtn.Enabled = false;
            this.deleteBtn.ForeColor = System.Drawing.Color.Coral;
            this.deleteBtn.Image = global::SpriteSheetDecomposer.Properties.Resources.delete;
            this.deleteBtn.Location = new System.Drawing.Point(566, 12);
            this.deleteBtn.Name = "deleteBtn";
            this.deleteBtn.Size = new System.Drawing.Size(75, 63);
            this.deleteBtn.TabIndex = 10;
            this.menuTooltip.SetToolTip(this.deleteBtn, "Delete Selected Squares (Shortcut: D)");
            this.deleteBtn.UseVisualStyleBackColor = false;
            this.deleteBtn.Click += new System.EventHandler(this.deleteBtn_Click);
            // 
            // sliceBtn
            // 
            this.sliceBtn.BackColor = System.Drawing.Color.White;
            this.sliceBtn.Enabled = false;
            this.sliceBtn.ForeColor = System.Drawing.Color.Coral;
            this.sliceBtn.Image = ((System.Drawing.Image)(resources.GetObject("sliceBtn.Image")));
            this.sliceBtn.Location = new System.Drawing.Point(323, 12);
            this.sliceBtn.Name = "sliceBtn";
            this.sliceBtn.Size = new System.Drawing.Size(75, 63);
            this.sliceBtn.TabIndex = 8;
            this.menuTooltip.SetToolTip(this.sliceBtn, "Splice into Squares (Shortcut: Ctrl + C)");
            this.sliceBtn.UseVisualStyleBackColor = false;
            this.sliceBtn.Click += new System.EventHandler(this.sliceBtn_Click);
            // 
            // openSpriteSheetBtn
            // 
            this.openSpriteSheetBtn.BackColor = System.Drawing.Color.White;
            this.openSpriteSheetBtn.ForeColor = System.Drawing.Color.Coral;
            this.openSpriteSheetBtn.Image = global::SpriteSheetDecomposer.Properties.Resources.images;
            this.openSpriteSheetBtn.Location = new System.Drawing.Point(12, 12);
            this.openSpriteSheetBtn.Name = "openSpriteSheetBtn";
            this.openSpriteSheetBtn.Size = new System.Drawing.Size(75, 63);
            this.openSpriteSheetBtn.TabIndex = 7;
            this.menuTooltip.SetToolTip(this.openSpriteSheetBtn, "Load Spritesheet (Shortcut: Ctrl + O)");
            this.openSpriteSheetBtn.UseVisualStyleBackColor = false;
            this.openSpriteSheetBtn.Click += new System.EventHandler(this.openSpriteSheetBtn_Click);
            // 
            // splitBtn
            // 
            this.splitBtn.BackColor = System.Drawing.Color.White;
            this.splitBtn.Enabled = false;
            this.splitBtn.ForeColor = System.Drawing.Color.Coral;
            this.splitBtn.Image = global::SpriteSheetDecomposer.Properties.Resources.arrow_split_up;
            this.splitBtn.Location = new System.Drawing.Point(485, 12);
            this.splitBtn.Name = "splitBtn";
            this.splitBtn.Size = new System.Drawing.Size(75, 63);
            this.splitBtn.TabIndex = 6;
            this.menuTooltip.SetToolTip(this.splitBtn, "Split Selected Squares (Shortcut: S)");
            this.splitBtn.UseVisualStyleBackColor = false;
            this.splitBtn.Click += new System.EventHandler(this.splitBtn_Click);
            // 
            // mergeBtn
            // 
            this.mergeBtn.BackColor = System.Drawing.Color.White;
            this.mergeBtn.Enabled = false;
            this.mergeBtn.ForeColor = System.Drawing.Color.Coral;
            this.mergeBtn.Image = global::SpriteSheetDecomposer.Properties.Resources.arrow_merge;
            this.mergeBtn.Location = new System.Drawing.Point(404, 12);
            this.mergeBtn.Name = "mergeBtn";
            this.mergeBtn.Size = new System.Drawing.Size(75, 63);
            this.mergeBtn.TabIndex = 5;
            this.menuTooltip.SetToolTip(this.mergeBtn, "Merge Selected Squares (Shortcut: A)");
            this.mergeBtn.UseVisualStyleBackColor = false;
            this.mergeBtn.Click += new System.EventHandler(this.mergeBtn_Click);
            // 
            // exportBtn
            // 
            this.exportBtn.BackColor = System.Drawing.Color.White;
            this.exportBtn.Enabled = false;
            this.exportBtn.ForeColor = System.Drawing.Color.Coral;
            this.exportBtn.Image = global::SpriteSheetDecomposer.Properties.Resources.export;
            this.exportBtn.Location = new System.Drawing.Point(647, 12);
            this.exportBtn.Name = "exportBtn";
            this.exportBtn.Size = new System.Drawing.Size(75, 63);
            this.exportBtn.TabIndex = 2;
            this.menuTooltip.SetToolTip(this.exportBtn, "Export All Squares to Images (Shortcut: Ctrl + E)");
            this.exportBtn.UseVisualStyleBackColor = false;
            this.exportBtn.Click += new System.EventHandler(this.exportBtn_Click);
            // 
            // saveXmlBtn
            // 
            this.saveXmlBtn.BackColor = System.Drawing.Color.White;
            this.saveXmlBtn.Enabled = false;
            this.saveXmlBtn.ForeColor = System.Drawing.Color.Coral;
            this.saveXmlBtn.Image = ((System.Drawing.Image)(resources.GetObject("saveXmlBtn.Image")));
            this.saveXmlBtn.Location = new System.Drawing.Point(207, 12);
            this.saveXmlBtn.Name = "saveXmlBtn";
            this.saveXmlBtn.Size = new System.Drawing.Size(75, 63);
            this.saveXmlBtn.TabIndex = 14;
            this.menuTooltip.SetToolTip(this.saveXmlBtn, "Save Configuration (Shortcut: Ctrl + S)");
            this.saveXmlBtn.UseVisualStyleBackColor = false;
            this.saveXmlBtn.Click += new System.EventHandler(this.saveXmlBtn_Click);
            // 
            // menuTooltip
            // 
            this.menuTooltip.AutomaticDelay = 100;
            // 
            // exportName
            // 
            this.exportName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportName.Location = new System.Drawing.Point(4, 34);
            this.exportName.Name = "exportName";
            this.exportName.Size = new System.Drawing.Size(127, 26);
            this.exportName.TabIndex = 18;
            this.exportName.TextChanged += new System.EventHandler(this.exportName_TextChanged);
            this.exportName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.exportName_KeyDown);
            // 
            // groupName
            // 
            this.groupName.AutoSize = true;
            this.groupName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupName.Location = new System.Drawing.Point(2, 1);
            this.groupName.Name = "groupName";
            this.groupName.Size = new System.Drawing.Size(121, 20);
            this.groupName.TabIndex = 17;
            this.groupName.Text = "Name Selection";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "(Exported Image Name)";
            // 
            // namePanel
            // 
            this.namePanel.Controls.Add(this.label1);
            this.namePanel.Controls.Add(this.exportName);
            this.namePanel.Controls.Add(this.groupName);
            this.namePanel.Enabled = false;
            this.namePanel.Location = new System.Drawing.Point(735, 12);
            this.namePanel.Name = "namePanel";
            this.namePanel.Size = new System.Drawing.Size(137, 63);
            this.namePanel.TabIndex = 20;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1043, 85);
            this.Controls.Add(this.namePanel);
            this.Controls.Add(this.zoomOutBtn);
            this.Controls.Add(this.zoomInBtn);
            this.Controls.Add(this.restoreBtn);
            this.Controls.Add(this.openSpriteSheetBtn);
            this.Controls.Add(this.saveXmlBtn);
            this.Controls.Add(this.loadXmlBtn);
            this.Controls.Add(this.deleteBtn);
            this.Controls.Add(this.scrollImagePangel);
            this.Controls.Add(this.sliceBtn);
            this.Controls.Add(this.splitBtn);
            this.Controls.Add(this.mergeBtn);
            this.Controls.Add(this.exportBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.Text = "Sprite Sheet Decomposer";
            this.SizeChanged += new System.EventHandler(this.MainWindow_SizeChanged);
            this.scrollImagePangel.ResumeLayout(false);
            this.namePanel.ResumeLayout(false);
            this.namePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button exportBtn;
        public System.Windows.Forms.Button zoomOutBtn;
        public System.Windows.Forms.Button zoomInBtn;
        public System.Windows.Forms.Button mergeBtn;
        public System.Windows.Forms.Button splitBtn;
        private System.Windows.Forms.Button openSpriteSheetBtn;
        public System.Windows.Forms.Button sliceBtn;
        private System.Windows.Forms.Panel scrollImagePangel;
        public System.Windows.Forms.Button deleteBtn;
        private System.Windows.Forms.Button loadXmlBtn;
        public System.Windows.Forms.Button restoreBtn;
        private System.Windows.Forms.Button saveXmlBtn;
        private System.Windows.Forms.ToolTip menuTooltip;
        public System.Windows.Forms.ProgressBar progressBar;
        public System.Windows.Forms.TextBox exportName;
        private System.Windows.Forms.Label groupName;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Panel namePanel;
    }
}

