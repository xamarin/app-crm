using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using XamarinCRMAppService.Models;

namespace XamarinCRMAppService.Controllers
{
    [MobileAppController]
    public class DataRefreshController : ApiController
    {
        private readonly MobileServiceContext _MobileServiceContext;

        public DataRefreshController()
        {
            _MobileServiceContext = new MobileServiceContext();
        }

        // GET api/DataRefresh
        public async Task<bool> Get()
        {
            // get all the orders
            var orders = _MobileServiceContext.Orders.ToList();

            if (orders.Count < 1)
                throw new Exception("There doesn't seem to be any orders currently in the database.");
            
            var lastUpdated = orders.First(x => x.UpdatedAt != null).UpdatedAt;

            if (lastUpdated != null)
            {
                var last = lastUpdated.Value.ToUniversalTime();

                DateTime now = DateTime.UtcNow;

                var today = DateTime.SpecifyKind(new DateTime(now.Year, now.Month, now.Day, 0, 0, 0), DateTimeKind.Utc);

                var lastUpdatedDay = DateTime.SpecifyKind(new DateTime(last.Year, last.Month, last.Day, 0, 0, 0), DateTimeKind.Utc);

                var daysElapsed = (int) (today - lastUpdatedDay).TotalDays;

                var weeksElapsed = (daysElapsed / 7);

                var daysToAdjust = weeksElapsed * 7;

                if (weeksElapsed > 0)
                {
                    foreach (var o in orders)
                    {
                        o.OrderDate = o.OrderDate.AddDays(daysToAdjust);
                        o.DueDate = o.DueDate.AddDays(daysToAdjust);
                        o.ClosedDate = o.ClosedDate?.AddDays(daysToAdjust);
                        o.UpdatedAt = o.UpdatedAt?.AddDays(daysToAdjust);

                        _MobileServiceContext.Entry(o).State = EntityState.Modified;

                        await _MobileServiceContext.SaveChangesAsync();
                    }
                }
            }
            else
            {
                throw new  Exception("No orders have an UpdatedAt value. This should not happen.");
            }

            // all went well, so return a success result
            return await Task.FromResult(true);
        }
    }
}
