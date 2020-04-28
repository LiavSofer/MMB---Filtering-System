using System;
using System.Windows.Forms;

namespace _0._1.menuForms
{
    public partial class FilteringSettings : UserControl
    {
        public FilteringSettings()
        {
            InitializeComponent();
            UpdateTogglesGUI();
            try
            {
                foreach (string site in Resources.profile.Default.customBlacklist)
                {
                    blockedUrlListBox.Items.Add(site);
                }
            }
            catch
            {
                Resources.profile.Default.customBlacklist = new System.Collections.Generic.List<string>();
                Resources.profile.Default.Save();
                foreach (string site in Resources.profile.Default.customBlacklist)
                {
                    blockedUrlListBox.Items.Add(site);
                }
            }
            FontSetup();
        }

        private void FontSetup()
        {
            foreach (Control control in tableLayoutPanel2.Controls)
            {
                control.Font = new System.Drawing.Font(Program.privateFontCollection.Families[0], control.Font.Size, control.Font.Style);
            }
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                control.Font = new System.Drawing.Font(Program.privateFontCollection.Families[0], control.Font.Size, control.Font.Style);
            }
            foreach (Control control in tableLayoutPanel3.Controls)
            {
                control.Font = new System.Drawing.Font(Program.privateFontCollection.Families[0], control.Font.Size, control.Font.Style);
            }
            foreach (Control control in tableLayoutPanel4.Controls)
            {
                control.Font = new System.Drawing.Font(Program.privateFontCollection.Families[0], control.Font.Size, control.Font.Style);
            }
            GC.Collect();
        }

        public void UpdateTogglesGUI()
        {
            try
            {
                safeServerToggle.Image = Resources.profile.Default.safe_server ? Properties.Resources.On : Properties.Resources.Off;
                strictYoutubeToggle.Image = Resources.profile.Default.strict_search ? Properties.Resources.On : Properties.Resources.Off;
                socialBlockToggle.Image = Resources.profile.Default.social_block ? Properties.Resources.On : Properties.Resources.Off;
                newsBlockToggle.Image = Resources.profile.Default.news_block ? Properties.Resources.On : Properties.Resources.Off;
                gamblingBlockToggle.Image = Resources.profile.Default.gambling_block ? Properties.Resources.On : Properties.Resources.Off;
                adBlockToggle.Image = Resources.profile.Default.ad_block ? Properties.Resources.On : Properties.Resources.Off;
            }
            catch { }
        }

        private void Toggle_Click(object sender, EventArgs e)
        {
           PictureBox clicked = sender as PictureBox;
            switch(clicked.Tag.ToString())
            {
                case "safe_server":
                    Resources.profile.Default.safe_server = !Resources.profile.Default.safe_server;
                    break;
                case "strict_youtube":
                    Resources.profile.Default.strict_search = !Resources.profile.Default.strict_search;
                    break;
                case "social_block":
                    Resources.profile.Default.social_block = !Resources.profile.Default.social_block;
                    break;
                case "gambling_block":
                    Resources.profile.Default.gambling_block = !Resources.profile.Default.gambling_block;
                    break;
                case "news_block":
                    Resources.profile.Default.news_block = !Resources.profile.Default.news_block;
                    break;
                case "ad_block":
                    Resources.profile.Default.ad_block = !Resources.profile.Default.ad_block;
                    break;
            }
            UpdateTogglesGUI();
            Resources.profile.Default.Save();
            FilteringSystem.UpdateSettings();
        }

        private void addUrlButton_Click(object sender, EventArgs e)
        {
            if (!blockedUrlListBox.Items.Contains(urlTB.Text))
            {
                Resources.profile.Default.customBlacklist.Add(urlTB.Text);
                Resources.profile.Default.Save();
                blockedUrlListBox.Items.Add(urlTB.Text);
                FilteringSystem.UpdateSettings();
            }
            else
            {
                MessageBox.Show("אתר זה כבר מופיע ברשימה");
            }
        }

        private void deleteUrlButton_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedSite = blockedUrlListBox.Items[blockedUrlListBox.SelectedIndex].ToString();
                Resources.profile.Default.customBlacklist.Remove(selectedSite);
                Resources.profile.Default.Save();
                FilteringSystem.UpdateSettings();
                blockedUrlListBox.Items.Remove(selectedSite);
            }
            catch
            {
                MessageBox.Show("המחיקה נכשלה, נסה שוב במועד מאוחר יותר","שגיאה",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void blockedUrlListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            deleteUrlButton.Enabled = true;
        }
    }
}
