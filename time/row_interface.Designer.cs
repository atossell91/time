namespace time
{
    partial class row_interface
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
            this.nud_overtime = new System.Windows.Forms.NumericUpDown();
            this.nud_premiums = new System.Windows.Forms.NumericUpDown();
            this.cb_Washup = new System.Windows.Forms.CheckBox();
            this.l_dayOfMonth = new System.Windows.Forms.Label();
            this.l_weekNum = new System.Windows.Forms.Label();
            this.l_DayOfWeek = new System.Windows.Forms.Label();
            this.rtb_Comment = new System.Windows.Forms.RichTextBox();
            this.mtb_Start = new System.Windows.Forms.MaskedTextBox();
            this.mtb_End = new System.Windows.Forms.MaskedTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nud_overtime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_premiums)).BeginInit();
            this.SuspendLayout();
            // 
            // nud_overtime
            // 
            this.nud_overtime.DecimalPlaces = 2;
            this.nud_overtime.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.nud_overtime.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.nud_overtime.InterceptArrowKeys = false;
            this.nud_overtime.Location = new System.Drawing.Point(510, 10);
            this.nud_overtime.Name = "nud_overtime";
            this.nud_overtime.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.nud_overtime.Size = new System.Drawing.Size(71, 32);
            this.nud_overtime.TabIndex = 2;
            this.nud_overtime.ValueChanged += new System.EventHandler(this.nud_overtime_ValueChanged);
            this.nud_overtime.Enter += new System.EventHandler(this.NumericUpDown_Enter);
            this.nud_overtime.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Child_KeyDown);
            // 
            // nud_premiums
            // 
            this.nud_premiums.DecimalPlaces = 2;
            this.nud_premiums.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.nud_premiums.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.nud_premiums.InterceptArrowKeys = false;
            this.nud_premiums.Location = new System.Drawing.Point(610, 10);
            this.nud_premiums.Name = "nud_premiums";
            this.nud_premiums.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.nud_premiums.Size = new System.Drawing.Size(71, 32);
            this.nud_premiums.TabIndex = 3;
            this.nud_premiums.ValueChanged += new System.EventHandler(this.nud_premiums_ValueChanged);
            this.nud_premiums.Enter += new System.EventHandler(this.NumericUpDown_Enter);
            this.nud_premiums.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Child_KeyDown);
            // 
            // cb_Washup
            // 
            this.cb_Washup.AutoSize = true;
            this.cb_Washup.Location = new System.Drawing.Point(710, 20);
            this.cb_Washup.Name = "cb_Washup";
            this.cb_Washup.Size = new System.Drawing.Size(15, 14);
            this.cb_Washup.TabIndex = 4;
            this.cb_Washup.UseVisualStyleBackColor = true;
            this.cb_Washup.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            this.cb_Washup.Enter += new System.EventHandler(this.CheckBox_Enter);
            this.cb_Washup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Child_KeyDown);
            // 
            // l_dayOfMonth
            // 
            this.l_dayOfMonth.AutoSize = true;
            this.l_dayOfMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_dayOfMonth.Location = new System.Drawing.Point(170, 10);
            this.l_dayOfMonth.Name = "l_dayOfMonth";
            this.l_dayOfMonth.Size = new System.Drawing.Size(38, 25);
            this.l_dayOfMonth.TabIndex = 5;
            this.l_dayOfMonth.Text = "00";
            // 
            // l_weekNum
            // 
            this.l_weekNum.AutoSize = true;
            this.l_weekNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_weekNum.Location = new System.Drawing.Point(230, 10);
            this.l_weekNum.Name = "l_weekNum";
            this.l_weekNum.Size = new System.Drawing.Size(36, 25);
            this.l_weekNum.TabIndex = 6;
            this.l_weekNum.Text = "00";
            // 
            // l_DayOfWeek
            // 
            this.l_DayOfWeek.AutoSize = true;
            this.l_DayOfWeek.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.l_DayOfWeek.Location = new System.Drawing.Point(20, 10);
            this.l_DayOfWeek.Name = "l_DayOfWeek";
            this.l_DayOfWeek.Size = new System.Drawing.Size(127, 26);
            this.l_DayOfWeek.TabIndex = 7;
            this.l_DayOfWeek.Text = "Wednesday";
            // 
            // rtb_Comment
            // 
            this.rtb_Comment.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.rtb_Comment.Location = new System.Drawing.Point(750, 10);
            this.rtb_Comment.Name = "rtb_Comment";
            this.rtb_Comment.Size = new System.Drawing.Size(430, 30);
            this.rtb_Comment.TabIndex = 8;
            this.rtb_Comment.Text = "";
            this.rtb_Comment.TextChanged += new System.EventHandler(this.Rtb_Comment_TextChanged);
            this.rtb_Comment.Enter += new System.EventHandler(this.RichTextBox_Enter);
            this.rtb_Comment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Child_KeyDown);
            // 
            // mtb_Start
            // 
            this.mtb_Start.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.mtb_Start.Location = new System.Drawing.Point(310, 10);
            this.mtb_Start.Mask = "00:00";
            this.mtb_Start.Name = "mtb_Start";
            this.mtb_Start.Size = new System.Drawing.Size(70, 32);
            this.mtb_Start.TabIndex = 9;
            this.mtb_Start.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mtb_Start.ValidatingType = typeof(System.DateTime);
            this.mtb_Start.Enter += new System.EventHandler(this.MaskedTextBox_Enter);
            this.mtb_Start.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mtb_Times_KeyDown);
            this.mtb_Start.Validating += new System.ComponentModel.CancelEventHandler(this.mtb_Start_Validating);
            // 
            // mtb_End
            // 
            this.mtb_End.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.mtb_End.Location = new System.Drawing.Point(410, 10);
            this.mtb_End.Mask = "00:00";
            this.mtb_End.Name = "mtb_End";
            this.mtb_End.Size = new System.Drawing.Size(70, 32);
            this.mtb_End.TabIndex = 10;
            this.mtb_End.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mtb_End.ValidatingType = typeof(System.DateTime);
            this.mtb_End.Enter += new System.EventHandler(this.MaskedTextBox_Enter);
            this.mtb_End.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mtb_Times_KeyDown);
            this.mtb_End.Validating += new System.ComponentModel.CancelEventHandler(this.mtb_End_Validating);
            // 
            // row_interface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mtb_End);
            this.Controls.Add(this.mtb_Start);
            this.Controls.Add(this.rtb_Comment);
            this.Controls.Add(this.l_DayOfWeek);
            this.Controls.Add(this.l_weekNum);
            this.Controls.Add(this.l_dayOfMonth);
            this.Controls.Add(this.cb_Washup);
            this.Controls.Add(this.nud_premiums);
            this.Controls.Add(this.nud_overtime);
            this.Name = "row_interface";
            this.Size = new System.Drawing.Size(1200, 52);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.row_interface_KeyDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.row_interface_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.nud_overtime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_premiums)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NumericUpDown nud_overtime;
        private System.Windows.Forms.NumericUpDown nud_premiums;
        private System.Windows.Forms.CheckBox cb_Washup;
        private System.Windows.Forms.Label l_dayOfMonth;
        private System.Windows.Forms.Label l_weekNum;
        private System.Windows.Forms.Label l_DayOfWeek;
        private System.Windows.Forms.RichTextBox rtb_Comment;
        private System.Windows.Forms.MaskedTextBox mtb_Start;
        private System.Windows.Forms.MaskedTextBox mtb_End;
    }
}
