using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YearEndCalculation.Entities.Abstract;
using YearEndCalculation2.Business.Abstract;
using YearEndCalculation2.Entities.Concrete;

namespace YearEndCalculation2.Business.Concrete.Managers
{
    public class YearEndManager : IYearEndService
    {
        public List<MkysEntry> CompareEntries(List<MkysEntry> mkysEntries, List<TdmsEntry> tdmsEntries)
        {
            for (int i = 0; i < mkysEntries.Count; i++)
            {
                for (int j = 0; j < tdmsEntries.Count; j++)
                {
                    if (Math.Abs(mkysEntries[i].Amount - tdmsEntries[j].Amount) < 0.1m && mkysEntries[i].ReceiptNumber == tdmsEntries[j].ReceiptNumber)
                    {
                        mkysEntries.RemoveAt(i);
                        tdmsEntries.RemoveAt(j);

                        j = tdmsEntries.Count;
                        i = -1;
                    }
                }
            }
            for (int i = 0; i < mkysEntries.Count; i++)
            {
                for (int j = 0; j < tdmsEntries.Count; j++)
                {
                    if (Math.Abs(mkysEntries[i].Amount - tdmsEntries[j].Amount) < 0.1m)
                    {
                        mkysEntries.RemoveAt(i);
                        tdmsEntries.RemoveAt(j);

                        j = tdmsEntries.Count;
                        i = -1;
                    }
                }
            }
            return mkysEntries;
        }

        public List<MkysExit> CompareExits(List<MkysExit> mkysExits, List<TdmsExit> tdmsExits)
        {
            for (int i = 0; i < mkysExits.Count; i++)
            {
                for (int j = 0; j < tdmsExits.Count; j++)
                {
                    if (Math.Abs(mkysExits[i].Amount - tdmsExits[j].Amount) < 0.1m && mkysExits[i].ReceiptNumber == tdmsExits[j].ReceiptNumber)
                    {
                        mkysExits.RemoveAt(i);
                        tdmsExits.RemoveAt(j);
                        j = tdmsExits.Count;
                        i = -1;
                    }
                }
            }
            for (int i = 0; i < mkysExits.Count; i++)
            {
                for (int j = 0; j < tdmsExits.Count; j++)
                {
                    if (Math.Abs(mkysExits[i].Amount - tdmsExits[j].Amount) < 0.1m)
                    {
                        mkysExits.RemoveAt(i);
                        tdmsExits.RemoveAt(j);

                        j = tdmsExits.Count;
                        i = -1;
                    }
                }
            }

            return mkysExits;
        }

        public List<MkysExit> CompareMkys(List<MkysEntry> mkysEntries, List<MkysExit> mkysExits)
        {
            for (int i = 0; i < mkysEntries.Count; i++)
            {
                for (int j = 0; j < mkysExits.Count; j++)
                {
                    if (mkysExits[j].Amount == mkysEntries[i].Amount && mkysExits[j].TypeOfExit.Contains(mkysEntries[i].TypeOfSupply))
                    {
                        mkysEntries.RemoveAt(i);
                        mkysExits.RemoveAt(j);
                        j = mkysExits.Count;
                        i = -1;
                    }
                }
            }

            RemoveZeroAmounts(mkysEntries);
            RemoveZeroAmounts(mkysExits);

            return mkysExits;
        }

        private static void RemoveZeroAmounts<T>(List<T> list) where T: IEntity
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Amount == 0)
                {
                    list.RemoveAt(i);
                    i = -1;
                }
            }
        }

        public List<TdmsExit> CompareTdms(List<TdmsEntry> tdmsEntries, List<TdmsExit> tdmsExits)
        {
            for (int i = 0; i < tdmsEntries.Count; i++)
            {
                for (int j = 0; j < tdmsExits.Count; j++)
                {
                    if (tdmsExits[j].Amount == tdmsEntries[i].Amount && tdmsExits[j].TypeOfProcess.Contains(tdmsEntries[i].TypeOfProcess))
                    {
                        tdmsEntries.RemoveAt(i);
                        tdmsExits.RemoveAt(j);
                        j = tdmsExits.Count;
                        i = -1;
                    }
                }
            }

            return tdmsExits;
        }

    }
}
