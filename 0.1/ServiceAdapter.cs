using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace _0._1
{
    class ServiceAdapter
    {
        private static readonly string SERVICE_NAME = "GUIAdapter";

        public enum CustomCommends { startScheduelBlocking=128, releaseScheduelBlocking=129}

        public static void InstallService(string exePath)
        {
            try
            {
                ManagedInstallerClass.InstallHelper(new string[] { exePath });
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("שגיאה בהתקנת תהליך: לא ניתן למצוא תהליך בנתיב" + Environment.NewLine + exePath);
            }
        }

        public static void UninstallService(string exePath)
        {
            try
            {
                ManagedInstallerClass.InstallHelper(new string[] { "/u", exePath });
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("שגיאה בהסרת תהליך: לא ניתן למצוא תהליך בנתיב" + Environment.NewLine + exePath);
            }
        }

        public static void StartService(string serviceName, int timeoutMilliseconds)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
            catch
            {}
        }

        public static Boolean StopService(string serviceName, int timeoutMilliseconds)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void CustomCommend(string serviceName,int commend)
        {
            ServiceController sc = new ServiceController(serviceName);
            ServiceControllerPermission scp = new ServiceControllerPermission(ServiceControllerPermissionAccess.Control, Environment.MachineName, serviceName);//this will grant permission to access the Service
            scp.Assert();
            sc.Refresh();

            sc.ExecuteCommand(commend);
        }

        public static void StartInternetBlocking()
        {
            CustomCommend(SERVICE_NAME, (int) CustomCommends.startScheduelBlocking);
        }

        public static void StopInterntBlocking()
        {
            CustomCommend(SERVICE_NAME, (int)CustomCommends.releaseScheduelBlocking);
        }

        public static string GetServiceStatus(string serviceName)
        {
            try
            {
                ServiceController service = new ServiceController(serviceName);
                return service.Status.ToString();
            }
            catch
            {
                return "Not Found";
            }
        }
    }
}
