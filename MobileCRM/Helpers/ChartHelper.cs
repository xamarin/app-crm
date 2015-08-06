using System;
using System.Collections.Generic;
using System.Linq;
using MobileCRM.Models;

namespace MobileCRM.Helpers
{
    public class ChartHelper
    {
        List<WeeklySalesData> _SalesData;
        List<CategorySalesData> _CategoryData;
        IEnumerable<Order> _Orders;
        bool _BolIsOpen;

        public ChartHelper(IEnumerable<Order> Orders, bool IsOpen)
        {
            _SalesData = new List<WeeklySalesData>();
            _CategoryData = new List<CategorySalesData>();

            _Orders = Orders;
            _BolIsOpen = IsOpen;

            this.ProcessDates();
            this.ProcessCategories();
        }
        //end ctor

        public List<WeeklySalesData> SalesData
        {
            get
            {
                return _SalesData;
            }
        }

        public List<CategorySalesData> CategoryData
        {
            get
            {
                return _CategoryData;
            }
        }

        void ProcessCategories()
        {
            foreach (string s in Order.ItemTypes)
            {
                double dblAmt = this.ProcessCategoryOrders(s);
                _CategoryData.Add(new CategorySalesData() { Category = s, Amount = dblAmt });
            }
        }

        double ProcessCategoryOrders(string category)
        {
            double dblTotal = 0;

            var results = _Orders.Where(o => o.IsOpen == _BolIsOpen && o.Item == category);

            foreach (var order in results)
            {
                dblTotal = dblTotal + order.Price;
            }

            return dblTotal;
        }

        //Aggregate sales for prior 6 weeks
        void ProcessDates()
        {
            DateTime dateStart = DateTime.Today;

            DateTime dateWkStart = dateStart.Subtract(new TimeSpan(dateStart.DayOfWeek.GetHashCode(), 0, 0, 0));
            DateTime dateWkEnd = dateWkStart.AddDays(6);

            double dblAmt = this.ProcessWeekOrders(dateWkStart, dateWkEnd);
            _SalesData.Add(new WeeklySalesData() { DateStart = dateWkStart, DateEnd = dateWkEnd, Amount = dblAmt });

            for (int i = 1; i < 6; i++)
            {
                dateWkStart = dateWkStart.AddDays(-7);
                dateWkEnd = dateWkStart.AddDays(6);
                dblAmt = this.ProcessWeekOrders(dateWkStart, dateWkEnd);
                _SalesData.Add(new WeeklySalesData() { DateStart = dateWkStart, DateEnd = dateWkEnd, Amount = dblAmt });
            } //end for
        }
        //end Processdates

        double ProcessWeekOrders(DateTime dateStart, DateTime dateEnd)
        {
            double dblTotal = 0;

            var results = _Orders.Where(o => o.IsOpen == _BolIsOpen && o.ClosedDate >= dateStart && o.ClosedDate <= dateEnd);

            foreach (var order in results)
            {
                dblTotal = dblTotal + order.Price;
            }

            return dblTotal;
        }
        //end ProcessWeekOrders
    }
    //end class

    public class CategorySalesData
    {
        string category;

        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        double amount;

        public double Amount
        {
            get { return amount; }
            set { amount = value; }
        }
    }

    public class WeeklySalesData
    {
        public WeeklySalesData()
        {
            dateStart = DateTime.MinValue;
            dateEnd = DateTime.MaxValue;
            dblAmt = 0;
        }
        //end ctor

        DateTime dateStart;

        public DateTime DateStart
        {
            get { return dateStart; }
            set { dateStart = value; }
        }

        DateTime dateEnd;

        public DateTime DateEnd
        {
            get { return dateEnd; }
            set { dateEnd = value; }
        }

        double dblAmt;

        public double Amount
        {
            get { return dblAmt; }
            set { dblAmt = value; }
        }

        public string DateStartString
        {
            get
            {
                return dateStart.ToString("M/dd");
            }
        }

        public string DateEndString
        {
            get
            {
                return dateEnd.ToString("M/dd");
            }
        }
    }
}