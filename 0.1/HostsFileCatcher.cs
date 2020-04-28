using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace _0._1
{

    //המחחלקה פותחת קריאה לקובץ הוסט ובכך מונעת עריכה שלו
    class HostsFileCatcher
    {
        private static List<string> paths = new List<string>();
        private static List<StreamReader> streamReaders = new List<StreamReader>();

        internal static void AddPath(string path)
        {
            paths.Add(path);
        }
        
        public static void StartCatching()
        {
            foreach (string path in paths)
            {
                File.SetAttributes(path, FileAttributes.Hidden | FileAttributes.ReadOnly | FileAttributes.System | FileAttributes.Encrypted);
                //try
                //{
                //    streamReaders.Add(new StreamReader(path));
                //}
                //catch { }
            }
        }

        public static void ReleaseCatch()
        {
            foreach(string path in paths)
                File.SetAttributes(path, FileAttributes.Normal);

            //for (int i = 0; i < streamReaders.Count; i++)
            //{
            //    StreamReader sr = streamReaders[i];
            //    if (sr != null)
            //        sr.Dispose();
            //    streamReaders.Remove(sr);
            //    sr = null;
            //}
        }

        public static void WriteSettings()
        {
            int temp = 0;
            ReleaseCatch();
            IEnumerable<string> toWrite = BlacklistCreator.getAsHosts();
            while (temp < 5)
            {
                try
                {
                    if (Resources.profile.Default.system_status)
                        File.WriteAllLines(Environment.SystemDirectory+@"\drivers\etc\hosts", toWrite);
                    else
                        File.WriteAllLines(Environment.SystemDirectory + @"\drivers\etc\hosts", new[] { "" });
                    break;
                }
                catch
                {
                    ReleaseCatch();
                    temp++;
                    System.Threading.Thread.Sleep(1000);
                }
            }
            if (temp == 5)
            {
               // System.Windows.Forms.MessageBox.Show("כתיבה לא הצליחה");
            }
            StartCatching();
        }

        public static Boolean getStatus()
        {
            try
            {
                using (var fs = new FileStream(Environment.SystemDirectory + @"\drivers\etc\hosts", FileMode.Open))
                {
                    return !(fs.CanWrite);
                }
            }
            catch
            {
                return true;
            }
        }
    }
}
