using System;
using System.Collections.Generic;
using XamarinCRM.Models;
using XamarinCRM.Clients;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Linq;
using XamarinCRM.Services;

[assembly: Dependency(typeof(ChartDataService))]

namespace XamarinCRM.Services
{
    public class ChartDataService : IChartDataService
    {
        ICatalogDataClient _CatalogClient;

        public ChartDataService()
        {
            _CatalogClient = DependencyService.Get<ICatalogDataClient>();
        }

        #region IChartDataService implementation

        public async Task<List<WeeklySalesDataPoint>> GetWeeklySalesDataPoints(IEnumerable<Order> orders, int numberOfWeeks = 6, bool isOpen = false)
        {
            DateTime dateStart = DateTime.Today;

            DateTime dateWkStart = dateStart.Subtract(new TimeSpan(dateStart.DayOfWeek.GetHashCode(), 0, 0, 0));
            DateTime dateWkEnd = dateWkStart.AddDays(6);

            double dblAmt = GetOrderTotalForPeriod(orders, dateWkStart, dateWkEnd, isOpen);

            List<WeeklySalesDataPoint> weeklySalesDataPoints = new List<WeeklySalesDataPoint>();

            weeklySalesDataPoints.Add(new WeeklySalesDataPoint() { DateStart = dateWkStart, DateEnd = dateWkEnd, Amount = dblAmt });

            for (int i = 1; i < numberOfWeeks; i++)
            {
                dateWkStart = dateWkStart.AddDays(-7);
                dateWkEnd = dateWkStart.AddDays(6);
                dblAmt = GetOrderTotalForPeriod(orders, dateWkStart, dateWkEnd);
                weeklySalesDataPoints.Add(new WeeklySalesDataPoint() { DateStart = dateWkStart, DateEnd = dateWkEnd, Amount = dblAmt });
            }

            return weeklySalesDataPoints;
        }

        public async Task<List<CategorySalesDataPoint>> GetCategorySalesDataPoints(IEnumerable<Order> orders, bool isOpen = false)
        {
            var categories = await _CatalogClient.GetCategoriesAsync();

            List<CategorySalesDataPoint> categorySalesDataPoints = new List<CategorySalesDataPoint>();

            foreach (string category in categories.Select(x => x.Name))
            {
                double dblAmt = GetOrderTotalForCategory(orders, category, isOpen);
                categorySalesDataPoints.Add(new CategorySalesDataPoint() { Category = category, Amount = dblAmt });
            }

            return categorySalesDataPoints;
        }

        #endregion

        static double GetOrderTotalForCategory(IEnumerable<Order> orders, string category, bool isOpen = false)
        {
            double dblTotal = 0;

            var results = orders.Where(o => o.IsOpen == isOpen && o.Item == category);

            foreach (var order in results)
            {
                dblTotal = dblTotal + order.Price;
            }

            return dblTotal;
        }

        static double GetOrderTotalForPeriod(IEnumerable<Order> orders, DateTime dateStart, DateTime dateEnd, bool isOpen = false)
        {
            double dblTotal = 0;

            var results = orders.Where(o => o.IsOpen == isOpen && o.ClosedDate >= dateStart && o.ClosedDate <= dateEnd);

            foreach (var order in results)
            {
                dblTotal = dblTotal + order.Price;
            }

            return dblTotal;
        }
    }
}

