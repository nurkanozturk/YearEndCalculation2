using Microsoft.Office.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;
using System.Xml;
using YearEndCalculation.Business.Concrete;
using YearEndCalculation.Business.Tools;
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
            if (FormMain.DgwItems == null)
            {
                return;
            }
            FillListView(FormMain.DgwItems[0], lvMkysEntry);         
            FillListView(FormMain.DgwItems[1], lvMkysExit);
            FillListView(FormMain.DgwItems[2], lvTdmsEntry);
            FillListView(FormMain.DgwItems[3], lvTdmsExit);
            
            Color bgColor, bgColor2, textColor;

            if (FormMain.darkMode)
            {
                bgColor = Color.FromArgb(37, 37, 41);
                textColor = Color.FromArgb(231, 231, 231);
                bgColor2 = Color.FromArgb(60, 60, 63);
                btnMatch.BackColor=btnRemove.BackColor = Color.FromArgb(32, 29, 41);
                btnMatch.ForeColor= btnRemove.ForeColor = Color.DarkGray;
            }
            else
            {
                bgColor = Color.FromArgb(226, 242, 225);
                bgColor2 = Color.Honeydew;
                textColor = SystemColors.WindowText;
                btnMatch.BackColor=btnRemove.BackColor = Color.Bisque;
                btnMatch.ForeColor=btnRemove.ForeColor = SystemColors.ControlText;
                

            }
            BackColor = bgColor;
            lvMkysEntry.BackColor = lvMkysExit.BackColor = lvTdmsEntry.BackColor = lvTdmsExit.BackColor = bgColor2;
            lvMkysEntry.ForeColor = lvMkysExit.ForeColor = lvTdmsEntry.ForeColor = lvTdmsExit.ForeColor = textColor;
            label2.ForeColor = textColor;
            flpMatched.ForeColor = textColor;
            lblMkysEntry.ForeColor = lblMkysExit.ForeColor = lblTdmsEntry.ForeColor = lblTdmsExit.ForeColor = textColor;
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
                        matchText += item.Attributes[1].Value + " -- " + item.Attributes[2].Value + " -- " + item.Attributes[3].Value + " -- " + item.Attributes[4].Value + " -- " + item.Attributes[5].Value + "\n";
                        matchId.Add(item.Attributes[0].Value);
                        matchedItems.Add(new ActionRecord
                        {
                            Id = item.Attributes[0].Value,
                            DocNumber = item.Attributes[1].Value,
                            DocDate = item.Attributes[2].Value,
                            Type = item.Attributes[3].Value,
                            Explanation = item.Attributes[4].Value,
                            Price = decimal.Parse(item.Attributes[5].Value),
                            DateBase = FormatDate.Format(item.Attributes[2].Value)
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
            recordList = recordList.OrderBy(a => a.DocNumber).ToList();
                      
            foreach (ActionRecord item in recordList)
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Tag = item.Id;
                listViewItem.Text = item.DocNumber.ToString();
                listViewItem.SubItems.Add(item.DocDate);
                listViewItem.SubItems.Add(item.Type);
                listViewItem.SubItems.Add(item.Explanation);
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
                mkysEntryPrice += Convert.ToDecimal(item.SubItems[4].Text);
            }
            foreach (ListViewItem item in lvTdmsEntry.CheckedItems)
            {
                tdmsEntryPrice += Convert.ToDecimal(item.SubItems[4].Text);
            }
            foreach (ListViewItem item in lvTdmsExit.CheckedItems)
            {
                tdmsExitPrice += Convert.ToDecimal(item.SubItems[4].Text);
            }
            foreach (ListViewItem item in lvMkysExit.CheckedItems)
            {
                mkysExitPrice += Convert.ToDecimal(item.SubItems[4].Text);
            }
            var sonuc = Math.Abs(mkysEntryPrice - tdmsEntryPrice - mkysExitPrice + tdmsExitPrice);
            if (sonuc > 0.1m)
            {
                MessageBox.Show("Seçilen kayıtların tutarları eşleşmiyor. Kontrol edip tekrar deneyiniz.", "Dikkat", MessageBoxButtons.OK);

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
                            unMatchedItems.Remove(newMatchItem);
                            rescuedItems.Remove(newMatchItem);
                            newItems.Add(newMatchItem);
                            matchedItemText += item.Text + " -- " + item.SubItems[1].Text + " -- " + item.SubItems[2].Text + " -- " + item.SubItems[3].Text + " -- " + item.SubItems[4].Text + "\n";
                            
                            matchedItemIds.Add(item.Tag.ToString());
                            ((ListView)control).Items.Remove(item);
                        }
                    }

                }
                if (newItems.Count == 0)
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
                        lvItem.Text = item.DocNumber == "" ? " " : item.DocNumber;
                        lvItem.SubItems.Add(item.DocDate);
                        lvItem.SubItems.Add(item.Type);
                        lvItem.SubItems.Add(item.Explanation);
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

        //bu deneme iç içe metodunu çözümle
        void Deneme(ListViewItem item ,List<string> list2,int ilkDeger, decimal comb, string suggestText,decimal sayac)
        {
            /*
            sayac++;
            for (int i = ilkDeger; i < list2.Count; i++)
            {
                comb += Convert.ToDecimal(list2[i].SubItems[4].Text);
                suggestText += list2[i].SubItems[4].Text + "\n";
                Console.WriteLine(suggestText + "-----------------------------");
                if (comb > Convert.ToDecimal(item.SubItems[4].Text))
                {
                    comb = 0;
                    suggestText = string.Empty;
                    continue;

                }

                if (Math.Abs(comb - Convert.ToDecimal(item.SubItems[4].Text)) < 0.01m)
                {
                    suggestText += 
                    list2[i].SubItems[3].Text + " - " + list2[i].SubItems[4].Text
                    + "\n";

                    DialogResult result = MessageBox.Show(suggestText + "\n\nSonraki öneri?", "Öneri", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        //continue;
                    }
                    else
                    {
                        break;
                    }
                }
                Deneme(item,list2,ilkDeger+1,comb,suggestText,sayac);
                
                comb = 0;
                suggestText = string.Empty;

            }
            */

            for (int i = ilkDeger; i < list2.Count; i++)
            {
                suggestText += list2[i] + "\n";
                Console.WriteLine(suggestText + "-----------------------------");
                Deneme(item, list2, ilkDeger + 1, 0, suggestText, 0);
                suggestText = "";
            }
        }
        private void btnSuggest_Click(object sender, EventArgs e)
        {

            var result = AllCombination(50);
            Console.WriteLine(result);
            

            /*
            foreach (ListViewItem mkysEn in lvMkysEntry.Items)
            {
                foreach (ListViewItem mkysEx in lvMkysExit.Items)
                {
                    if (Math.Abs(Convert.ToDecimal(mkysEn.SubItems[4].Text)- Convert.ToDecimal(mkysEx.SubItems[4].Text)) < 0.01m)
                    {
                        string suggestText = mkysEn.SubItems[0].Text + " = " + mkysEx.SubItems[0].Text;
                       DialogResult result = MessageBox.Show(suggestText+ "\n\nSonraki öneri?", "Öneri", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            //continue;
                        }
                        else
                        {
                            goto Final;
                        }
                    }  
                }


            }
            foreach (ListViewItem tdmsEn in lvTdmsEntry.Items)
            {
                foreach (ListViewItem tdmsEx in lvTdmsExit.Items)
                {
                    if (Math.Abs(Convert.ToDecimal(tdmsEn.SubItems[4].Text) - Convert.ToDecimal(tdmsEx.SubItems[4].Text)) < 0.01m)
                    {
                        string suggestText = tdmsEx.SubItems[3].Text +" - "+ tdmsEx.SubItems[4].Text +
                            "\n = \n" + 
                            tdmsEn.SubItems[3].Text + " - "+tdmsEn.SubItems[4].Text;
                        DialogResult result = MessageBox.Show(suggestText + "\n\nSonraki öneri?", "Öneri", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            //continue;
                        }
                        else
                        {
                            goto Final;
                        }
                    }
                }


            }
            
            //ikili kombinasyon karşılaştırma
            ListView.ListViewItemCollection mkysEntryListFor2 = lvMkysEntry.Items;
            foreach (ListViewItem tdmsEn in lvTdmsEntry.Items)
            {
                for (int i = 0; i < mkysEntryListFor2.Count - 1; i++)
                {
                    if (Convert.ToDecimal(mkysEntryListFor2[i].SubItems[4].Text) > Convert.ToDecimal(tdmsEn.SubItems[4].Text))
                    {
                        continue;
                    }
                    for (int j = i + 1; j < mkysEntryListFor2.Count; j++)
                    {
                        decimal comb =
                            Convert.ToDecimal(mkysEntryListFor2[i].SubItems[4].Text) +
                            Convert.ToDecimal(mkysEntryListFor2[j].SubItems[4].Text);


                        if (Math.Abs(comb - Convert.ToDecimal(tdmsEn.SubItems[4].Text)) < 0.01m)
                        {
                            string suggestText = tdmsEn.SubItems[3].Text + " - " + tdmsEn.SubItems[4].Text +
                            "\n = \n" +
                            mkysEntryListFor2[i].SubItems[3].Text + " - " + mkysEntryListFor2[i].SubItems[4].Text
                            + "\n" +
                            mkysEntryListFor2[j].SubItems[3].Text + " - " + mkysEntryListFor2[j].SubItems[4].Text;
                            
                            DialogResult result = MessageBox.Show(suggestText + "\n\nSonraki öneri?", "Öneri", MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                //continue;
                            }
                            else
                            {
                                goto Final;
                            }
                        }

                    }
                }

            }
            ListView.ListViewItemCollection mkysExitListFor2 = lvMkysExit.Items;
            foreach (ListViewItem tdmsEx in lvTdmsExit.Items)
            {
                for (int i = 0; i < mkysExitListFor2.Count - 1; i++)
                {
                    if (Convert.ToDecimal(mkysExitListFor2[i].SubItems[4].Text) > Convert.ToDecimal(tdmsEx.SubItems[4].Text))
                    {
                        continue;
                    }
                    for (int j = i + 1; j < mkysExitListFor2.Count; j++)
                    {
                        decimal comb =
                            Convert.ToDecimal(mkysExitListFor2[i].SubItems[4].Text) +
                            Convert.ToDecimal(mkysExitListFor2[j].SubItems[4].Text);


                        if (Math.Abs(comb - Convert.ToDecimal(tdmsEx.SubItems[4].Text)) < 0.01m)
                        {
                            string suggestText = tdmsEx.SubItems[3].Text + " - " + tdmsEx.SubItems[4].Text +
                                                        "\n = \n" +
                                                        mkysExitListFor2[i].SubItems[3].Text + " - " + mkysExitListFor2[i].SubItems[4].Text
                                                        + "\n" +
                                                        mkysExitListFor2[j].SubItems[3].Text + " - " + mkysExitListFor2[j].SubItems[4].Text;
                            DialogResult result = MessageBox.Show(suggestText + "\n\nSonraki öneri?", "Öneri", MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                //continue;
                            }
                            else
                            {
                                goto Final;
                            }
                        }
                    }
                }

            }
            Console.WriteLine("ikili bitti");

            //üçlü kombinasyon karşılaştırma
            ListView.ListViewItemCollection mkysEntryListFor3 = lvMkysEntry.Items;
            foreach (ListViewItem tdmsEn in lvTdmsEntry.Items)
            {
                for (int i = 0; i < mkysEntryListFor3.Count - 1; i++)
                {
                    if (Convert.ToDecimal(mkysEntryListFor3[i].SubItems[4].Text) > Convert.ToDecimal(tdmsEn.SubItems[4].Text))
                    {
                        continue;
                    }
                    for (int j = i + 1; j < mkysEntryListFor3.Count; j++)
                    {
                        if (Convert.ToDecimal(mkysEntryListFor3[i].SubItems[4].Text) + Convert.ToDecimal(mkysEntryListFor3[j].SubItems[4].Text) > Convert.ToDecimal(tdmsEn.SubItems[4].Text))
                        {
                            continue;
                        }
                        for (int k = j + 1; k < mkysEntryListFor3.Count; k++)
                        {
                            decimal comb = Convert.ToDecimal(mkysEntryListFor3[i].SubItems[4].Text) +
                                           Convert.ToDecimal(mkysEntryListFor3[j].SubItems[4].Text) +
                                           Convert.ToDecimal(mkysEntryListFor3[k].SubItems[4].Text);


                            if (Math.Abs(comb - Convert.ToDecimal(tdmsEn.SubItems[4].Text)) < 0.01m)
                            {
                                string suggestText = tdmsEn.SubItems[3].Text + " - " + tdmsEn.SubItems[4].Text +
                                                       "\n = \n" +
                                                       mkysEntryListFor3[i].SubItems[0].Text + " - " + mkysEntryListFor3[i].SubItems[3].Text + " - " + mkysEntryListFor3[i].SubItems[4].Text
                                                       + "\n" +
                                                       mkysEntryListFor3[j].SubItems[0].Text + " - " + mkysEntryListFor3[j].SubItems[3].Text + " - " + mkysEntryListFor3[j].SubItems[4].Text
                                                       + "\n" +
                                                       mkysEntryListFor3[k].SubItems[0].Text + " - " + mkysEntryListFor3[k].SubItems[3].Text + " - " + mkysEntryListFor3[k].SubItems[4].Text
                                                      ;


                                DialogResult result = MessageBox.Show(suggestText + "\n\nSonraki öneri?", "Öneri", MessageBoxButtons.YesNo);
                                if (result == DialogResult.Yes)
                                {
                                    //continue;
                                }
                                else
                                {
                                    goto Final;
                                }
                            }
                        }
                    }

                }

            }
            ListView.ListViewItemCollection mkysExitListFor3 = lvMkysExit.Items;
            foreach (ListViewItem tdmsEx in lvTdmsExit.Items)
            {
                for (int i = 0; i < mkysExitListFor3.Count - 1; i++)
                {
                    if (Convert.ToDecimal(mkysExitListFor3[i].SubItems[4].Text) > Convert.ToDecimal(tdmsEx.SubItems[4].Text))
                    {
                        continue;
                    }
                    for (int j = i + 1; j < mkysExitListFor3.Count; j++)
                    {
                        if (Convert.ToDecimal(mkysExitListFor3[i].SubItems[4].Text) + Convert.ToDecimal(mkysExitListFor3[j].SubItems[4].Text) > Convert.ToDecimal(tdmsEx.SubItems[4].Text))
                        {
                            continue;
                        }
                        for (int k = j + 1; k < mkysExitListFor3.Count; k++)
                        {
                            decimal comb = Convert.ToDecimal(mkysExitListFor3[i].SubItems[4].Text) +
                                           Convert.ToDecimal(mkysExitListFor3[j].SubItems[4].Text) +
                                           Convert.ToDecimal(mkysExitListFor3[k].SubItems[4].Text);


                            if (Math.Abs(comb - Convert.ToDecimal(tdmsEx.SubItems[4].Text)) < 0.01m)
                            {
                                string suggestText = tdmsEx.SubItems[3].Text + " - " + tdmsEx.SubItems[4].Text +
                                                        "\n = \n" +
                                                        mkysExitListFor3[i].SubItems[0].Text + " - " + mkysExitListFor3[i].SubItems[3].Text + " - " + mkysExitListFor3[i].SubItems[4].Text
                                                        + "\n" +
                                                        mkysExitListFor3[j].SubItems[0].Text + " - " + mkysExitListFor3[j].SubItems[3].Text + " - " + mkysExitListFor3[j].SubItems[4].Text
                                                        + "\n" +
                                                        mkysExitListFor3[k].SubItems[0].Text + " - " + mkysExitListFor3[k].SubItems[3].Text + " - " + mkysExitListFor3[k].SubItems[4].Text
                                                        ;

                                DialogResult result = MessageBox.Show(suggestText + "\n\nSonraki öneri?", "Öneri", MessageBoxButtons.YesNo);
                                if (result == DialogResult.Yes)
                                {
                                    //continue;
                                }
                                else
                                {
                                    goto Final;
                                }
                            }
                        }
                    }

                }

            }
            Console.WriteLine("üçlü bitti");

            //dörtlü kombinasyon karşılaştırma
            ListView.ListViewItemCollection mkysEntryListFor4 = lvMkysEntry.Items;
            foreach (ListViewItem tdmsEn in lvTdmsEntry.Items)
            {
                for (int i = 0; i < mkysEntryListFor4.Count - 1; i++)
                {
                    if (Convert.ToDecimal(mkysEntryListFor4[i].SubItems[4].Text) > Convert.ToDecimal(tdmsEn.SubItems[4].Text))
                    {
                        continue;
                    }
                    for (int j = i + 1; j < mkysEntryListFor4.Count; j++)
                    {
                        if (Convert.ToDecimal(mkysEntryListFor4[i].SubItems[4].Text) + Convert.ToDecimal(mkysEntryListFor4[j].SubItems[4].Text) > Convert.ToDecimal(tdmsEn.SubItems[4].Text))
                        {
                            continue;
                        }
                        for (int k = j + 1; k < mkysEntryListFor4.Count; k++)
                        {
                            if (Convert.ToDecimal(mkysEntryListFor4[i].SubItems[4].Text) + Convert.ToDecimal(mkysEntryListFor4[j].SubItems[4].Text) + Convert.ToDecimal(mkysEntryListFor4[k].SubItems[4].Text) > Convert.ToDecimal(tdmsEn.SubItems[4].Text))
                            {
                                continue;
                            }
                            for (int y = k + 1; y < mkysEntryListFor4.Count; y++)
                            {
                                decimal comb = Convert.ToDecimal(mkysEntryListFor4[i].SubItems[4].Text) +
                                           Convert.ToDecimal(mkysEntryListFor4[j].SubItems[4].Text) +
                                           Convert.ToDecimal(mkysEntryListFor4[k].SubItems[4].Text) +
                                           Convert.ToDecimal(mkysEntryListFor4[y].SubItems[4].Text);


                                if (Math.Abs(comb - Convert.ToDecimal(tdmsEn.SubItems[4].Text)) < 0.01m)
                                {
                                    string suggestText = tdmsEn.SubItems[3].Text + " - " + tdmsEn.SubItems[4].Text +
                                                        "\n = \n" +
                                                        mkysEntryListFor4[i].SubItems[0].Text + " - " + mkysEntryListFor4[i].SubItems[3].Text + " - " + mkysEntryListFor4[i].SubItems[4].Text
                                                        + "\n" +
                                                        mkysEntryListFor4[j].SubItems[0].Text + " - " + mkysEntryListFor4[j].SubItems[3].Text + " - " + mkysEntryListFor4[j].SubItems[4].Text
                                                        + "\n" +
                                                        mkysEntryListFor4[k].SubItems[0].Text + " - " + mkysEntryListFor4[k].SubItems[3].Text + " - " + mkysEntryListFor4[k].SubItems[4].Text
                                                        + "\n" +
                                                        mkysEntryListFor4[y].SubItems[0].Text + " - " + mkysEntryListFor4[y].SubItems[3].Text + " - " + mkysEntryListFor4[y].SubItems[4].Text;
                                    DialogResult result = MessageBox.Show(suggestText + "\n\nSonraki öneri?", "Öneri", MessageBoxButtons.YesNo);
                                    if (result == DialogResult.Yes)
                                    {
                                        //continue;
                                    }
                                    else
                                    {
                                        goto Final;
                                    }
                                }
                            }
                        }
                    }

                }

            }
            ListView.ListViewItemCollection mkysExitListFor4 = lvMkysExit.Items;
            foreach (ListViewItem tdmsEx in lvTdmsExit.Items)
            {
                for (int i = 0; i < mkysExitListFor4.Count - 1; i++)
                {
                    if (Convert.ToDecimal(mkysExitListFor4[i].SubItems[4].Text) > Convert.ToDecimal(tdmsEx.SubItems[4].Text))
                    {
                        continue;
                    }
                    for (int j = i + 1; j < mkysExitListFor4.Count; j++)
                    {
                        if (Convert.ToDecimal(mkysExitListFor4[i].SubItems[4].Text) + Convert.ToDecimal(mkysExitListFor4[j].SubItems[4].Text) > Convert.ToDecimal(tdmsEx.SubItems[4].Text))
                        {
                            continue;
                        }
                        for (int k = j + 1; k < mkysExitListFor4.Count; k++)
                        {
                            if (Convert.ToDecimal(mkysExitListFor4[i].SubItems[4].Text) + Convert.ToDecimal(mkysExitListFor4[j].SubItems[4].Text) + Convert.ToDecimal(mkysExitListFor4[k].SubItems[4].Text) > Convert.ToDecimal(tdmsEx.SubItems[4].Text))
                            {
                                continue;
                            }
                            for (int y = k + 1; y < mkysExitListFor4.Count; y++)
                            {
                                decimal comb = Convert.ToDecimal(mkysExitListFor4[i].SubItems[4].Text) +
                                           Convert.ToDecimal(mkysExitListFor4[j].SubItems[4].Text) +
                                           Convert.ToDecimal(mkysExitListFor4[k].SubItems[4].Text) +
                                           Convert.ToDecimal(mkysExitListFor4[y].SubItems[4].Text);


                                if (Math.Abs(comb - Convert.ToDecimal(tdmsEx.SubItems[4].Text)) < 0.01m)
                                {
                                    string suggestText = tdmsEx.SubItems[3].Text + " - " + tdmsEx.SubItems[4].Text +
                                                        "\n = \n" +
                                                        mkysExitListFor4[i].SubItems[0].Text + " - " + mkysExitListFor4[i].SubItems[3].Text + " - " + mkysExitListFor4[i].SubItems[4].Text
                                                        + "\n" +
                                                        mkysExitListFor4[j].SubItems[0].Text + " - " + mkysExitListFor4[j].SubItems[3].Text + " - " + mkysExitListFor4[j].SubItems[4].Text
                                                        + "\n" +
                                                        mkysExitListFor4[k].SubItems[0].Text + " - " + mkysExitListFor4[k].SubItems[3].Text + " - " + mkysExitListFor4[k].SubItems[4].Text
                                                        + "\n" +
                                                        mkysExitListFor4[y].SubItems[0].Text + " - " + mkysExitListFor4[y].SubItems[3].Text + " - " + mkysExitListFor4[y].SubItems[4].Text;

                                    DialogResult result = MessageBox.Show(suggestText + "\n\nSonraki öneri?", "Öneri", MessageBoxButtons.YesNo);
                                    if (result == DialogResult.Yes)
                                    {
                                        //continue;
                                    }
                                    else
                                    {
                                        goto Final;
                                    }
                                }
                            }
                        }
                    }

                }

            }
        
            MessageBox.Show("Yeni öneri mevcut değil.", "Tamamlandı", MessageBoxButtons.OK);
        Final:
            Console.WriteLine("");

            */
        }

        private void CountDifference(List<string> strings)
        {
            string s = "";
            foreach (string str in strings)
            {
                s += str +"-";
            }
            Console.WriteLine(s);
        }

        static double Factorial(int n)
        {
            double sonuc = n;
            if (n == 0 || n == 1)
                return 1;
            else

                for (int i = n - 1; i > 1; i--)
                {
                    sonuc *= i;
                }
            return sonuc;
        }

        static double Combination(int n, int r)
        {
            if (n < r)
                return 0;
            return Factorial(n) / (Factorial(r) * Factorial(n - r));
        }

        static double AllCombination(int n)
        {
            double result = 0;
            for (int i = 1; i <= n; i++)
            {
                result += Combination(n, i);

            }
            return result;
        }
        private void Combination4(List<string> liste1)
        {
            //{ "0", "1", "2", "3", "4", "5", "6" }
            int n = 4;
            List<List<string>> combs = new List<List<string>>();
            for (int i = 0; i < liste1.Count - n + 1; i++)
            {
                for (int j = i + 1; j < liste1.Count; j++)
                {
                    for (int k = j + 1; k < liste1.Count; k++)
                    {
                        for (int y = k + 1; y < liste1.Count; y++)
                        {
                            List<string> comb = new List<string>
                            {
                            liste1[i],
                            liste1[j],
                            liste1[k],
                            liste1[y]
                            };
                            combs.Add(comb);
                        }
                    }
                }

            }
            foreach (var item in combs)
            {
                Console.WriteLine(item[0] + "-" + item[1] + "-" + item[2] + "-" + item[3]);

            }
        }

        private void Combination3(List<string> liste1)
        {
            //{ "0", "1", "2", "3", "4", "5", "6" }
            int n = 3;
            List<List<string>> combs = new List<List<string>>();
            for (int i = 0; i < liste1.Count - n + 1; i++)
            {
                for (int j = i + 1; j < liste1.Count; j++)
                {
                    for (int k = j + 1; k < liste1.Count; k++)
                    {
                        List<string> comb = new List<string>
                    {
                        liste1[i],
                        liste1[j],
                        liste1[k]
                    };
                        combs.Add(comb);
                    }


                }

            }
            foreach (var item in combs)
            {
                Console.WriteLine(item[0] + "-" + item[1] + "-" + item[2]);

            }
        }

        private List<decimal> Combination2(ListView.ListViewItemCollection liste1)
        {
            int n = 2;
            List<decimal> combs = new List<decimal>();
            for (int i = 0; i < liste1.Count - n + 1; i++)
            {
                for (int j = i + 1; j < liste1.Count; j++)
                {
                    decimal comb = 
                        Convert.ToDecimal(liste1[i].SubItems[4].Text) +
                        Convert.ToDecimal(liste1[j].SubItems[4].Text);

                    
                    combs.Add(comb);

                }

            }
            return combs;
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Tüm manuel eşleştirmeler iptal edilecek, devam edilsin mi?", "Tümünü sil?", MessageBoxButtons.YesNoCancel);
            if(dialogResult == DialogResult.Yes)
            {
                foreach (CheckBox checkBox in flpMatched.Controls)
                {
                    string matchId = "";
                    foreach (string itemId in (List<string>)checkBox.Tag)
                    {
                        matchId += itemId + "-";
                    }
                    _matchManager.RemoveMatchFromXml(queryId, matchId);
                    foreach (string itemId in (List<string>)checkBox.Tag)
                    {
                        ActionRecord item = matchedItems.Find(m => m.Id == itemId);
                        ListViewItem lvItem = new ListViewItem();
                        lvItem.Tag = item.Id;
                        lvItem.Text = item.DocNumber == "" ? " " : item.DocNumber;
                        lvItem.SubItems.Add(item.DocDate);
                        lvItem.SubItems.Add(item.Type);
                        lvItem.SubItems.Add(item.Explanation);
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
                flpMatched.Controls.Clear();
            }
           
            
        }
    }

}
