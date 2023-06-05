using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace app
{
    internal class ReadyDateAndTime
    {
        public string GetDate()
        {
            DateTime dt = DateTime.Now;
            string date;
            int day = dt.Day;
            int month = dt.Month;
            int year = dt.Year;

            if (day < 10 && month < 10)
            {
                date = "0" + day.ToString() + "/0" + month.ToString() + "/" + year.ToString();
            }
            else if (day < 10 && !(month < 10))
            {
                date = "0" + day.ToString() + month.ToString() + "/" + year.ToString();
            }
            else if (!(day < 10) && month < 10)
            {
                date = day.ToString() + "/0" + month.ToString() + "/" + year.ToString();
            }
            else
            {
                date = day.ToString() + "/" + month.ToString() + "/" + year.ToString();
            }
            return date;
        }
        public string GetTime()
        {
            DateTime dt = DateTime.Now;
            string time;
            int hour = dt.Hour;
            int minute = dt.Minute;
            if (hour < 10 && minute < 10)
            {
                time = "0" + hour.ToString() + ":0" + minute.ToString();
            }
            else if (hour < 10 && !(minute < 10))
            {
                time = "0" + hour.ToString() + ":" + minute.ToString();
            }
            else if (!(hour < 10) && minute < 10)
            {
                time = hour.ToString() + ":0" + minute.ToString();
            }
            else
            {
                time = hour.ToString() + ":" + minute.ToString();
            }
            return time;
        }
    }
}
