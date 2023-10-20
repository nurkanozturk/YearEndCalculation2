using System;
using System.Collections.Generic;
using YearEndCalculation.Entities.Concrete;
using YearEndCalculation2.Business.Abstract;

namespace YearEndCalculation2.Business.Concrete.Managers
{
    public class YearEndManager : IYearEndService
    {        
        public static List<Match> matches = new List<Match>();

        public List<ActionRecord> CompareMkysTdms(List<ActionRecord> mkys, List<ActionRecord> tdms)
        {
            //fatura numarası karşılaştırması
            for (int i = 0; i < mkys.Count; i++)
            {
                for (int j = 0; j < tdms.Count; j++)
                {
                    if (mkys[i].InvoiceNumber !="" && mkys[i].InvoiceNumber == tdms[j].InvoiceNumber && Math.Abs(mkys[i].Price - tdms[j].Price) < 0.1m )
                    {
                        matches.Add(
                            new Match { 
                                Id = mkys[i].Id, 
                                MkysRecord = mkys[i], 
                                TdmsRecord = tdms[j], 
                                IsInvoiceNumberMatch = true 
                            }); 

                        mkys.RemoveAt(i);
                        tdms.RemoveAt(j);

                        j = tdms.Count;
                        i = -1;
                    }
                }
            }

            //hastane adı karşılaştırması
            for (int i = 0; i < mkys.Count; i++)
            {
                for (int j = 0; j < tdms.Count; j++)
                {
                    if (Math.Abs(mkys[i].Price - tdms[j].Price) <= 0.1m && sameHospital(mkys[i], tdms[j]))
                    {
                        matches.Add(
                            new Match
                            {
                                Id = mkys[i].Id,
                                MkysRecord = mkys[i],
                                TdmsRecord = tdms[j],
                                IsInvoiceNumberMatch = true
                            });

                        mkys.RemoveAt(i);
                        tdms.RemoveAt(j);

                        j = tdms.Count;
                        i = -1;
                    }
                }
            }

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
                                IsInvoiceNumberMatch = false
                            });

                        mkys.RemoveAt(i);
                        tdms.RemoveAt(j);

                        j = tdms.Count;
                        i = -1;
                    }
                }
            }

            //normal tutar karşılaştırması
            for (int i = 0; i < mkys.Count; i++)
            {
                for (int j = 0; j < tdms.Count; j++)
                {
                    if (Math.Abs(mkys[i].Price - tdms[j].Price) < 0.1m)
                    {
                        matches.Add(
                            new Match
                            {
                                Id = mkys[i].Id,
                                MkysRecord = mkys[i],
                                TdmsRecord = tdms[j],
                                IsInvoiceNumberMatch = false
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

        

        public List<ActionRecord> CompareInSelf(List<ActionRecord> entries, List<ActionRecord> exits)
        {
            
            for (int i = 0; i < entries.Count; i++)
            {
                for (int j = 0; j < exits.Count; j++)
                {
                    if (exits[j].Price == entries[i].Price && exits[j].Type.Contains(entries[i].Type))
                    {
                       
                        entries.RemoveAt(i);
                        exits.RemoveAt(j);

                        j = exits.Count;
                        i = -1;
                    }
                }
            }

            RemoveZeroAmounts(entries);
            RemoveZeroAmounts(exits);

            return exits;
        }
        public List<ActionRecord> CompareCorrections(List<ActionRecord> entries, List<ActionRecord> exits)
        {

            for (int i = 0; i < entries.Count; i++)
            {
                for (int j = 0; j < exits.Count; j++)
                {
                    // girişin tipi düzeltme diye yazıyor mu diye bakmak lazım
                    if (exits[j].Price == entries[i].Price && exits[j].Type.ToLower()=="düzeltme çıkış")
                    {
                        

                        entries.RemoveAt(i);
                        exits.RemoveAt(j);

                        j = exits.Count;
                        i = -1;
                    }
                }
            }

            RemoveZeroAmounts(entries);
            RemoveZeroAmounts(exits);

            return exits;
        }

        private static void RemoveZeroAmounts(List<ActionRecord> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Price == 0)
                {
                    list.RemoveAt(i);
                    i = -1;
                }
            }
        }
        private bool sameHospital(ActionRecord mkysRecord, ActionRecord tdmsRecord)
        {
            
            var mkysHospital = "";
            var i = 0;
            foreach (var word in mkysRecord.Explanation.Split(' '))
            {
                if(word.ToLower() == "devlet")
                {
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
