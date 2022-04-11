using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YearEndCalculation2.Business.Abstract;
using YearEndCalculation2.Business.DependencyResolvers.Ninject;
using YearEndCalculation2.Entities.Concrete;

namespace YearEndCalculation2.WindowsFormUI
{
    public class FillMkys
    {
        public List<MkysEntry> FillEntries(List<string> lines, List<MkysEntry> mkysEntries)
        {

            lines.Remove(lines[0]);
            foreach (var line in lines)
            {
                string[] words = line.Split(';');
                string receiptNumber = words[0].StartsWith("\"")?words[0].Remove(0,1):words[0];

                mkysEntries.Add(new MkysEntry
                {
                    ReceiptNumber =receiptNumber,
                    ReceiptDate = words[1],
                    TypeOfSupply = words[3],
                    InvoiceNumber = words[4],
                    InvoiceDate = words[5],
                    Amount = Convert.ToDecimal(words[7])
                });
            }

            return mkysEntries;
        }

        public List<MkysExit> FillExits(List<string> lines, List<MkysExit> mkysExits)
        {
            lines.Remove(lines[0]);
            foreach (var line in lines)
            {
                string[] words = line.Split(';');

                string receiptNumber = words[0].StartsWith("\"") ? words[0].Remove(0, 1) : words[0];
                decimal amount = words[7].EndsWith("\"") ? Convert.ToDecimal(words[7].Remove(words.Length-1,1)) : Convert.ToDecimal(words[7]);

                if (!words[4].ToLower().Contains("tüketim"))
                {
                    if (words[5].Length > 149)
                    {
                        words[5] = words[5].Remove(149);
                    }
                    mkysExits.Add(new MkysExit
                    {
                        ReceiptNumber = receiptNumber,
                        ReceiptDate = words[1],
                        TypeOfExit = words[4],
                        ToWhere = words[5],
                        Amount = amount
                    });
                }
            }

            return mkysExits;
        }

        public List<MkysExit> FillMountlyExits(List<string> lines, List<MkysExit> mkysExits)
        {
           

            for (int i = 0; i < 12; i++)
            {
                string[] months = { "OCA", "ŞUB", "MAR", "NIS", "MAY", "HAZ", "TEM", "AĞU", "EYL", "EKI", "KAS", "ARA" };
                decimal totalAmount = 0;

                foreach (var line in lines)
                {
                    string[] words = line.Split(';');

                    decimal amount = words[7].EndsWith("\"") ?Convert.ToDecimal( words[7].Remove(words[7].Length - 1, 1)) : Convert.ToDecimal(words[7]);
                    if (words[4].ToLower().Contains("tüketim") && words[1].Contains(months[i]))
                    {
                        if (words[5].Length > 149)
                        {
                            words[5] = words[5].Remove(149);
                        }
                        totalAmount += amount;
                    }
                }
                if (totalAmount != 0)
                {
                    mkysExits.Add(new MkysExit
                    {
                        ReceiptNumber = "",
                        ReceiptDate = months[i],
                        TypeOfExit = "AYLIK TÜKETİM",
                        ToWhere = "",
                        Amount = totalAmount
                    });
                }
            }
            return mkysExits;
        }
    }

}
