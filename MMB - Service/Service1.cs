using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace MMBSERVICE
{
    public partial class Service1 : ServiceBase
    {
        [DllImport("wtsapi32.dll", SetLastError = true)]
        static extern bool WTSSendMessage(IntPtr hServer, [MarshalAs(UnmanagedType.I4)] int SessionId, String pTitle, [MarshalAs(UnmanagedType.U4)] int TitleLength, String pMessage, [MarshalAs(UnmanagedType.U4)] int MessageLength, [MarshalAs(UnmanagedType.U4)] int Style, [MarshalAs(UnmanagedType.U4)] int Timeout, [MarshalAs(UnmanagedType.U4)] out int pResponse, bool bWait);

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int description, int reservedValue);

        public static IntPtr WTS_CURRENT_SERVER_HANDLE = IntPtr.Zero;
        public static int WTS_CURRENT_SESSION = 1;

        Boolean msgShowed = false;
        System.Timers.Timer timer = new System.Timers.Timer();

        public Service1()
        {
            InitializeComponent();
        }

        public static bool IsInternetAvailable()
        {
            try
            {
                int description;
                return InternetGetConnectedState(out description, 0);
            }
            catch
            {
                return false;
            }
        }


        protected override void OnStart(string[] args)
        {
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 3500; //number in milisecinds  
            timer.Enabled = true;
        }    
        protected override void OnCustomCommand(int command)
        {
            switch (command)
            {
                case 128:
                    scheduelBlock = true;
                    break;
                case 129:
                    scheduelBlock = false;
                    break;
            }
        }
        protected override void OnStop()
        {
            timer.Stop();
            timer.Enabled = false;
            InternetBlocker.block(false);
        }

        private Boolean processOpen()
        {
            foreach (Process p in Process.GetProcesses())
            {
                if (p.ProcessName == "MMB-Filter")
                {
                    return true;
                }
            }
            return false;
        }

        int count = 0;
        Boolean scheduelBlock;

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            if (!processOpen())
            {
                if (IsInternetAvailable())
                    InternetBlocker.block(true);

                if (count % 15 == 0 && count > 3)
                    showMessage("MMB Filtering Sysetm", "The system recognized that the main process was unexpectedly shut down. The Internet is disabled until the filtering system will be restart.");

                count++;
            }
            else
            {
                if (scheduelBlock)
                {
                    if(IsInternetAvailable())
                        InternetBlocker.block(true);
                }
                else
                {
                    count = 15;
                    if (!IsInternetAvailable())
                    {
                        InternetBlocker.block(false);
                    }
                }
            }
        }
        
        private void showMessage(string title, string msg)
        {
            if (!msgShowed)
            {
                for (int user_session = 10; user_session > 0; user_session--)
                {
                    Thread t = new Thread(() =>
                    {
                        try
                        {
                            msgShowed = true;
                            bool result = false;
                            int tlen = title.Length;
                            int mlen = msg.Length;
                            int resp = 7;
                            result = WTSSendMessage(WTS_CURRENT_SERVER_HANDLE, user_session, title, tlen, msg, mlen, 0, 0, out resp, true);
                            int err = Marshal.GetLastWin32Error();
                            if (err == 0)
                            {
                                if (result) //user responded to box
                                {
                                    //if (resp == 1) //user clicked ok
                                    //{

                                    //}
                                    msgShowed = false;
                                    Debug.WriteLine("user_session:" + user_session + " err:" + err + " resp:" + resp);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("no such thread exists", ex);
                        }
                        //Application App = new Application();
                        //App.Run(new MessageForm());
                    });
                    t.SetApartmentState(ApartmentState.STA);
                    t.Start();
                }
            }
        }

    }
}