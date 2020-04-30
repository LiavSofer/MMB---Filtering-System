
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;

namespace MMB_GUI.menuForms
{
    public partial class About : UserControl
    {
        public About()
        {
            InitializeComponent();
            font_setup();
            versionLabel.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://mmb.org.il");
        }
    }
}
