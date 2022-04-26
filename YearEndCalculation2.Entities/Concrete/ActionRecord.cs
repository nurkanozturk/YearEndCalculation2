using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YearEndCalculation.Entities.Concrete
{
    public class ActionRecord
    {
        public string Id { get; set; }
        public string DocNumber { get; set; }
        public string DocDate { get; set; }
        public string Type { get; set; }
        public string Explanation { get; set; }
        public decimal Price { get; set; }
    }
}
