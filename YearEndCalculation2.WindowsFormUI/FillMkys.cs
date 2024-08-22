using System;
using System.Collections.Generic;
using YearEndCalculation.Business.Tools;
using YearEndCalculation.Entities.Concrete;

namespace YearEndCalculation2.WindowsFormUI
{
    public class FillMkys
    {
       
       
        public List<ActionRecord> FillEntries(List<string> lines, List<ActionRecord> mkysEntries)
        {
            int docIndex = 2;
            int priceIndex = 18;
            int invoiceDateIndex = 11;
            int dateIndex = 3;
            int typeIndex = 6;
            int invoiceIndex = 10;
            int expIndex = 8;
            int detailIndex = 1;

            lines.Remove(lines[0]);
            foreach (var line in lines)
            {
                string[] words = line.Split(';');
                if (words[detailIndex] != "")
                {
                    continue;
                }

                string docNumber = words[docIndex].StartsWith("\"") ? words[docIndex].Remove(0, 1) : words[docIndex];

                var recordPrice = 0m;
                if (words[priceIndex] != "")
                {
                    recordPrice = Convert.ToDecimal(words[priceIndex]);
                }
                 

                mkysEntries.Add(new ActionRecord
                {
                    Id = "mkysEntry-"+docNumber+"-"+words[invoiceDateIndex]+"-"+words[priceIndex],
                    DocNumber = docNumber,
                    DocDate = words[dateIndex].ToString(),
                    Type = words[typeIndex],
                    InvoiceNumber = words[invoiceIndex],
                    Explanation = words[expIndex]+"   Fatura No: " + words[invoiceIndex],
                    Price = recordPrice,
                    DateBase = FormatDate.Format(words[dateIndex].ToString())
                });
                
            }

            return mkysEntries;
        }

        public List<ActionRecord> FillExits(List<string> lines, List<ActionRecord> mkysExits)
        {
            int docIndex = 2;
            int priceIndex = 10;
            int dateIndex = 3;
            int typeIndex = 6;
            int expIndex = 8;
            int detailIndex = 1;

            lines.Remove(lines[0]);
            foreach (var line in lines)
            {
                string[] words = line.Split(';');
                if (words[detailIndex] != "")
                {
                    continue;
                }
                string docNumber = words[docIndex].StartsWith("\"") ? words[docIndex].Remove(0, 1) : words[docIndex];

                var price = 0m;
                if (words[priceIndex] != "")
                {
                    price = words[priceIndex].EndsWith("\"") ? Convert.ToDecimal(words[priceIndex].Remove(words.Length - 1, 1)) : Convert.ToDecimal(words[priceIndex]);
                }


                if (!words[typeIndex].ToLower().Contains("tüketim"))
                {
                    

                    mkysExits.Add(new ActionRecord
                    {
                        Id = "mkysExit-"+docNumber+"-"+words[typeIndex]+"-"+price.ToString(),
                        DocNumber = docNumber,
                        DocDate =  words[dateIndex].ToString(),
                        Type = words[typeIndex],
                        InvoiceNumber = "",
                        Explanation = words[expIndex],
                        Price = price,
                        DateBase = FormatDate.Format(words[dateIndex].ToString()),
                    });
                    
                }
            }

            return mkysExits;
        }

        public List<ActionRecord> FillMountlyExits(List<string> lines, List<ActionRecord> mkysExits)
        {            
            int priceIndex = 10;
            int dateIndex = 3;
            int typeIndex = 6;
            int detailIndex = 1;

            int currentYear = 0;

            for (int i = 0; i < 12; i++)
            {
                string[] months = { "OCA", "ŞUB", "MAR", "NIS", "MAY", "HAZ", "TEM", "AĞU", "EYL", "EKI", "KAS", "ARA" };
                string[] monthsFullText = { "OCAK", "ŞUBAT", "MART", "NİSAN", "MAYIS", "HAZİRAN", "TEMMUZ", "AĞUSTOS", "EYLÜL", "EKİM", "KASIM", "ARALIK" };
                decimal totalPrice = 0;
                
                foreach (var line in lines)
                {
                    string[] words = line.Split(';');
                    if (words[detailIndex] != "")
                    {
                        continue;
                    }
                    if (currentYear==0)
                    {
                        try { currentYear = Convert.ToInt32(words[dateIndex].Remove(0, words[dateIndex].Length - 4)); } catch { }
                        
                    }

                    var price = 0m;
                    if (words[priceIndex] != "")
                    {
                        price = words[priceIndex].EndsWith("\"") ? Convert.ToDecimal(words[priceIndex].Remove(words[priceIndex].Length - 1, 1)) : Convert.ToDecimal(words[priceIndex]);
                    }

                    if (words[typeIndex].ToLower().Contains("tüketim") && words[dateIndex].Contains(months[i]))
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
