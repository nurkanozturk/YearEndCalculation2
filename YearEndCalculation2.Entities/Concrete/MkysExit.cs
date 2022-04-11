using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YearEndCalculation.Entities.Abstract;

namespace YearEndCalculation2.Entities.Concrete
{
    public class MkysExit:IEntity
    {
        public int Id { get; set; }
        public string ReceiptNumber { get; set; }
        public string ReceiptDate { get; set; }
        public string TypeOfExit { get; set; }
        public string ToWhere { get; set; }
        public decimal Amount { get; set; }
    }
}