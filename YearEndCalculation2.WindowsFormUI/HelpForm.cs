using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YearEndCalculation.Business.Concrete;
using YearEndCalculation2.WindowsFormUI;

namespace YearEndCalculation.WindowsFormUI
{
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();
        }

        private void HelpForm_Load(object sender, EventArgs e)
        {
            
            if (FormMain.darkMode)
            {
                this.BackColor = Color.FromArgb(37, 37, 41);
                Color textColor = Color.FromArgb(231, 231, 231);
                label1.ForeColor = label2.ForeColor = label3.ForeColor = label4.ForeColor = label5.ForeColor = label6.ForeColor = label7.ForeColor = label9.ForeColor = textColor;
            }
        }

        private void linkLblOleDb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.microsoft.com/en-us/download/details.aspx?id=13255");
        }
    }
}
