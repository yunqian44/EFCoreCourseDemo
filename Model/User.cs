using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public BirthDate BirthDate { get; set; }
    }

    public class BirthDate 
    {
        public BirthDate(int year,int month,int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        /// <summary>
        /// 年份
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// 月份
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// 日
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// 下一次生日的天数
        /// </summary>
        public int DaysOfNextBirthday()
        {
            var today = DateTime.Today;

            var next = new DateTime(today.Year, Month, Day);

            if (next < today)
                next = next.AddYears(1);
            var days = (next - today).Days;

            return days;
        }
    }
}
