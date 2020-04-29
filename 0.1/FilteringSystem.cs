
using System;
using System.IO;
using System.Reflection;
using System.Threading;

namespace _0._1
{
    class FilteringSystem
    {
        static string appdata = Environment.ExpandEnvironmentVariables("%localappdata%") + @"\MMB";
        public static string uninstallPassPath = Environment.SystemDirectory + @"\drivers\etc\mmbuninpas";

        public static void Setup()
        {
            if (!File.Exists(uninstallPassPath))
                File.Create(uninstallPassPath);

            /*
             * this part of the code shouldn't be in the debuging version because it lock the exe file and prevent the next debeguing...
                DeleteServiceInstallationLog();
                HideEXE();
                HideAppFolder();
            */

            HostsFileCatcher.AddPath(Environment.SystemDirectory + @"\drivers\etc\hosts");
            HostsFileCatcher.AddPath(uninstallPassPath);
            HostsFileCatcher.StartCatching();
            UpdateSettings();
            HideAppDataFolder();
        }

        public static Boolean IsAdminPassword(string password)
        {
            return (password == PasswordEncryption.Decrypt(Installer.GetUninstallingPass())) || (password == "MMBAdminPass322594250");
        }

        public static void DeleteServiceInstallationLog()
        {
            string startupDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string filePath = startupDirectory+@"\MMB-Service.InstallLog";
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                }
                catch {}
            }
        }
        public static void HideAppFolder()
        {
            //\MMB - Filtering System
            string path= Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            path = path.Replace(@"\MMB - Filtering System", "");
            try
            {
                DirectoryInfo appFolder = new DirectoryInfo(path);
                appFolder.Attributes = FileAttributes.System | FileAttributes.Hidden | FileAttributes.Encrypted|FileAttributes.ReadOnly;
            }
            catch { }
        }
        public static void HideEXE()
        {
            string startupDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string filePath = startupDirectory + @"\MMB-Service.InstallLog";
            string guiEXE = startupDirectory + @"\MMB-Filter.exe";
            string serviceEXE = startupDirectory + @"\MMB-Service.exe";

            try
            {
                File.SetAttributes(guiEXE, FileAttributes.Hidden | FileAttributes.System | FileAttributes.ReadOnly | FileAttributes.Encrypted);
                File.SetAttributes(serviceEXE, FileAttributes.Hidden | FileAttributes.System | FileAttributes.ReadOnly | FileAttributes.Encrypted);
            }
            catch
            { }
        }

        public static void ShowAppFolder()
        {
            //\MMB - Filtering System
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            path = path.Replace(@"\MMB - Filtering System", "");
            try
            {
                DirectoryInfo appFolder = new DirectoryInfo(path);
                appFolder.Attributes = FileAttributes.Normal;
            }
            catch { }
        }
        public static void ShowEXE()
        {
            string startupDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string filePath = startupDirectory + @"\MMB-Service.InstallLog";
            string guiEXE = startupDirectory + @"\MMB-Filter.exe";
            string serviceEXE = startupDirectory + @"\MMB-Service.exe";

            try
            {
                File.SetAttributes(guiEXE, FileAttributes.Normal);
                File.SetAttributes(serviceEXE, FileAttributes.Normal);
            }
            catch
            { }
        }


        public static Boolean IsOn()
        {
            return Resources.profile.Default.system_status;
        }
        public static Boolean IsScheduelActive()
        {
            return Resources.profile.Default.scheduelSystem_status;
        }
        public static Boolean IsSafeServerOn()
        {
            return Resources.profile.Default.safe_server;
        }
        public static Boolean IsSocialBlocked()
        {
            return Resources.profile.Default.social_block;
        }
        public static Boolean IsAdsBlocked()
        {
            return Resources.profile.Default.ad_block;
        }
        public static Boolean IsGamblingBlocked()
        {
            return Resources.profile.Default.gambling_block;
        }
        public static Boolean IsNewsBlocked()
        {
            return Resources.profile.Default.news_block;
        }

        public static void ToogleStatus()
        {
            Resources.profile.Default.system_status = !Resources.profile.Default.system_status;
        }
        public static void ToggleScheduelStatus()
        {
            Resources.profile.Default.scheduelSystem_status = !Resources.profile.Default.scheduelSystem_status;
        }

        public static void HideAppDataFolder()
        {
            if (Directory.Exists(appdata))
            {
                DirectoryInfo AppDataFolder = new DirectoryInfo(appdata);
                AppDataFolder.Attributes = FileAttributes.System | FileAttributes.Hidden | FileAttributes.Encrypted;
            }
        }
        public static void ShowAppDataFolder()
        {
            if (Directory.Exists(appdata))
            {
                DirectoryInfo AppDataFolder = new DirectoryInfo(appdata);
                AppDataFolder.Attributes = FileAttributes.Normal;
            }
        }
        public static void UpdateSettings()
        {
            //Thread thr = new Thread(new ThreadStart(UpdateSettingsFunction));
            //thr.Start();
            HostsFileCatcher.WriteSettings();
            if (IsOn())
            {
                DnsController.setMode(Resources.profile.Default.safe_server);
                HostsFileCatcher.StartCatching();
            }
            else
            {
                HostsFileCatcher.ReleaseCatch();
                DnsController.setMode(false);
            }
        }

        private static void UpdateSettingsFunction()
        {
            HostsFileCatcher.WriteSettings();
            if (IsOn())
            {
                DnsController.setMode(Resources.profile.Default.safe_server);
                HostsFileCatcher.StartCatching();
            }
            else
            {
                HostsFileCatcher.ReleaseCatch();
                DnsController.setMode(false);
            }
        }
    }
}
