using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace _0._1.menuForms
{
    public partial class FirstOpening: UserControl
    {
        const int WS_EX_TRANSPARENT = 0x20;

        public FirstOpening()
        {
            InitializeComponent();
            font_setup();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | WS_EX_TRANSPARENT;

                return cp;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                saveChangesButton.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void font_setup()
        {
            foreach (Control control in tableLayoutPanel2.Controls)
            {
                control.Font = new System.Drawing.Font(Program.privateFontCollection.Families[0], control.Font.Size, control.Font.Style);
            }
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                control.Font = new System.Drawing.Font(Program.privateFontCollection.Families[0], control.Font.Size, control.Font.Style);
            }
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(email);       
        }

        private void saveChangesButton_Click(object sender, EventArgs e)
        {
            if(adminPassTB.Text!=""&&adminMailTB.Text!="")
            {
                if(adminPassTB.Text.Length>=4&&adminPassTB.Text.IsNormalized())
                {
                    if(IsValidEmail(adminMailTB.Text))
                    {
                        if (adminPassTB.Text == confirmPassTB.Text)
                        {
                            save();
                        }
                        else
                        {
                            MessageBox.Show("הסיסמאות לא תואמות", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("יש להזין כתובת מייל חוקית", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("אורך הסיסמה חייב להיות לפחות 4 תווים", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("השדות לא יכולים להיות ריקים", "יש למלא את כל השדות", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void save()
        {
            Resources.profile.Default.admin_mail = adminMailTB.Text;
            Resources.profile.Default.admin_password = PasswordEncryption.Encrypt(adminPassTB.Text);
            Installer.SetUninstallingPass(PasswordEncryption.Encrypt(adminPassTB.Text));
            Resources.profile.Default.Save();
            MessageBox.Show("השינויים נשמרו בהצלחה!", "לוקחים אחריות", MessageBoxButtons.OK, MessageBoxIcon.None);
            Program.menuForm.unlockForm();
        }

        private void confirmPassTB_TextChanged(object sender, EventArgs e)
        {
            confirmPassTB.ForeColor = confirmPassTB.Text == adminPassTB.Text ? System.Drawing.Color.LimeGreen : System.Drawing.Color.Red;
        }
    }
}
