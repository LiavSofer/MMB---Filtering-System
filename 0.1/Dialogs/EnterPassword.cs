using System;
using System.Collections.Generic;

using System.Windows.Forms;

namespace _0._1.Dialogs
{
    public partial class EnterPassword : Form
    {
        public string gettedText = "";
        public EnterPassword(string title, Boolean UseSystemPasswordChar)
        {
            InitializeComponent();
            label1.Text = title;
            textbox.UseSystemPasswordChar = UseSystemPasswordChar;
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            gettedText = textbox.Text;
        }

        private void EnterPassword_Shown(object sender, EventArgs e)
        {
            this.BringToFront();
        }
    }
}
