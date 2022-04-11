using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YearEndCalculation.Entities.Abstract;

namespace YearEndCalculation2.Entities.Concrete
{
    public class MkysEntry:IEntity
    {
        public int Id { get; set; }
        public string ReceiptNumber { get; set; }
        public string ReceiptDate { get; set; }
        public string TypeOfSupply { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public decimal Amount { get; set; }
    }
}
