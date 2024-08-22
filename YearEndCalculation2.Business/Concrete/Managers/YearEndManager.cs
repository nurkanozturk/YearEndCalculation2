using System;
using System.Collections.Generic;
using YearEndCalculation.Entities.Concrete;
using YearEndCalculation2.Business.Abstract;

namespace YearEndCalculation2.Business.Concrete.Managers
{
    public class YearEndManager : IYearEndService
    {        
        public static List<Match> matches = new List<Match>();

        public List<ActionRecord> CompareInvoiceNumber(List<ActionRecord> mkys, List<ActionRecord> tdms)
        {
            //fatura numarası karşılaştırması
            for (int i = 0; i < mkys.Count; i++)
            {
                for (int j = 0; j < tdms.Count; j++)
                {
                    if (mkys[i].InvoiceNumber !="" && mkys[i].InvoiceNumber == tdms[j].InvoiceNumber && Math.Abs(mkys[i].Price - tdms[j].Price) < 0.03m )
                    {
                        matches.Add(
                            new Match { 
                                Id = mkys[i].Id, 
                                MkysRecord = mkys[i], 
                                TdmsRecord = tdms[j], 
                                IsSafeMatch = true

                            }); 

                        mkys.RemoveAt(i);
                        tdms.RemoveAt(j);

                        j = tdms.Count;
                        i = -1;
                    }
                }
            }
            return mkys;
        }

        public List<ActionRecord> CompareDocNumber(List<ActionRecord> mkys, List<ActionRecord> tdms)
        {
            //fiş numarası karşılaştırması
            for (int i = 0; i < mkys.Count; i++)
            {
                for (int j = 0; j < tdms.Count; j++)
                {
                    if (mkys[i].DocNumber != "" && mkys[i].DocNumber == tdms[j].DocNumber && Math.Abs(mkys[i].Price - tdms[j].Price) < 0.03m)
                    {
                        matches.Add(
                            new Match
                            {
                                Id = mkys[i].Id,
                                MkysRecord = mkys[i],
                                TdmsRecord = tdms[j],
                                IsSafeMatch = true
                            });

                        mkys.RemoveAt(i);
                        tdms.RemoveAt(j);

                        j = tdms.Count;
                        i = -1;
                    }
                }
            }

            return mkys;
        }

        public List<ActionRecord> CompareMassRecords(List<ActionRecord> mkys, List<ActionRecord> tdms)
        {
            //birden fazla kaydın tek seferde kaydının kontrolü
            for (int i = 0; i < tdms.Count; i++)
            {
                string[] docNumbers = tdms[i].DocNumber.Split('-');
                if (docNumbers.Length < 2)
                {
                    continue;
                }

                int tdmsDocNumberCount = docNumbers.Length;
                List<ActionRecord> mkysRecords = new List<ActionRecord>();

                //tdms nin bir kaydındaki çoklu dosya numarasını tek tek mkys de aratıp mkysDocNumbers listesine ekliyoruz.
                for (int j = 0; j < tdmsDocNumberCount; j++)
                {
                    for (int k = 0; k < mkys.Count; k++)
                    {
                        if (docNumbers[j].Trim() == mkys[k].DocNumber)
                        {
                            mkysRecords.Add(mkys[k]);
                            k = mkys.Count;
                        }
                    }
                }

                if (docNumbers.Length != mkysRecords.Count)
                {
                    continue;
                }

                decimal mkysTotalPrice = 0;
                foreach (ActionRecord record in mkysRecords)
                {
                    mkysTotalPrice += record.Price;
                }

                if (Math.Abs(tdms[i].Price - mkysTotalPrice) < 0.1m)
                {
                    for(int j = 0;j < mkysRecords.Count; j++)
                    {
                        if (j ==0)
                        {
                            matches.Add(
                          new Match
                          {
                              Id = mkysRecords[j].Id,
                              MkysRecord = mkysRecords[j],
                              TdmsRecord = tdms[i],
                              IsSafeMatch = true

                          });
                        }
                        else
                        {
                            matches.Add(
                          new Match
                          {
                              Id = mkysRecords[j].Id,
                              MkysRecord = mkysRecords[j],
                              TdmsRecord = tdms[i],
                              IsSafeMatch = true
                          });
                        }
                       
                        mkys.Remove(mkysRecords[j]);
                    }
                    

                    tdms.RemoveAt(i);
                    i = -1;
                }

            }
            return mkys;
        }

        public List<ActionRecord> CompareForHospitalName(List<ActionRecord> mkys, List<ActionRecord> tdms)
        {
            //hastane adı karşılaştırması
            for (int i = 0; i < mkys.Count; i++)
            {
                for (int j = 0; j < tdms.Count; j++)
                {
                    if (Math.Abs(mkys[i].Price - tdms[j].Price) <= 0.03m && sameHospital(mkys[i], tdms[j]))
                    {
                        matches.Add(
                            new Match
                            {
                                Id = mkys[i].Id,
                                MkysRecord = mkys[i],
                                TdmsRecord = tdms[j],
                                IsSafeMatch = false
                            });

                        mkys.RemoveAt(i);
                        tdms.RemoveAt(j);

                        j = tdms.Count;
                        i = -1;
                    }
                }
            }
            return mkys;
        }

        public List<ActionRecord> CompareSensitivePrice(List<ActionRecord> mkys, List<ActionRecord> tdms)
        {
            //hassas tutar karşılaştırması
            for (int i = 0; i < mkys.Count; i++)
            {
                for (int j = 0; j < tdms.Count; j++)
                {
                    if (Math.Abs(mkys[i].Price - tdms[j].Price) <= 0.01m)
                    {
                        matches.Add(
                            new Match
                            {
                                Id = mkys[i].Id,
                                MkysRecord = mkys[i],
                                TdmsRecord = tdms[j],
                                IsSafeMatch = false
                            });

                        mkys.RemoveAt(i);
                        tdms.RemoveAt(j);

                        j = tdms.Count;
                        i = -1;
                    }
                }
            }
            return mkys;
        }

        public List<ActionRecord> CompareNotSensitivePrice(List<ActionRecord> mkys, List<ActionRecord> tdms)
        {
            //normal tutar karşılaştırması
            for (int i = 0; i < mkys.Count; i++)
            {
                for (int j = 0; j < tdms.Count; j++)
                {
                    if (Math.Abs(mkys[i].Price - tdms[j].Price) < 0.03m)
                    {
                        matches.Add(
                            new Match
                            {
                                Id = mkys[i].Id,
                                MkysRecord = mkys[i],
                                TdmsRecord = tdms[j],
                                IsSafeMatch = false
                            });

                        mkys.RemoveAt(i);
                        tdms.RemoveAt(j);

                        j = tdms.Count;
                        i = -1;
                    }
                }
            }
            return mkys;
        }

        public List<ActionRecord> RemoveZeroAmounts(List<ActionRecord> entries, List<ActionRecord> exits)
        {
            for (int i = 0; i < entries.Count; i++)
            {
                if (entries[i].Price == 0)
                {
                    entries.RemoveAt(i);
                    i = -1;
                }
            }
            for (int i = 0; i < exits.Count; i++)
            {
                if (exits[i].Price == 0)
                {
                    exits.RemoveAt(i);
                    i = -1;
                }
            }


            return exits;
        }
        
        private bool sameHospital(ActionRecord mkysRecord, ActionRecord tdmsRecord)
        {
            
            var mkysHospital = "";
            var i = 0;
            foreach (var word in mkysRecord.Explanation.Split(' '))
            {
                if(word.ToLower() == "devlet")
                {
                    if (i == 0) continue;
                    mkysHospital = mkysRecord.Explanation.Split(' ')[i-1].ToLower();
                    
                }
                i++;
            }
            

            if (mkysHospital == "") 
            {
                return false;
            }
            else
            {
                return tdmsRecord.Explanation.ToLower().Contains(mkysHospital);
            }
        }
    }
}
