using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YearEndCalculation.WindowsFormUI
{
    public partial class LoginForm : Form
    {

        public LoginForm()
        {
            InitializeComponent();

        }



        private void btnSerial_Click(object sender, EventArgs e)
        {
            if (mtbxSerial.Text.ToUpper() == createPass())
            {
                MessageBox.Show("Teşekkür eder iyi çalışmalar dileriz.");
                Properties.LicenceSetting.Default.IsLicensed = true;
                Properties.LicenceSetting.Default.Save();
                this.Close();
            }
            else
            {
                MessageBox.Show("Geçersiz anahtar. Lütfen kontrol ediniz.", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public bool isOk()
        {
            return Properties.LicenceSetting.Default.IsLicensed;
        }
        private string createPass()
        {
            string mnth = DateTime.Now.ToString("MMMM");
            int mnthTotal = 0;
            foreach (var item in mnth)
            {
                mnthTotal += (int)item;
            }
            int i = (DateTime.Now.Hour * DateTime.Now.Day * (mnthTotal + DateTime.Now.Year)) % 50;

            string keys = "3292-9c77-44be-b3b2-00bbefe8-47f0-4487-a317-e677884e-5558-4f89-aea4-1123ae5a-285c-48aa-b0fc-2a001441-9b2a-4167-a6a6-50c8ef04-777f-496c-9d30-fa5e0fc1-73b3-4f8f-9f0d-2663b08d-d5d8-4435-a8be-7f63ce77-7c9b-4cb4-8b91-9d151146-17ef-42e4-8849-2f121823-55f7-417a-a770-e5abc50e-9d1f-4ead-9d3e-0e05ee7b-3fbe-4f61-9e91-a2449401-f41f-40cf-bc6d-54138480-561e-46f0-9fdc-f8e732be-cf1e-48fc-9721-ef2727c7-3da5-451f-a9cb-e2fdf27e-12fe-47af-98f9-b2261145-7cf2-447c-9bb1-c9addbf9-d21c-47fb-b7cb-664e28dd-c963-47eb-b350-49f5f4d8-42db-4e17-892d-ad33a105-caf4-4208-83c3-a3c44861-e662-42eb-ab73-24457b0f-4ad4-4241-a7a2-9d77f088-1a73-4cfb-bd2e-f7a2986b-240e-45f0-9d4a-4ab834f8-76e7-421a-a50a-bef48c01-9825-4d52-860b-d5f33e46-0553-4d8f-a6ae-b6082dc6-7708-4e27-a40f-c20d4224-4137-40de-ac45-0d539759-f8b8-44e3-8d5e-2bb007fa-bb95-46ca-83e2-6b8f1c01-01ef-409b-ac71-1db774fd-6049-4d12-9247-fa9270ff-ac1d-44c1-8b86-fcfddc2f-143a-4926-a68e-082e36ef-7e5e-4e29-a446-bf9c49ab-9fb0-4f48-a5c3-37211bc6-69c0-455e-b823-2de39cb2-aca3-4b0c-84b6-75c1762f-05b7-40b4-92b9-8be811d5-bb28-4d3b-ae4a-74f62ae3-eb93-414d-9433-4b30aa6a-4ef7-44ed-bb3c-cf16da29-b3eb-4b77-a0ce-0f03b552-ebe9-44f0-b2be-09279477-6198-4250-a0b7-890330bb-b65c-483d-a472-a242";
            return keys.Substring(i * 24, 24).ToUpper();
        }

        private void mtbxSerial_KeyDown(object sender, KeyEventArgs e)
        {
            if (mtbxSerial.Text == "    -    -    -    -")
            {
                mtbxSerial.Select(0, 0);
            }

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            this.Icon = SystemIcons.Application;
        }
    }
}
