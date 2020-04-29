using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace _0._1.menuForms
{
    public partial class CurrentStatus : UserControl
    {
        public CurrentStatus()
        {
            InitializeComponent();
            UpdateTogglesGUI();
            FontSetup();
        }

        private void FontSetup()
        {
            foreach (Control control in tableLayoutPanel2.Controls)
            {
                control.Font = new Font(Program.privateFontCollection.Families[0], control.Font.Size, control.Font.Style);
            }
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                control.Font = new Font(Program.privateFontCollection.Families[0], control.Font.Size, control.Font.Style);
            }
        }

        private void Toggle_Click(object sender, EventArgs e)
        {
            PictureBox clicked = sender as PictureBox;
            switch (clicked.Tag.ToString())
            {
                case "status":
                    FilteringSystem.ToogleStatus();
                    break;
                case "scheduel":
                    FilteringSystem.ToggleScheduelStatus();
                    Program.menuForm.scheduleUC.RefreshStatus();
                    break;
            }
            Resources.profile.Default.Save();
            FilteringSystem.UpdateSettings();
            UpdateTogglesGUI();
        }

        private void UpdateTogglesGUI()
        {
            FilterStatusToggle.Image = FilteringSystem.IsOn() ? Properties.Resources.On : Properties.Resources.Off;
            if (FilteringSystem.IsOn())
            {
                scheduelStatusToggle.Image = FilteringSystem.IsScheduelActive() ? Properties.Resources.On : Properties.Resources.Off;
            }
            else
            {
                scheduelStatusToggle.Image = Properties.Resources.Off;
                scheduelStatusToggle.Enabled = false;
            }
        }

        private void statusChecker_Tick(object sender, EventArgs e)
        {
            CheckStatus();
        }

        private void CheckStatus()
        {
            try
            {
                if (HostCatchingChecker() && ServerChecking() && CloseingPreventionChecking())
                {
                    Program.menuForm.SetStatusIcon(MenuForm.StatusIcon.green);
                }
                else
                {
                    Program.menuForm.SetStatusIcon(MenuForm.StatusIcon.yellow);
                    if (!ServerChecking() || !CloseingPreventionChecking())
                        Program.menuForm.SetStatusIcon(MenuForm.StatusIcon.red);
                }
            }
            catch { }
        }

        private Boolean ServerChecking()
        {
            if (DnsController.isSafe(false))
            {
                currentServerLabel.Text = "שרת מסונן";
                currentServerLabel.ForeColor = Color.LimeGreen;
                return true;
            }
            else
            {
                currentServerLabel.Text = "שרת פתוח";
                currentServerLabel.ForeColor = Color.Red;
                if (FilteringSystem.IsOn() && FilteringSystem.IsSafeServerOn())
                    DnsController.setMode(true);
                return false;
            }
        }

        private Boolean HostCatchingChecker()
        {
            if (HostsFileCatcher.getStatus())
            {
                HostCatchingStatusLabel.Text = "פעיל";
                HostCatchingStatusLabel.ForeColor = Color.LimeGreen;
                return true;
            }
            else
            {
                HostCatchingStatusLabel.Text = "לא פעיל";
                HostCatchingStatusLabel.ForeColor = Color.Red;
                if (FilteringSystem.IsOn())
                    HostsFileCatcher.StartCatching();
                return false;
            }
        }

        private Boolean CloseingPreventionChecking()
        {
            if (ServiceAdapter.GetServiceStatus("GUIAdapter") == "Running")
            {
                CloseingPreventionStatusLabel.Text = "פעיל";
                CloseingPreventionStatusLabel.ForeColor = Color.LimeGreen;
                return true;
            }
            else
            {
                CloseingPreventionStatusLabel.Text = "לא פעיל";
                CloseingPreventionStatusLabel.ForeColor = Color.Red;
                ServiceAdapter.StartService("GUIAdapter", 10000);
                return false;
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            InternetBlocker.block(false);
        }

        private void currentServerLabel_Click(object sender, EventArgs e)
        {
            MessageBox.Show(DnsController.isSafe(true).ToString());
        }

        private void CloseingPreventionStatusLabel_Click(object sender, EventArgs e)
        {
            if (CloseingPreventionChecking() == false)
            {
                try
                {
                    string servicePath = (System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Replace("file:///", "").Replace("MMB-Filter.exe", "MMB-Service.exe");
                    MessageBox.Show(servicePath.Replace("MMB-Filter.exe", "MMB - Service.exe") + "    ," + File.Exists(servicePath));
                    ServiceAdapter.InstallService(servicePath);
                    ServiceAdapter.StartService("GUIAdapter", 10000);
                }
                catch
                {
                    MessageBox.Show("לא ניתן להתקין תוסף שירות");
                }
            }
        }
    }
}