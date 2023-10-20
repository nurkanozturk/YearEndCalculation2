using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using YearEndCalculation.Business.Tools;
using YearEndCalculation.Entities.Concrete;

namespace YearEndCalculation2.WindowsFormUI
{
    public class FillMkys
    {
       
        public List<ActionRecord> FillEntries(List<string> lines, List<ActionRecord> mkysEntries)
        {
            

            lines.Remove(lines[0]);
            foreach (var line in lines)
            {
                string[] words = line.Split(';');
                string docNumber = words[0].StartsWith("\"") ? words[0].Remove(0, 1) : words[0];
                

                mkysEntries.Add(new ActionRecord
                {
                    Id = "mkysEntry-"+docNumber+"-"+words[5]+"-"+words[7],
                    DocNumber = docNumber,
                    DocDate = words[1].ToString(),
                    Type = words[3],
                    InvoiceNumber = words[4],
                    Explanation = words[11]+"   Fatura No: " + words[4],
                    Price = Convert.ToDecimal(words[7]),
                    DateBase = FormatDate.Format(words[1].ToString())
                });
                
            }

            return mkysEntries;
        }

        public List<ActionRecord> FillExits(List<string> lines, List<ActionRecord> mkysExits)
        {
            lines.Remove(lines[0]);
            foreach (var line in lines)
            {
                string[] words = line.Split(';');

                string docNumber = words[0].StartsWith("\"") ? words[0].Remove(0, 1) : words[0];
                
                decimal price = words[7].EndsWith("\"") ? Convert.ToDecimal(words[7].Remove(words.Length - 1, 1)) : Convert.ToDecimal(words[7]);

                if (!words[4].ToLower().Contains("tüketim"))
                {

                    mkysExits.Add(new ActionRecord
                    {
                        Id = "mkysExit-"+docNumber+"-"+words[4]+"-"+price.ToString(),
                        DocNumber = docNumber,
                        DocDate =  words[1].ToString(),
                        Type = words[4],
                        InvoiceNumber = "",
                        Explanation = words[5],
                        Price = price,
                        DateBase = FormatDate.Format(words[1].ToString()),
                    });
                    
                }
            }

            return mkysExits;
        }

        public List<ActionRecord> FillMountlyExits(List<string> lines, List<ActionRecord> mkysExits)
        {
            int currentYear = 0;

            for (int i = 0; i < 12; i++)
            {
                string[] months = { "OCA", "ŞUB", "MAR", "NIS", "MAY", "HAZ", "TEM", "AĞU", "EYL", "EKI", "KAS", "ARA" };
                string[] monthsFullText = { "OCAK", "ŞUBAT", "MART", "NİSAN", "MAYIS", "HAZİRAN", "TEMMUZ", "AĞUSTOS", "EYLÜL", "EKİM", "KASIM", "ARALIK" };
                decimal totalPrice = 0;
                
                foreach (var line in lines)
                {
                    string[] words = line.Split(';');
                    if (currentYear==0)
                    {
                        try { currentYear = Convert.ToInt32(words[1].Remove(0, words[1].Length - 4)); } catch { }
                        
                    }
                    decimal price = words[7].EndsWith("\"") ? Convert.ToDecimal(words[7].Remove(words[7].Length - 1, 1)) : Convert.ToDecimal(words[7]);
                    if (words[4].ToLower().Contains("tüketim") && words[1].Contains(months[i]))
                    {
                        totalPrice += price;
                    }
                }
                if (totalPrice != 0)
                {
                    mkysExits.Add(new ActionRecord
                    {
                        Id = months[i]+"-"+totalPrice.ToString(),
                        DocNumber = "",
                        DocDate = monthsFullText[i],
                        Type = "AYLIK TÜKETİM",
                        Explanation = "",
                        Price = totalPrice,
                        DateBase = new DateTime(currentYear,i + 1, 1)
                    });
                    
                }
            }
            return mkysExits;
        }

       
    }

}
