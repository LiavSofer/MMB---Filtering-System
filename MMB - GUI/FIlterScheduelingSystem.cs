using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MMB_GUI
{
    class FIlterScheduelingSystem
    {
        static Button[,] buttons = new Button[49, 8];
        public static readonly Color startColor = Color.LightGray;
        static Color BlockedColor = Color.OrangeRed;

        static char[,] ScheduelData;

        static Panel table;

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        public static void LoadSavedTable()
        {
            Stream stream = null;
            try
            {
                stream = File.Open("SavedScheduelTable.bin", FileMode.Open);
                ScheduelData = (char[,])new BinaryFormatter().Deserialize(stream);
            }
            catch
            {
                ScheduelData = new char[49, 8];
                for (int y = 0; y < 49; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        ScheduelData[y, x] = 'F';
                    }
                }
            }
        }
        public static void SaveTable()
        {
            for (int y = 0; y < 49; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (buttons[y, x].BackColor == BlockedColor)
                        ScheduelData[y, x] = 'B';
                    if (buttons[y, x].BackColor == startColor)
                        ScheduelData[y, x] = 'F';
                }
            }

            Stream stream = null;
            try
            {
                stream = File.Open("SavedScheduelTable.bin", FileMode.Create);
                new BinaryFormatter().Serialize(stream, ScheduelData);
            }
            finally
            {
                stream.Close();
            }

        }

        public static void drawTable(int unitXSize, int unitYSize, int xDeviation, int yDeviation)
        {
            if (table != null)
                table.Dispose();
            table = new Panel();
            table.Location = new Point(300, 0);
            table.BackColor = Color.White;
            table.AutoScroll = true;
            table.Dock = DockStyle.Fill;

            DateTime dateTime = DateTime.Today;
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 49; y++)
                {
                    Button button = new Button();
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderSize = 0;
                    button.Tag = x.ToString() + y.ToString();
                    if (x < 7)
                    {
                        if (y > 0)
                        {
                            button.BackColor = ScheduelData[y, x] == 'B' ? BlockedColor : startColor;
                            button.MouseMove += singleHour_MouseMove;
                            button.MouseDown += Button_MouseDown;
                            button.Size = new Size(unitXSize, unitYSize);
                            button.Location = new Point((xDeviation + x * unitXSize), (yDeviation + 6 + y * unitYSize));
                        }
                        else
                        {
                            button.BackColor = Color.FromArgb(139, 201, 244);
                            button.Size = new Size(unitXSize, unitYSize + 6);
                            button.Location = new Point((xDeviation + x * unitXSize), (yDeviation + y * unitYSize));
                            button.ForeColor = Color.Black;
                            button.Font = new Font(Program.privateFontCollection.Families[0], 11, FontStyle.Bold);
                            string letters = "אבגדהוש";
                            button.Text = letters[6 - x].ToString();
                            button.MouseDown += Button_MouseDown;
                            button.MouseMove += day_MouseMove;
                        }
                    }
                    else
                    {
                        if (y > 0)
                        {
                            button.BackColor = Color.White;
                            button.Size = new Size(unitXSize + 10, unitYSize);
                            button.Location = new Point((xDeviation + x * unitXSize), (yDeviation + 6 + y * unitYSize));
                            button.Text = dateTime.ToShortTimeString();
                            button.Font = new Font(button.Font.FontFamily, 7, FontStyle.Bold);
                            button.TextAlign = ContentAlignment.MiddleRight;
                            dateTime = dateTime.AddMinutes(30);
                            button.MouseDown += Button_MouseDown;
                            button.MouseMove += weekly_hour_MouseMove;
                        }
                    }

                    buttons[y, x] = button;
                    table.Controls.Add(button);
                }
            }
        }

        private static void day_MouseMove(object sender, MouseEventArgs e)
        {
            int x = Convert.ToInt32(((sender as Button).Tag.ToString())[0].ToString());
            int y = Convert.ToInt32((sender as Button).Tag.ToString().Substring(1));
            for (int i = 1; i < 49; i++)
            {
                if (e.Button == MouseButtons.Left)
                {
                    buttons[i, x].BackColor = BlockedColor;
                }
                if (e.Button == MouseButtons.Right)
                {
                    buttons[i, x].BackColor = startColor;
                }
            }
        }

        private static void weekly_hour_MouseMove(object sender, MouseEventArgs e)
        {
            int x = Convert.ToInt32(((sender as Button).Tag.ToString())[0].ToString());
            int y = Convert.ToInt32((sender as Button).Tag.ToString().Substring(1));

            for (int i = 0; i < 7; i++)
            {
                if (e.Button == MouseButtons.Left)
                {
                    buttons[y, i].BackColor = BlockedColor;
                }
                if (e.Button == MouseButtons.Right)
                {
                    buttons[y, i].BackColor = startColor;
                }
            }
        }

        private static void singleHour_MouseMove(object sender, MouseEventArgs e)
        {
            int x = Convert.ToInt32(((sender as Button).Tag.ToString())[0].ToString());
            int y = Convert.ToInt32((sender as Button).Tag.ToString().Substring(1));

            if (Control.MouseButtons == MouseButtons.Left)
            {
                buttons[y, x].BackColor = BlockedColor;
            }
            if (Control.MouseButtons == MouseButtons.Right)
            {
                buttons[y, x].BackColor = startColor;
            }
        }
        private static void Button_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
        }

        public static Boolean isBlockdAt(DateTime dateTime)
        {
            int x = 7-((int)dateTime.DayOfWeek+1);
            int y = (int)dateTime.Hour * 2+1;
            if (dateTime.Minute >= 30)
                y += 1;
            return ScheduelData[y, x] == 'B';
        }

        public static Panel getTable()
        {
            return table;
        }
    }
}
