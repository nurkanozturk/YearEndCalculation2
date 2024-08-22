using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YearEndCalculation.WindowsFormUI;
using YearEndCalculation.WindowsFormUI.Properties;
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
            LoginForm form = new LoginForm();

            //LicenceSetting.Default.Licenced = false;
            //LicenceSetting.Default.Save();

            if (LicenceSetting.Default.Licenced)
            {
                Application.Run(new FormMain());
            }
            else
            {
                if (form.licencedBefore())
                {
                    Application.Run(new FormMain());
                }else
                {
                    Application.Run(new LoginForm());

                }
            }

            

        }
    }


}
