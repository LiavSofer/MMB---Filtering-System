using System;
using System.Windows.Forms;

namespace MMB_GUI.menuForms
{
    public partial class lockScreen : UserControl
    {
        private int attempts = 3;

        public lockScreen()
        {
            InitializeComponent();
            font_setup();
        }

        private void font_setup()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                control.Font = new System.Drawing.Font(Program.privateFontCollection.Families[0], control.Font.Size, control.Font.Style);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                confirmButton.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


       
        private void confirmButton_Click(object sender, EventArgs e)
        {
            if(FilteringSystem.IsAdminPassword(passTB.Text))
            {
                //Unlock the system
                Program.menuForm.unlockForm();

                //Release the internet blocking
                ServiceAdapter.CustomCommend("GUIAdapter", (int)ServiceAdapter.CustomCommends.releaseScheduelBlocking); 
            }
            else
            {
                if (attempts > 1)
                {
                    attempts--;
                    attemptsLable.Text = attempts.ToString();
                    attemptsLable.ForeColor = attempts == 1 ? System.Drawing.Color.Red : attemptsLable.ForeColor;
                    label4.ForeColor = attempts == 1 ? System.Drawing.Color.Red : attemptsLable.ForeColor;
                }
                else
                {
                    label4.ForeColor = System.Drawing.Color.Red;
                    label4.Text = "התראה על שימוש בסיסמה שגויה נשלחה למנהל המערכת";
                    label5.Text = "הגלישה ברשת במחשב זה נחסמה.";
                    attemptsLable.Visible = false;
                    label2.Visible = false;
                    ServiceAdapter.CustomCommend("GUIAdapter", (int)ServiceAdapter.CustomCommends.startScheduelBlocking); //tell the service to bloke the internt
                    MessageBox.Show("התראה על שימוש בסיסמה שגויה נשלחה למנהל המערכת", "סיסמה שגויה!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            passTB.Text = "";
        }

        public void reset()
        {
            attempts = 3;
            label2.Text = "נותרו לך עוד:";
            label2.Visible = true;
            attemptsLable.Text = "3";
            attemptsLable.Visible = true;
            attemptsLable.ForeColor = System.Drawing.Color.Gray;
            label4.Text = "ניסיונות";
            label4.ForeColor = System.Drawing.Color.Gray;
            label5.Text = "לאחר מכן, התראה תשלח למנהל המערכת והגלישה במחשב ברשת במחשב זה תחסם עד להזנת סיסמת ניהול.";
        }
    }
}
