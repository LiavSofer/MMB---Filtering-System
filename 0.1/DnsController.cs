using System;
using System.Net.NetworkInformation;
using System.Management;
using System.Net;
namespace _0._1
{
    class DnsController
    {
        private static readonly string PREFERRED_SAFE_DNS = "185.228.168.168";
        private static readonly string ALTERNATE_SAFE_DNS = "185.228.169.168";

        public static void setMode(Boolean safeServer)
        {
            if (safeServer)
            {
                setSafeDNS();
            }
            else
            {
                setDHCP_DNS();
            }
        }
        public static void setSafeDNS()
        {
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in adapters)
            {
                SetDNS(adapter, PREFERRED_SAFE_DNS, ALTERNATE_SAFE_DNS);
            }
        }

        public static void setDHCP_DNS()
        {
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in adapters)
            {
                UnsetDNS(adapter);
            }
        }


        private static void SetDNS(NetworkInterface adapter, string PreferredDNS, string AlternateDNS)
        {
            string[] Dns = { PreferredDNS, AlternateDNS };
            var CurrentInterface = adapter;
            if (CurrentInterface == null) return;

            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMC.GetInstances();
            foreach (ManagementObject objMO in objMOC)
            {
                if ((bool)objMO["IPEnabled"])
                {
                    if (objMO["Caption"].ToString().Contains(CurrentInterface.Description))
                    {
                        ManagementBaseObject objdns = objMO.GetMethodParameters("SetDNSServerSearchOrder");
                        if (objdns != null)
                        {
                            objdns["DNSServerSearchOrder"] = Dns;
                            objMO.InvokeMethod("SetDNSServerSearchOrder", objdns, null);
                        }
                    }
                }
            }
        }
        private static void UnsetDNS(NetworkInterface adapter)
        {
            var CurrentInterface = adapter;
            if (CurrentInterface == null) return;

            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMC.GetInstances();
            foreach (ManagementObject objMO in objMOC)
            {
                if ((bool)objMO["IPEnabled"])
                {
                    if (objMO["Caption"].ToString().Contains(CurrentInterface.Description))
                    {
                        ManagementBaseObject objdns = objMO.GetMethodParameters("SetDNSServerSearchOrder");
                        if (objdns != null)
                        {
                            objdns["DNSServerSearchOrder"] = null;
                            objMO.InvokeMethod("SetDNSServerSearchOrder", objdns, null);
                        }
                    }
                }
            }
        }

        public static Boolean isSafe(Boolean b)
        {
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                if (networkInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet || networkInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                {
                    if (networkInterface.OperationalStatus == OperationalStatus.Up)
                    {
                        IPInterfaceProperties ipProperties = networkInterface.GetIPProperties();
                        IPAddressCollection dnsAddresses = ipProperties.DnsAddresses;

                        foreach (IPAddress dnsAddress in dnsAddresses)
                        {
                            if (dnsAddress.ToString() != PREFERRED_SAFE_DNS && dnsAddress.ToString() != ALTERNATE_SAFE_DNS)
                            {
                                if (b)
                                    System.Windows.Forms.MessageBox.Show(networkInterface.Name + "   " + networkInterface.NetworkInterfaceType + "     " + dnsAddress.ToString());
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
    }
}
