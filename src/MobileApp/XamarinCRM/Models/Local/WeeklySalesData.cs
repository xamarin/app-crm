
using System;

namespace XamarinCRM.Models.Local
{
    public class WeeklySalesDataPoint
    {
        public WeeklySalesDataPoint()
        {
            DateStart = DateTime.MinValue;
            DateEnd = DateTime.MaxValue;
            Amount = 0;
        }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public double Amount { get; set; }

        public string DateStartString
        {
            get { return DateStart.ToString("M/dd"); }
        }

        public string DateEndString
        {
            get { return DateEnd.ToString("M/dd"); }
        }
    }
}

