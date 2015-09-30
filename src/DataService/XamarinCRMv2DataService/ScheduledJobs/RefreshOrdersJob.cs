using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.ScheduledJobs;
using XamarinCRMv2DataService.DataObjects;
using XamarinCRMv2DataService.Models;

namespace XamarinCRMv2DataService
{
    /*
    A simple scheduled job which can be invoked manually by submitting an HTTP
    POST request to the path "/jobs/sample".
    */

    /// <summary>
    /// Reresh the dates on the orders.
    /// </summary>
    public class RefreshOrdersJob : ScheduledJob
    {
        private MobileServiceContext _MobileServiceContext;

        protected override void Initialize(ScheduledJobDescriptor scheduledJobDescriptor, CancellationToken cancellationToken)
        {
            base.Initialize(scheduledJobDescriptor, cancellationToken);

            _MobileServiceContext = new MobileServiceContext();
        }

        public async override Task ExecuteAsync()
        {
            try
            {
                var orderList = _MobileServiceContext.Orders as IList<Order> ?? _MobileServiceContext.Orders.ToList();

                var oldestUpdatedOrder = orderList.OrderBy(o => o.UpdatedAt).FirstOrDefault();

                if (oldestUpdatedOrder?.UpdatedAt != null)
                {
                    DateTime lastUpdatedDate = oldestUpdatedOrder.UpdatedAt.Value.UtcDateTime;

                    int daysSinceUpdate = (DateTime.UtcNow - lastUpdatedDate).Days;

                    foreach (var o in orderList)
                    {
                        o.OrderDate = o.OrderDate.AddDays(daysSinceUpdate);
                        o.DueDate = o.DueDate.AddDays(daysSinceUpdate);
                        o.ClosedDate = o.ClosedDate?.AddDays(daysSinceUpdate);

                        _MobileServiceContext.Entry(o).State = EntityState.Modified;

                        await _MobileServiceContext.SaveChangesAsync();
                    }

                    Services.Log.Info($"Orders successfully refreshed. Total orders: {orderList.Count}");
                    await Task.FromResult(true);
                }
                else
                {
                    Services.Log.Warn("None of the orders seem to have an UpdatedAt value. This may mean there's no orders at all in the database.");
                    await Task.FromResult(false);
                }
            }
            catch (Exception ex)
            {
                Services.Log.Error($"Orders failed to refresh refresh: {ex.Message}");
                throw;
            }
        }
    }
}