using Microsoft.Vbe.Interop;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Security;
using System.Windows.Forms;
using System.Xml;
using YearEndCalculation.Business.Concrete;
using YearEndCalculation.Business.Tools;
using YearEndCalculation.Entities.Concrete;
using YearEndCalculation.WindowsFormUI;
using YearEndCalculation.WindowsFormUI.Properties;
using YearEndCalculation2.Business.Abstract;
using YearEndCalculation2.Business.Concrete.Managers;
using YearEndCalculation2.Business.DependencyResolvers.Ninject;

namespace YearEndCalculation2.WindowsFormUI
{
    public partial class FormMain : Form
    {
        private decimal _mkysEntryTotal, _mkysExitTotal, _wrongTdmsEntry, _wrongTdmsExit;
        private string dgwPrint = "MKYS GİRİŞLERİ";
        private decimal _mkysRemain;
        private decimal _tdmsRemain;
        private List<YearEndCalculation.Entities.Concrete.Match> _matchListForExcel;
        public static List<List<ActionRecord>> DgwItems;
        public static bool _skipAllChoose = false;
        private bool isThereChoose = false;



        public static bool darkMode = new ThemeManager().WatchTheme() == 0 ? true : false;
        bool calculated = false;

        List<ActionRecord> _mkysEntries = new List<ActionRecord>();
        List<ActionRecord> _mkysExits = new List<ActionRecord>();
        List<ActionRecord> _tdmsEntries = new List<ActionRecord>();
        List<ActionRecord> _tdmsExits = new List<ActionRecord>();

        List<ActionRecord> _mkysEntriesDgw = new List<ActionRecord>();
        List<ActionRecord> _mkysExitsDgw = new List<ActionRecord>();
        List<ActionRecord> _tdmsEntriesDgw = new List<ActionRecord>();
        List<ActionRecord> _tdmsExitsDgw = new List<ActionRecord>();

        List<ActionRecord> _mkysEntriesBase = new List<ActionRecord>();
        List<ActionRecord> _mkysExitsBase = new List<ActionRecord>();
        List<ActionRecord> _tdmsEntriesBase = new List<ActionRecord>();
        List<ActionRecord> _tdmsExitsBase = new List<ActionRecord>();

        

        IYearEndService _yearEnd = InstanceFactory.GetInstance<IYearEndService>();

        FillMkys _fillMkys = new FillMkys();
        FillTdms _fillTdms = new FillTdms();

        ProgressForm progressForm = new ProgressForm();
        public FormMain()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            if (CheckLicenceDate())
            {
                MessageBox.Show("Bu sürümün lisans süresi sona erdi. Lütfen yeni lisans satın alınız.");
                LicenceSetting.Default.Licenced = false;
                LicenceSetting.Default.Save();
                this.Close();
            }
            //tgglBtn.Checked = darkMode;

            lblNoProblem.Visible = false;
            dgw.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(225, 225, 225);
            dgw.EnableHeadersVisualStyles = false;
            Color bgColor, bgColor2, headerColor, textColor, caclTextColor, cellBgColor, selectionBgColor, foreColor, calcBgColor, tabTextColor, prcTextColor, rtbxBgColor, tabBgColor;

            if (darkMode)
            {
                bgColor = Color.FromArgb(37, 37, 41);
                textColor = Color.FromArgb(231, 231, 231);
                foreColor = Color.DarkGray;
                bgColor2 = rtbxBgColor = tabBgColor = Color.FromArgb(60, 60, 63);
                caclTextColor = Color.FromArgb(40, 40, 40);
                headerColor = Color.FromArgb(16, 27, 26);
                cellBgColor = Color.FromArgb(45, 53, 51);
                selectionBgColor = Color.DarkSlateGray;
                prcTextColor = Color.OrangeRed;
                calcBgColor = Color.Chocolate;
                tabTextColor = Color.WhiteSmoke;
                btnMatch.BackColor= Color.FromArgb(32, 29, 41);
                btnMatch.ForeColor = Color.DarkGray;
                btnCorrect.BackColor = Color.FromArgb(32, 29, 41);
                btnCorrect.ForeColor = Color.DarkGray;
                btnPrint.Image = Image.FromFile("icons8-print-50-2.png");
            }
            else
            {
                bgColor = rtbxBgColor = Color.FromArgb(250, 248, 245);
                bgColor2 = Color.OldLace;
                headerColor = SystemColors.ControlLight;
                textColor = SystemColors.WindowText;
                cellBgColor = SystemColors.Window;
                selectionBgColor = Color.PowderBlue;
                foreColor = Color.Black;
                calcBgColor = Color.DarkOrange;
                tabTextColor = Color.WhiteSmoke;
                prcTextColor = Color.DarkRed;
                caclTextColor = SystemColors.WindowText;
                tabBgColor = SystemColors.InactiveBorder;
                btnMatch.BackColor = Color.FromArgb(216, 214, 226);
                btnMatch.ForeColor = SystemColors.ControlText;
                btnCorrect.BackColor = Color.FromArgb(216, 214, 226);
                btnCorrect.ForeColor = SystemColors.ControlText;
                btnPrint.Image = Image.FromFile("icons8-print-50.png");

            }
            BackColor = bgColor;

            dgw.BackgroundColor = bgColor2;
            dgw.ColumnHeadersDefaultCellStyle.BackColor = headerColor;
            dgw.ColumnHeadersDefaultCellStyle.ForeColor = textColor;
            dgw.RowHeadersDefaultCellStyle.BackColor = bgColor2;
            dgw.DefaultCellStyle.BackColor = cellBgColor;
            dgw.ForeColor = textColor;
            dgw.ColumnHeadersDefaultCellStyle.SelectionBackColor = headerColor;
            dgw.RowsDefaultCellStyle.SelectionForeColor = textColor;
            dgw.RowsDefaultCellStyle.SelectionBackColor = selectionBgColor;

            ForeColor = gbxEntrySelect.ForeColor = gbxExitSelect.ForeColor = gbxTdmsSelect.ForeColor = gbxRemain.ForeColor = gbxResult.ForeColor = foreColor;
            
            gbxResult.BackColor = bgColor;

            tbxMkysRemain.BackColor = bgColor2;
            tbxTdmsRemain.BackColor = bgColor2;

            prcDifrence.FlatAppearance.MouseOverBackColor = prcDifrence.FlatAppearance.MouseDownBackColor = bgColor;

            prcDifrence.ForeColor = prcTextColor;

            rtbxMkysEntry.BackColor = rtbxMkysExit.BackColor = rtbxTdms.BackColor = rtbxBgColor;
            rtbxMkysEntry.ForeColor = rtbxMkysExit.ForeColor = rtbxTdms.ForeColor = textColor;

            lblMkysEntryCount.FlatAppearance.MouseOverBackColor = lblMkysExitCount.FlatAppearance.MouseOverBackColor = lblTdmsCount.FlatAppearance.MouseOverBackColor = bgColor;

            lblMkysEntryCount.FlatAppearance.MouseDownBackColor = lblMkysExitCount.FlatAppearance.MouseDownBackColor = lblTdmsCount.FlatAppearance.MouseDownBackColor = bgColor;

            lblNoProblem.BackColor = bgColor2;
            lblNoProblem.ForeColor = textColor;

            tbxMkysRemain.ForeColor = tbxTdmsRemain.ForeColor = textColor;

            btnCalculate.BackColor = calcBgColor;
            btnCalculate.ForeColor = caclTextColor;

            btnTab1.ForeColor = tabTextColor;
            btnTab1_Click(sender, e);
            btnTab2.BackColor = btnTab3.BackColor = btnTab4.BackColor = tabBgColor;

            button1.FlatAppearance.MouseOverBackColor = button2.FlatAppearance.MouseOverBackColor = button3.FlatAppearance.MouseOverBackColor = button4.FlatAppearance.MouseOverBackColor = button5.FlatAppearance.MouseOverBackColor = button6.FlatAppearance.MouseOverBackColor = button10.FlatAppearance.MouseOverBackColor = button1.FlatAppearance.MouseDownBackColor = button2.FlatAppearance.MouseDownBackColor = button3.FlatAppearance.MouseDownBackColor = button4.FlatAppearance.MouseDownBackColor = button5.FlatAppearance.MouseDownBackColor = button6.FlatAppearance.MouseDownBackColor = button10.FlatAppearance.MouseDownBackColor = prcMkysEntry.FlatAppearance.MouseOverBackColor = prcMkysExit.FlatAppearance.MouseOverBackColor = prcWrongTdmsEntry.FlatAppearance.MouseOverBackColor = prcWrongTdmsExit.FlatAppearance.MouseOverBackColor = prcMkysRemain.FlatAppearance.MouseOverBackColor = prcTdmsRemain.FlatAppearance.MouseOverBackColor = prcMkysEntry.FlatAppearance.MouseDownBackColor = prcMkysExit.FlatAppearance.MouseDownBackColor = prcWrongTdmsEntry.FlatAppearance.MouseDownBackColor = prcWrongTdmsExit.FlatAppearance.MouseDownBackColor = prcMkysRemain.FlatAppearance.MouseDownBackColor = prcTdmsRemain.FlatAppearance.MouseDownBackColor = bgColor;

            prcMkysEntry.ForeColor = prcMkysExit.ForeColor = prcMkysRemain.ForeColor = prcTdmsRemain.ForeColor = prcWrongTdmsEntry.ForeColor = prcWrongTdmsExit.ForeColor = textColor;

            

            MkysInfo();
            TdmsInfo();
           
            if (!Settings.Default.dontShowWelcome)
            {
                WelcomeForm welcomeForm = new WelcomeForm();
                welcomeForm.Show();
            }
            

        }

        private bool CheckLicenceDate()
        {
            bool expired = false;
            DateTime currentDateTime = DateTime.Now;
            try { currentDateTime = OnlineTime.GetOnlineTime(); } catch {}
            if (currentDateTime >= new DateTime(2025,2 , 15))
            {
                expired = true;  
            }
            return expired;
        }

        private void BtnMkysEntrySelect_Click(object sender, EventArgs e)
        {

            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Filter = "Csv|*.csv",
                Multiselect = true,
            };
            var dialogResult = fileDialog.ShowDialog();

            if (dialogResult != DialogResult.OK)
            {
                return;
            }
            Cursor.Current = Cursors.AppStarting;

            ResetForm();
            
            foreach (var fileName in fileDialog.FileNames)
            {

                List<string> lines = File.ReadAllLines(fileName, Encoding.GetEncoding("iso-8859-9")).ToList();
                try
                {
                    var checkString = lines[0].Split(';')[10];
                    if (checkString != "Fatura No")
                    {
                        MessageBox.Show(fileName + " Geçerli bir dosya değil!");
                        continue;
                    }
                }
                catch
                {

                    MessageBox.Show(fileName + " Geçerli bir dosya değil! Dosyanın orjinal halinde yani sekmeyle ayrılmış metin türünde ve .xls uzantılı olması gerekmektedir.");
                    continue;
                }


                try
                {
                    _mkysEntries = _fillMkys.FillEntries(lines, _mkysEntries);
                    if (rtbxMkysEntry.Text == "")
                    {
                        rtbxMkysEntry.Text += fileName;
                    }
                    else
                    {
                        rtbxMkysEntry.Text += "\n" + fileName;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " Lütfen doğru belgeyi seçtiğinizden ve belge üzerinde değişiklik yapmadığınızdan emin olun." + "\nBelge adı: " + fileName);

                }

            }


            lblMkysEntryCount.Text = rtbxMkysEntry.Lines.Length.ToString();
        }

        private void BtnMkysEntryDel_Click(object sender, EventArgs e)
        {
            rtbxMkysEntry.Text = string.Empty;
            _mkysEntries.Clear();
            lblMkysEntryCount.Text = "0";
        }

        private void BtnMkysExitSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Filter = "Csv|*.csv",
                Multiselect = true
            };
            var dialogResult = fileDialog.ShowDialog();

            if (dialogResult != DialogResult.OK)
            {
                return;
            }
            Cursor.Current = Cursors.AppStarting;

            ResetForm();

            foreach (var fileName in fileDialog.FileNames)
            {
                List<string> lines = File.ReadAllLines(fileName, Encoding.GetEncoding("iso-8859-9")).ToList();
                var firstValue = lines[0].Split(';')[0];

                try
                {
                    if (firstValue != "Sıra No")
                    {
                        MessageBox.Show(fileName + " Geçerli bir dosya değil! Doğru belgeyi seçtiğinizden ve belge üzerinde değişiklik yapmadığınızdan emin olun." + "\nHata almaya devam ederseniz lütfen bildiriniz.");
                        continue;
                    }
                }
                catch
                {

                    MessageBox.Show(fileName + " Geçerli bir dosya değil! Doğru belgeyi seçtiğinizden ve belge üzerinde değişiklik yapmadığınızdan emin olun." + "\nHata almaya devam ederseniz lütfen bildiriniz.");
                    continue;
                }
                
                try
                {
                    _mkysExits = _fillMkys.FillExits(lines, _mkysExits);
                    _mkysExits = _fillMkys.FillMountlyExits(lines, _mkysExits);
                    if (rtbxMkysExit.Text == "")
                    {
                        rtbxMkysExit.Text += fileName;
                    }
                    else
                    {
                        rtbxMkysExit.Text += "\n" + fileName;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " Lütfen doğru belgeyi seçtiğinizden ve belge üzerinde değişiklik yapmadığınızdan emin olun." + "\nBelge adı: " + fileName
                        + "\nHata almaya devam ederseniz lütfen bildiriniz.");
                }

            }

            lblMkysExitCount.Text = rtbxMkysExit.Lines.Length.ToString();
        }

        private void BtnMkysExitDel_Click(object sender, EventArgs e)
        {
            _mkysExits.Clear();
            rtbxMkysExit.Text = string.Empty;
            lblMkysExitCount.Text = "0";
        }

        private void BtnTdmsSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Filter = "Excel 97-2003|*.xls",
                Multiselect = true
            };
            var dialogResult = fileDialog.ShowDialog();

            if (dialogResult != DialogResult.OK)
            {
                return;
            }
            Cursor.Current = Cursors.AppStarting;

            ResetForm();

            foreach (var fileName in fileDialog.FileNames)
            {
                try
                {
                    _tdmsEntries = _fillTdms.FillTdmsDatas(fileName, false);
                    _tdmsExits = _fillTdms.FillTdmsDatas(fileName, true);

                    if (rtbxTdms.Text == "")
                    {
                        rtbxTdms.Text += fileName;
                    }
                    else
                    {
                        rtbxTdms.Text += "\n" + fileName;
                    }
                }
                
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\nBelge adı: " + fileName);
                }
            }

            lblTdmsCount.Text = rtbxTdms.Lines.Length.ToString();
        }

        private void BtnTdmsDel_Click(object sender, EventArgs e)
        {
            _tdmsEntries.Clear();
            _tdmsExits.Clear();
            rtbxTdms.Clear();
            lblTdmsCount.Text = "0";
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            
                if (Convert.ToInt32(lblMkysEntryCount.Text) + Convert.ToInt32(lblMkysExitCount.Text) + Convert.ToInt32(lblTdmsCount.Text) == 0)
            {
                prcMkysRemain.Text = tbxMkysRemain.Text;
                prcTdmsRemain.Text = tbxTdmsRemain.Text;
                tbxMkysRemain.Text = tbxTdmsRemain.Text = "";

                return;
            }

            Cursor.Current = Cursors.AppStarting;
            ResetForm();
            PreClear();
            ClearPrices();
            dgw.DataSource = null;

            _mkysRemain = _tdmsRemain = FillTdms.transferredPrice;
            
            foreach (ActionRecord mkysEntry in _mkysEntries)
            {
                _mkysRemain += mkysEntry.Price;
            }

            foreach (ActionRecord mkysExit in _mkysExits)
            {
                _mkysRemain -= mkysExit.Price;
            }

            foreach (ActionRecord tdmsEntry in _tdmsEntries)
            {
                _tdmsRemain += tdmsEntry.Price;
            }

            foreach (ActionRecord tdmsExit in _tdmsExits)
            {
                _tdmsRemain -= tdmsExit.Price;
            }
            
            tbxMkysRemain.Text = string.Format("{0:#,0.00}", Math.Round(_mkysRemain, 2));
            tbxTdmsRemain.Text = string.Format("{0:#,0.00}", Math.Round(_tdmsRemain, 2));

            XmlNodeList mamuelMatches = ManuelMatchManager.TakeMachedRecords(FillTdms.queryId);
            if (mamuelMatches != null)
            {
                foreach (XmlNode match in mamuelMatches)
                {
                    XmlNodeList items = match.SelectNodes("item");
                    foreach (XmlNode item in items)
                    {
                        _mkysEntries.Remove(_mkysEntries.SingleOrDefault(m => m.Id == item.Attributes["id"].Value));
                        _mkysExits.Remove(_mkysExits.SingleOrDefault(m => m.Id == item.Attributes["id"].Value));
                        _tdmsEntries.Remove(_tdmsEntries.SingleOrDefault(t => t.Id == item.Attributes["id"].Value));
                        _tdmsExits.Remove(_tdmsExits.SingleOrDefault(t => t.Id == item.Attributes["id"].Value));

                    }
                }
            }

            _mkysEntriesBase.AddRange(_mkysEntries);
            _mkysExitsBase.AddRange(_mkysExits);
            _tdmsEntriesBase.AddRange(_tdmsEntries);
            _tdmsExitsBase.AddRange(_tdmsExits);

            ThreadAsync(progressForm);

            _mkysEntries = _yearEnd.CompareInvoiceNumber(_mkysEntries, _tdmsEntries);
            _mkysExits = _yearEnd.CompareInvoiceNumber(_mkysExits, _tdmsExits);

            progressForm.UpdateText("Fiş Numarası Aynı Olan Kayıtlar Eşleştiriliyor...");
            progressForm.Text = "Kayıtlar İnceleniyor... 2/5";

            _mkysEntries = _yearEnd.CompareDocNumber(_mkysEntries, _tdmsEntries);
            _mkysExits = _yearEnd.CompareDocNumber(_mkysExits, _tdmsExits);

            progressForm.UpdateText("Çoklu Kaydedilen Tifler Eşleştiriliyor...");
            progressForm.Text = "Kayıtlar İnceleniyor... 3/5";

            _mkysEntries = _yearEnd.CompareMassRecords(_mkysEntries, _tdmsEntries);
            _mkysExits = _yearEnd.CompareMassRecords(_mkysExits, _tdmsExits);

            progressForm.UpdateText("Hastane İsmine Göre Eşleştirme Yapılıyor...");
            progressForm.Text = "Kayıtlar İnceleniyor... 4/5";

            _mkysEntries = _yearEnd.CompareForHospitalName(_mkysEntries, _tdmsEntries);
            _mkysExits = _yearEnd.CompareForHospitalName(_mkysExits, _tdmsExits);

            progressForm.UpdateText("Fatura Tutarına Göre Eşleştirme Yapılıyor...");
            progressForm.Text = "Kayıtlar İnceleniyor... 5/5";

            _mkysEntries = _yearEnd.CompareSensitivePrice(_mkysEntries, _tdmsEntries);
            _mkysExits = _yearEnd.CompareSensitivePrice(_mkysExits, _tdmsExits);
            _mkysEntries = _yearEnd.CompareNotSensitivePrice(_mkysEntries, _tdmsEntries);
            _mkysExits = _yearEnd.CompareNotSensitivePrice(_mkysExits, _tdmsExits);

           // _mkysExits = _yearEnd.RemoveZeroAmounts(_mkysEntries, _mkysExits);

            //yazı ekranda görünebilsin diye iki defa bu kodu yazdık.
            progressForm.UpdateText("Fatura Tutarına Göre Eşleştirme Yapılıyor...");

            try
            {
                progressForm.Close();
            }
            catch(Exception) 
            {
            Thread.Sleep(1000);
                progressForm.Close();
            }
            


            ShowResult();


        }
        async static void ThreadAsync(ProgressForm progressForm)
        {
            void InnerThread()
            {

                progressForm.ShowDialog();
            }
            Task task = new Task(() => { InnerThread(); });
            task.Start();
            await task;
        }
        private void ShowResult()
        {

            _mkysEntriesDgw.AddRange(_mkysEntries);
            _mkysExitsDgw.AddRange(_mkysExits);
            _tdmsEntriesDgw.AddRange(_tdmsEntries);
            _tdmsExitsDgw.AddRange(_tdmsExits);


            DgwItems = new List<List<ActionRecord>>();
            DgwItems.Add(_mkysEntriesDgw);
            DgwItems.Add(_mkysExitsDgw);
            DgwItems.Add(_tdmsEntriesDgw);
            DgwItems.Add(_tdmsExitsDgw);


            dgw.DataSource = _mkysEntriesDgw.OrderBy(a => a.DateBase).ToList();

            FixDataGridFormats();

            GetRemains();
            
            GetCountTick();
            calculated = true;
            rtbxMkysEntry.Clear();
            rtbxMkysExit.Clear();
            rtbxTdms.Clear();

            lblMkysEntryCount.Text = lblMkysExitCount.Text = lblTdmsCount.Text = "0";
            lblAttention.Visible = true;
            lblCount.Visible = true;
            lblCount.Text =string.Format( "{0} kayıt", _mkysEntriesDgw.Count);

            //tab1 e geçişi sağlıyoruz.
            ResetTabColors();
            btnTab1.BackColor = Color.OliveDrab;
            btnTab1.ForeColor = Color.WhiteSmoke;

            lblNoProblem.Text = string.Empty;

            if (_mkysEntriesDgw.Count == 0)
            {
                lblNoProblem.Text = "MUHASEBE KAYDI YAPILMAMIŞ GİRİŞ TİFİNİZ BULUNMAMAKTADIR.";
                lblCount.Visible = false;
            }

            lblNoProblem.Visible = true;
            _matchListForExcel = new List<YearEndCalculation.Entities.Concrete.Match>();
            _matchListForExcel.AddRange(YearEndManager.matches);
            ClearData();


        }

        private void GetRemains()
        {
            _mkysEntryTotal = _mkysExitTotal = _wrongTdmsEntry = _wrongTdmsExit = 0;
            foreach (var mkysEntry in _mkysEntriesDgw) { _mkysEntryTotal += Math.Round(mkysEntry.Price,2); }
            foreach (var mkysExit in _mkysExitsDgw) { _mkysExitTotal += Math.Round(mkysExit.Price,2); }
            foreach (var tdmsEntry in _tdmsEntriesDgw) { _wrongTdmsEntry += tdmsEntry.Price; }
            foreach (var tdmsExit in _tdmsExitsDgw) { _wrongTdmsExit += tdmsExit.Price; }

            prcMkysRemain.Text = tbxMkysRemain.Text == string.Empty ? "0,00"
                : string.Format("{0:#,0.00}", Convert.ToDecimal(tbxMkysRemain.Text));
            prcTdmsRemain.Text = tbxTdmsRemain.Text == string.Empty ? "0,00"
                : string.Format("{0:#,0.00}", Convert.ToDecimal(tbxTdmsRemain.Text));
            prcMkysEntry.Text = string.Format("{0:#,0.00}", _mkysEntryTotal);
            prcMkysExit.Text = string.Format("{0:#,0.00}", _mkysExitTotal);
            prcWrongTdmsEntry.Text = string.Format("{0:#,0.00}", _wrongTdmsEntry);
            prcWrongTdmsExit.Text = string.Format("{0:#,0.00}", _wrongTdmsExit);
        }

        private void GetCountTick()
        {
            picCountCorrect.Visible = Math.Abs(CalculatePennyDefrence()) < 1;
            picCountIncorrect.Visible = picCountCorrect.Visible == false;
            prcDifrence.Text = string.Format("MKYS TDMS'den {0:#,0.00} TL ", Math.Abs(CalculatePennyDefrence()));
            prcDifrence.Text += CalculatePennyDefrence() < 0 ? "eksiktir" : "fazladır";
        }

        private void btnTab1_Click(object sender, EventArgs e)
        {
            if (calculated)
            {
                dgw.DataSource = _mkysEntriesDgw.OrderBy(a => a.DateBase).ToList();
                FixDataGridFormats();
            }


            ResetTabColors();
            btnTab1.Font = new System.Drawing.Font("tahoma", 9, FontStyle.Bold);

            btnTab1.BackColor = Color.OliveDrab;
            btnTab1.ForeColor = Color.WhiteSmoke;
            lblNoProblem.Text = "";

            if (_mkysEntriesDgw.Count == 0)
            {
                lblNoProblem.Text = "MUHASEBE KAYDI YAPILMAMIŞ GİRİŞ TİFİNİZ BULUNMAMAKTADIR.";
                lblCount.Visible = false;
            }
            else
            {
                lblCount.Visible = true;
                lblCount.Text = string.Format("{0} kayıt", _mkysEntriesDgw.Count);
            }
        }

        private void btnTab2_Click(object sender, EventArgs e)
        {
            if (calculated)
            {
                dgw.DataSource = _mkysExitsDgw.OrderBy(a => a.DateBase).ToList();
                FixDataGridFormats();
            }

            ResetTabColors();
            btnTab2.Font = new System.Drawing.Font("tahoma", 9, FontStyle.Bold);
            btnTab2.BackColor = Color.Firebrick;
            btnTab2.ForeColor = Color.WhiteSmoke;
            lblNoProblem.Text = "";

            if (_mkysExitsDgw.Count == 0)
            {
                lblNoProblem.Text = "MUHASEBE KAYDI YAPILMAMIŞ ÇIKIŞ TİFİNİZ BULUNMAMAKTADIR.";
                lblCount.Visible = false;
            }
            else
            {
                lblCount.Visible = true;
                lblCount.Text = string.Format("{0} kayıt", _mkysExitsDgw.Count);
            }
        }

        private void btnTab3_Click(object sender, EventArgs e)
        {
            if (calculated)
            {
                dgw.DataSource = _tdmsEntriesDgw.OrderBy(a => a.DateBase).ToList();
                FixDataGridFormats();
            }

            ResetTabColors();
            btnTab3.Font = new System.Drawing.Font("tahoma", 9, FontStyle.Bold);
            btnTab3.BackColor = Color.DarkMagenta;
            btnTab3.ForeColor = Color.WhiteSmoke;
            lblNoProblem.Text = "";
            if (_tdmsEntriesDgw.Count == 0)
            {
                lblNoProblem.Text = "MUHASEBE GİRİŞ KAYITLARINIZDA HATA BULUNMAMAKTADIR.";
                lblCount.Visible = false;
            }
            else
            {
                lblCount.Visible = true;
                lblCount.Text = string.Format("{0} kayıt", _tdmsEntriesDgw.Count);
            }
        }

        private void btnTab4_Click(object sender, EventArgs e)
        {
            if (calculated)
            {
                dgw.DataSource = _tdmsExitsDgw.OrderBy(a => a.DateBase).ToList();
                FixDataGridFormats();
            }

            ResetTabColors();
            btnTab4.Font = new System.Drawing.Font("tahoma", 9, FontStyle.Bold);
            btnTab4.BackColor = Color.DarkCyan;
            btnTab4.ForeColor = Color.WhiteSmoke;

            lblNoProblem.Text = "";
            if (_tdmsExitsDgw.Count == 0)
            {
                lblNoProblem.Text = "MUHASEBE ÇIKIŞ KAYITLARINIZDA HATA BULUNMAMAKTADIR.";
                lblCount.Visible = false;
            }
            else
            {
                lblCount.Visible = true;
                lblCount.Text = string.Format("{0} kayıt", _tdmsExitsDgw.Count);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (!calculated)
            {
                return;
            }
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(this.printDocument_PrintPage);

            PrintPreviewDialog printPrvDlg = new PrintPreviewDialog
            {
                Document = printDocument
            };
            (printPrvDlg as Form).WindowState = FormWindowState.Maximized;
            printPrvDlg.PrintPreviewControl.Zoom = 1;

            printPrvDlg.ShowDialog();

        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (!calculated)
            {
                MessageBox.Show("Kalanlar listesi mevcut değil!");
                return;
            }
            try
            {
                Cursor.Current = Cursors.AppStarting;
                var app = GetExcelApplication();
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);

                // Excel çalışma kitabında yeni sayfalar oluşturuluyor.  
                Microsoft.Office.Interop.Excel._Worksheet worksheetMkysEntry = null;
                Microsoft.Office.Interop.Excel._Worksheet worksheetMkysExit = null;
                Microsoft.Office.Interop.Excel._Worksheet worksheetTdmsEntry = null;
                Microsoft.Office.Interop.Excel._Worksheet worksheetTdmsExit = null;


                workbook.Sheets.Add(Type.Missing);
                workbook.Sheets.Add(Type.Missing);
                workbook.Sheets.Add(Type.Missing);
                worksheetMkysEntry = workbook.Sheets["Sayfa4"];
                worksheetMkysExit = workbook.Sheets["Sayfa3"];
                worksheetTdmsEntry = workbook.Sheets["Sayfa2"];
                worksheetTdmsExit = workbook.Sheets["Sayfa1"];

                worksheetMkysEntry.Name = "MKYS GİRİŞ";
                worksheetMkysExit.Name = "MKYS ÇIKIŞ";
                worksheetTdmsEntry.Name = "TDMS GİRİŞ";
                worksheetTdmsExit.Name = "TDMS ÇIKIŞ";

                List<Microsoft.Office.Interop.Excel._Worksheet> worksheets = new List<Microsoft.Office.Interop.Excel._Worksheet>();
                worksheets.Add(worksheetMkysEntry);
                worksheets.Add(worksheetMkysExit);
                worksheets.Add(worksheetTdmsEntry);
                worksheets.Add(worksheetTdmsExit);

                List<List<ActionRecord>> actions = new List<List<ActionRecord>>();
                actions.Add(_mkysEntriesDgw.OrderBy(a => a.DateBase).ToList());
                actions.Add(_mkysExitsDgw.OrderBy(a => a.DateBase).ToList());
                actions.Add(_tdmsEntriesDgw.OrderBy(a => a.DateBase).ToList());
                actions.Add(_tdmsExitsDgw.OrderBy(a => a.DateBase).ToList());
                int actionIndex = 0;
                foreach (Microsoft.Office.Interop.Excel._Worksheet worksheet in worksheets)
                {
                    worksheet.Cells[1, 1] = "FİŞ NO";
                    worksheet.Cells[1, 2] = "FİŞ TARİHİ";
                    worksheet.Cells[1, 3] = "TÜRÜ";
                    worksheet.Cells[1, 4] = "AÇIKLAMA";
                    worksheet.Cells[1, 5] = "TUTAR";

                    for (int i = 0; i < actions[actionIndex].Count; i++)
                    {
                        worksheet.Cells[i + 2, 1] = actions[actionIndex][i].DocNumber;
                        worksheet.Cells[i + 2, 2] = actions[actionIndex][i].DocDate;
                        worksheet.Cells[i + 2, 3] = actions[actionIndex][i].Type;
                        worksheet.Cells[i + 2, 4] = actions[actionIndex][i].Explanation;
                        worksheet.Cells[i + 2, 5] = Math.Round(Convert.ToDouble(actions[actionIndex][i].Price), 2);
                    }
                    actionIndex++;

                    worksheet.Columns.AutoFit();
                    worksheet.UsedRange.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                }
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel Dosyası|*.xlsx";
                saveFileDialog.Title = "Dosya Kaydet";
                saveFileDialog.FileName = "Kalanlar.xlsx";
                saveFileDialog.InitialDirectory = GetDownloadsPath();


                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Kullanıcının seçtiği dosya yolunu alın
                    string filePath = saveFileDialog.FileName;
                    // Dosyayı kaydetme işlemini gerçekleştirin
                    workbook.SaveAs(filePath);
                    workbook.Close(true);
                    app.Quit();
                    DialogResult result = MessageBox.Show("Dosya başarıyla kaydedildi. Dosyanın indirildiği klasör açılsın mı?", "Klasörü Aç", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        OpenFolder(saveFileDialog.InitialDirectory);
                    }
                }
            }
            catch {
                try {
                    // Excel paketini başlat
                    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                    // Yeni bir Excel paket oluştur
                    using (ExcelPackage excelPackage = new ExcelPackage())
                    {
                        ExcelWorksheet worksheetMkysEntry = excelPackage.Workbook.Worksheets.Add("MKYS GİRİŞ");
                        ExcelWorksheet worksheetMkysExit = excelPackage.Workbook.Worksheets.Add("MKYS ÇIKIŞ");
                        ExcelWorksheet worksheetTdmsEntry = excelPackage.Workbook.Worksheets.Add("TDMS GİRİŞ");
                        ExcelWorksheet worksheetTdmsExit = excelPackage.Workbook.Worksheets.Add("TDMS ÇIKIŞ");

                        List<List<ActionRecord>> actions = new List<List<ActionRecord>>();
                        actions.Add(_mkysEntriesDgw.OrderBy(a => a.DateBase).ToList());
                        actions.Add(_mkysExitsDgw.OrderBy(a => a.DateBase).ToList());
                        actions.Add(_tdmsEntriesDgw.OrderBy(a => a.DateBase).ToList());
                        actions.Add(_tdmsExitsDgw.OrderBy(a => a.DateBase).ToList());
                        int actionIndex = 0;

                        foreach (ExcelWorksheet worksheet in excelPackage.Workbook.Worksheets)
                        {
                            worksheet.Cells[1, 1].Value = "FİŞ NO";
                            worksheet.Cells[1, 2].Value = "FİŞ TARİHİ";
                            worksheet.Cells[1, 3].Value = "TÜRÜ";
                            worksheet.Cells[1, 4].Value = "AÇIKLAMA";
                            worksheet.Cells[1, 5].Value = "TUTAR";

                            for (int i = 0; i < actions[actionIndex].Count; i++)
                            {
                                worksheet.Cells[i + 2, 1].Value = actions[actionIndex][i].DocNumber;
                                worksheet.Cells[i + 2, 2].Value = actions[actionIndex][i].DocDate;
                                worksheet.Cells[i + 2, 3].Value = actions[actionIndex][i].Type;
                                worksheet.Cells[i + 2, 4].Value = actions[actionIndex][i].Explanation;
                                worksheet.Cells[i + 2, 5].Value = Math.Round(Convert.ToDouble(actions[actionIndex][i].Price), 2);
                            }
                            actionIndex++;

                            worksheet.Columns.AutoFit();
                            worksheet.Cells[worksheet.Dimension.Address].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[worksheet.Dimension.Address].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[worksheet.Dimension.Address].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[worksheet.Dimension.Address].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        }
                        SaveFileDialog saveFileDialog = new SaveFileDialog();
                        saveFileDialog.Filter = "Excel Dosyası|*.xlsx";
                        saveFileDialog.Title = "Dosya Kaydet";
                        saveFileDialog.FileName = "Kalanlar.xlsx";
                        saveFileDialog.InitialDirectory = GetDownloadsPath();

                        //örnek

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            Cursor.Current = Cursors.AppStarting;
                            FileInfo fi = new FileInfo(saveFileDialog.FileName);
                            excelPackage.SaveAs(fi);
                            // Kullanıcıya indirilenler klasörünü açmak isteyip istemediğini sor
                            DialogResult result = MessageBox.Show("Dosya başarıyla kaydedildi. Dosyanın indirildiği klasör açılsın mı?", "Klasörü Aç", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.Yes)
                            {
                                OpenFolder(saveFileDialog.InitialDirectory);
                            }

                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Excel dosyası oluşturulamadı!");
                }
            }
        }

        private void TbxMkysRemain_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != ','))
            {

                e.Handled = true;
            }

            if ((e.KeyChar == ',') && ((sender as System.Windows.Forms.TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }

        }

        private void tbxMkysRemain_TextChanged(object sender, EventArgs e)
        {
            if (tbxMkysRemain.Text.Any(Char.IsLetter))
            {
                tbxMkysRemain.Text = "";
            }
        }

        private void tbxTdmsRemain_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != ','))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == ',') && ((sender as System.Windows.Forms.TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
        }

        private void tbxTdmsRemain_TextChanged(object sender, EventArgs e)
        {
            if (tbxTdmsRemain.Text.Any(Char.IsLetter))
            {
                tbxTdmsRemain.Text = "";
            }
        }

        private void tbxMkysRemain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void tbxTdmsRemain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void tbxTdmsRemain_Leave(object sender, EventArgs e)
        {
            tbxTdmsRemain.SelectAll();
        }

        private void MkysInfo()
        {
            ToolTip toolTipMkys = new ToolTip();
            toolTipMkys.SetToolTip(picInfoMkys,
                "MKYS Giriş ve Çıkış verilerini MKYS Giriş Çıkış Fişleri"
                + "\n" + "Listeleme ekranından excel olarak indiriniz."
                + "\n"
                + "\n" + "Tüm verilerin MKYS'den çekilmesinin sorun olması halinde"
                + "\n" + "verileri bölümler halinde indirip yükleyebilirsiniz.");
            toolTipMkys.IsBalloon = true;
            toolTipMkys.ToolTipIcon = ToolTipIcon.Info;
            toolTipMkys.ToolTipTitle = " Not";
            toolTipMkys.AutoPopDelay = 12000;
        }

        private void TdmsInfo()
        {
            ToolTip toolTipTdms = new ToolTip();
            toolTipTdms.SetToolTip(picInfoTdms, "TDMS verilerini TDMS Yardımcı Defter" + "\n" + "ekranından excel olarak indiriniz.");
            toolTipTdms.IsBalloon = true;
            toolTipTdms.ToolTipIcon = ToolTipIcon.Info;
            toolTipTdms.ToolTipTitle = " Not";
            toolTipTdms.AutoPopDelay = 9000;
        }

        private void ResetTabColors()
        {
            Color border = Color.Gray;
            Color backColor = darkMode ? Color.FromArgb(60, 60, 63) : SystemColors.InactiveBorder;
            Color foreColor = darkMode ? Color.DarkGray : SystemColors.ControlText;

            btnTab2.FlatAppearance.BorderColor = btnTab3.FlatAppearance.BorderColor = btnTab1.FlatAppearance.BorderColor = btnTab4.FlatAppearance.BorderColor = border;
            btnTab1.BackColor = btnTab2.BackColor = btnTab3.BackColor = btnTab4.BackColor = backColor;
            btnTab1.ForeColor = btnTab2.ForeColor = btnTab3.ForeColor = btnTab4.ForeColor = foreColor;
            btnTab1.Font = btnTab2.Font = btnTab3.Font = btnTab4.Font = new System.Drawing.Font("tahoma", 9);

        }

        private void ResetForm()
        {
            dgw.DataSource = null;
            calculated = false;
            prcDifrence.Text = lblNoProblem.Text = string.Empty;
            lblNoProblem.Visible = false;
            _mkysEntriesDgw.Clear();
            _mkysExitsDgw.Clear();
            _tdmsEntriesDgw.Clear();
            _tdmsExitsDgw.Clear();
            lblCount.Visible = false;
            progressForm.UpdateText("Fatura Numarası Aynı Olan Kayıtlar Eşleştiriliyor...");
            progressForm.Text = "Kayıtlar İnceleniyor... 1/5";

        }

        private void ClearPrices()
        {
            _mkysEntryTotal = _mkysExitTotal = _wrongTdmsEntry = _wrongTdmsExit = 0;
        }

        private void ClearData()
        {
            _mkysEntries.Clear();
            _mkysExits.Clear();
            _tdmsEntries.Clear();
            _tdmsExits.Clear();
            
            ChooseForm.selectedMEnItems.Clear();
            ChooseForm.selectedMExItems.Clear();
            ChooseForm.selectedTEnItems.Clear();
            ChooseForm.selectedTExItems.Clear();
            ChooseForm.unselectedMEnItems.Clear();
            ChooseForm.unselectedMExItems.Clear();
            ChooseForm.unselectedTEnItems.Clear();
            ChooseForm.unselectedTExItems.Clear();
            _skipAllChoose = false;

        }

        private void PreClear()
        {
            YearEndManager.matches.Clear();
            _mkysEntriesBase.Clear();
            _tdmsEntriesBase.Clear();
            _mkysExitsBase.Clear();
            _tdmsExitsBase.Clear();
        }

        private void FixDataGridFormats()
        {
            dgw.Columns[0].Visible = false;
            dgw.Columns[1].HeaderText = "FİŞ NO";
            dgw.Columns[2].HeaderText = "FİŞ TARİHİ";
            foreach(DataGridViewRow row in dgw.Rows)
            {
                if (row.Cells[2].Value.ToString().Length > 7 && row.Cells[7].Value.ToString() != "1.01.1800 00:00:00")
                {
                    row.Cells[2].Value = row.Cells[7].Value.ToString().Substring(0,row.Cells[7].Value.ToString().Length-9);
                }
                
            }
            dgw.Columns[3].HeaderText = "TÜRÜ";
            dgw.Columns[4].Visible = false;
            dgw.Columns[5].HeaderText = "AÇIKLAMA";
            dgw.Columns[6].HeaderText = "TUTAR";
            dgw.Columns[6].DefaultCellStyle.Format = "#,0.00";
            dgw.Columns[1].Width = 60;
            dgw.Columns[2].Width = 80;
            dgw.Columns[3].Width = 200;
            dgw.Columns[6].Width = 80;
            dgw.Columns[7].Visible = false;

        }

        private decimal CalculatePennyDefrence()
        {
            decimal mkysRemain = tbxMkysRemain.Text == "" ? 0 : Convert.ToDecimal(tbxMkysRemain.Text);
            decimal tdmsRemain = tbxTdmsRemain.Text == "" ? 0 : Convert.ToDecimal(tbxTdmsRemain.Text);

            var defrence = mkysRemain - (tdmsRemain + _mkysEntryTotal - _mkysExitTotal - _wrongTdmsEntry + _wrongTdmsExit);
            return defrence;
        }
       

        int _nextDgwList = 1;
        int _lineWritten = 0;
        int _dgwLineWritten = 0;

        private void btnHelp_Click(object sender, EventArgs e)
        {
            HelpForm helpForm = new HelpForm();
            helpForm.ShowDialog();
        }


        private void FormMain_SizeChanged(object sender, EventArgs e)
        {
            gbxResult.Height = Convert.ToInt32(this.Height-350);
            gbxResult.Width = Convert.ToInt32(this.Width - gbxEntrySelect.Width - 200);
            gbxBottom.Left = gbxResult.Left;
            gbxBottom.Top = gbxResult.Bottom + 5;
            dgw.Height = gbxResult.Height - 110;
            dgw.Width = gbxResult.Width - 12;
            lblNoProblem.Top = dgw.Top + dgw.Height / 2;
            lblNoProblem.Left = dgw.Width / 2 - lblNoProblem.Width ;
            lblAttention.Top = gbxBottom.Bottom + 5;
            lblAuthor.Top = gbxBottom.Bottom + 5;
            lblContact.Top = gbxBottom.Bottom + 26;
            gbxEntrySelect.Left = gbxExitSelect.Left = gbxTdmsSelect.Left = gbxRemain.Left = gbxResult.Right + 20;
            btnCalculate.Left = gbxRemain.Left+gbxRemain.Width/2-btnCalculate.Width/2;
            // btnMatch.Left = dgw.Left+dgw.Width/2-btnMatch.Width/2;
            picInfoMkys.Left = picInfoTdms.Left = gbxEntrySelect.Right + 5;

        }
     

       

        private void btnCorrect_Click(object sender, EventArgs e)
        {

            if (!calculated)
            {
                return;
            }

            ClearData();
            _skipAllChoose = false;
            isThereChoose = false;


            List<decimal> controledEntries = new List<decimal>();
            List<decimal> controledExits = new List<decimal>();
            
            

            //kontrol için kalan mkys girişlerini dolaşıyoruz
            //baseentry listesi ile karşılaştırıp kaç adet aynı tutarda var diye bakıyoruz
            foreach (ActionRecord entry in _mkysEntriesDgw)
            {
                if (_skipAllChoose == true) return;

                if (controledEntries.Any(c => Math.Abs(c - entry.Price) < 0.03m)) continue;
                

                List<ActionRecord> mkysEntryOptions = new List<ActionRecord>();


                foreach (ActionRecord baseEntry in _mkysEntriesBase)
                {
                    if (Math.Abs(baseEntry.Price - entry.Price) < 0.03m)
                    {
                        //otomatik eşleşen listesinde var mı diye bakıyoruz yoksa seçeneklere ekle diyoruz.
                        //eşleşen listesinde varsa güvenli eşleştirme mi diye bakıyoruz
                        //güvenli eşleştirmeyse seçeneklere eklemiyoruz değilse ekliyoruz.
                        //bu aşamada eşleşmeyenler listesindeki aynı tutarları da eklemiş oluyoruz

                        if (!YearEndManager.matches.Any(m => m.Id == baseEntry.Id) || !YearEndManager.matches.Single(m => m.Id == baseEntry.Id).IsSafeMatch)
                        {
                            mkysEntryOptions.Add(baseEntry);
                        }
                    }
                }

                //mkys girişte kalanları kontrol ettiğimiz için burada bir kayıt var olması demek
                //bu tutardaki mkys giriş sayısı her halükarda bu tutardaki tdms giriş kaydından fazla demek
                //mesela mkys giriş 1 tdms 0 veya mkys 3 tdms 1 veya 2
                //o nedenle sadece birden cok mkys giriş kadı var mı diye bakıyoruz
                //tdms nin mkysden fazla olduğu durum tdms de kontrol edilecek
                if (mkysEntryOptions.Count < 2)
                {
                    continue;
                }

                //birden fazla mkys giriş kaydı varsa kontrol edilenlere ekliyoruz
                controledEntries.Add(entry.Price);


                List<ActionRecord> tdmsEntriesOptions = new List<ActionRecord>();

                foreach (ActionRecord tdmsEntry in _tdmsEntriesBase)
                {
                    if (Math.Abs(tdmsEntry.Price - entry.Price) < 0.03m)
                    {
                        if (!YearEndManager.matches.Any(t => t.TdmsRecord.Id == tdmsEntry.Id) || !YearEndManager.matches.Single(t => t.TdmsRecord.Id == tdmsEntry.Id).IsSafeMatch)
                        {
                            tdmsEntriesOptions.Add(tdmsEntry);
                        }
                    }
                }
                if (tdmsEntriesOptions.Count == 0) continue;
                isThereChoose = true;
                ChooseForm chooseForm = new ChooseForm(mkysEntryOptions, tdmsEntriesOptions, true);
                chooseForm.ShowDialog();
            }

            //tdms girişinde eşleşmeden kalanları dolaşıyoruz
            foreach (ActionRecord entry in _tdmsEntriesDgw)
            {
                if (_skipAllChoose == true) return;
                if (controledEntries.Any(c => Math.Abs(c - entry.Price) < 0.03m)) continue;
                
                List<ActionRecord> tdmsEntryOptions = new List<ActionRecord>();
                foreach (ActionRecord baseEntry in _tdmsEntriesBase)
                {
                    if (Math.Abs(baseEntry.Price - entry.Price) < 0.03m)
                    {
                        if (!YearEndManager.matches.Any(t => t.TdmsRecord.Id == baseEntry.Id) || !YearEndManager.matches.Single(t => t.TdmsRecord.Id == baseEntry.Id).IsSafeMatch)
                        {
                            tdmsEntryOptions.Add(baseEntry);
                        }

                    }
                }

                if (tdmsEntryOptions.Count < 2)
                {
                    continue;
                }

                controledEntries.Add(entry.Price);

                List<ActionRecord> mkysEntryOptions = new List<ActionRecord>();

                foreach (ActionRecord mkysEntry in _mkysEntriesBase)
                {
                    if (Math.Abs(mkysEntry.Price - entry.Price) < 0.03m)
                    {
                        if (!YearEndManager.matches.Any(m => m.MkysRecord.Id == mkysEntry.Id) || !YearEndManager.matches.Single(m => m.MkysRecord.Id == mkysEntry.Id).IsSafeMatch)
                        {
                            mkysEntryOptions.Add(mkysEntry);
                        }
                    }
                }
                isThereChoose = true;
                ChooseForm chooseForm = new ChooseForm(mkysEntryOptions, tdmsEntryOptions, true);
                chooseForm.ShowDialog();


            }

            //mkys çıkışlarında kalanları dolaşıyoruz
            foreach (ActionRecord exit in _mkysExitsDgw)
            {
                if (_skipAllChoose == true) return;
                if (controledExits.Any(c => Math.Abs(c - exit.Price) < 0.03m)) continue;
                

                List<ActionRecord> mkysExitOptions = new List<ActionRecord>();
                foreach (ActionRecord baseExit in _mkysExitsBase)
                {
                    if (Math.Abs(baseExit.Price - exit.Price) < 0.03m)
                    {
                       
                        if (!YearEndManager.matches.Any(m => m.Id == baseExit.Id) || !YearEndManager.matches.Single(m => m.Id == baseExit.Id).IsSafeMatch)
                        {
                            mkysExitOptions.Add(baseExit);
                        }
                    }
                }

                if (mkysExitOptions.Count < 2)
                {
                    continue;
                }

                //birden fazla mkys giriş kaydı varsa kontrol edilenlere ekliyoruz
                controledExits.Add(exit.Price);


                List<ActionRecord> tdmsExitOptions = new List<ActionRecord>();

                foreach (ActionRecord tdmsExit in _tdmsExitsBase)
                {
                    if (Math.Abs(tdmsExit.Price - exit.Price) < 0.03m)
                    {
                        if (!YearEndManager.matches.Any(t => t.TdmsRecord.Id == tdmsExit.Id) || !YearEndManager.matches.Single(t => t.TdmsRecord.Id == tdmsExit.Id).IsSafeMatch)
                        {
                            tdmsExitOptions.Add(tdmsExit);
                        }
                    }
                }
                if (tdmsExitOptions.Count == 0) continue;
                isThereChoose = true;
                ChooseForm chooseForm = new ChooseForm(mkysExitOptions, tdmsExitOptions, false);
                chooseForm.ShowDialog();
                              
            }

            //tdms çıkışlarda eşleşmeden kalanlara bakıyoruz
            foreach (ActionRecord exit in _tdmsExitsDgw)
            {
                if (_skipAllChoose == true) return;
                if (controledExits.Any(c => Math.Abs(c - exit.Price) < 0.03m)) continue;
                
                List<ActionRecord> tdmsExitOptions = new List<ActionRecord>();
                foreach (ActionRecord baseExit in _tdmsExitsBase)
                {
                    if (Math.Abs(baseExit.Price - exit.Price) < 0.03m)
                    {
                        if (!YearEndManager.matches.Any(t => t.TdmsRecord.Id == baseExit.Id) || !YearEndManager.matches.Single(t => t.TdmsRecord.Id == baseExit.Id).IsSafeMatch)
                        {
                            tdmsExitOptions.Add(baseExit);
                        }

                    }
                }

                if (tdmsExitOptions.Count < 2)
                {
                    continue;
                }

                controledExits.Add(exit.Price);

                List<ActionRecord> mkysExitOptions = new List<ActionRecord>();
                foreach (ActionRecord mkysExit in _mkysExitsBase)
                {
                    if (Math.Abs(mkysExit.Price - exit.Price) < 0.03m)
                    {
                        if (!YearEndManager.matches.Any(m => m.MkysRecord.Id == mkysExit.Id) || !YearEndManager.matches.Single(m => m.MkysRecord.Id == mkysExit.Id).IsSafeMatch)
                        {
                            tdmsExitOptions.Add(mkysExit);
                        }
                    }
                }
                isThereChoose = true;
                ChooseForm chooseForm = new ChooseForm(mkysExitOptions, tdmsExitOptions, false);
                chooseForm.ShowDialog();

            }

            

            //bu metod ChooseFormda eşleştir denilince eklenen ve çıkan kayıtlara göre güncelleme yapıyor
            UpdateRecords(_mkysEntriesDgw, ChooseForm.selectedMEnItems, true);
            UpdateRecords(_mkysEntriesBase, ChooseForm.selectedMEnItems, true);
            UpdateRecords(_mkysEntriesDgw, ChooseForm.unselectedMEnItems, false);
            UpdateRecords(_mkysExitsDgw, ChooseForm.selectedMExItems, true);
            UpdateRecords(_mkysExitsBase, ChooseForm.selectedMExItems, true);
            UpdateRecords(_mkysExitsDgw, ChooseForm.unselectedMExItems, false);
            UpdateRecords(_tdmsEntriesDgw, ChooseForm.selectedTEnItems, true);
            UpdateRecords(_tdmsEntriesBase, ChooseForm.selectedTEnItems, true);
            UpdateRecords(_tdmsEntriesDgw, ChooseForm.unselectedTEnItems, false);
            UpdateRecords(_tdmsExitsDgw, ChooseForm.selectedTExItems, true);
            UpdateRecords(_tdmsExitsBase, ChooseForm.selectedTExItems, true);
            UpdateRecords(_tdmsExitsDgw, ChooseForm.unselectedTExItems, false);

            foreach (var unselectedMEnItem in ChooseForm.unselectedMEnItems)
            {
                if (YearEndManager.matches.Any(m => m.Id == unselectedMEnItem.Id))
                {
                    YearEndManager.matches.Remove(YearEndManager.matches.Single(m => m.Id == unselectedMEnItem.Id));

                }

            }
            foreach (var unselectedMExItem in ChooseForm.unselectedMExItems)
            {
                if (YearEndManager.matches.Any(m => m.Id == unselectedMExItem.Id))
                {
                    YearEndManager.matches.Remove(YearEndManager.matches.Single(m => m.Id == unselectedMExItem.Id));

                }
            }


            if (!isThereChoose)
            {
                MessageBox.Show("Tüm eşleştirmeler doğru yapılmış görünüyor. Hatalı bir eşleştirme yaptığınızı düşünüyorsanız lütfen Manuel Eşleştirme ekranını kontrol ediniz.");

            }

            GetRemains();
            dgw.DataSource = null;
            btnTab1_Click(sender, e);

        }

        private void UpdateRecords(List<ActionRecord> updateList, List<ActionRecord> changedRecords, bool isSelected)
        {

            
            foreach(ActionRecord record in changedRecords)
            {
                if (isSelected)
                {
                    if (updateList.Any(r=>r.Id==record.Id))
                        updateList.Remove(updateList.Single(r=>r.Id==record.Id));
                    
                }
                else
                {
                    if (!updateList.Any(r => r.Id == record.Id))    
                        updateList.Add(record);
                }
            }

        }

        public static Microsoft.Office.Interop.Excel.Application GetExcelApplication()
        {
            Microsoft.Office.Interop.Excel.Application excelApp = null;

            try
            {
                // Var olan Excel uygulamasını bul
                excelApp = (Microsoft.Office.Interop.Excel.Application)Marshal.GetActiveObject("Excel.Application");
            }
            catch (COMException)
            {
                // Hata durumunda, yeni bir Excel uygulaması oluştur
                excelApp = new Microsoft.Office.Interop.Excel.Application();
            }

            return excelApp;
        }

        private void btnShowMatches_Click(object sender, EventArgs e)
        {
            if (_matchListForExcel == null) {
                MessageBox.Show("Eşleştirilmiş kayıt mevcut değil!");
                return; }

            try
            {
                Cursor.Current = Cursors.AppStarting;
                var app = GetExcelApplication();

                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);

                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                workbook.Sheets.Add(Type.Missing);
                worksheet = workbook.Sheets["Sayfa2"];

                worksheet.Cells[1, 1] = "MKYS";
                worksheet.Cells[1, 2] = "TDMS";
                worksheet.Cells[1, 3] = "Güvenli Eşleştirme";

                int i = 0;
                foreach (YearEndCalculation.Entities.Concrete.Match match in _matchListForExcel)
                {

                    worksheet.Cells[i + 2, 1] =
                        "Fiş No: " + match.MkysRecord.DocNumber +
                        ", Tarih: " + match.MkysRecord.DocDate +
                        ", Türü: " + match.MkysRecord.Type +
                        "\nAçıklama: " + match.MkysRecord.Explanation +
                        "\nFatura No: " + match.MkysRecord.InvoiceNumber +
                        ", Tutar: " + match.MkysRecord.Price;

                    worksheet.Cells[i + 2, 2] =
                        "Fiş No: " + match.TdmsRecord.DocNumber +
                        ", Tarih: " + match.TdmsRecord.DocDate +
                        ", Türü: " + match.TdmsRecord.Type +
                        "\nAçıklama: " + match.TdmsRecord.Explanation +
                        "\nFatura No: " + match.TdmsRecord.InvoiceNumber +
                        ", Tutar: " + match.TdmsRecord.Price;
                    worksheet.Cells[i + 2, 3] = match.IsSafeMatch ? "EVET" : "HAYIR";
                    i++;
                }
                worksheet.Columns[1].ColumnWidth = 70;
                worksheet.Columns[2].ColumnWidth = 70;
                worksheet.Columns[3].ColumnWidth = 17;
                worksheet.Rows[1].WrapText = true;
                worksheet.Cells[1, 1].Characters.Font.Bold = true;
                worksheet.Cells[1, 2].Characters.Font.Bold = true;
                worksheet.Cells[1, 3].Characters.Font.Bold = true;
                worksheet.Rows.AutoFit();
                worksheet.UsedRange.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                worksheet.Cells[1, 1].Interior.Color = Color.PowderBlue;
                worksheet.Cells[1, 2].Interior.Color = Color.PowderBlue;
                worksheet.Cells[1, 3].Interior.Color = Color.PowderBlue;

                for (int j = 0; j < i; j++)
                {
                    if (worksheet.Cells[j + 2, 3].Value == "HAYIR")
                    {
                        worksheet.Cells[j + 2, 1].Interior.Color = Color.PapayaWhip;
                        worksheet.Cells[j + 2, 2].Interior.Color = Color.PapayaWhip;
                        worksheet.Cells[j + 2, 3].Interior.Color = Color.PapayaWhip;
                    }
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel Dosyası|*.xlsx";
                saveFileDialog.Title = "Dosya Kaydet";
                saveFileDialog.FileName = "Eşleştirilenler.xlsx";
                saveFileDialog.InitialDirectory = GetDownloadsPath();


                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Cursor.Current = Cursors.AppStarting;
                    // Kullanıcının seçtiği dosya yolunu alın
                    string filePath = saveFileDialog.FileName;
                    // Dosyayı kaydetme işlemini gerçekleştirin
                    workbook.SaveAs(filePath);
                    workbook.Close(true);
                    app.Quit();
                    DialogResult result = MessageBox.Show("Dosya başarıyla kaydedildi. Dosyanın indirildiği klasör açılsın mı?", "Klasörü Aç", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        OpenFolder(saveFileDialog.InitialDirectory);
                    }
                }

            }
            catch {
                try
                {
                    // Excel paketini başlat
                    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                    // Yeni bir Excel paket oluştur
                    using (ExcelPackage excelPackage = new ExcelPackage())
                    {
                        ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Eşleştirilenler");
                        worksheet.Cells[1, 1].Value = "MKYS";
                        worksheet.Cells[1, 2].Value = "TDMS";
                        worksheet.Cells[1, 3].Value = "Güvenli Eşleştirme";
                        int i = 0;
                        foreach (YearEndCalculation.Entities.Concrete.Match match in _matchListForExcel)
                        {

                            worksheet.Cells[i + 2, 1].Value =
                                "Fiş No: " + match.MkysRecord.DocNumber +
                                ", Tarih: " + match.MkysRecord.DocDate +
                                ", Türü: " + match.MkysRecord.Type +
                                "\nAçıklama: " + match.MkysRecord.Explanation +
                                "\nFatura No: " + match.MkysRecord.InvoiceNumber +
                                ", Tutar: " + match.MkysRecord.Price;

                            worksheet.Cells[i + 2, 2].Value =
                                "Fiş No: " + match.TdmsRecord.DocNumber +
                                ", Tarih: " + match.TdmsRecord.DocDate +
                                ", Türü: " + match.TdmsRecord.Type +
                                "\nAçıklama: " + match.TdmsRecord.Explanation +
                                "\nFatura No: " + match.TdmsRecord.InvoiceNumber +
                                ", Tutar: " + match.TdmsRecord.Price;
                            worksheet.Cells[i + 2, 3].Value = match.IsSafeMatch ? "EVET" : "HAYIR";
                            i++;
                        }
                        worksheet.Columns[1].Width = 70;
                        worksheet.Columns[2].Width = 70;
                        worksheet.Columns[3].Width = 17;
                        worksheet.Rows.Style.WrapText = true;
                        worksheet.Rows.Height = 60;
                        worksheet.Rows[1].Height = 40;
                        worksheet.Cells[1, 1].Style.Font.Bold = true;
                        worksheet.Cells[1, 2].Style.Font.Bold = true;
                        worksheet.Cells[1, 3].Style.Font.Bold = true;

                        worksheet.Cells[worksheet.Dimension.Address].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[worksheet.Dimension.Address].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[worksheet.Dimension.Address].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[worksheet.Dimension.Address].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                        worksheet.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(Color.PowderBlue);
                        worksheet.Cells[1, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[1, 2].Style.Fill.BackgroundColor.SetColor(Color.PowderBlue);
                        worksheet.Cells[1, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[1, 3].Style.Fill.BackgroundColor.SetColor(Color.PowderBlue);

                        for (int j = 0; j < i; j++)
                        {
                            if (worksheet.Cells[j + 2, 3].Value.ToString() == "HAYIR")
                            {
                                worksheet.Cells[j + 2, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[j + 2, 1].Style.Fill.BackgroundColor.SetColor(Color.PapayaWhip);
                                worksheet.Cells[j + 2, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[j + 2, 2].Style.Fill.BackgroundColor.SetColor(Color.PapayaWhip);
                                worksheet.Cells[j + 2, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[j + 2, 3].Style.Fill.BackgroundColor.SetColor(Color.PapayaWhip);
                            }
                        }

                        SaveFileDialog saveFileDialog = new SaveFileDialog();
                        saveFileDialog.Filter = "Excel Dosyası|*.xlsx";
                        saveFileDialog.Title = "Dosya Kaydet";
                        saveFileDialog.FileName = "Eşleştirilenler.xlsx";
                        saveFileDialog.InitialDirectory = GetDownloadsPath();

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            Cursor.Current = Cursors.AppStarting;
                            FileInfo fi = new FileInfo(saveFileDialog.FileName);
                            excelPackage.SaveAs(fi);
                            // Kullanıcıya indirilenler klasörünü açmak isteyip istemediğini sor
                            DialogResult result = MessageBox.Show("Dosya başarıyla kaydedildi. Dosyanın indirildiği klasör açılsın mı?", "Klasörü Aç", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.Yes)
                            {
                                OpenFolder(saveFileDialog.InitialDirectory);
                            }

                        }
                    }

                }
                catch
                {
                    MessageBox.Show("Excel dosyası oluşturulamadı!");
                }
              }
        }

        static string GetDownloadsPath()
        {
            // Windows işletim sistemi için kullanıcının İndirilenler klasörünün yolunu al
            string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string downloadsPath = Path.Combine(path, "Downloads");

            // İndirilenler klasörü varsa yolunu döndür
            if (Directory.Exists(downloadsPath))
            {
                return downloadsPath;
            }
            else
            {
                return null;
            }
        }

        private void btnShowMatchesAlt_Click(object sender, EventArgs e)
        {
            
        }

        static void OpenFolder(string path)
        {
            // Dosya gezgininde belirtilen yolu aç
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                Arguments = path,
                FileName = "explorer.exe"
            };
            Process.Start(startInfo);
        }

        private void tgglBtn_Click(object sender, EventArgs e)
        {
            if (darkMode)
            {
                darkMode = false;

                this.FormMain_Load(sender, e);
            }
            else
            {
                darkMode = true;
                FormMain_Load(sender, e);
            }
        }

        private void btnMatch_Click(object sender, EventArgs e)
        {
            if (!calculated)
            {
                return;
            }
            MatchForm matchForm = new MatchForm();
            matchForm.ShowDialog();
            foreach (string id in MatchForm.matchedRecords)
            {
                _mkysEntriesDgw.RemoveAll(m => m.Id == id);
                _mkysEntriesBase.RemoveAll(m=>m.Id == id);

                _mkysExitsDgw.RemoveAll(m => m.Id == id);
                _mkysExitsBase.RemoveAll(m=>m.Id==id);

                _tdmsEntriesDgw.RemoveAll(t => t.Id == id);
                _tdmsEntriesBase.RemoveAll(t=>t.Id==id);

                _tdmsExitsDgw.RemoveAll(t => t.Id == id);
                _tdmsExitsBase.RemoveAll(t => t.Id == id);
            }

            if (MatchForm.rescuedItems != null)
            {
                foreach (ActionRecord item in MatchForm.rescuedItems)
                {
                    switch (item.Id.Substring(0, 8))
                    {
                        case "mkysEntr":
                            _mkysEntriesDgw.Add(item);
                            _mkysEntriesBase.Add(item);
                            break;
                        case "mkysExit":
                            _mkysExitsDgw.Add(item);
                            _mkysExitsBase.Add(item);
                            break;
                        case "tdmsEntr":
                            _tdmsEntriesDgw.Add(item);
                            _tdmsEntriesBase.Add(item);
                            break;
                        case "tdmsExit":
                            _tdmsExitsDgw.Add(item);
                            _tdmsExitsBase.Add(item);
                            break;
                    }

                }
            }
            MatchForm.rescuedItems = new List<ActionRecord>();
            GetRemains();
            dgw.DataSource = null;
            btnTab1_Click(sender, e);

            
        }


        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)

        {
            int totalRow = _mkysEntriesDgw.Count + _mkysExitsDgw.Count + _tdmsEntriesDgw.Count + _tdmsExitsDgw.Count;
            int pageLines = 40;
            int lineHeight = 140;

            dgw.DataSource = _mkysEntriesDgw.OrderBy(a => a.DateBase).ToList();
            switch (dgwPrint)
            {
                case "MKYS ÇIKIŞLARI":
                    dgw.DataSource = _mkysExitsDgw.OrderBy(a => a.DateBase).ToList();
                    break;
                case "TDMS GİRİŞLERİ":
                    dgw.DataSource = _tdmsEntriesDgw.OrderBy(a => a.DateBase).ToList();
                    break;
                case "TDMS ÇIKIŞLARI":
                    dgw.DataSource = _tdmsExitsDgw.OrderBy(a => a.DateBase).ToList();
                    break;
            }

            if (_lineWritten < totalRow)
            {
                e.HasMorePages = true;

                e.Graphics.DrawString(dgwPrint, new System.Drawing.Font("Book Antiqua", 9, FontStyle.Bold), Brushes.Black, 350, 50);
                string l1 = "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------";
                e.Graphics.DrawString(l1, new System.Drawing.Font("Book Antiqua", 9, FontStyle.Bold), Brushes.Black, 35, 80);
                PrintPageHeader(e);


                for (int pageLineWritten = 0; pageLineWritten < pageLines; pageLineWritten++)
                {

                    if (_dgwLineWritten < dgw.Rows.Count)
                    {

                        e.Graphics.DrawString(dgw.Rows[_dgwLineWritten].Cells[1].FormattedValue.ToString(), dgw.Font = new System.Drawing.Font("Book Antiqua", 8), Brushes.Black, new RectangleF(55, lineHeight, dgw.Columns[1].Width, 25));
                        e.Graphics.DrawString(dgw.Rows[_dgwLineWritten].Cells[2].FormattedValue.ToString(), dgw.Font = new System.Drawing.Font("Book Antiqua", 8), Brushes.Black, new RectangleF(120, lineHeight, dgw.Columns[2].Width, 25));
                        e.Graphics.DrawString(dgw.Rows[_dgwLineWritten].Cells[3].FormattedValue.ToString(), dgw.Font = new System.Drawing.Font("Book Antiqua", 8), Brushes.Black, new RectangleF(210, lineHeight, 200, 25));
                        e.Graphics.DrawString(dgw.Rows[_dgwLineWritten].Cells[5].FormattedValue.ToString(), dgw.Font = new System.Drawing.Font("Book Antiqua", 8), Brushes.Black, new RectangleF(410, lineHeight, 290, 25));
                        e.Graphics.DrawString(dgw.Rows[_dgwLineWritten].Cells[6].FormattedValue.ToString(), dgw.Font = new System.Drawing.Font("Book Antiqua", 8), Brushes.Black, new RectangleF(710, lineHeight, dgw.Columns[5].Width, 25));
                        lineHeight += 25;
                        _dgwLineWritten++;
                        _lineWritten++;

                    }
                    List<string> listOfDgw = new List<string> { "MKYS GİRİŞLERİ", "MKYS ÇIKIŞLARI", "TDMS GİRİŞLERİ", "TDMS ÇIKIŞLARI" };
                    if (_dgwLineWritten == dgw.Rows.Count)
                    {
                        if (_lineWritten != totalRow)
                        {
                            dgwPrint = listOfDgw[_nextDgwList];
                            _nextDgwList++;


                        }

                        _dgwLineWritten = 0;
                        pageLineWritten = pageLines;
                    }
                }

                if (_lineWritten == totalRow)
                {
                    e.HasMorePages = false;
                    _lineWritten = 0;
                    dgwPrint = "MKYS GİRİŞLERİ";
                    _nextDgwList = 1;
                    _dgwLineWritten = 0;

                }
            }

            btnTab1_Click(sender, e);

        }

        private static void PrintPageHeader(PrintPageEventArgs e)
        {

            string g1 = "Fiş No ";
            e.Graphics.DrawString(g1, new System.Drawing.Font("Book Antiqua", 9, FontStyle.Bold), Brushes.Black, 55, 100);

            string g2 = "Fiş Tarihi";
            e.Graphics.DrawString(g2, new System.Drawing.Font("Book Antiqua", 9, FontStyle.Bold), Brushes.Black, 120, 100);

            string g3 = "Türü";
            e.Graphics.DrawString(g3, new System.Drawing.Font("Book Antiqua", 9, FontStyle.Bold), Brushes.Black, 210, 100);

            string g5 = "Açıklama";
            e.Graphics.DrawString(g5, new System.Drawing.Font("Book Antiqua", 9, FontStyle.Bold), Brushes.Black, 410, 100);

            string g6 = "Tutar";
            e.Graphics.DrawString(g6, new System.Drawing.Font("Book Antiqua", 9, FontStyle.Bold), Brushes.Black, 710, 100);

            string l2 = "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------";
            e.Graphics.DrawString(l2, new System.Drawing.Font("Book Antiqua", 9, FontStyle.Bold), Brushes.Black, 35, 120);
        }

    }

    public class ProgressForm : Form
    {
        private System.Windows.Forms.Label label;

        public ProgressForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = "Kayıtlar İnceleniyor... 1/5";
            Size = new Size(300, 100);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            ControlBox = false;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            TopMost = true;

            label = new System.Windows.Forms.Label();
            label.Text = "Fatura Numarası Aynı Olan Kayıtlar Eşleştiriliyor...";
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Dock = DockStyle.Fill;
            this.Controls.Add(label);
        }
        public void UpdateText(string progressText)
        {
            if(InvokeRequired)
            {
                label.Invoke(new System.Action(() =>
                {
                    Thread.Sleep(800);
                    label.Text = progressText;

                }));
            }
        }

    }

}
