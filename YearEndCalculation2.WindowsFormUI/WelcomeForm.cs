using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YearEndCalculation.WindowsFormUI.Properties;

namespace YearEndCalculation.WindowsFormUI
{
    public partial class WelcomeForm : Form
    {
        public WelcomeForm()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (chbDontShow.Checked)
            {
                Settings.Default.dontShowWelcome = true;
                Settings.Default.Save();
            }
            Close();
        }
    }
}
