using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class ReservationController : Controller
    {

        public static String dataweek = "Monday:8h10-8h30/2h20-2h10;TuesDay:15h30-20h10;Friday:20h30-23h30";
        public static String datayear = "9/10;20/11;1/1;2/9";
        public static DateTime datetime = new DateTime(2019, 12, 31, 10, 5, 30);
        // GET: Reservation
        public void CheckDayOfWeek()
        {
            var DayOfWeek = datetime.ToString("dddd");
            string[] dayseperate = dataweek.Split(':', ';');
            int arrindex = Array.IndexOf(dayseperate, DayOfWeek);
            if (arrindex != -1)
            {
                string hour = dayseperate[arrindex + 1];
                string[] hourseperate = hour.Split('h', '-', '/');
                List<int> number = new List<int>();
                foreach(string x in hourseperate)
                {
                    number.Add(Convert.ToInt32(x));
                }
                if (checkTime(number, datetime.TimeOfDay))
                    Console.WriteLine("Time ok!");
                else
                    Console.WriteLine("TIme not ok!");
            }
        }

        public Boolean checkTime(List<int> list, TimeSpan timecheck)
        {
            Boolean result = true;
            for(int i = 0; i < list.Count; i = i + 4)
            {
                var timestart = new TimeSpan(list[i], list[i+1], 0);
                var timefinish = new TimeSpan(list[i+2], list[i+3], 0);
                if (TimeSpan.Compare(timestart, timecheck) != 1 && TimeSpan.Compare(timecheck, timefinish) != 1)
                    result = false;
            }
            return result;
        }

        public void CheckDayOfYear()
        {
            var DayOfYear = datetime.Date;
            string exampleDate = "9/10";
            DateTime oDate = DateTime.ParseExact(exampleDate, "dd/MM"
        }
    }
}