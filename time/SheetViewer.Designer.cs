namespace time
{
    partial class SheetViewer
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
            this.b_Print = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rb_EditMode = new System.Windows.Forms.RadioButton();
            this.rb_ViewMode = new System.Windows.Forms.RadioButton();
            this.nud_ScaleFactor = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.arrowNavigators1 = new time.ArrowNavigators();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_ScaleFactor)).BeginInit();
            this.SuspendLayout();
            // 
            // b_Print
            // 
            this.b_Print.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.b_Print.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.b_Print.Location = new System.Drawing.Point(12, 398);
            this.b_Print.Name = "b_Print";
            this.b_Print.Size = new System.Drawing.Size(75, 33);
            this.b_Print.TabIndex = 0;
            this.b_Print.Text = "Print";
            this.b_Print.UseVisualStyleBackColor = true;
            this.b_Print.Click += new System.EventHandler(this.B_Print_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(776, 377);
            this.panel1.TabIndex = 2;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.Enabled = false;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 130);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.rb_EditMode);
            this.panel2.Controls.Add(this.rb_ViewMode);
            this.panel2.Location = new System.Drawing.Point(666, 398);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(122, 40);
            this.panel2.TabIndex = 3;
            // 
            // rb_EditMode
            // 
            this.rb_EditMode.AutoSize = true;
            this.rb_EditMode.Location = new System.Drawing.Point(3, 20);
            this.rb_EditMode.Name = "rb_EditMode";
            this.rb_EditMode.Size = new System.Drawing.Size(73, 17);
            this.rb_EditMode.TabIndex = 4;
            this.rb_EditMode.Text = "Edit Mode";
            this.rb_EditMode.UseVisualStyleBackColor = true;
            this.rb_EditMode.CheckedChanged += new System.EventHandler(this.Rb_EditMode_CheckedChanged);
            // 
            // rb_ViewMode
            // 
            this.rb_ViewMode.AutoSize = true;
            this.rb_ViewMode.Checked = true;
            this.rb_ViewMode.Location = new System.Drawing.Point(3, 3);
            this.rb_ViewMode.Name = "rb_ViewMode";
            this.rb_ViewMode.Size = new System.Drawing.Size(78, 17);
            this.rb_ViewMode.TabIndex = 0;
            this.rb_ViewMode.TabStop = true;
            this.rb_ViewMode.Text = "View Mode";
            this.rb_ViewMode.UseVisualStyleBackColor = true;
            this.rb_ViewMode.CheckedChanged += new System.EventHandler(this.Rb_ViewMode_CheckedChanged);
            // 
            // nud_ScaleFactor
            // 
            this.nud_ScaleFactor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nud_ScaleFactor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nud_ScaleFactor.Location = new System.Drawing.Point(590, 410);
            this.nud_ScaleFactor.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nud_ScaleFactor.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_ScaleFactor.Name = "nud_ScaleFactor";
            this.nud_ScaleFactor.Size = new System.Drawing.Size(61, 22);
            this.nud_ScaleFactor.TabIndex = 4;
            this.nud_ScaleFactor.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nud_ScaleFactor.ValueChanged += new System.EventHandler(this.Nud_ScaleFactor_ValueChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(500, 400);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 30);
            this.button1.TabIndex = 5;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // arrowNavigators1
            // 
            this.arrowNavigators1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.arrowNavigators1.Location = new System.Drawing.Point(303, 395);
            this.arrowNavigators1.MaxNavValue = 1;
            this.arrowNavigators1.MinNavValue = 1;
            this.arrowNavigators1.Name = "arrowNavigators1";
            this.arrowNavigators1.NavigationIndex = 1;
            this.arrowNavigators1.Size = new System.Drawing.Size(158, 43);
            this.arrowNavigators1.TabIndex = 1;
            // 
            // SheetViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.nud_ScaleFactor);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.arrowNavigators1);
            this.Controls.Add(this.b_Print);
            this.Name = "SheetViewer";
            this.Text = "SheetViewer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SheetViewer_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_ScaleFactor)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button b_Print;
        private ArrowNavigators arrowNavigators1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rb_EditMode;
        private System.Windows.Forms.RadioButton rb_ViewMode;
        private System.Windows.Forms.NumericUpDown nud_ScaleFactor;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
    }
}