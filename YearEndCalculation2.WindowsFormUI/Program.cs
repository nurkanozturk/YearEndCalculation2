using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YearEndCalculation.WindowsFormUI;
using YearEndCalculation2.Business.Abstract;
using YearEndCalculation2.Business.DependencyResolvers.Ninject;

namespace YearEndCalculation2.WindowsFormUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 


        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //LoginForm form = new LoginForm();
            //if (!form.isOk())
            //{
               // Application.Run(new LoginForm());
            //}

            //if (form.isOk())
            //{
                Application.Run(new FormMain());
            //}

        }
    }


}
