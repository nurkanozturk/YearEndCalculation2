using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using YearEndCalculation.Business.Concrete;
using YearEndCalculation.Entities.Concrete;
using YearEndCalculation.WindowsFormUI;
using YearEndCalculation2.Business.Abstract;
using YearEndCalculation2.Business.DependencyResolvers.Ninject;

namespace YearEndCalculation2.WindowsFormUI
{
    public partial class FormMain : Form
    {
        private decimal _mkysEntryTotal, _mkysExitTotal, _wrongTdmsEntry, _wrongTdmsExit;
        private string dgwPrint = "MKYS GİRİŞLERİ";

        public static List<List<ActionRecord>> DgwItems;


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

        IYearEndService _yearEnd = InstanceFactory.GetInstance<IYearEndService>();

        FillMkys _fillMkys = new FillMkys();
        FillTdms _fillTdms = new FillTdms();
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            tglBtnDarkMode.Checked = darkMode;
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
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void BtnMkysEntrySelect_Click(object sender, EventArgs e)
        {

            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Filter = "Excel 97-2003|*.xls",
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
                    if (lines[0].Split(';')[4] != "Fatura No")
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
                List<string> lines = File.ReadAllLines(fileName, Encoding.GetEncoding("iso-8859-9")).ToList();
                var firstValue = lines[0].Split(';')[0];

                try
                {
                    if (firstValue != "Belge No")
                    {
                        MessageBox.Show(fileName + " Geçerli bir dosya değil!");
                        continue;
                    }
                }
                catch
                {

                    MessageBox.Show(fileName + " Geçerli bir dosya değil!");
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
                    MessageBox.Show(ex.Message + " Lütfen doğru belgeyi seçtiğinizden ve belge üzerinde değişiklik yapmadığınızdan emin olun." + "\nBelge adı: " + fileName);
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
                catch (OleDbException)
                {
                    DialogResult result = MessageBox.Show("TDMS verilerinin Excel'den okunabilmesi için \"Microsoft Access Database Engine 2010\" dosyasını indirip kurmanız gerekmektedir. İndirme bağlantısına şimdi gidilsin mi?", "Sistem Gereksinimleri Eksikliği!", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start("https://www.microsoft.com/en-us/download/details.aspx?id=13255");
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
            ClearPrices();
            dgw.DataSource = null;

            _mkysEntries = _yearEnd.CompareMkysTdms(_mkysEntries, _tdmsEntries);
            _mkysExits = _yearEnd.CompareMkysTdms(_mkysExits, _tdmsExits);
            _mkysExits = _yearEnd.CompareInSelf(_mkysEntries, _mkysExits);
            _tdmsExits = _yearEnd.CompareInSelf(_tdmsEntries, _tdmsExits);

            _mkysEntriesDgw.AddRange(_mkysEntries);
            _mkysExitsDgw.AddRange(_mkysExits);
            _tdmsEntriesDgw.AddRange(_tdmsEntries);
            _tdmsExitsDgw.AddRange(_tdmsExits);

            XmlNodeList matches = ManuelMatchManager.TakeMachedRecords(FillTdms.queryId);
            if (matches != null)
            {
                foreach (XmlNode match in matches)
                {
                    XmlNodeList items = match.SelectNodes("item");
                    foreach (XmlNode item in items)
                    {
                        _mkysEntriesDgw.Remove(_mkysEntries.SingleOrDefault(m => m.Id == item.Attributes["id"].Value));
                        _mkysExitsDgw.Remove(_mkysExits.SingleOrDefault(m => m.Id == item.Attributes["id"].Value));
                        _tdmsEntriesDgw.Remove(_tdmsEntries.SingleOrDefault(t => t.Id == item.Attributes["id"].Value));
                        _tdmsExitsDgw.Remove(_tdmsExits.SingleOrDefault(t => t.Id == item.Attributes["id"].Value));

                    }

                }
            }
            DgwItems = new List<List<ActionRecord>>();
            DgwItems.Add(_mkysEntriesDgw);
            DgwItems.Add(_mkysExitsDgw);
            DgwItems.Add(_tdmsEntriesDgw);
            DgwItems.Add(_tdmsExitsDgw);

            dgw.DataSource = _mkysEntriesDgw;

            FixDataGridFormats();

            foreach (var mkysEntry in _mkysEntries) { _mkysEntryTotal += mkysEntry.Price; }
            foreach (var mkysExit in _mkysExits) { _mkysExitTotal += mkysExit.Price; }
            foreach (var tdmsEntry in _tdmsEntries) { _wrongTdmsEntry += tdmsEntry.Price; }
            foreach (var tdmsExit in _tdmsExits) { _wrongTdmsExit += tdmsExit.Price; }

            prcMkysRemain.Text = tbxMkysRemain.Text == string.Empty ? "0,00"
                : string.Format("{0:0.00}", Convert.ToDecimal(tbxMkysRemain.Text));
            prcTdmsRemain.Text = tbxTdmsRemain.Text == string.Empty ? "0,00"
                : string.Format("{0:0.00}", Convert.ToDecimal(tbxTdmsRemain.Text));
            prcMkysEntry.Text = string.Format("{0:0.00}", _mkysEntryTotal);
            prcMkysExit.Text = string.Format("{0:0.00}", _mkysExitTotal);
            prcWrongTdmsEntry.Text = string.Format("{0:0.00}", _wrongTdmsEntry);
            prcWrongTdmsExit.Text = string.Format("{0:0.00}", _wrongTdmsExit);
            GetCountTick();
            calculated = true;
            rtbxMkysEntry.Clear();
            rtbxMkysExit.Clear();
            rtbxTdms.Clear();

            lblMkysEntryCount.Text = lblMkysExitCount.Text = lblTdmsCount.Text = "0";
            lblAttention.Visible = true;

            //tab1 e geçişi sağlıyoruz.
            ResetTabColors();
            btnTab1.BackColor = Color.OliveDrab;
            btnTab1.ForeColor = Color.WhiteSmoke;

            lblNoProblem.Text = string.Empty;

            if (_mkysEntriesDgw.Count == 0)
            {
                lblNoProblem.Text = "MUHASEBE KAYDI YAPILMAMIŞ GİRİŞ TİFİNİZ BULUNMAMAKTADIR.";
            }

            lblNoProblem.Visible = true;
            ClearData();
        }

        private void GetCountTick()
        {
            picCountCorrect.Visible = Math.Abs(CalculatePennyDefrence()) < 1;
            picCountIncorrect.Visible = picCountCorrect.Visible == false;
            prcDifrence.Text = string.Format("MKYS TDMS'den {0:0.00} TL ", Math.Abs(CalculatePennyDefrence()));
            prcDifrence.Text += CalculatePennyDefrence() < 0 ? "eksiktir" : "fazladır";
        }

        private void btnTab1_Click(object sender, EventArgs e)
        {
            if (calculated)
            {
                dgw.DataSource = _mkysEntriesDgw;
                FixDataGridFormats();
            }


            ResetTabColors();
            btnTab1.Font = new Font("tahoma", 9, FontStyle.Bold);

            btnTab1.BackColor = Color.OliveDrab;
            btnTab1.ForeColor = Color.WhiteSmoke;
            lblNoProblem.Text = "";

            if (_mkysEntriesDgw.Count == 0)
            {
                lblNoProblem.Text = "MUHASEBE KAYDI YAPILMAMIŞ GİRİŞ TİFİNİZ BULUNMAMAKTADIR.";
            }
        }

        private void btnTab2_Click(object sender, EventArgs e)
        {
            if (calculated)
            {
                dgw.DataSource = _mkysExitsDgw;
                FixDataGridFormats();
            }

            ResetTabColors();
            btnTab2.Font = new Font("tahoma", 9, FontStyle.Bold);
            btnTab2.BackColor = Color.Firebrick;
            btnTab2.ForeColor = Color.WhiteSmoke;
            lblNoProblem.Text = "";

            if (_mkysExitsDgw.Count == 0)
            {
                lblNoProblem.Text = "MUHASEBE KAYDI YAPILMAMIŞ ÇIKIŞ TİFİNİZ BULUNMAMAKTADIR.";
            }
        }

        private void btnTab3_Click(object sender, EventArgs e)
        {
            if (calculated)
            {
                dgw.DataSource = _tdmsEntriesDgw;
                FixDataGridFormats();
            }

            ResetTabColors();
            btnTab3.Font = new Font("tahoma", 9, FontStyle.Bold);
            btnTab3.BackColor = Color.DarkMagenta;
            btnTab3.ForeColor = Color.WhiteSmoke;
            lblNoProblem.Text = "";
            if (_tdmsEntriesDgw.Count == 0)
            {
                lblNoProblem.Text = "MUHASEBE GİRİŞ KAYITLARINIZDA HATA BULUNMAMAKTADIR.";
            }
        }

        private void btnTab4_Click(object sender, EventArgs e)
        {
            if (calculated)
            {
                dgw.DataSource = _tdmsExitsDgw;
                FixDataGridFormats();
            }

            ResetTabColors();
            btnTab4.Font = new Font("tahoma", 9, FontStyle.Bold);
            btnTab4.BackColor = Color.DarkCyan;
            btnTab4.ForeColor = Color.WhiteSmoke;

            lblNoProblem.Text = "";
            if (_tdmsExitsDgw.Count == 0)
            {
                lblNoProblem.Text = "MUHASEBE ÇIKIŞ KAYITLARINIZDA HATA BULUNMAMAKTADIR.";
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
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
            Cursor.Current = Cursors.AppStarting;
            var app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            app.WindowState = Microsoft.Office.Interop.Excel.XlWindowState.xlMinimized;
            app.WindowState = Microsoft.Office.Interop.Excel.XlWindowState.xlMaximized;
            // Excel çalışma kitabında yeni sayfalar oluşturuluyor.  
            Microsoft.Office.Interop.Excel._Worksheet worksheetMkysEntry = null;
            Microsoft.Office.Interop.Excel._Worksheet worksheetMkysExit = null;
            Microsoft.Office.Interop.Excel._Worksheet worksheetTdmsEntry = null;
            Microsoft.Office.Interop.Excel._Worksheet worksheetTdmsExit = null;

            app.Visible = true;
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
            actions.Add(_mkysEntriesDgw);
            actions.Add(_mkysExitsDgw);
            actions.Add(_tdmsEntriesDgw);
            actions.Add(_tdmsExitsDgw);
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
        }

        private void TbxMkysRemain_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != ','))
            {

                e.Handled = true;
            }

            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
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

            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
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
            btnTab1.Font = btnTab2.Font = btnTab3.Font = btnTab4.Font = new Font("tahoma", 9);

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
        }

        private void FixDataGridFormats()
        {
            dgw.Columns[0].Visible = false;
            dgw.Columns[1].HeaderText = "FİŞ NO";
            dgw.Columns[2].HeaderText = "FİŞ TARİHİ";
            dgw.Columns[3].HeaderText = "TÜRÜ";
            dgw.Columns[4].HeaderText = "AÇIKLAMA";
            dgw.Columns[5].HeaderText = "TUTAR";
            dgw.Columns[5].DefaultCellStyle.Format = "0.00";

        }

        private decimal CalculatePennyDefrence()
        {
            decimal mkysRemain = tbxMkysRemain.Text == "" ? 0 : Convert.ToDecimal(tbxMkysRemain.Text);
            decimal tdmsRemain = tbxTdmsRemain.Text == "" ? 0 : Convert.ToDecimal(tbxTdmsRemain.Text);

            var defrence = mkysRemain - (tdmsRemain + _mkysEntryTotal - _mkysExitTotal - _wrongTdmsEntry + _wrongTdmsExit);
            return defrence;
        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            _fillTdms.PreLoadOleDb();
        }

        int _nextDgwList = 1;
        int _lineWritten = 0;
        int _dgwLineWritten = 0;

        private void btnHelp_Click(object sender, EventArgs e)
        {
            HelpForm helpForm = new HelpForm();
            helpForm.ShowDialog();
        }


        private void tglBtnDarkMode_Click(object sender, EventArgs e)
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

        private void FormMain_SizeChanged(object sender, EventArgs e)
        {
            gbxResult.Width = this.Width - gbxEntrySelect.Width - 210;
            dgw.Width = gbxResult.Width - 10;
        }

        private void pbxFacebook_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/groups/466729348151176/?ref=share");
        }

        private void btnMatch_Click(object sender, EventArgs e)
        {
            MatchForm matchForm = new MatchForm();
            matchForm.ShowDialog();
            foreach (string id in MatchForm.matchedRecords)
            {
                _mkysEntriesDgw.RemoveAll(m => m.Id == id);
                _mkysExitsDgw.RemoveAll(m => m.Id == id);
                _tdmsEntriesDgw.RemoveAll(t => t.Id == id);
                _tdmsExitsDgw.RemoveAll(t => t.Id == id);
            }
            dgw.DataSource = null;
            btnTab1_Click(sender, e);


        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)

        {
            int totalRow = _mkysEntriesDgw.Count + _mkysExitsDgw.Count + _tdmsEntriesDgw.Count + _tdmsExitsDgw.Count;
            int pageLines = 40;
            int lineHeight = 140;

            dgw.DataSource = _mkysEntriesDgw;
            switch (dgwPrint)
            {
                case "MKYS ÇIKIŞLARI":
                    dgw.DataSource = _mkysExitsDgw;
                    break;
                case "TDMS GİRİŞLERİ":
                    dgw.DataSource = _tdmsEntriesDgw;
                    break;
                case "TDMS ÇIKIŞLARI":
                    dgw.DataSource = _tdmsExitsDgw;
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

                        e.Graphics.DrawString(dgw.Rows[_dgwLineWritten].Cells[1].FormattedValue.ToString(), dgw.Font = new Font("Book Antiqua", 8), Brushes.Black, new RectangleF(55, lineHeight, dgw.Columns[1].Width, 25));
                        e.Graphics.DrawString(dgw.Rows[_dgwLineWritten].Cells[2].FormattedValue.ToString(), dgw.Font = new Font("Book Antiqua", 8), Brushes.Black, new RectangleF(120, lineHeight, dgw.Columns[2].Width, 25));
                        e.Graphics.DrawString(dgw.Rows[_dgwLineWritten].Cells[3].FormattedValue.ToString(), dgw.Font = new Font("Book Antiqua", 8), Brushes.Black, new RectangleF(210, lineHeight, 200, 25));
                        e.Graphics.DrawString(dgw.Rows[_dgwLineWritten].Cells[4].FormattedValue.ToString(), dgw.Font = new Font("Book Antiqua", 8), Brushes.Black, new RectangleF(410, lineHeight, 290, 25));
                        e.Graphics.DrawString(dgw.Rows[_dgwLineWritten].Cells[5].FormattedValue.ToString(), dgw.Font = new Font("Book Antiqua", 8), Brushes.Black, new RectangleF(710, lineHeight, dgw.Columns[5].Width, 25));
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
}
