using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MMB_GUI.menuForms
{
    public partial class GeneralSettings : UserControl
    {
        public GeneralSettings()
        {
            InitializeComponent();
            Font_Setup();
        }

        private void Font_Setup()
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

        private void Text_Change(object sender, EventArgs e)
        {
            if (adminPassTB.Text.Length >= 4 && IsValidEmail(adminMailTB.Text))
            {
                ShowConfirmPassTB();
                saveChangesButton.Enabled = true;
            }
            else
            {
                HideConfirmPassTB();
                saveChangesButton.Enabled = false;
            }
            confirmPassTB_TextChanged(null, null);
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }

        private void saveChangesButton_Click(object sender, EventArgs e)
        {
            if (adminPassTB.Text == confirmPassTB.Text)
            {
                Resources.profile.Default.admin_password = PasswordEncryption.Encrypt(adminPassTB.Text);
                Installer.SetUninstallingPass(PasswordEncryption.Encrypt(adminPassTB.Text));
                Resources.profile.Default.admin_mail = adminMailTB.Text;
                Resources.profile.Default.Save();
                HideConfirmPassTB();
                MessageBox.Show("השינויים נשמרו בהצלחה", "השינויים נשמרו", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("הסיסמאות לא תואמות","שגיאה",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        public void UpdateData()
        {
            adminMailTB.Text = Resources.profile.Default.admin_mail;
            adminPassTB.Text = PasswordEncryption.Decrypt(Resources.profile.Default.admin_password);
            saveChangesButton.Enabled = false;
            HideConfirmPassTB();
        }

        private void confirmPassTB_TextChanged(object sender, EventArgs e)
        {
            if(confirmPassTB.Text==adminPassTB.Text)
            {
                confirmPassTB.ForeColor = System.Drawing.Color.LimeGreen;
                saveChangesButton.Enabled = true;
            }
            else
            {
                confirmPassTB.ForeColor = System.Drawing.Color.Red;
                saveChangesButton.Enabled = false;
            }
        }

        private void HideConfirmPassTB()
        {
            confirmPassTB.Text = "";
            label7.Visible = false;
            label6.Visible = false;
            confirmPassTB.Visible = false;
        }

        private void ShowConfirmPassTB()
        {
            label7.Visible = true;
            label6.Visible = true;
            confirmPassTB.Visible =true;
        }
    }
}
