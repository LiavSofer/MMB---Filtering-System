using System;
using Microsoft.Win32;

namespace MMB_GUI
{
    class TaskingSchedule
    {
        public static bool RunOnStartup(string AppTitle, string AppPath)
        {
            RegistryKey rk;
            try
            {
                rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                rk.SetValue(AppTitle, AppPath);
                return true;
            }
            catch (Exception)
            {
            }

            try
            {
                rk = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                rk.SetValue(AppTitle, AppPath);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}