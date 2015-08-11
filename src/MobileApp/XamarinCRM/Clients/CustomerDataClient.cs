using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using SQLitePCL;
using Xamarin;
using Xamarin.Forms;
using XamarinCRM.Clients;
using XamarinCRM.Models;
using XamarinCRM.Services;

[assembly: Dependency(typeof(CustomerDataClient))]

namespace XamarinCRM.Clients
{
    public class CustomerDataClient : ICustomerDataClient
    {
        IMobileServiceSyncTable<Order> orderTable;
        IMobileServiceSyncTable<Contact> contactTable;
        IMobileServiceSyncTable<Account> accountTable;

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
            store.DefineTable<Contact>();

            try
            {
                await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"Sync Failed: {0}", ex.Message);
                Insights.Report(ex, Insights.Severity.Error);
            }

            orderTable = MobileService.GetSyncTable<Order>();

            accountTable = MobileService.GetSyncTable<Account>();

            contactTable = MobileService.GetSyncTable<Contact>();
        }

        public async Task SeedData()
        {
            using (var handle = Insights.TrackTime("TimeToSyncDB"))
            {
                handle.Start();

                try
                {
                    await Init();

                    await orderTable.PullAsync(null, orderTable.CreateQuery());
                    await accountTable.PullAsync(null, accountTable.CreateQuery());
                    await contactTable.PullAsync(null, contactTable.CreateQuery());

                }
                catch (Exception exc)
                {
                    Insights.Report(exc, Insights.Severity.Error);
                    Debug.WriteLine("ERROR AzureService.SeedData(): " + exc.Message);
                }
                finally
                {
                    //Insights
                    handle.Stop();
                }
            }
        }

        #region Orders

      
        public async Task SyncOrders()
        {
            try
            {

                await Init();

                //SYI: For public demo, only allow pull, not push. The service is configured to disallow pushes anyway.
                //await MobileService.SyncContext.PushAsync();

                await orderTable.PullAsync(null, orderTable.CreateQuery());
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
                    await orderTable.InsertAsync(item);
                else
                    await orderTable.UpdateAsync(item);
            }
        }

        public async Task DeleteOrderAsync(Order item)
        {
            try
            {
                using (var handle = Insights.TrackTime("TimeToDeleteOrder"))
                {
                    await orderTable.DeleteAsync(item);
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

        public async Task SyncAccounts()
        {
            try
            {
                await Init();

                await accountTable.PullAsync(null, accountTable.CreateQuery());
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
                        await accountTable.InsertAsync(item);
                    else
                        await accountTable.UpdateAsync(item);
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
                await accountTable.DeleteAsync(item);
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
                    return await accountTable.Where(a => a.IsLead == leads).OrderBy(b => b.Company).ToEnumerableAsync();
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

        public async Task<IEnumerable<Order>> GetAccountOrdersAsync(string accountId)
        {
            try
            {
                using (var handle = Insights.TrackTime("TimeToGetAccountOrders"))
                {
                    return await orderTable.Where(j => j.AccountId == accountId &&
                        j.IsOpen == true).OrderBy(j => j.DueDate).ToEnumerableAsync();
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

        public async Task<IEnumerable<Order>> GetAccountOrderHistoryAsync(string accountId)
        {
            try
            {
                using (var handle = Insights.TrackTime("TimeToGetAccountHistory"))
                {
                    return await orderTable.Where(j => j.AccountId == accountId &&
                        j.IsOpen == false).OrderByDescending(j => j.ClosedDate).ToEnumerableAsync();
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

        public async Task<IEnumerable<Order>> GetAllAccountOrdersAsync()
        {
            try
            {
                using (var handle = Insights.TrackTime("TimeToGetAllAccountOrders"))
                {
                    return await orderTable.Where(j => j.IsOpen == false).ToEnumerableAsync();
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

        #region Contacts

        public async Task SyncContacts()
        {
            try
            {
                await Init();

                //SYI: Only pull in public demo
                //await MobileService.SyncContext.PushAsync();

                //await contactTable.PullAsync();
                await contactTable.PullAsync(null, contactTable.CreateQuery());
            }
            catch (MobileServiceInvalidOperationException e)
            {
                Debug.WriteLine(@"Sync Failed: {0}", e.Message);
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
        }

        public async Task SaveContactAsync(Contact item)
        {
            try
            {
                using (var handle = Insights.TrackTime("TimeToSaveContact"))
                {
                    if (item.Id == null)
                        await contactTable.InsertAsync(item);
                    else
                        await contactTable.UpdateAsync(item);
                }

                //await SyncContacts();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                Debug.WriteLine(@"Sync Failed: {0}", e.Message);
            }
        }

        public async Task DeleteContactAsync(Contact item)
        {
            try
            {
                await contactTable.DeleteAsync(item);
                //await SyncContacts();
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
        }

        public async Task<IEnumerable<Contact>> GetContactsAsync()
        {
            try
            {
                using (var handle = Insights.TrackTime("TimeToGetContacts"))
                {
                    //SYI - Sort contacts by last name
                    IMobileServiceTableQuery<Contact> query = contactTable.OrderBy(c => c.LastName);
                    return await query.ToListAsync();
                }
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (SQLiteException sqex)
            {
                Debug.WriteLine(@"ERROR {0}", sqex.Message);
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
            return new List<Contact>();
        }

        public async Task<Contact> GetContactAsync(string contactId)
        {
            try
            {
                using (var handle = Insights.TrackTime("TimeToGetContact"))
                {
                    await SyncContacts();
                    return await contactTable.LookupAsync(contactId);
                }
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            catch (Exception ex2)
            {
                Debug.WriteLine(@"ERROR {0}", ex2.Message);
            }
            return null;
        }

        #endregion

        static readonly CustomerDataClient instance = new CustomerDataClient();

        /// <summary>
        /// Gets the instance of the Azure Web Service
        /// </summary>
        public static CustomerDataClient Instance
        {
            get
            {
                return instance;
            }
        }
    }
}