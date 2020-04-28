using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows.Forms;

namespace _0._1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        public static MenuForm menuForm;
        public static PrivateFontCollection privateFontCollection = new PrivateFontCollection();

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);


            //Start by looking for administrator privilege
            if (IsAdmin())   
            {
                //If administrator privilege is granted

                //close the current proceess if same process already opened.
                if (Environment.GetCommandLineArgs().Length > 1)
                {
                    if (Environment.GetCommandLineArgs()[0] != "runAgainAsAdmin")
                        if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1) System.Diagnostics.Process.GetCurrentProcess().Kill();
                }

                // setup to custom font.
                SetupFont();

                //add the app to startup applications.
                TaskingSchedule.RunOnStartup("MMB", Application.StartupPath + "\\" + System.AppDomain.CurrentDomain.FriendlyName);

                //run the app service
                ServiceAdapter.StartService("GUIAdapter", 10000);

                //setup global menuForm.
                menuForm = new MenuForm();

                //check if this is the first time the app is opening on this computer.
                if (IsFirstOpening())
                    ActAsFirstOpening();

                //setup the filtering system.
                FilteringSystem.Setup();

                //start new menu form.
                Application.Run(menuForm);
            }
            else //
            {
                //If administrator privilege not granted, run again as administrator
                if (StartAgainAsAdmin())
                {
                    //By opening a new administrator-privileged process, close the current process.
                    Application.ExitThread();
                }
                else
                {
                    //If the second attempt of opening as an administrator permission failed, Block the Internet (by the service) and close the current procees.
                    ServiceAdapter.StartInternetBlocking();
                    MessageBox.Show("אפליקציה זו חייבת לרוץ כמנהל, האינטרנט מושבת.", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    Application.Exit();
                }
            }
        }

        static Boolean IsFirstOpening()
        {
            return Resources.profile.Default.first_opening || Resources.profile.Default.admin_password =="@NULL@";
        }

        static Boolean IsAdmin()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);

            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        static void ActAsFirstOpening()
        {
            Resources.profile.Default.Reset();
            Resources.profile.Default.Reload();
            Resources.profile.Default.customBlacklist = new List<string>();
            Resources.profile.Default.safe_server = true;
            Resources.profile.Default.system_status = true;
            Resources.profile.Default.first_opening = false;
            Resources.profile.Default.Save();
            menuForm.setPanel("first_opening");
        }

        private static void SetupFont()
        {
            // specify embedded resource name
            string resource = "_0._1.Resources.SecularOne-Regular.ttf";

            // receive resource stream
            Stream fontStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource);

            // create an unsafe memory block for the font data
            System.IntPtr data = Marshal.AllocCoTaskMem((int)fontStream.Length);

            // create a buffer to read in to
            byte[] fontdata = new byte[fontStream.Length];

            // read the font data from the resource
            fontStream.Read(fontdata, 0, (int)fontStream.Length);

            // copy the bytes to the unsafe memory block
            Marshal.Copy(fontdata, 0, data, (int)fontStream.Length);

            // pass the font to the font collection
            privateFontCollection.AddMemoryFont(data, (int)fontStream.Length);

            // close the resource stream
            fontStream.Close();

            // free up the unsafe memory
            Marshal.FreeCoTaskMem(data);
        }

        public static Boolean StartAgainAsAdmin()
        {
            System.Diagnostics.ProcessStartInfo processInfo = new System.Diagnostics.ProcessStartInfo(Assembly.GetExecutingAssembly().CodeBase);
            processInfo.UseShellExecute = true;
            processInfo.Verb = "runas";
            processInfo.Arguments = "runAgainAsAdmin";
            try
            {
                System.Diagnostics.Process.Start(processInfo);
                return true;
            }
            catch (Exception)
            {
                // The user did not allow the application to run as administrator
                return false;
            }
        }
    }
}
