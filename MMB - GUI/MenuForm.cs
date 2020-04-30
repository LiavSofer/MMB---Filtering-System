using System;
using System.Drawing;
using System.Windows.Forms;

namespace MMB_GUI
{
    public partial class MenuForm : Form
    {
        public enum StatusIcon
        {
            green=1, yellow=2, red=3
        }

        Label currentClickedLabel;
        public menuForms.CurrentStatus currentStatusUC = new menuForms.CurrentStatus();
        public menuForms.FilteringSettings filteringSettingsUC = new menuForms.FilteringSettings();
        public menuForms.GeneralSettings generalSettingsUC = new menuForms.GeneralSettings();
        public menuForms.About aboutUC = new menuForms.About();
        public menuForms.lockScreen lockUC = new menuForms.lockScreen();
        public menuForms.FirstOpening firstOpeningUC = new menuForms.FirstOpening();
        public menuForms.Schedule scheduleUC = new menuForms.Schedule();


        public MenuForm()
        {
            InitializeComponent();
            font_setup();
            currentClickedLabel = currentStatusLabel;
            lockForm();
            CustomNotifyIcon.update();
            NotifyIcon notifyIcon = CustomNotifyIcon.getNotifyIcon();
        }

        private void font_setup()
        {
            foreach (Control control in menuPanel.Controls)
            {
                control.Font = new Font(Program.privateFontCollection.Families[0], control.Font.Size, control.Font.Style);   
            }
        }

        private void MenuButton_Clicked(object sender, EventArgs e)
        {
            currentClickedLabel.BackColor = Color.FromArgb(49, 55, 56);
            currentClickedLabel.ForeColor = Color.White;
            Label clicked = sender as Label;
            currentClickedLabel = clicked;
            clicked.BackColor = Color.FromArgb(39, 44, 44);
            clicked.ForeColor = Color.FromArgb(139, 201, 244);
            menuClickedMark.Location = new Point(menuClickedMark.Location.X, clicked.Location.Y);
            setPanel(clicked.Tag.ToString());
        }
        public void setPanel(string key)
        {
            StatusIconPB.BackColor = Color.FromArgb(49, 55, 56);
            viewPanel.Controls.Clear();
            UserControl form = null;
            switch (key)
            { 
                case "filtering_settings":
                    form = filteringSettingsUC;
                    break;
                case "schedule":
                    form = scheduleUC;
                    break;
                case "current_status":
                    StatusIconPB.BackColor = Color.FromArgb(39, 44, 44);
                    form = currentStatusUC;
                    break;
                case "general_settings":
                    generalSettingsUC.UpdateData();
                    form = generalSettingsUC;
                    break;
                case "about":
                    form = aboutUC;
                    break;
                case "lock":
                    if(PasswordEncryption.Decrypt(Resources.profile.Default.admin_password)=="")
                    {
                        form = firstOpeningUC;
                        break; }
                    form = lockUC;
                    break;
                case "first_opening":
                    form = firstOpeningUC;
                    break;
            }
            form.Dock = DockStyle.Fill;
            viewPanel.Controls.Add(form);
        }
        private void MenuForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.lockForm();
            this.Hide();
            CustomNotifyIcon.showBalloonTip(500, "לוקחים אחריות!", "המערכת ממשיכה לעבוד ברקע, ניתן לפתוח את חלון הניהול בלחיצה על האייקון", ToolTipIcon.Info);
        }

        public void SetStatusIcon(StatusIcon statusIcon)
        {
            try
            {
                switch(statusIcon)
                {
                    case StatusIcon.green:
                        StatusIconPB.Image = Properties.Resources.green_v;
                        break;
                    case StatusIcon.yellow:
                        StatusIconPB.Image = Properties.Resources.attention;
                        break;
                    case StatusIcon.red:
                        StatusIconPB.Image = Properties.Resources.critical_attention;
                        break;
                }
            }
            catch
            {}
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("יציאה תוביל לסגירה מלאה של המערכת ולהשבתת הסינון" + Environment.NewLine + "האם אתה בטוח שברצונך לצאת?", "יציאה", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                exitButton.Text = "יוצא....";
                CustomNotifyIcon.hide();
                if (ServiceAdapter.GetServiceStatus("GUIAdapter") == "Running")
                {
                    ServiceAdapter.StopService("GUIAdapter", 10000);
                    while (ServiceAdapter.GetServiceStatus("GUIAdapter") != "Stopped")
                        {
                        exitButton.Text = ServiceAdapter.GetServiceStatus("GUIAdapter");
                        ServiceAdapter.StopService("GUIAdapter", 10000);
                        System.Threading.Thread.Sleep(3000);
                        }
                }
                InternetBlocker.block(false);
                Application.ExitThread();
            }
        }

        public void lockForm()
        {
            MenuButton_Clicked(currentStatusLabel,null);
            menuPanel.Visible = false;
            menuPanel.Enabled = false;
            setPanel("lock");
        }

        public void unlockForm()
        {
            menuPanel.Visible = true;
            menuPanel.Enabled = true;
            setPanel("current_status");
            lockUC.reset();
        }

        private void MenuForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                lockForm();
            }    
        }
    }
}