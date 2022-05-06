using System;
using System.Collections.Generic;
using System.Drawing;
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
        ManuelMatchManager _matchManager = new ManuelMatchManager();

        public static List<string> matchedRecords = new List<string>();
        public static List<ActionRecord> rescuedItems = new List<ActionRecord>();
        string queryId = FillTdms.queryId;
        List<ActionRecord> unMatchedItems = new List<ActionRecord>();
        List<ActionRecord> matchedItems = new List<ActionRecord>();
        private void MatchForm_Load(object sender, EventArgs e)
        {
            if (FormMain.DgwItems==null)
            {
                return;
            }
            FillListView(FormMain.DgwItems[0], lvMkysEntry);
            FillListView(FormMain.DgwItems[1], lvMkysExit);
            FillListView(FormMain.DgwItems[2], lvTdmsEntry);
            FillListView(FormMain.DgwItems[3], lvTdmsExit);
            int columnWidth = 100;
            lvMkysEntry.Columns[1].Width = columnWidth;
            lvMkysExit.Columns[1].Width = columnWidth;
            lvTdmsEntry.Columns[1].Width = columnWidth;
            lvTdmsExit.Columns[1].Width = columnWidth;
            Color bgColor, bgColor2, headerColor, textColor, caclTextColor, cellBgColor, selectionBgColor, foreColor, calcBgColor, tabTextColor, prcTextColor, rtbxBgColor, tabBgColor;

            if (FormMain.darkMode)
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
                btnMatch.BackColor = Color.FromArgb(32, 29, 41);
                btnMatch.ForeColor = Color.DarkGray;
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

            }
            BackColor = bgColor;
            lvMkysEntry.BackColor = lvMkysExit.BackColor= lvTdmsEntry.BackColor= lvTdmsExit.BackColor = bgColor2;
            lvMkysEntry.ForeColor = lvMkysExit.ForeColor = lvTdmsEntry.ForeColor = lvTdmsExit.ForeColor = textColor;
            label1.ForeColor = label2.ForeColor = textColor;
            flpMatched.ForeColor = textColor;
            XmlNodeList matches = ManuelMatchManager.TakeMachedRecords(queryId);
            if (matches != null)
            {
                foreach (XmlNode match in matches)
                {
                    XmlNodeList items = match.SelectNodes("item");
                    string matchText = "";
                    List<string> matchId = new List<string>();
                    foreach (XmlNode item in items)
                    {
                        matchText += item.Attributes[1].Value + " " + item.Attributes[2].Value + " " + item.Attributes[3].Value + "\n";
                        matchId.Add(item.Attributes[0].Value);
                        matchedItems.Add(new ActionRecord 
                            {
                                Id = item.Attributes[0].Value, 
                                DocNumber = item.Attributes[1].Value,
                                DocDate = item.Attributes[2].Value,
                                Type= item.Attributes[3].Value,
                                Explanation= item.Attributes[4].Value,
                                Price = decimal.Parse(item.Attributes[5].Value)
                            });
                    }
                    flpMatched.Controls.Add(new CheckBox
                    {
                        AutoSize = true,
                        Text = matchText,
                        Tag = matchId
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


        private void btnMatch_Click(object sender, EventArgs e)
        {
            List<string> matchedItemIds = new List<string>();
            List<ActionRecord> newItems = new List<ActionRecord>();
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

            if (Math.Abs(mkysEntryPrice - tdmsEntryPrice - mkysExitPrice + tdmsExitPrice) > 0.1m)
            {
                MessageBox.Show("Sadece tutarları eşleşen kayıtları ekleyebilirsiniz.","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);

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
                            ActionRecord newMatchItem = unMatchedItems.Find(u => u.Id == item.Tag.ToString());
                            matchedItems.Add(newMatchItem);
                            newItems.Add(newMatchItem);
                            matchedItemText += item.Text + " " + item.SubItems[1].Text + " " + item.SubItems[2].Text + "\n";
                            matchedItemIds.Add(item.Tag.ToString());
                            ((ListView)control).Items.Remove(item);
                        }
                    }

                }
                if (newItems.Count==0)
                {
                    return;
                }
                matchedRecords = _matchManager.SaveMatches(queryId, newItems);
                flpMatched.Controls.Add(new CheckBox { AutoSize = true, Text = matchedItemText, Tag = matchedItemIds });
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            List<CheckBox> checkedBoxes = new List<CheckBox>();
            foreach (CheckBox checkBox in flpMatched.Controls)
            {
                if (checkBox.Checked)
                {
                    string matchId = "";
                    foreach (string itemId in (List<string>)checkBox.Tag)
                    {
                        matchId += itemId + "-";
                    }
                    _matchManager.RemoveMatchFromXml(queryId, matchId);
                    checkedBoxes.Add(checkBox);
                    foreach (string itemId in (List<string>)checkBox.Tag)
                    {
                        ActionRecord item = matchedItems.Find(m => m.Id == itemId);
                        ListViewItem lvItem = new ListViewItem();
                        lvItem.Tag = item.Id;
                        lvItem.Text = item.DocNumber;
                        lvItem.SubItems.Add(item.Type);
                        lvItem.SubItems.Add(item.Price.ToString());

                        AddItemToLv(lvMkysEntry, lvItem);
                        AddItemToLv(lvMkysExit, lvItem);
                        AddItemToLv(lvTdmsEntry, lvItem);
                        AddItemToLv(lvTdmsExit, lvItem);
                        matchedItems.Remove(item);
                        matchedRecords.Remove(item.Id);
                        unMatchedItems.Add(item);
                        rescuedItems.Add(item);
                    }

                }

            }
            foreach (CheckBox checkedBox in checkedBoxes)
            {
                flpMatched.Controls.Remove(checkedBox);
            }
            
        }

        private void AddItemToLv(ListView listView, ListViewItem lvItem)
        {
            if (lvItem.Tag.ToString().ToLower().StartsWith(listView.Name.ToLower().Remove(0, 2)))
            {
                listView.Items.Add(lvItem);

            }
        }
    }
}
