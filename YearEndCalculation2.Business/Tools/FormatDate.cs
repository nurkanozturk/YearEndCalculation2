using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YearEndCalculation.Business.Tools
{
    public class FormatDate
    {
        public static DateTime Format(String dateText)
        {
            DateTime date = new DateTime(1800,1,1);
            try
            {
                Dictionary<string, int> months = new Dictionary<string, int>
            {
                {"OCA",1 },{"ŞUB",2 },{"MAR",3 },{"NIS", 4},{ "MAY",5 },{ "HAZ",6 },{ "TEM",7  },{"AĞU",8 },{ "EYL",9  },{"EKI",10  },{"KAS",11  },{ "ARA",12 }
            };
                string monthText = dateText.Substring(3, 3);


                int day = Convert.ToInt32(dateText.Substring(0, 2));
                int month = Convert.ToInt32(months[monthText]);
                int year = Convert.ToInt32(dateText.Substring(7, 4));


                date = new DateTime(year, month, day);
            }
            catch{ }
           
                return date;
            
            



        }
    }
}
