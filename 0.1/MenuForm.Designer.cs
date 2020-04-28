namespace _0._1
{
    partial class MenuForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuForm));
            this.menuPanel = new System.Windows.Forms.Panel();
            this.StatusIconPB = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.exitButton = new System.Windows.Forms.Label();
            this.menuClickedMark = new System.Windows.Forms.PictureBox();
            this.aboutLabel = new System.Windows.Forms.Label();
            this.settingsLabel = new System.Windows.Forms.Label();
            this.profilesSettingsLabel = new System.Windows.Forms.Label();
            this.currentStatusLabel = new System.Windows.Forms.Label();
            this.viewPanel = new System.Windows.Forms.Panel();
            this.menuPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StatusIconPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.menuClickedMark)).BeginInit();
            this.SuspendLayout();
            // 
            // menuPanel
            // 
            this.menuPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(55)))), ((int)(((byte)(56)))));
            this.menuPanel.Controls.Add(this.StatusIconPB);
            this.menuPanel.Controls.Add(this.label1);
            this.menuPanel.Controls.Add(this.exitButton);
            this.menuPanel.Controls.Add(this.menuClickedMark);
            this.menuPanel.Controls.Add(this.aboutLabel);
            this.menuPanel.Controls.Add(this.settingsLabel);
            this.menuPanel.Controls.Add(this.profilesSettingsLabel);
            this.menuPanel.Controls.Add(this.currentStatusLabel);
            this.menuPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.menuPanel.Location = new System.Drawing.Point(0, 0);
            this.menuPanel.Margin = new System.Windows.Forms.Padding(2);
            this.menuPanel.Name = "menuPanel";
            this.menuPanel.Size = new System.Drawing.Size(302, 695);
            this.menuPanel.TabIndex = 1;
            // 
            // StatusIconPB
            // 
            this.StatusIconPB.Image = ((System.Drawing.Image)(resources.GetObject("StatusIconPB.Image")));
            this.StatusIconPB.Location = new System.Drawing.Point(48, 41);
            this.StatusIconPB.Name = "StatusIconPB";
            this.StatusIconPB.Size = new System.Drawing.Size(30, 50);
            this.StatusIconPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.StatusIconPB.TabIndex = 13;
            this.StatusIconPB.TabStop = false;
            this.StatusIconPB.Tag = "current_status";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(55)))), ((int)(((byte)(56)))));
            this.label1.Font = new System.Drawing.Font("Secular One", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(2, 144);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 0, 30, 0);
            this.label1.Size = new System.Drawing.Size(300, 50);
            this.label1.TabIndex = 12;
            this.label1.Tag = "schedule";
            this.label1.Text = "מערכת שעות";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Click += new System.EventHandler(this.MenuButton_Clicked);
            // 
            // exitButton
            // 
            this.exitButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(55)))), ((int)(((byte)(56)))));
            this.exitButton.Font = new System.Drawing.Font("Secular One", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.exitButton.ForeColor = System.Drawing.Color.White;
            this.exitButton.Location = new System.Drawing.Point(0, 606);
            this.exitButton.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.exitButton.Name = "exitButton";
            this.exitButton.Padding = new System.Windows.Forms.Padding(0, 0, 30, 0);
            this.exitButton.Size = new System.Drawing.Size(300, 50);
            this.exitButton.TabIndex = 11;
            this.exitButton.Tag = "about";
            this.exitButton.Text = "יציאה";
            this.exitButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // menuClickedMark
            // 
            this.menuClickedMark.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(201)))), ((int)(((byte)(244)))));
            this.menuClickedMark.Location = new System.Drawing.Point(296, 41);
            this.menuClickedMark.Margin = new System.Windows.Forms.Padding(2);
            this.menuClickedMark.Name = "menuClickedMark";
            this.menuClickedMark.Size = new System.Drawing.Size(8, 50);
            this.menuClickedMark.TabIndex = 7;
            this.menuClickedMark.TabStop = false;
            // 
            // aboutLabel
            // 
            this.aboutLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(55)))), ((int)(((byte)(56)))));
            this.aboutLabel.Font = new System.Drawing.Font("Secular One", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.aboutLabel.ForeColor = System.Drawing.Color.White;
            this.aboutLabel.Location = new System.Drawing.Point(0, 244);
            this.aboutLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.aboutLabel.Name = "aboutLabel";
            this.aboutLabel.Padding = new System.Windows.Forms.Padding(0, 0, 30, 0);
            this.aboutLabel.Size = new System.Drawing.Size(300, 50);
            this.aboutLabel.TabIndex = 10;
            this.aboutLabel.Tag = "about";
            this.aboutLabel.Text = "אודות";
            this.aboutLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.aboutLabel.Click += new System.EventHandler(this.MenuButton_Clicked);
            // 
            // settingsLabel
            // 
            this.settingsLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(55)))), ((int)(((byte)(56)))));
            this.settingsLabel.Font = new System.Drawing.Font("Secular One", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.settingsLabel.ForeColor = System.Drawing.Color.White;
            this.settingsLabel.Location = new System.Drawing.Point(2, 194);
            this.settingsLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.settingsLabel.Name = "settingsLabel";
            this.settingsLabel.Padding = new System.Windows.Forms.Padding(0, 0, 30, 0);
            this.settingsLabel.Size = new System.Drawing.Size(300, 50);
            this.settingsLabel.TabIndex = 9;
            this.settingsLabel.Tag = "general_settings";
            this.settingsLabel.Text = "הגדרות כלליות";
            this.settingsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.settingsLabel.Click += new System.EventHandler(this.MenuButton_Clicked);
            // 
            // profilesSettingsLabel
            // 
            this.profilesSettingsLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(55)))), ((int)(((byte)(56)))));
            this.profilesSettingsLabel.Font = new System.Drawing.Font("Secular One", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.profilesSettingsLabel.ForeColor = System.Drawing.Color.White;
            this.profilesSettingsLabel.Location = new System.Drawing.Point(2, 94);
            this.profilesSettingsLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.profilesSettingsLabel.Name = "profilesSettingsLabel";
            this.profilesSettingsLabel.Padding = new System.Windows.Forms.Padding(0, 0, 30, 0);
            this.profilesSettingsLabel.Size = new System.Drawing.Size(300, 50);
            this.profilesSettingsLabel.TabIndex = 6;
            this.profilesSettingsLabel.Tag = "filtering_settings";
            this.profilesSettingsLabel.Text = "הגדרות סינון";
            this.profilesSettingsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.profilesSettingsLabel.Click += new System.EventHandler(this.MenuButton_Clicked);
            // 
            // currentStatusLabel
            // 
            this.currentStatusLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.currentStatusLabel.Font = new System.Drawing.Font("Secular One", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.currentStatusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(201)))), ((int)(((byte)(244)))));
            this.currentStatusLabel.Location = new System.Drawing.Point(2, 41);
            this.currentStatusLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.currentStatusLabel.Name = "currentStatusLabel";
            this.currentStatusLabel.Padding = new System.Windows.Forms.Padding(0, 0, 30, 0);
            this.currentStatusLabel.Size = new System.Drawing.Size(300, 50);
            this.currentStatusLabel.TabIndex = 5;
            this.currentStatusLabel.Tag = "current_status";
            this.currentStatusLabel.Text = "מצב הגנה נוכחי";
            this.currentStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.currentStatusLabel.Click += new System.EventHandler(this.MenuButton_Clicked);
            // 
            // viewPanel
            // 
            this.viewPanel.AutoScroll = true;
            this.viewPanel.BackColor = System.Drawing.Color.White;
            this.viewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPanel.Location = new System.Drawing.Point(302, 0);
            this.viewPanel.Margin = new System.Windows.Forms.Padding(2);
            this.viewPanel.Name = "viewPanel";
            this.viewPanel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.viewPanel.Size = new System.Drawing.Size(880, 695);
            this.viewPanel.TabIndex = 2;
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 695);
            this.Controls.Add(this.viewPanel);
            this.Controls.Add(this.menuPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1200, 742);
            this.MinimizeBox = false;
            this.Name = "MenuForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "לוקחים אחריות";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MenuForm_FormClosing);
            this.Resize += new System.EventHandler(this.MenuForm_Resize);
            this.menuPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.StatusIconPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.menuClickedMark)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel menuPanel;
        private System.Windows.Forms.PictureBox menuClickedMark;
        private System.Windows.Forms.Label aboutLabel;
        private System.Windows.Forms.Label settingsLabel;
        private System.Windows.Forms.Label profilesSettingsLabel;
        private System.Windows.Forms.Label currentStatusLabel;
        private System.Windows.Forms.Panel viewPanel;
        private System.Windows.Forms.Label exitButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox StatusIconPB;
    }
}

