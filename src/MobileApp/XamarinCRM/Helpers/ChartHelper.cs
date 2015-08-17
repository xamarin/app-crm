using System;
using System.Collections.Generic;
using System.Linq;
using XamarinCRM.Models;
using XamarinCRM.Clients;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace XamarinCRM.Helpers
{
    public class ChartHelper
    {
        List<WeeklySalesData> _SalesData;
        List<CategorySalesData> _CategoryData;
        readonly IEnumerable<Order> _Orders;
        bool _BolIsOpen;

        ICatalogDataClient _CatalogClient;

        public ChartHelper(IEnumerable<Order> Orders, bool IsOpen)
        {
            _CatalogClient = DependencyService.Get<ICatalogDataClient>();

            _SalesData = new List<WeeklySalesData>();
            _CategoryData = new List<CategorySalesData>();

            _Orders = Orders;
            _BolIsOpen = IsOpen;
        }

        public async Task ProcessData()
        {
            this.ProcessDates();

            await this.ProcessCategories();
        }

        public List<WeeklySalesData> SalesData
        {
            get { return _SalesData; }
        }

        public List<CategorySalesData> CategoryData
        {
            get { return _CategoryData; }
        }

        async Task ProcessCategories()
        {
            var categories = await _CatalogClient.GetCategoriesAsync();

            foreach (string s in categories.Select(x => x.Name))
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
            }
        }

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
    }

    public class CategorySalesData
    {
        public string Category { get; set; }

        public double Amount { get; set; }
    }

    public class WeeklySalesData
    {
        public WeeklySalesData()
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