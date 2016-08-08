namespace SpriteSheetDecomposer
{
    partial class Splice
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
            this.widthLabel = new System.Windows.Forms.Label();
            this.splicerWidth = new System.Windows.Forms.NumericUpDown();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.spliceBtn = new System.Windows.Forms.Button();
            this.splicerHeight = new System.Windows.Forms.NumericUpDown();
            this.heightLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splicerWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splicerHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // widthLabel
            // 
            this.widthLabel.AutoSize = true;
            this.widthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.widthLabel.Location = new System.Drawing.Point(12, 11);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(126, 20);
            this.widthLabel.TabIndex = 0;
            this.widthLabel.Text = "Sprite Width (px)";
            // 
            // splicerWidth
            // 
            this.splicerWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.splicerWidth.Location = new System.Drawing.Point(16, 34);
            this.splicerWidth.Name = "splicerWidth";
            this.splicerWidth.Size = new System.Drawing.Size(92, 26);
            this.splicerWidth.TabIndex = 1;
            this.splicerWidth.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(16, 78);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(92, 30);
            this.cancelBtn.TabIndex = 2;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // spliceBtn
            // 
            this.spliceBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.spliceBtn.Location = new System.Drawing.Point(163, 78);
            this.spliceBtn.Name = "spliceBtn";
            this.spliceBtn.Size = new System.Drawing.Size(92, 30);
            this.spliceBtn.TabIndex = 3;
            this.spliceBtn.Text = "Splice";
            this.spliceBtn.UseVisualStyleBackColor = true;
            this.spliceBtn.Click += new System.EventHandler(this.spliceBtn_Click);
            // 
            // splicerHeight
            // 
            this.splicerHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.splicerHeight.Location = new System.Drawing.Point(163, 34);
            this.splicerHeight.Name = "splicerHeight";
            this.splicerHeight.Size = new System.Drawing.Size(92, 26);
            this.splicerHeight.TabIndex = 5;
            this.splicerHeight.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // heightLabel
            // 
            this.heightLabel.AutoSize = true;
            this.heightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.heightLabel.Location = new System.Drawing.Point(159, 11);
            this.heightLabel.Name = "heightLabel";
            this.heightLabel.Size = new System.Drawing.Size(132, 20);
            this.heightLabel.TabIndex = 4;
            this.heightLabel.Text = "Sprite Height (px)";
            // 
            // Splice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 120);
            this.Controls.Add(this.splicerHeight);
            this.Controls.Add(this.heightLabel);
            this.Controls.Add(this.spliceBtn);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.splicerWidth);
            this.Controls.Add(this.widthLabel);
            this.Name = "Splice";
            this.Text = "Splice";
            ((System.ComponentModel.ISupportInitialize)(this.splicerWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splicerHeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label widthLabel;
        private System.Windows.Forms.NumericUpDown splicerWidth;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button spliceBtn;
        private System.Windows.Forms.NumericUpDown splicerHeight;
        private System.Windows.Forms.Label heightLabel;
    }
}