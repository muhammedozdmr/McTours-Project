using McTours.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McTours
{
    public static class DateHelper
    {
        public static IEnumerable<ValueNameObject> GetAvailableYears()
        {
            var currentYear = DateTime.Today.Year;
            const int beginningYear = 1950;
            var availableYears = new List<ValueNameObject>();

            for (int year = beginningYear; year <= currentYear; year++)
            {
                availableYears.Add(new ValueNameObject()
                {
                    Value = year,
                    Name = year.ToString()
                });
            }

            //var availableYears = new List<int>()
            //{
            //    currentYear - 5,
            //    currentYear - 4,
            //    currentYear - 3,
            //    currentYear - 2,
            //    currentYear - 1,
            //    currentYear,
            //    currentYear + 1,
            //};
            return availableYears;
        }
        public static IEnumerable<ValueNameObject> GetAvailableMonth()
        {
            var currentMounth = 12;
            const int beginningMounth = 1;
            var availableMounth = new List<ValueNameObject>();
            for (int mounth = beginningMounth; mounth < currentMounth; mounth++)
            {
                availableMounth.Add(new ValueNameObject()
                {
                    Value= mounth,
                    Name = mounth.ToString()
                });
            }
            return availableMounth;
        }
        public static IEnumerable<ValueNameObject> GetAvailableDay()
        {
            var currentDay = 31;
            const int beginningDay = 1;
            var availableDay = new List<ValueNameObject>();
            for (int day = 0; day < currentDay; day++)
            {
                availableDay.Add(new ValueNameObject()
                {
                    Value = day,
                    Name = day.ToString()
                });
            }
            return availableDay;
        }
    }

}
