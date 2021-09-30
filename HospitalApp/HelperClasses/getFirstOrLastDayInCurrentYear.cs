using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApp.HelperClasses
{
    public static class getFirstOrLastDayInCurrentYear
    {
        // get first and last day in current year
        public static DateTime getFirstDayInCurrentYear(this DateTime? startDay)
        {
            int currentYear = DateTime.Now.Year;
            DateTime firstDayInCurrentYear = new DateTime(currentYear, 01, 01, 0, 0, 0, 0);

            return startDay ?? firstDayInCurrentYear;
        }

        public static DateTime getLastDayInCurrentYear(this DateTime? endDay)
        {
            int currentYear = DateTime.Now.Year;
            DateTime lastDayInCurrentYear = new DateTime(currentYear, 12, 31, 23, 59, 59, 999);

            return endDay ?? lastDayInCurrentYear;
        }

        public static DateTime getEndTimeOFDay(this DateTime endTime)
        {
            
            DateTime endOfDay = new DateTime(endTime.Year, endTime.Month, endTime.Day, 23, 59, 59, 999);

            return endOfDay;
        }

    }
}
