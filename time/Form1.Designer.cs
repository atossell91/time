namespace time
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.b_ViewWeeks = new System.Windows.Forms.Button();
            this.b_ViewWashup = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addCommentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewStatuatoryHolidaysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.row_interface_group1 = new time.row_interface_group();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(1084, 531);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Clear Row";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // b_ViewWeeks
            // 
            this.b_ViewWeeks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.b_ViewWeeks.Location = new System.Drawing.Point(996, 531);
            this.b_ViewWeeks.Name = "b_ViewWeeks";
            this.b_ViewWeeks.Size = new System.Drawing.Size(75, 23);
            this.b_ViewWeeks.TabIndex = 14;
            this.b_ViewWeeks.Text = "View Weeks";
            this.b_ViewWeeks.UseVisualStyleBackColor = true;
            this.b_ViewWeeks.Click += new System.EventHandler(this.b_ViewTime_Click);
            // 
            // b_ViewWashup
            // 
            this.b_ViewWashup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.b_ViewWashup.Location = new System.Drawing.Point(10, 531);
            this.b_ViewWashup.Name = "b_ViewWashup";
            this.b_ViewWashup.Size = new System.Drawing.Size(85, 23);
            this.b_ViewWashup.TabIndex = 15;
            this.b_ViewWashup.Text = "View Washup";
            this.b_ViewWashup.UseVisualStyleBackColor = true;
            this.b_ViewWashup.Click += new System.EventHandler(this.b_ViewWashup_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1164, 24);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addCommentToolStripMenuItem,
            this.viewStatuatoryHolidaysToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(61, 20);
            this.toolStripMenuItem1.Text = "Settings";
            // 
            // addCommentToolStripMenuItem
            // 
            this.addCommentToolStripMenuItem.Name = "addCommentToolStripMenuItem";
            this.addCommentToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.addCommentToolStripMenuItem.Text = "Add Comment";
            this.addCommentToolStripMenuItem.Click += new System.EventHandler(this.addCommentToolStripMenuItem_Click);
            // 
            // viewStatuatoryHolidaysToolStripMenuItem
            // 
            this.viewStatuatoryHolidaysToolStripMenuItem.Name = "viewStatuatoryHolidaysToolStripMenuItem";
            this.viewStatuatoryHolidaysToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.viewStatuatoryHolidaysToolStripMenuItem.Text = "View Statuatory Holidays";
            this.viewStatuatoryHolidaysToolStripMenuItem.Click += new System.EventHandler(this.viewStatuatoryHolidaysToolStripMenuItem_Click);
            // 
            // row_interface_group1
            // 
            this.row_interface_group1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.row_interface_group1.Location = new System.Drawing.Point(16, 30);
            this.row_interface_group1.Month = 0;
            this.row_interface_group1.Name = "row_interface_group1";
            this.row_interface_group1.Size = new System.Drawing.Size(1127, 491);
            this.row_interface_group1.TabIndex = 17;
            this.row_interface_group1.Year = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1164, 562);
            this.Controls.Add(this.row_interface_group1);
            this.Controls.Add(this.b_ViewWashup);
            this.Controls.Add(this.b_ViewWeeks);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button b_ViewWeeks;
        private System.Windows.Forms.Button b_ViewWashup;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addCommentToolStripMenuItem;
        private row_interface_group row_interface_group1;
        private System.Windows.Forms.ToolStripMenuItem viewStatuatoryHolidaysToolStripMenuItem;
    }
}

