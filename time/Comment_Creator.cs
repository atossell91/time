using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace time
{
    public partial class Comment_Creator : Form
    {
        private PersonalInfo pInfo;
        public Comment_Creator(PersonalInfo pi)
        {
            pInfo = pi;

            InitializeComponent();

            setName(checkBox1.Checked);
            l_Date.Text = DateTime.Now.ToString("MMMM dd, yyyy HH:mm");
        }
        public void saveComment()
        {
            if (String.IsNullOrEmpty(textBox1.Text) ||
                String.IsNullOrWhiteSpace(textBox1.Text))
            {
                return;
            }

            string filename = Directory.GetCurrentDirectory() + @"\user_comments.txt";
            string[] output = new string[6];

            string line_separator = "#--------------------------------#";

            output[0] = line_separator;
            output[1] = l_Name.Text;
            output[2] = l_Date.Text;
            output[4] = textBox1.Text;
            output[5] = line_separator;

            try
            {
                File.AppendAllLines(filename, output);
            }
            catch (Exception e) { }
        }
        private void setName(bool isAnnon)
        {
            if (isAnnon)
            {
                l_Name.Text = "Annonymous";
            }
            else
            {
                l_Name.Text = pInfo.GivenNames + " " + pInfo.Surname;
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            setName(cb.Checked);
        }

        private void b_Submit_Click(object sender, EventArgs e)
        {
            saveComment();
            this.Close();
        }

        private void b_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
