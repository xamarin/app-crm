
using System.Collections.Generic;
using System.Threading.Tasks;
using Syncfusion.SfChart.XForms;
using XamarinCRM.Models;
using XamarinCRM.Models.Local;
using System.Linq;

namespace XamarinCRM.Services
{
    public interface IChartDataService
    {
        Task<IEnumerable<WeeklySalesDataPoint>> GetWeeklySalesDataPointsAsync(IEnumerable<Order> orders, int numberOfWeeks = 6, OrderStatusOption statusOption = OrderStatusOption.Both);

        Task<IEnumerable<IGrouping<string, CategorySalesDataPoint>>> GetCategorySalesDataPointsAsync(IEnumerable<Order> orders, int numberOfWeeks = 6, OrderStatusOption statusOption = OrderStatusOption.Both);
    }
}

