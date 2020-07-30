namespace time
{
    partial class ArrowNavigators
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.b_Left = new System.Windows.Forms.Button();
            this.b_Right = new System.Windows.Forms.Button();
            this.nud_NavIndex = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nud_NavIndex)).BeginInit();
            this.SuspendLayout();
            // 
            // b_Left
            // 
            this.b_Left.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.b_Left.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.b_Left.Location = new System.Drawing.Point(3, 3);
            this.b_Left.Name = "b_Left";
            this.b_Left.Size = new System.Drawing.Size(39, 35);
            this.b_Left.TabIndex = 0;
            this.b_Left.Text = "<";
            this.b_Left.UseVisualStyleBackColor = true;
            this.b_Left.Click += new System.EventHandler(this.B_Left_Click);
            // 
            // b_Right
            // 
            this.b_Right.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.b_Right.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.b_Right.Location = new System.Drawing.Point(112, 3);
            this.b_Right.Name = "b_Right";
            this.b_Right.Size = new System.Drawing.Size(39, 35);
            this.b_Right.TabIndex = 2;
            this.b_Right.Text = ">";
            this.b_Right.UseVisualStyleBackColor = true;
            this.b_Right.Click += new System.EventHandler(this.B_Right_Click);
            // 
            // nud_NavIndex
            // 
            this.nud_NavIndex.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.nud_NavIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nud_NavIndex.Location = new System.Drawing.Point(50, 6);
            this.nud_NavIndex.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nud_NavIndex.Name = "nud_NavIndex";
            this.nud_NavIndex.Size = new System.Drawing.Size(56, 31);
            this.nud_NavIndex.TabIndex = 3;
            this.nud_NavIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ArrowNavigators
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nud_NavIndex);
            this.Controls.Add(this.b_Right);
            this.Controls.Add(this.b_Left);
            this.Name = "ArrowNavigators";
            this.Size = new System.Drawing.Size(158, 43);
            ((System.ComponentModel.ISupportInitialize)(this.nud_NavIndex)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button b_Left;
        private System.Windows.Forms.Button b_Right;
        private System.Windows.Forms.NumericUpDown nud_NavIndex;
    }
}
