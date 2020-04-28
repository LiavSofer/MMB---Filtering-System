using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace _0._1.menuForms
{
    public partial class Schedule : UserControl
    {

        public Schedule()
        {
            InitializeComponent();
            FontSetup();
            FIlterScheduelingSystem.LoadSavedTable();
            FIlterScheduelingSystem.drawTable(40, 20, 10, 10);
            schedulePanel.Controls.Add(FIlterScheduelingSystem.getTable());
            scheduleBlockChecker_Tick(null, null);
        }

        private void FontSetup()
        {
            foreach (Control control in tableLayoutPanel2.Controls)
            {
                control.Font = new Font(Program.privateFontCollection.Families[0], control.Font.Size, control.Font.Style);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) //Accept Button
        {
            if (keyData == Keys.Enter)
            {
                saveChangsButton.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void saveChangsButton_Click(object sender, EventArgs e)
        {
            //Save changes in the schedueling table.
            FIlterScheduelingSystem.SaveTable();

            MessageBox.Show("השינויים נשמרו בהצלחה!", "לוקחים אחריות", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Refresh for current status
            RefreshStatus();
        }

        public void RefreshStatus()
        {
            if (FilteringSystem.IsScheduelActive())
            {
                if (FIlterScheduelingSystem.isBlockdAt(DateTime.Now))
                {
                    statusLabel.Text = "חסימה מתוזמנת";
                    statusLabel.ForeColor = Color.Red;

                    //Block the Internt using the service
                    ServiceAdapter.StartInternetBlocking();
                }
                else
                {
                    statusLabel.Text = "גלישה מאופשרת";
                    statusLabel.ForeColor = Color.LimeGreen;

                    //release service's internet blocking
                    ServiceAdapter.StopInterntBlocking();
                }
            }
            else
            {
                statusLabel.Text = "לא פעיל";
                statusLabel.ForeColor = Color.Red;
                ServiceAdapter.StopInterntBlocking();
            }
        }

        public void scheduleBlockChecker_Tick(object sender, EventArgs e)
        {
            //On Tick
            RefreshStatus();
        }
    }
}
