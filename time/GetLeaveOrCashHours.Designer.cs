namespace time
{
    partial class GetLeaveOrCashHours
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
            this.l_AvailableHours = new System.Windows.Forms.Label();
            this.nud_CashHours = new System.Windows.Forms.NumericUpDown();
            this.nud_LeaveHours = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nud_CashHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_LeaveHours)).BeginInit();
            this.SuspendLayout();
            // 
            // l_AvailableHours
            // 
            this.l_AvailableHours.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_AvailableHours.Location = new System.Drawing.Point(94, 42);
            this.l_AvailableHours.Name = "l_AvailableHours";
            this.l_AvailableHours.Size = new System.Drawing.Size(82, 34);
            this.l_AvailableHours.TabIndex = 0;
            this.l_AvailableHours.Text = "0.0";
            this.l_AvailableHours.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nud_CashHours
            // 
            this.nud_CashHours.DecimalPlaces = 3;
            this.nud_CashHours.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nud_CashHours.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nud_CashHours.Location = new System.Drawing.Point(25, 126);
            this.nud_CashHours.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.nud_CashHours.Name = "nud_CashHours";
            this.nud_CashHours.Size = new System.Drawing.Size(94, 31);
            this.nud_CashHours.TabIndex = 1;
            this.nud_CashHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nud_CashHours.ValueChanged += new System.EventHandler(this.Nud_CashHours_ValueChanged);
            // 
            // nud_LeaveHours
            // 
            this.nud_LeaveHours.DecimalPlaces = 3;
            this.nud_LeaveHours.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nud_LeaveHours.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nud_LeaveHours.Location = new System.Drawing.Point(160, 126);
            this.nud_LeaveHours.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.nud_LeaveHours.Name = "nud_LeaveHours";
            this.nud_LeaveHours.Size = new System.Drawing.Size(94, 31);
            this.nud_LeaveHours.TabIndex = 2;
            this.nud_LeaveHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nud_LeaveHours.ValueChanged += new System.EventHandler(this.Nud_LeaveHours_ValueChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(35, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 45);
            this.label1.TabIndex = 3;
            this.label1.Text = "Hours in Cash:";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(170, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 45);
            this.label2.TabIndex = 4;
            this.label2.Text = "Hours in Leave:";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(63, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 35);
            this.label3.TabIndex = 5;
            this.label3.Text = "Available Hours:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(101, 185);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 38);
            this.button1.TabIndex = 6;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // GetLeaveOrCashHours
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 235);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nud_LeaveHours);
            this.Controls.Add(this.nud_CashHours);
            this.Controls.Add(this.l_AvailableHours);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GetLeaveOrCashHours";
            this.Text = "GetLeaveOrCashHours";
            ((System.ComponentModel.ISupportInitialize)(this.nud_CashHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_LeaveHours)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label l_AvailableHours;
        private System.Windows.Forms.NumericUpDown nud_CashHours;
        private System.Windows.Forms.NumericUpDown nud_LeaveHours;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
    }
}