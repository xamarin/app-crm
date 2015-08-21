using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinCRM.Clients;
using XamarinCRM.Models;
using XamarinCRM.Services;
using Syncfusion.SfChart.XForms;

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

        public async Task<List<WeeklySalesDataPoint>> GetWeeklySalesDataPointsAsync(IEnumerable<Order> orders, int numberOfWeeks = 6, bool isOpen = false)
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

        public async Task<List<ChartDataPoint>> GetCategorySalesDataPointsAsync(IEnumerable<Order> orders, Account account = null, int numberOfWeeks = 6, bool isOpen = false)
        {
            // get top-level categories by passing no parent categoryId
            var categories = await _CatalogClient.GetCategoriesAsync();

            List<ChartDataPoint> categorySalesDataPoints = new List<ChartDataPoint>();

            orders = (account == null) ? orders : orders.Where(order => order.AccountId == account.Id);

            foreach (var category in categories)
            {
                double amount = await GetOrderTotalForCategoryAsync(orders, category, numberOfWeeks, isOpen);
                categorySalesDataPoints.Add(new ChartDataPoint(category.Name, amount));
            }

            return categorySalesDataPoints;
        }

        #endregion

        async Task<double> GetOrderTotalForCategoryAsync(IEnumerable<Order> orders, CatalogCategory category, int numberOfWeeks = 6, bool isOpen = false)
        {
            double total = 0;

            var categoryProducts = await _CatalogClient.GetAllChildProductsAsync(category.Id);

            DateTime dateEnd = DateTime.Today;
            DateTime dateStart = dateEnd.AddDays(-numberOfWeeks * 7);

            if (isOpen)
            {
                orders = orders.Where(
                    order => order.IsOpen == isOpen &&
                    order.OrderDate >= dateStart &&
                    order.OrderDate <= dateEnd &&
                    categoryProducts.Any(product => product.Name.ToLower() == order.Item.ToLower()));
            }
            else
            {
                orders = orders.Where(
                    order => order.IsOpen == isOpen &&
                    order.ClosedDate >= dateStart &&
                    order.ClosedDate <= dateEnd &&
                    categoryProducts.Any(product => product.Name.ToLower() == order.Item.ToLower()));
            }
                
            foreach (var order in orders)
            {
                total += order.Price;
            }

            return total;
        }

        static double GetOrderTotalForPeriod(IEnumerable<Order> orders, DateTime dateStart, DateTime dateEnd, bool isOpen = false)
        {
            double total = 0;

            if (isOpen)
            {
                orders = orders.Where(
                    order => order.IsOpen == isOpen &&
                    order.OrderDate >= dateStart &&
                    order.OrderDate <= dateEnd);
            }
            else
            {
                orders = orders.Where(
                    order => order.IsOpen == isOpen &&
                    order.ClosedDate >= dateStart &&
                    order.ClosedDate <= dateEnd);
            }

            foreach (var order in orders)
            {
                total += order.Price;
            }

            return total;
        }
    }
}

