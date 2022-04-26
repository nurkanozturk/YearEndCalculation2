using System;
using System.Collections.Generic;
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
                    DocDate = words[1],
                    Type = words[3],
                    Explanation = words[11],
                    Price = Convert.ToDecimal(words[7])
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
                        DocDate = words[1],
                        Type = words[4],
                        Explanation = words[5],
                        Price = price
                    });
                }
            }

            return mkysExits;
        }

        public List<ActionRecord> FillMountlyExits(List<string> lines, List<ActionRecord> mkysExits)
        {


            for (int i = 0; i < 12; i++)
            {
                string[] months = { "OCA", "ŞUB", "MAR", "NIS", "MAY", "HAZ", "TEM", "AĞU", "EYL", "EKI", "KAS", "ARA" };
                decimal totalPrice = 0;

                foreach (var line in lines)
                {
                    string[] words = line.Split(';');

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
                        DocDate = months[i],
                        Type = "AYLIK TÜKETİM",
                        Explanation = "",
                        Price = totalPrice
                    });
                }
            }
            return mkysExits;
        }
    }

}
