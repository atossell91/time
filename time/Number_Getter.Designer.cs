namespace time
{
    partial class Number_Getter
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
            this.nud_Number = new System.Windows.Forms.NumericUpDown();
            this.b_Ok = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Number)).BeginInit();
            this.SuspendLayout();
            // 
            // nud_Number
            // 
            this.nud_Number.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nud_Number.Location = new System.Drawing.Point(12, 6);
            this.nud_Number.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nud_Number.Name = "nud_Number";
            this.nud_Number.Size = new System.Drawing.Size(83, 31);
            this.nud_Number.TabIndex = 0;
            this.nud_Number.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nud_Number.Value = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this.nud_Number.ValueChanged += new System.EventHandler(this.Nud_Number_ValueChanged);
            // 
            // b_Ok
            // 
            this.b_Ok.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.b_Ok.Location = new System.Drawing.Point(12, 43);
            this.b_Ok.Name = "b_Ok";
            this.b_Ok.Size = new System.Drawing.Size(83, 37);
            this.b_Ok.TabIndex = 1;
            this.b_Ok.Text = "Ok";
            this.b_Ok.UseVisualStyleBackColor = true;
            this.b_Ok.Click += new System.EventHandler(this.B_Ok_Click);
            // 
            // Number_Getter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(120, 92);
            this.Controls.Add(this.b_Ok);
            this.Controls.Add(this.nud_Number);
            this.Name = "Number_Getter";
            this.Text = "Date_Getter";
            ((System.ComponentModel.ISupportInitialize)(this.nud_Number)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nud_Number;
        private System.Windows.Forms.Button b_Ok;
    }
}