using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace time
{
    public partial class PersonalnfoGetter : UserControl
    {
        public PersonalInfo Info;
        public bool IsFilledOut = false;

        private readonly string filename = @"person_info.txt";

        public PersonalnfoGetter()
        {
            InitializeComponent();
            Info = new PersonalInfo();
        }
        public void SavePersonalInfoToFile()
        {
            string file = this.filename;
            File.WriteAllText(file, Info.printInfo());
        }
        
        private bool loadFromFile(string filename)
        {
            if (!File.Exists(filename))
            {
                return false;
            }

            string[] file = File.ReadAllLines(filename);

            PersonalInfo p;

            if (PersonalInfo.TryParse(file[0], out p))
            {
                this.Info = p;
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool isBlankString(string s)
        {
            return String.IsNullOrEmpty(s) &&
                string.IsNullOrWhiteSpace(s);
        }
        private void checkIfComplete()
        {
            string surname = tb_Surname.Text;
            string givenname = tb_givenNames.Text;
            string pri = mtb_PRI.Text;

            this.IsFilledOut = isBlankString(surname) &&
                isBlankString(givenname) &&
                isBlankString(pri);
        }

        private void tb_givenNames_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            Info.GivenNames = tb.Text;

            checkIfComplete();
        }

        private void tb_Surname_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;

            string t = tb.Text;

            Info.Surname = t;

            checkIfComplete();
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

            checkIfComplete();
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

        private void PersonalnfoGetter_Load(object sender, EventArgs e)
        {
            if (loadFromFile(this.filename))
            {
                tb_givenNames.Text = this.Info.GivenNames;
                tb_Surname.Text = this.Info.Surname;
                Debug.WriteLine("Date: " + this.Info.DateOfBirth);
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
    }
}
