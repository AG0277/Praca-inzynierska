using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infastructure.Utils
{
    public static class Dates
    {
        public static DateTime NormalizeDate(DateTime date)
        {
            date= date.ToUniversalTime();
            return  new DateTime(date.Year, date.Month, date.Day);
        }
    }
}
