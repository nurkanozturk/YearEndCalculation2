using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using YearEndCalculation.Business.Concrete;
using YearEndCalculation.Entities.Concrete;
using YearEndCalculation2.WindowsFormUI;

namespace YearEndCalculation.WindowsFormUI
{
    public partial class MatchForm : Form
    {
        public MatchForm()
        {
            InitializeComponent();
        }

        public static List<string> matchedRecords = new List<string>();
        string queryId = FillTdms.queryId;
        List<ActionRecord> unMatchedItems = new List<ActionRecord>();
        List<ActionRecord> matchedItems = new List<ActionRecord>();
        private void MatchForm_Load(object sender, EventArgs e)
        {
            FillListView(FormMain.DgwItems[0], lvMkysEntry);
            FillListView(FormMain.DgwItems[1], lvMkysExit);
            FillListView(FormMain.DgwItems[2], lvTdmsEntry);
            FillListView(FormMain.DgwItems[3], lvTdmsExit);
            int columnWidth = 100;
            lvMkysEntry.Columns[1].Width = columnWidth;
            lvMkysExit.Columns[1].Width = columnWidth;
            lvTdmsEntry.Columns[1].Width = columnWidth;
            lvTdmsExit.Columns[1].Width = columnWidth;

            XmlNodeList matches = ManuelMatchManager.TakeMachedRecords(queryId);
            if (matches != null)
            {
                foreach (XmlNode match in matches)
                {
                    XmlNodeList items = match.SelectNodes("item");
                    string matchText = "";
                    foreach (XmlNode item in items)
                    {
                        matchText += item.Attributes[1].Value + " " + item.Attributes[2].Value + " " + item.Attributes[3].Value + "\n";
                    }
                    flpMatched.Controls.Add(new CheckBox
                    {
                        AutoSize = true,
                        Text = matchText
                    });

                }
            }

        }

        private void FillListView(List<ActionRecord> recordList, ListView listView)
        {
            foreach (ActionRecord item in recordList)
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Tag = item.Id;
                listViewItem.Text = item.DocNumber;
                listViewItem.SubItems.Add(item.Type);
                listViewItem.SubItems.Add(item.Price.ToString());
                listView.Items.Add(listViewItem);
                unMatchedItems.Add(item);
            }

        }

        string matchId = "";
        private void btnMatch_Click(object sender, EventArgs e)
        {
            decimal mkysEntryPrice = 0;
            decimal mkysExitPrice = 0;
            decimal tdmsEntryPrice = 0;
            decimal tdmsExitPrice = 0;
            foreach (ListViewItem item in lvMkysEntry.CheckedItems)
            {
                mkysEntryPrice += Convert.ToDecimal(item.SubItems[2].Text);
            }
            foreach (ListViewItem item in lvTdmsEntry.CheckedItems)
            {
                tdmsEntryPrice += Convert.ToDecimal(item.SubItems[2].Text);
            }
            foreach (ListViewItem item in lvTdmsExit.CheckedItems)
            {
                tdmsExitPrice += Convert.ToDecimal(item.SubItems[2].Text);
            }
            foreach (ListViewItem item in lvMkysExit.CheckedItems)
            {
                mkysExitPrice += Convert.ToDecimal(item.SubItems[2].Text);
            }

            if (Math.Abs(mkysEntryPrice + tdmsEntryPrice - mkysExitPrice - tdmsExitPrice) > 0.1m)
            {
                MessageBox.Show("Seçilen kayıtların tutarları eşleşmemektedir! Lütfen kontrol edip tekrar deneyin.");

            }
            else
            {
                string matchedItemText = "";
                foreach (Control control in this.Controls)
                {
                    if (control is ListView)
                    {
                        foreach (ListViewItem item in ((ListView)control).CheckedItems)
                        {
                            matchedItems.Add(unMatchedItems.Find(u => u.Id == item.Tag.ToString()));
                            matchedItemText += item.Text + " "+item.SubItems[1].Text+" "+item.SubItems[2].Text+"\n";
                            matchId += item.Tag + "-";
                            ((ListView)control).Items.Remove(item);
                        }
                    }

                }

                ManuelMatchManager matchManager = new ManuelMatchManager();
                matchedRecords = matchManager.SaveMatches(queryId, matchedItems);
                flpMatched.Controls.Add(new CheckBox { AutoSize = true, Text = matchedItemText, Tag=matchId });
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            foreach (CheckBox checkBox in flpMatched.Controls)
            {
                if (checkBox.Checked)
                {
                    //xml den kaldir
                    flpMatched.Controls.Remove(checkBox);
                    //eslesmemislere ekle
                    string matchid= checkBox.Tag.ToString();
                }
                
            }
        }
    }
}
