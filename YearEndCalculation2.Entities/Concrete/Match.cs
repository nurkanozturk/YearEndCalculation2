using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YearEndCalculation.Entities.Concrete
{
    public class Match
    {
        public string Id { get; set; }
        public ActionRecord MkysRecord { get; set; }
        public ActionRecord TdmsRecord { get; set; }
        public bool IsInvoiceNumberMatch { get; set; }
    }
}
