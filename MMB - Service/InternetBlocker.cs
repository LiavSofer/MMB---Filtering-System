using System;
using System.Management;
using System.Net.NetworkInformation;
using System.Threading;

namespace MMBSERVICE
{
    class InternetBlocker
    {
        static Thread thread = new Thread(new ParameterizedThreadStart(BlockByStatus));
        public static void block(Boolean status)
        {
            try
            {
                thread.Start(status);
            }
            catch
            {
                thread = new Thread(new ParameterizedThreadStart(BlockByStatus));
                thread.Start(status);
            }
        }

        private static void BlockByStatus(object data)
        {
            Boolean status = (Boolean) data;
            try
            {
                ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection objMOC = objMC.GetInstances();
                foreach (ManagementObject objMO in objMOC)
                {
                    if (status)
                    {
                        objMO.InvokeMethod("ReleaseDHCPLease", null, null);
                    }
                    else
                    {
                        objMO.InvokeMethod("RenewDHCPLease", null, null);
                    }
                }
            }
            catch { }
        }

        public static Boolean isInternetIsReachable()
        {
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in adapters)
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                if (properties.DnsSuffix != "" || properties.GatewayAddresses.Count > 0)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
