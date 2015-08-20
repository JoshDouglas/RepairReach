using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairReach.Data.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// First tick of the day.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static DateTime DayMin(this DateTime date)
        {
            return date.Date;   // minimum of this day
        }


        /// <summary>
        /// Last tick of the day.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static DateTime DayMax(this DateTime date)
        {
            return date.Date.AddDays(1).AddTicks(-1);   // last tick of this day
        }

        /// <summary>
        /// Return the date that is the start of the month relative to the specified date.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetStartOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        /// <summary>
        /// Return the date that is the end of the month relative to the specified date.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetEndOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.GetDaysInMonth(), 23, 59, 59, 999);
        }

        /// <summary>
        /// Returns the number of days in the month of the specified date.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int GetDaysInMonth(this DateTime date)
        {
            return DateTime.DaysInMonth(date.Year, date.Month);
        }
    }
}
