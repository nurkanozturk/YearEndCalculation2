using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YearEndCalculation2.Entities.Concrete;

namespace YearEndCalculation2.Business.Abstract
{
    public interface IYearEndService
    {

        List<MkysEntry> CompareEntries(List<MkysEntry> mkysEntries, List<TdmsEntry> tdmsEntries);
        List<MkysExit> CompareExits(List<MkysExit> mkysExits, List<TdmsExit> tdmsExits);
        List<MkysExit> CompareMkys(List<MkysEntry> mkysEntries, List<MkysExit> mkysExits);
        List<TdmsExit> CompareTdms(List<TdmsEntry> tdmsEntries, List<TdmsExit> tdmsExits);
    }
}
