using System;
using System.Collections.Generic;
using System.Windows.Forms;
using YearEndCalculation.Business.Concrete;
using YearEndCalculation.Entities.Concrete;
using YearEndCalculation2.Business.Concrete.Managers;
using YearEndCalculation2.WindowsFormUI;

namespace YearEndCalculation.WindowsFormUI
{
    public partial class ChooseForm : Form
    {
        ManuelMatchManager _manuelMatchManager = new ManuelMatchManager();
        public static List<ActionRecord> selectedMEnItems = new List<ActionRecord>();
        public static List<ActionRecord> unselectedMEnItems = new List<ActionRecord>();
        public static List<ActionRecord> selectedMExItems = new List<ActionRecord>();
        public static List<ActionRecord> unselectedMExItems = new List<ActionRecord>();
        public static List<ActionRecord> selectedTEnItems = new List<ActionRecord>();
        public static List<ActionRecord> unselectedTEnItems = new List<ActionRecord>();
        public static List<ActionRecord> selectedTExItems = new List<ActionRecord>();
        public static List<ActionRecord> unselectedTExItems = new List<ActionRecord>();
        bool _isEntry=false;

        public ChooseForm(List<ActionRecord> mkysOptions, List<ActionRecord> tdmsOptions, bool isEntry)
        {
            InitializeComponent();
            _isEntry= isEntry;
            
            mkysOptions.ForEach(actionRecord => {
                ListViewItem listItem = new ListViewItem();
                listItem.Tag = actionRecord;
                listItem.Text = actionRecord.DocNumber;
                listItem.SubItems.Add(actionRecord.DocDate);
                listItem.SubItems.Add(actionRecord.Type);
                listItem.SubItems.Add(actionRecord.Explanation);
                listItem.SubItems.Add(actionRecord.Price.ToString());
                lvMkys.Items.Add(listItem);
                
            });
            tdmsOptions.ForEach(actionRecord => { 
                ListViewItem listItem = new ListViewItem();
                listItem.Tag = actionRecord;
                listItem.Text = actionRecord.DocNumber;
                listItem.SubItems.Add(actionRecord.DocDate);
                listItem.SubItems.Add(actionRecord.Type);
                listItem.SubItems.Add(actionRecord.Explanation.ToString());
                listItem.SubItems.Add(actionRecord.Price.ToString());
                lvTdms.Items.Add(listItem);
                });

            
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            decimal mkysTotalPrice = 0;
            decimal tdmsTotalPrice = 0;
           
            List<ActionRecord> mkysRecords = new List<ActionRecord>();
            List<ActionRecord> tdmsRecords = new List<ActionRecord>();

            foreach (ListViewItem mkysItem in lvMkys.Items)
            {
                if (mkysItem.Checked)
                {
                    mkysTotalPrice += ((ActionRecord)mkysItem.Tag).Price;
                }
            }
            foreach (ListViewItem tdmsItem in lvTdms.Items)
            {
                if (tdmsItem.Checked)
                {
                    tdmsTotalPrice += ((ActionRecord)tdmsItem.Tag).Price;
                }
            }
            if (mkysTotalPrice + tdmsTotalPrice == 0) return;
            if (Math.Abs(mkysTotalPrice - tdmsTotalPrice) > 0.1m)
            {
                MessageBox.Show("Seçilen kayıtların tutarları eşleşmiyor. Kontrol edip tekrar deneyiniz.", "Dikkat", MessageBoxButtons.OK);
                return;
            }

            foreach (ListViewItem item in lvMkys.Items)
            {
                if (item.Checked)
                {
                    if (_isEntry)
                    {
                        selectedMEnItems.Add(item.Tag as ActionRecord);
                    }
                    else
                    {
                        selectedMExItems.Add(item.Tag as ActionRecord);
                    }
                    mkysRecords.Add(item.Tag as ActionRecord);
                }
                else
                {
                    if (_isEntry)
                    {
                        unselectedMEnItems.Add(item.Tag as ActionRecord);
                    }
                    else
                    {
                        unselectedMExItems.Add(item.Tag as ActionRecord);
                    }
                }
            }
            foreach (ListViewItem item in lvTdms.Items)
            {
                if (item.Checked)
                {
                    if (_isEntry)
                    {
                        selectedTEnItems.Add(item.Tag as ActionRecord);
                    }
                    else
                    {
                        selectedTExItems.Add(item.Tag as ActionRecord);
                    }
                    tdmsRecords.Add(item.Tag as ActionRecord);
                }
                else
                {
                    if (_isEntry)
                    {
                        unselectedTEnItems.Add(item.Tag as ActionRecord);
                    }
                    else
                    {
                        unselectedTExItems.Add(item.Tag as ActionRecord);
                    }
                }

            }
            /*int i = 0;
            foreach (ActionRecord mkysRecord in mkysRecords)
            {
                YearEndManager.matches.Add(
                    new Match 
                    { 
                        Id = mkysRecord.Id, 
                        MkysRecord = mkysRecord,
                        TdmsRecord = tdmsRecords[i],
                        IsSafeMatch = true
                    });
                ++i;
            }*/
            List<ActionRecord> newItems = new List<ActionRecord>();

            newItems.AddRange(mkysRecords);
            newItems.AddRange(tdmsRecords);

            _ = _manuelMatchManager.SaveMatches(FillTdms.queryId, newItems);

            Close();
        }

        private void btnSkip_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lvMkys_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void lvTdms_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void btnSkipAll_Click(object sender, EventArgs e)
        {
            FormMain._skipAllChoose = true;
            Close();
        }

       
    }
}
