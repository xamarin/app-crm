using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Xamarin;
using Xamarin.Forms;
using XamarinCRM.Clients;
using XamarinCRM.Models;
using XamarinCRM.Services;

[assembly: Dependency(typeof(CustomerDataClient))]

namespace XamarinCRM.Clients
{
    public class CustomerDataClient : IDataClient
    {
        IMobileServiceSyncTable<Order> _OrderTable;
        IMobileServiceSyncTable<Account> _AccountTable;

        public IMobileServiceClient MobileService { get; set; }

        public CustomerDataClient()
        {
            MobileService = AuthInfo.Instance.GetMobileServiceClient();
        }

        public bool DoesLocalDBExist()
        {
            return MobileService.SyncContext.IsInitialized;
        }

        public async Task Init()
        {
            if (MobileService.SyncContext.IsInitialized)
                return;

            var store = new MobileServiceSQLiteStore("syncstore.db");

            store.DefineTable<Order>();
            store.DefineTable<Account>();

            try
            {
                await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"Sync Failed: {0}", ex.Message);
                Insights.Report(ex, Insights.Severity.Error);
            }

            _OrderTable = MobileService.GetSyncTable<Order>();

            _AccountTable = MobileService.GetSyncTable<Account>();
        }

        public async Task SeedDataAsync()
        {
            ITrackHandle handle = null;

            try
            {
                //Insights tracking
                handle = Insights.TrackTime("TimeToSyncDB");
                handle.Start();

                await Init();

                await _OrderTable.PullAsync(null, _OrderTable.CreateQuery());
                await _AccountTable.PullAsync(null, _AccountTable.CreateQuery());
            }
            catch (Exception exc)
            {
                Insights.Report(exc, Insights.Severity.Error);
                Debug.WriteLine("ERROR AzureService.SeedData(): " + exc.Message);
            }
            finally
            {
                //Insights
                if (handle != null)
                    handle.Stop();

            }
        }

        #region Orders
      
        public async Task SynchronizeOrdersAsync()
        {
            try
            {
                await Init();

                //SYI: For public demo, only allow pull, not push.
//                await MobileService.SyncContext.PushAsync();

                await _OrderTable.PullAsync(null, _OrderTable.CreateQuery());
            }
            catch (MobileServiceInvalidOperationException e)
            {
                Insights.Report(e, Insights.Severity.Error);
                Debug.WriteLine(@"Sync Failed: {0}", e.Message);
            }
            catch (Exception ex2)
            {
                Insights.Report(ex2, Insights.Severity.Error);
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
        }

        public async Task SaveOrderAsync(Order item)
        {
            //Insights
            using (var handle = Insights.TrackTime("TimeToSaveOrder"))
            {
                if (item.Id == null)
                    await _OrderTable.InsertAsync(item);
                else
                    await _OrderTable.UpdateAsync(item);
            }
        }

        public async Task DeleteOrderAsync(Order item)
        {
            try
            {
                using (var handle = Insights.TrackTime("TimeToDeleteOrder"))
                {
                    await _OrderTable.DeleteAsync(item);
                }
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Insights.Report(ex, Insights.Severity.Error);
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Insights.Report(ex2, Insights.Severity.Error);
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
        }

        #endregion

        #region Accounts

        public async Task SynchronizeAccountsAsync()
        {
            try
            {
                await Init();

                await _AccountTable.PullAsync(null, _AccountTable.CreateQuery());
            }
            catch (MobileServiceInvalidOperationException e)
            {
                Insights.Report(e, Insights.Severity.Error);
                Debug.WriteLine(@"Sync Failed: {0}", e.Message);
            }
            catch (Exception ex2)
            {
                Insights.Report(ex2, Insights.Severity.Error);
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
        }

        public async Task SaveAccountAsync(Account item)
        {
            try
            {
                using (var handle = Insights.TrackTime("TimeToSaveAccount"))
                {
                    if (item.Id == null)
                        await _AccountTable.InsertAsync(item);
                    else
                        await _AccountTable.UpdateAsync(item);
                }

            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Insights.Report(ex, Insights.Severity.Error);
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Insights.Report(ex2, Insights.Severity.Error);
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
        }

        public async Task DeleteAccountAsync(Account item)
        {
            try
            {
                await _AccountTable.DeleteAsync(item);
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Insights.Report(ex, Insights.Severity.Error);
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Insights.Report(ex2, Insights.Severity.Error);
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
        }

        public async Task<IEnumerable<Account>> GetAccountsAsync(bool leads = false)
        {
            try
            {
                using (var handle = Insights.TrackTime("TimeToGetAccountList"))
                {
                    return await _AccountTable
                        .Where(account => account.IsLead == leads)
                        .OrderBy(b => b.Company)
                        .ToEnumerableAsync();
                }
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Insights.Report(ex, Insights.Severity.Error);
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Insights.Report(ex2, Insights.Severity.Error);
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
            return new List<Account>();
        }

        public async Task<IEnumerable<Order>> GetOpenOrdersForAccountAsync(string accountId)
        {
            try
            {
                using (var handle = Insights.TrackTime("TimeToGetOrders"))
                {
                    return await _OrderTable
                        .Where(order => order.AccountId == accountId && order.IsOpen == true)
                        .OrderBy(order => order.DueDate)
                        .ToEnumerableAsync();
                }
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Insights.Report(ex, Insights.Severity.Error);
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Insights.Report(ex2, Insights.Severity.Error);
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
            return new List<Order>();
        }

        public async Task<IEnumerable<Order>> GetClosedOrdersForAccountAsync(string accountId)
        {
            try
            {
                using (var handle = Insights.TrackTime("TimeToGetAccountHistory"))
                {
                    return await _OrderTable
                        .Where(order => order.AccountId == accountId && order.IsOpen == false)
                        .OrderByDescending(order => order.ClosedDate)
                        .ToEnumerableAsync();
                }
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Insights.Report(ex, Insights.Severity.Error);
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Insights.Report(ex2, Insights.Severity.Error);
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
            return new List<Order>();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            try
            {
                using (var handle = Insights.TrackTime("TimeToGetAllOrders"))
                {
                    return await _OrderTable
//                        .Where(order => order.IsOpen == false)
                        .ToEnumerableAsync();
                }
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Insights.Report(ex, Insights.Severity.Error);
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Insights.Report(ex2, Insights.Severity.Error);
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
            return new List<Order>();
        }

        #endregion
    }
}