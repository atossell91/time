using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace time
{
    public partial class PersonalInfoForm : Form
    {
        public PersonalInfo Info;
        private readonly string filename = @"person_info.txt";

        public PersonalInfoForm()
        {
            InitializeComponent();
        }

        private void saveToFile(string filename)
        {
            File.WriteAllText(filename, Info.printInfo());
        }
        private bool loadFromFile(string filename)
        {
            string[] file = File.ReadAllLines(filename);

            PersonalInfo p;

            if(PersonalInfo.TryParse(file[0], out p))
            {
                this.Info = p;
                return true;
            }
            else
            {
                return false;
            }
        }

        private void PersonalInfoForm_Load(object sender, EventArgs e)
        {
            if (loadFromFile(this.filename))
            {
                tb_givenNames.Text = this.Info.GivenNames;
                tb_Surname.Text = this.Info.Surname;
                dtp_DOB.Value = this.Info.DateOfBirth;
                mtb_PRI.Text = this.Info.PRI;
                tb_Group.Text = this.Info.Group;
                tb_Level.Text = this.Info.Level;
                tb_HomeAddress.Text = this.Info.HomeAddress;
                tb_WorkAddress.Text = this.Info.WorkAddress;
                mtb_HomePhone.Text = this.Info.HomePhoneNumber;
                mtb_WorkPhone.Text = this.Info.WorkPhoneNumber;
            }
        }

        private void tb_givenNames_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            Info.GivenNames = tb.Text;
        }

        private void tb_Surname_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            Info.Surname = tb.Text;
        }

        private void dtp_DOB_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = (DateTimePicker)sender;
            Info.DateOfBirth = dtp.Value;
        }

        private void mtb_PRI_Validated(object sender, EventArgs e)
        {
            MaskedTextBox mtb = (MaskedTextBox)sender;
            Info.PRI = mtb.Text;
        }

        private void tb_Group_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            Info.Group = tb.Text;
        }

        private void tb_Level_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            Info.Level = tb.Text;
        }

        private void tb_WorkAddress_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            Info.WorkAddress = tb.Text;
        }

        private void tb_HomeAddress_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            Info.HomeAddress = tb.Text;
        }

        private void mtb_WorkPhone_Validated(object sender, EventArgs e)
        {
            MaskedTextBox mtb = (MaskedTextBox)sender;
            Info.WorkPhoneNumber = mtb.Text;
        }

        private void mtb_HomePhone_Validated(object sender, EventArgs e)
        {
            MaskedTextBox mtb = (MaskedTextBox)sender;
            Info.HomePhoneNumber = mtb.Text;
        }

        private void b_Ok_Click(object sender, EventArgs e)
        {
            saveToFile(this.filename);
            this.Close();
        }
    }
}
