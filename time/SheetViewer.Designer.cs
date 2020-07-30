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
            this.arrowNavigators1 = new time.ArrowNavigators();
            this.panel1 = new System.Windows.Forms.Panel();
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
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(776, 377);
            this.panel1.TabIndex = 2;
            // 
            // SheetViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.arrowNavigators1);
            this.Controls.Add(this.b_Print);
            this.Name = "SheetViewer";
            this.Text = "SheetViewer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button b_Print;
        private ArrowNavigators arrowNavigators1;
        private System.Windows.Forms.Panel panel1;
    }
}