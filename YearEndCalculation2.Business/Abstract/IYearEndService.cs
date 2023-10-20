using System.Collections.Generic;
using YearEndCalculation.Entities.Concrete;

namespace YearEndCalculation2.Business.Abstract
{
    public interface IYearEndService
    {

        List<ActionRecord> CompareMkysTdms(List<ActionRecord> mkysEntries, List<ActionRecord> tdmsEntries);
        List<ActionRecord> CompareInSelf(List<ActionRecord> mkysEntries, List<ActionRecord> mkysExits);
        List<ActionRecord> CompareCorrections(List<ActionRecord> mkysEntries, List<ActionRecord> tdmsEntries);
    }
}
