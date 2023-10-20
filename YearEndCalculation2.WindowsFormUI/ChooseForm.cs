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
        public static bool _skipAll=false;

        public ChooseForm(List<ActionRecord> mkysOptions, List<ActionRecord> tdmsOptions, bool isEntry, List<ActionRecord> machedRecordsForChoose)
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
                if (machedRecordsForChoose.Contains(actionRecord)) { listItem.Checked = true; listItem.Selected = true; }
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
                if (machedRecordsForChoose.Contains(actionRecord)) { listItem.Checked = true;listItem.Selected = true; }
                lvTdms.Items.Add(listItem);
                });

            
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            decimal mkysEntryTotalPrice = 0;
            decimal tdmsEntryTotalPrice = 0;
            decimal mkysExitTotalPrice = 0;
            decimal tdmsExitTotalPrice = 0;
            int checkedItemCount = 0;
            List<ActionRecord> mkysRecords = new List<ActionRecord>();
            List<ActionRecord> tdmsRecords = new List<ActionRecord>();
            if (_isEntry)
            {
                foreach (ListViewItem mkysItem in lvMkys.Items)
                {
                    if (mkysItem.Checked)
                    {
                        mkysEntryTotalPrice += ((ActionRecord)mkysItem.Tag).Price;
                        checkedItemCount++;
                    }
                }
                foreach (ListViewItem tdmsItem in lvTdms.Items)
                {
                    if (tdmsItem.Checked)
                    {
                        tdmsEntryTotalPrice += ((ActionRecord)tdmsItem.Tag).Price;
                    }
                }
                if (Math.Abs(mkysEntryTotalPrice - tdmsEntryTotalPrice) > 0.1m || checkedItemCount == 0)
                {
                    return;
                }
                List<ActionRecord> newItems = new List<ActionRecord>();
                foreach (ListViewItem item in lvMkys.Items)
                {
                    if (item.Checked)
                    {
                        selectedMEnItems.Add(item.Tag as ActionRecord);
                        newItems.Add(item.Tag as ActionRecord);
                        mkysRecords.Add(item.Tag as ActionRecord);
                    }
                    else
                    {
                        unselectedMEnItems.Add(item.Tag as ActionRecord);
                    }
                }
                foreach (ListViewItem item in lvTdms.Items)
                {
                    if (item.Checked)
                    {
                        selectedTEnItems.Add(item.Tag as ActionRecord);
                        newItems.Add(item.Tag as ActionRecord);
                        tdmsRecords.Add(item.Tag as ActionRecord);
                    }
                    else
                    {
                        unselectedTEnItems.Add(item.Tag as ActionRecord);
                    }
                    
                }
                int i = 0;
                foreach (ActionRecord mkysRecord in mkysRecords)
                {
                    YearEndManager.matches.Add(
                        new Match 
                        { 
                            Id = mkysRecord.Id, 
                            MkysRecord = mkysRecord,
                            TdmsRecord = tdmsRecords[i],
                            IsInvoiceNumberMatch = false
                        });
                    ++i;
                }

                _ = _manuelMatchManager.SaveMatches(FillTdms.queryId, newItems);
            }
            else
            {
                foreach (ListViewItem mkysItem in lvMkys.Items)
                {
                    if (mkysItem.Checked)
                    {
                        mkysExitTotalPrice += ((ActionRecord)mkysItem.Tag).Price;
                        checkedItemCount++;
                    }
                }
                foreach (ListViewItem tdmsItem in lvTdms.Items)
                {
                    if (tdmsItem.Checked)
                    {
                        tdmsExitTotalPrice += ((ActionRecord)tdmsItem.Tag).Price;
                    }
                }
                if (Math.Abs(mkysExitTotalPrice - tdmsExitTotalPrice) > 0.1m || checkedItemCount == 0)
                {
                    return;
                }
                List<ActionRecord> newItems = new List<ActionRecord>();
                foreach (ListViewItem item in lvMkys.Items)
                {
                    if (item.Checked)
                    {
                        selectedMExItems.Add(item.Tag as ActionRecord);
                        newItems.Add(item.Tag as ActionRecord);
                        mkysRecords.Add(item.Tag as ActionRecord);
                    }
                    else
                    {
                        unselectedMExItems.Add(item.Tag as ActionRecord);
                    }
                }
                foreach (ListViewItem item in lvTdms.Items)
                {
                    if (item.Checked)
                    {
                        selectedTExItems.Add(item.Tag as ActionRecord);
                        newItems.Add(item.Tag as ActionRecord);
                        tdmsRecords.Add(item.Tag as ActionRecord);
                    }
                    else
                    {
                        unselectedTExItems.Add(item.Tag as ActionRecord);
                    }
                }

                int i = 0;
                foreach (ActionRecord mkysRecord in mkysRecords)
                {
                    YearEndManager.matches.Add(
                        new Match
                        {
                            Id = mkysRecord.Id,
                            MkysRecord = mkysRecord,
                            TdmsRecord = tdmsRecords[i],
                            IsInvoiceNumberMatch = false
                        });
                    ++i;
                }

                _ = _manuelMatchManager.SaveMatches(FillTdms.queryId, newItems);
            }

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
            _skipAll = true;
            Close();
        }

       
    }
}
