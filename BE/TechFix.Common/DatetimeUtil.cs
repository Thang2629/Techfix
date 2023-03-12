using System;

namespace TechFix.Common
{
	public static class DatetimeUtil
	{
		public static DateTime StartOfWeek(DateTime dt, DayOfWeek startOfWeek)
		{
			var diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
			return dt.AddDays(-1 * diff).Date;
		}
		
		public static DateTime StartQuarter(DateTime dt)
		{
            var quarterNumber = (dt.Month - 1) / 3 + 1;
            var firstDayOfQuarter = new DateTime(dt.Year, (quarterNumber - 1) * 3 + 1, 1);
            return firstDayOfQuarter;
		}

        public static DateTime EndQuarter(DateTime dt)
        {
            var startQuarter = StartQuarter(dt);
            var lastDayOfQuarter = startQuarter.AddMonths(3).AddTicks(-1);
            return lastDayOfQuarter;
        }


    }
}
