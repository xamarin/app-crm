using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinCRM.Clients;
using XamarinCRM.Models;
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
            // get top-level categories
            var categories = await _CatalogClient.GetCategoriesAsync();

            List<CategorySalesDataPoint> categorySalesDataPoints = new List<CategorySalesDataPoint>();

            foreach (var category in categories)
            {
                double dblAmt = await GetOrderTotalForCategory(orders, category, isOpen);
                categorySalesDataPoints.Add(new CategorySalesDataPoint() { Category = category.Name, Amount = dblAmt });
            }

            return categorySalesDataPoints;
        }

        #endregion

        async Task<double> GetOrderTotalForCategory(IEnumerable<Order> orders, CatalogCategory category, bool isOpen = false)
        {
            double dblTotal = 0;

            var categoryProducts = await _CatalogClient.GetAllChildProductsAsync(category.Id);

            var results = orders.Where(o => o.IsOpen == isOpen && categoryProducts.Any(x => x.Name.ToLower() == o.Item.ToLower()));

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

