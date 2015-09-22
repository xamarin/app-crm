using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Xamarin;
using XamarinCRM.Models;
using Xamarin.Forms;
using XamarinCRM;

[assembly: Dependency(typeof(DataClient))]

namespace XamarinCRM
{
    public class DataClient : IDataClient
    {
        readonly IMobileServiceClient _MobileServiceClient;

        // sync tables
        IMobileServiceSyncTable<Order> _OrderTable;
        IMobileServiceSyncTable<Account> _AccountTable;
        IMobileServiceSyncTable<CatalogCategory> _CatalogCategoryTable;
        IMobileServiceSyncTable<CatalogProduct> _CatalogProductTable;

        public DataClient()
        {
            _MobileServiceClient = MobileDataSync.Instance.GetMobileServiceClient();
        }

        public async Task Init()
        {
            if (LocalDBExists)
                return;

            var store = new MobileServiceSQLiteStore("syncstore.db");

            store.DefineTable<Order>();
            store.DefineTable<Account>();
            store.DefineTable<CatalogCategory>();
            store.DefineTable<CatalogProduct>();

            try
            {
                await _MobileServiceClient.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"Sync Failed: {0}", ex.Message);
                Insights.Report(ex, Insights.Severity.Error);
            }

            _OrderTable = _MobileServiceClient.GetSyncTable<Order>();
            _AccountTable = _MobileServiceClient.GetSyncTable<Account>();
            _CatalogCategoryTable = _MobileServiceClient.GetSyncTable<CatalogCategory>();
            _CatalogProductTable = _MobileServiceClient.GetSyncTable<CatalogProduct>();
        }

        #region data seeding and local DB status

        public bool LocalDBExists
        {
            get { return _MobileServiceClient.SyncContext.IsInitialized; }

        }

        public async Task SeedLocalDataAsync()
        {
            await Execute(
                "TimeToSyncDB",
                async () =>
                {
                    await Init();
                    await _OrderTable.PullAsync(null, _OrderTable.CreateQuery());
                    await _AccountTable.PullAsync(null, _AccountTable.CreateQuery());
                    await _CatalogCategoryTable.PullAsync(null, _CatalogCategoryTable.CreateQuery());
                    await _CatalogProductTable.PullAsync(null, _CatalogProductTable.CreateQuery());
                }
            );
        }

        #endregion


        #region Orders

        public async Task SynchronizeOrdersAsync()
        {
            await Execute(
                "TimeToSynchronizeOrders",
                async () =>
                {
                    await Init();

                    // For public demo, only allow pull, not push.
                    // Disabled in the backend service code as well.
                    // await _MobileServiceClient.SyncContext.PushAsync();

                    await _OrderTable.PullAsync(null, _OrderTable.CreateQuery());
                }
            );
        }

        public async Task SaveOrderAsync(Order item)
        {
            await Execute(
                "TimeToSaveOrder",
                async () =>
                {
                    if (item.Id == null)
                        await _OrderTable.InsertAsync(item);
                    else
                        await _OrderTable.UpdateAsync(item);
                }
            );
        }

        public async Task DeleteOrderAsync(Order item)
        {
            await Execute(
                "TimeToDeleteOrder",
                async () =>
                {
                    await _OrderTable.DeleteAsync(item);
                }
            );
        }

        #endregion


        #region Accounts

        public async Task SynchronizeAccountsAsync()
        {
            await Execute(
                "TimeToSynchronizeAccounts",
                async () => 
                {
                    await Init();

                    // For public demo, only allow pull, not push.
                    // Disabled in the backend service code as well.
                    // await _MobileServiceClient.SyncContext.PushAsync();

                    await _AccountTable.PullAsync(null, _AccountTable.CreateQuery());
                }
            );
        }

        public async Task SaveAccountAsync(Account item)
        {
            await Execute(
                "TimeToSaveAccount",
                async () =>
                {
                    if (item.Id == null)
                        await _AccountTable.InsertAsync(item);
                    else
                        await _AccountTable.UpdateAsync(item);
                }
            );
        }

        public async Task DeleteAccountAsync(Account item)
        {
            await Execute(
                "TimeToDeleteAccount",
                async () => await _AccountTable.DeleteAsync(item)
            );
        }

        public async Task<IEnumerable<Account>> GetAccountsAsync(bool leads = false)
        {
            return await Execute<IEnumerable<Account>>(
                "TimeToGetAccountList",
                async () =>
                {
                    return await _AccountTable
                        .Where(account => account.IsLead == leads)
                        .OrderBy(b => b.Company)
                        .ToEnumerableAsync();
                },
                new List<Account>()
            );
        }

        public async Task<IEnumerable<Order>> GetOpenOrdersForAccountAsync(string accountId)
        {
            return await Execute<IEnumerable<Order>>(
                "TimeToGetOrders",
                async () =>
                {
                    return await _OrderTable
                        .Where(order => order.AccountId == accountId && order.IsOpen == true)
                        .OrderBy(order => order.DueDate)
                        .ToEnumerableAsync();
                },
                new List<Order>()
            );
        }

        public async Task<IEnumerable<Order>> GetClosedOrdersForAccountAsync(string accountId)
        {
            return await Execute<IEnumerable<Order>>(
                "TimeToGetAccountHistory",
                async () =>
                {
                    return await _OrderTable
                        .Where(order => order.AccountId == accountId && order.IsOpen == false)
                        .OrderByDescending(order => order.ClosedDate)
                        .ToEnumerableAsync();
                },
                new List<Order>()
            );
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await Execute<IEnumerable<Order>>(
                "TimeToGetAllOrders",
                async () =>
                {
                    return await _OrderTable
                        .ToEnumerableAsync();
                },
                new List<Order>()
            );
        }

        #endregion


        #region product catalog data

        public async Task SynchronizeCategoriesAsync()
        {
            await Execute(
                "TimeToSynchronizeCategories",
                async () =>
                {
                    await Init();

                    // For public demo, only allow pull, not push.
                    // Disabled in the backend service code as well.
                    // await _MobileServiceClient.SyncContext.PushAsync();

                    await _CatalogCategoryTable.PullAsync(null, _CatalogCategoryTable.CreateQuery());
                }
            );
        }

        public async Task SynchronizeProductsAsync()
        {
            await Execute(
                "TimeToSynchronizeProducts",
                async () =>
                {
                    await Init();

                    // For public demo, only allow pull, not push.
                    // Disabled in the backend service code as well.
                    // await _MobileServiceClient.SyncContext.PushAsync();

                    await _CatalogProductTable.PullAsync(null, _CatalogProductTable.CreateQuery());
                }
            );
        }

        public async Task<IEnumerable<CatalogCategory>> GetCategoriesAsync(string parentCategoryId = null)
        {
            return await Execute<IEnumerable<CatalogCategory>>(
                "TimeToGetCategories",
                async () =>
                {
                    if (String.IsNullOrWhiteSpace(parentCategoryId))
                    {
                        var rootCategories = await _CatalogCategoryTable
                            .Where(category => category.ParentCategoryId == null)
                            .ToEnumerableAsync();

                        var rootCategory = rootCategories.SingleOrDefault();

                        if (rootCategory == null)
                        {
                            throw new Exception("The catalog category hierarchy contains no root. This should never happen.");
                        }
                        return await _CatalogCategoryTable
                            .Where(category => category.ParentCategoryId == rootCategory.Id)
                            .OrderBy(category => category.Sequence)
                            .ToEnumerableAsync();
                    }
                    else
                    {
                        return await _CatalogCategoryTable
                            .Where(category => category.ParentCategoryId == parentCategoryId)
                            .OrderBy(category => category.Sequence)
                            .ToEnumerableAsync();
                    }
                },
                new List<CatalogCategory>());
        }

        public async Task<IEnumerable<CatalogProduct>> GetProductsAsync(string categoryId)
        {
            return await Execute<IEnumerable<CatalogProduct>>(
                "TimeToGetProducts", 
                async () =>
                {
                    return await _CatalogProductTable
                        .Where(category => category.Id == categoryId)
                        .ToEnumerableAsync();
                }, 
                new List<CatalogProduct>());
        }

        public async Task<IEnumerable<CatalogProduct>> GetAllChildProductsAsync(string topLevelCategoryId)
        {
            return await Execute<IEnumerable<CatalogProduct>>(
                "TimeToGetAllChildProducts", 
                async () =>
                {
                    if (String.IsNullOrWhiteSpace(topLevelCategoryId))
                        throw new ArgumentException("topLevelCategoryId must not be null or empty", "topLevelCategoryId");

                    var rootCategories = await _CatalogCategoryTable
                        .Where(category => category.ParentCategoryId == null)
                        .ToEnumerableAsync();

                    var rootCategory = rootCategories.SingleOrDefault();

                    if (rootCategory == null)
                    {
                        throw new Exception("The catalog category hierarchy contains no root. This should never happen.");
                    }

                    var categories = await _CatalogCategoryTable
                        .Where(category => category.Id == topLevelCategoryId)
                        .ToEnumerableAsync();

                    var topLevelCategory = categories.SingleOrDefault();

                    if (topLevelCategory == null)
                    {
                        throw new Exception(String.Format("The category for id {0} is null", topLevelCategoryId));
                    }

                    if (topLevelCategory.ParentCategoryId != rootCategory.Id)
                    {
                        throw new Exception(String.Format("The specified category {0} is not a top level category.", topLevelCategory.Name));
                    }

                    var leafLevelCategories = await GetLeafLevelCategories(topLevelCategoryId);

                    List<CatalogProduct> products = new List<CatalogProduct>();

                    foreach (var c in leafLevelCategories)
                    {
                        products.AddRange(await GetProductsAsync(c.Id));
                    }

                    return products;
                },
                new List<CatalogProduct>()
            );
        }

        public async Task<CatalogProduct> GetProductByNameAsync(string productName)
        {
            return await Execute<CatalogProduct>(
                "TimeToGetProductByName", 
                async () =>
                {
                    var products = await _CatalogProductTable
                        .Where(p => p.Name == productName)
                        .ToEnumerableAsync();

                    return products.SingleOrDefault();
                },
                null
            );
        }

        public async Task<IEnumerable<CatalogProduct>> SearchAsync(string searchTerm)
        {
            return await Execute<IEnumerable<CatalogProduct>>(
                "TimeToSearchProducts", 
                async () =>
                {
                    var products = await _CatalogProductTable
                        .Where(x =>
                            x.Name.ToLower().Contains(searchTerm.ToLower()) ||
                                       x.Description.ToLower().Contains(searchTerm.ToLower()) ||
                                       x.CatalogCategory.Name.ToLower().Contains(searchTerm.ToLower()))
                        .ToEnumerableAsync();

                    return products.Distinct();
                },
                new List<CatalogProduct>()
            );
        }

        #endregion


        #region some nifty helpers

        private async Task Execute(string insightsIdentifier, Func<Task> execute)
        {
            try
            {
                using (var handle = Insights.TrackTime(insightsIdentifier))
                {
                    await execute();
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

        private async Task<T> Execute<T>(string insightsIdentifier, Func<Task<T>> execute, T defaultReturnObject)
        {
            try
            {
                using (var handle = Insights.TrackTime(insightsIdentifier))
                {
                    return await execute();
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

            return defaultReturnObject;
        }

        private async Task<IEnumerable<CatalogCategory>> GetLeafLevelCategories(string id)
        {
            var resultCategories = new List<CatalogCategory>();

            var categories = await _CatalogCategoryTable
                .Where(c => c.Id == id)
                .ToEnumerableAsync();

            var category = categories.SingleOrDefault();

            if (category.HasSubCategories)
            {
                var subCategories = await _CatalogCategoryTable
                    .Where(c => c.ParentCategoryId == category.Id)
                    .ToEnumerableAsync();

                foreach (var c in subCategories)
                {
                    resultCategories.AddRange(await GetLeafLevelCategories(c.Id));
                }
            }
            else
            {
                resultCategories.Add(category);
            }

            return resultCategories;
        }

        #endregion
    }
}

