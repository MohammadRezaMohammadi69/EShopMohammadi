using System.Globalization;

namespace Utilities
{
    public class PersianDateTime
    {
        public static string GetDate(DateTime NOW)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            return persianCalendar.GetYear(NOW) + "/" + persianCalendar.GetMonth(NOW) + "/" + persianCalendar.GetDayOfMonth(NOW);
        }

        public static string GetTime(DateTime NOW)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            return persianCalendar.GetHour(NOW) + ":" + persianCalendar.GetMinute(NOW) + ":" + persianCalendar.GetSecond(NOW);
        }

        public static string GetDate()
        {
            return GetDate(DateTime.Now);
        }

        public static string GetDateTime()
        {
            new PersianCalendar();
            DateTime now = DateTime.Now;
            return GetDate(now) + " " + GetTime(now);
        }

        public static string GetDateTime(DateTime? NOW)
        {
            return GetDate(NOW.Value);
        }

        public static DateTime ToGregorian(string inputDateTime, bool exDate)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            string[] array = inputDateTime.Split('/');
            if (array[1].Contains("فروردین"))
            {
                array[1] = "1";
            }
            else if (array[1].Contains("اردیبهشت"))
            {
                array[1] = "2";
            }
            else if (array[1].Contains("خرداد"))
            {
                array[1] = "3";
            }
            else if (array[1].Contains("تیر"))
            {
                array[1] = "4";
            }
            else if (array[1].Contains("مرداد"))
            {
                array[1] = "5";
            }
            else if (array[1].Contains("شهریور"))
            {
                array[1] = "6";
            }
            else if (array[1].Contains("مهر"))
            {
                array[1] = "7";
            }
            else if (array[1].Contains("آبان"))
            {
                array[1] = "8";
            }
            else if (array[1].Contains("آذر"))
            {
                array[1] = "9";
            }
            else if (array[1].Contains("دی"))
            {
                array[1] = "10";
            }
            else if (array[1].Contains("بهمن"))
            {
                array[1] = "11";
            }
            else if (array[1].Contains("اسفند"))
            {
                array[1] = "12";
            }

            if (exDate)
            {
                return persianCalendar.ToDateTime(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]), 0, 0, 0, 0);
            }

            return persianCalendar.ToDateTime(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]), 0, 0, 0, 0);
        }
    }
}
