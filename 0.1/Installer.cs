using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Security.AccessControl;
using System.Reflection;

namespace _0._1
{
    [RunInstaller(true)]
    public partial class Installer : System.Configuration.Install.Installer
    {
        private Boolean installingProcess = true;

        public Installer()
        {
            InitializeComponent();
            this.Committed += new InstallEventHandler(On_Committed);
        }

        public static void SetUninstallingPass(string pass)
        {
            int attempts = 0;
            string path = FilteringSystem.uninstallPassPath;
            HostsFileCatcher.ReleaseCatch();
            while (attempts < 5)
            {
                try
                {
                    File.SetAttributes(path, FileAttributes.Normal);
                    File.WriteAllText(path,pass);
                    File.SetAttributes(path, FileAttributes.Hidden | FileAttributes.ReadOnly | FileAttributes.System| FileAttributes.Encrypted);
                    HostsFileCatcher.StartCatching();
                    break;
                }
                catch
                {
                    HostsFileCatcher.ReleaseCatch();
                    attempts++;
                    System.Threading.Thread.Sleep(250);
                }
            }
            if (attempts == 5)
            {
                MessageBox.Show("השינויים לא נשמרו כראוי");
            }
            HostsFileCatcher.StartCatching();
        }

        public static string GetUninstallingPass()
        {
            return File.ReadAllLines(FilteringSystem.uninstallPassPath)[0];
        }

        protected override void OnBeforeInstall(IDictionary savedState)
        {
            base.OnBeforeInstall(savedState);
            try
            {
                string servicePath = Assembly.GetExecutingAssembly().CodeBase;
                servicePath = servicePath.Replace("MMB-Filter.exe", "MMB-Service.exe");
                ServiceAdapter.InstallService(servicePath);
                ServiceAdapter.StartService("GUIAdapter", 10000);
            }
            catch
            {
                MessageBox.Show("לא ניתן להתקין תוסף שירות");
            }
        }

        private Boolean IsAllAppClosed(string mainModuleFileName)
        {
            try
            {
                foreach (Process process in Process.GetProcesses())
                {
                    if (process.ProcessName == mainModuleFileName)
                    {
                        return false;
                    }
                }
            }
            catch { }
            return true;
        }

        protected override void OnBeforeUninstall(IDictionary savedState)
        {
            base.OnBeforeInstall(savedState);

            //Prevent uninstalling while the app is on
            if(!IsAllAppClosed("MMB-Filter"))
            {
                MessageBox.Show("לפני הסרת התקנה יש לסגור את המערכת. סגור את המערכת ולאחר מכן נסה שנית.");
                throw new ApplicationException("לא ניתן להסיר את התוכנה בזמן שאחד המופעים שלה פתוח");
            }

            installingProcess = false;
            
            //ask for password
            Dialogs.EnterPassword enterPassword = new Dialogs.EnterPassword("על מנת להסיר את ההתקנה יש להזין סיסמת מנהל", true);
            DialogResult dialogResult = enterPassword.ShowDialog();
            enterPassword.BringToFront();

            if (dialogResult == DialogResult.OK)
            {
                string gettedPass = enterPassword.gettedText;
                if (gettedPass == PasswordEncryption.Decrypt(Installer.GetUninstallingPass()) || FilteringSystem.IsAdminPassword(gettedPass))
                {
                    string localDataPath = Environment.ExpandEnvironmentVariables("%localappdata%") + @"\MMB\";

                    //Delete the appData
                    Directory.Delete(localDataPath,true);

                    //Set dhcp dns
                    DnsController.setMode(false);

                    //Stop the internt blocker service
                    ServiceAdapter.StopService("GUIAdapter", 10000);

                    //Uninstall the Service
                    string servicePath = Assembly.GetExecutingAssembly().CodeBase;
                    servicePath = servicePath.Replace("MMB-Filter.exe", "MMB-Service.exe");
                    ServiceAdapter.UninstallService(servicePath);

                    //realese the internet blocking
                    InternetBlocker.block(false);

                    MessageBox.Show("הסרת ההתקנה הסתיימה", "לוקחים אחריות", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information, System.Windows.Forms.MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.ServiceNotification);
                }
                else
                {
                    MessageBox.Show("הסיסמה שגויה, התראה נשלחה למנהל המערכת", "לוקחים אחריות", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    throw new ApplicationException("לא ניתן להסיר באמצעות סיסמה שגויה");
                }
            }
        } 
        private void On_Committed(object sender, InstallEventArgs e)
        {
            InternetBlocker.block(false);
            if (installingProcess)
            {
                MessageBox.Show("ההתקנה הסתיימה, לחץ למעבר למסך ההגדרה הראשוני", "לוקחים אחריות", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information, System.Windows.Forms.MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.ServiceNotification);
                Program.StartAgainAsAdmin();
            }    
        }
    }
}
