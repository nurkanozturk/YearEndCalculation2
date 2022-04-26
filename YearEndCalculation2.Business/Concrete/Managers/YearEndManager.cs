using System;
using System.Collections.Generic;
using YearEndCalculation.Entities.Concrete;
using YearEndCalculation2.Business.Abstract;

namespace YearEndCalculation2.Business.Concrete.Managers
{
    public class YearEndManager : IYearEndService
    {
        public List<ActionRecord> CompareMkysTdms(List<ActionRecord> mkys, List<ActionRecord> tdms)
        {
            for (int i = 0; i < mkys.Count; i++)
            {
                for (int j = 0; j < tdms.Count; j++)
                {
                    if (Math.Abs(mkys[i].Price - tdms[j].Price) < 0.1m && mkys[i].DocNumber == tdms[j].DocNumber)
                    {
                        mkys.RemoveAt(i);
                        tdms.RemoveAt(j);

                        j = tdms.Count;
                        i = -1;
                    }
                }
            }
            for (int i = 0; i < mkys.Count; i++)
            {
                for (int j = 0; j < tdms.Count; j++)
                {
                    if (Math.Abs(mkys[i].Price - tdms[j].Price) < 0.1m)
                    {
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


    }
}
