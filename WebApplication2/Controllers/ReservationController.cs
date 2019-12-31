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
        public static String datatime = "10/10:8h00-10h30; 20/10:8h40-10-55";
        public static DateTime datetime = new DateTime(2019, 12, 31, 10, 5, 30);
        // GET: Reservation
        public void CheckDayOfWeek(DateTime dateinput)
        {
            var DayOfWeek = datetime.ToString("dddd");
            string[] dayseperate = dataweek.Split(':', ';');
            if (checkDay(dayseperate, DayOfWeek, dateinput))
                System.Diagnostics.Trace.WriteLine("Can do!");
            else
                System.Diagnostics.Trace.WriteLine("Can not do!");
        }

        public Boolean checkDay(string[] dayseperate, string day, DateTime dateinput)
        {
            int arrindex = Array.IndexOf(dayseperate, day);
            if (arrindex != -1)
            {
                string hour = dayseperate[arrindex + 1];
                string[] hourseperate = hour.Split('h', '-', '/');
                List<int> number = new List<int>();
                foreach (string x in hourseperate)
                    number.Add(Convert.ToInt32(x));
                return checkTime(number, dateinput.TimeOfDay);
            }
            return false;
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

        public void CheckDayOfMonth(DateTime input)
        {
            var DayOfYear = input.Date;
            string[] dayseperate = datayear.Split(';');
            // string exampleDate = "31/12";
            foreach (var day in dayseperate)
            {
                DateTime date = DateTime.ParseExact(day, "dd/MM", null);
                if (DateTime.Compare(date, DayOfYear) == 0)
                {
                    Console.WriteLine("Cant do!");
                }
            }
        }

        public void CheckTimeOfDay(DateTime dateinput)
        {
            var DayOfYear = datetime.Date.ToString("dd/MM");
            string[] dayseperate = datatime.Split(';',':');
            if (checkDay(dayseperate, DayOfYear, dateinput))
                System.Diagnostics.Trace.WriteLine("Can do!");
            else
                System.Diagnostics.Trace.WriteLine("Can not do!");
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}