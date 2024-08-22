using System.Collections.Generic;
using YearEndCalculation.Entities.Concrete;

namespace YearEndCalculation2.Business.Abstract
{
    public interface IYearEndService
    {

        List<ActionRecord> CompareInvoiceNumber(List<ActionRecord> mkysEntries, List<ActionRecord> tdmsEntries);
        List<ActionRecord> CompareDocNumber(List<ActionRecord> mkysEntries, List<ActionRecord> tdmsEntries);

        List<ActionRecord> CompareMassRecords(List<ActionRecord> mkysEntries, List<ActionRecord> tdmsEntries);

        List<ActionRecord> CompareForHospitalName(List<ActionRecord> mkysEntries, List<ActionRecord> tdmsEntries);

        List<ActionRecord> CompareSensitivePrice(List<ActionRecord> mkysEntries, List<ActionRecord> tdmsEntries);

        List<ActionRecord> CompareNotSensitivePrice(List<ActionRecord> mkysEntries, List<ActionRecord> tdmsEntries);


        List<ActionRecord> RemoveZeroAmounts(List<ActionRecord> mkysEntries, List<ActionRecord> mkysExits);
    }
}
