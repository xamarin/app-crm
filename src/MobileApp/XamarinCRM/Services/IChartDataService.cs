using XamarinCRM.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XamarinCRM.Services
{
    public interface IChartDataService
    {
        Task<List<WeeklySalesDataPoint>> GetWeeklySalesDataPoints(IEnumerable<Order> orders, int numberOfWeeks = 6, bool isOpen = false);

        Task<List<CategorySalesDataPoint>> GetCategorySalesDataPoints(IEnumerable<Order> orders, bool isOpen = false);
    }
}

