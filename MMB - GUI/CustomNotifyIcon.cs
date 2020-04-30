using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MMB_GUI
{
    class CustomNotifyIcon
    {
        private static NotifyIcon notifyIcon = new NotifyIcon();

        public static void update()
        {
            notifyIcon.Text = "לוקחים אחריות (" + (Resources.profile.Default.system_status ? "פעיל" : "לא פעיל") + ")";
            notifyIcon.Icon = Resources.profile.Default.system_status ? Properties.Resources.active_icon : Properties.Resources.inactive_icon;
            notifyIcon.Click += NotifyIcon_Click;
            notifyIcon.Visible = true;
        }

        public static void NotifyIcon_Click(object sender, EventArgs e)
        {
            MenuForm menuForm = (MenuForm) MenuForm.ActiveForm;
            try
            {
                Program.menuForm.Show();
                Program.menuForm.WindowState = FormWindowState.Normal;
                Program.menuForm.BringToFront();
            }
            catch
            {
                Program.menuForm = new MenuForm();
                Program.menuForm.Show();
            }
        }

        public static void showBalloonTip(int timeout, string title, string text, ToolTipIcon toolTipIcon)
        {
            notifyIcon.ShowBalloonTip(500, title, text, toolTipIcon);
        }

        public static NotifyIcon getNotifyIcon()
        {
            update();
            return notifyIcon;
        }

        public static void hide()
        {
            notifyIcon.Visible = false;
        }
    }
}
