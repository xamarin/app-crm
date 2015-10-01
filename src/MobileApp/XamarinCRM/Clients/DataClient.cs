//
//  Copyright 2015  Xamarin Inc.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Xamarin;
using Xamarin.Forms;
using XamarinCRM.Clients;
using XamarinCRM.Models;
using XamarinCRM.Statics;

[assembly: Dependency(typeof(DataClient))]

namespace XamarinCRM.Clients
{
    public class DataClient : IDataClient
    {
        readonly IMobileServiceClient _MobileServiceClient;

        // sync tables
        IMobileServiceSyncTable<Order> _OrderTable;
        IMobileServiceSyncTable<Account> _AccountTable;
        IMobileServiceSyncTable<Category> _CategoryTable;
        IMobileServiceSyncTable<Product> _ProductTable;

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

            store.DefineTable<Category>();

            store.DefineTable<Product>();

            try
            {
                await _MobileServiceClient.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"Failed to initialize sync context: {0}", ex.Message);
                Insights.Report(ex, Insights.Severity.Error);
            }

            _OrderTable = _MobileServiceClient.GetSyncTable<Order>();

            _AccountTable = _MobileServiceClient.GetSyncTable<Account>();

            _CategoryTable = _MobileServiceClient.GetSyncTable<Category>();

            _ProductTable = _MobileServiceClient.GetSyncTable<Product>();
        }

        #region data seeding and local DB status

        public bool LocalDBExists
        {
            get { return _MobileServiceClient.SyncContext.IsInitialized; }
        }

        bool _IsSeeded;
        public bool IsSeeded { get { return _IsSeeded; } }

        public async Task SeedLocalDataAsync()
        {                
            string dbSyncInsightsIdentifier = InsightsReportingConstants.TIME_DB_SYNC_INCREMENTAL;

            if (!LocalDBExists)
            {
                dbSyncInsightsIdentifier = InsightsReportingConstants.TIME_DB_SYNC_INITIAL;
            }

            await Execute(
                dbSyncInsightsIdentifier,
                async () =>
                {
                    await SynchronizeAccountsAsync();

                    await SynchronizeOrdersAsync();

                    await SynchronizeCategoriesAsync();

                    await SynchronizeProductsAsync();

                    _IsSeeded = true;
                }
            );
        }

        #endregion


        #region Orders

        public async Task SynchronizeOrdersAsync()
        {
            await Execute(
                InsightsReportingConstants.TIME_ORDERS_SYNC,
                async () =>
                {
                    if (!LocalDBExists)
                    {    
                        await Init();
                    }

                    // For public demo, only allow pull, not push.
                    // Disabled in the backend service code as well.
                    // await _MobileServiceClient.SyncContext.PushAsync();

                    await _OrderTable.PullAsync("syncOrders", _OrderTable.CreateQuery());
                }
            );
        }

        public async Task SaveOrderAsync(Order item)
        {
            await Execute(
                InsightsReportingConstants.TIME_ORDERS_SAVE_SINGLE,
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
                InsightsReportingConstants.TIME_ORDERS_DELETE_SINGLE,
                async () =>
                    await _OrderTable.DeleteAsync(item)
            );
        }

        public async Task<IEnumerable<Order>> GetOpenOrdersForAccountAsync(string accountId)
        {
            return await Execute<IEnumerable<Order>>(
                InsightsReportingConstants.TIME_ORDERS_GET_OPEN,
                async () =>
                    await _OrderTable
                        .Where(order => order.AccountId.ToLower() == accountId.ToLower() && order.IsOpen == true)
                        .OrderBy(order => order.DueDate)
                        .ToEnumerableAsync(),
                    new List<Order>()
            );
        }

        public async Task<IEnumerable<Order>> GetClosedOrdersForAccountAsync(string accountId)
        {
            return await Execute<IEnumerable<Order>>(
                InsightsReportingConstants.TIME_ORDERS_GET_CLOSED,
                async () =>
                await _OrderTable
                .Where(order => order.AccountId.ToLower() == accountId.ToLower() && order.IsOpen == false)
                .OrderByDescending(order => order.ClosedDate)
                .ToEnumerableAsync(),
                new List<Order>()
            );
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await Execute<IEnumerable<Order>>(
                InsightsReportingConstants.TIME_ORDERS_GET_ALL,
                async () =>
                    await _OrderTable.ToEnumerableAsync(),
                    new List<Order>()
            );
        }

        #endregion


        #region Accounts

        public async Task SynchronizeAccountsAsync()
        {
            await Execute(
                InsightsReportingConstants.TIME_ACCOUNTS_SYNC,
                async () =>
                {
                    if (!LocalDBExists)
                    {    
                        await Init();
                    }

                    // For public demo, only allow pull, not push.
                    // Disabled in the backend service code as well.
                    // await _MobileServiceClient.SyncContext.PushAsync();

                    await _AccountTable.PullAsync("syncAccounts", _AccountTable.CreateQuery());
                }
            );
        }

        public async Task SaveAccountAsync(Account item)
        {
            await Execute(
                InsightsReportingConstants.TIME_ACCOUNTS_SAVE_SINGLE,
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
                InsightsReportingConstants.TIME_ACCOUNTS_DELETE_SINGLE,
                async () => 
                    await _AccountTable.DeleteAsync(item)
            );
        }

        public async Task<IEnumerable<Account>> GetAccountsAsync(bool leads = false)
        {
            return await Execute<IEnumerable<Account>>(
                InsightsReportingConstants.TIME_ACCOUNTS_GET_ALL,
                async () =>
                    await _AccountTable
                        .Where(account => account.IsLead == leads).OrderBy(b => b.Company)
                        .ToEnumerableAsync(),
                new List<Account>()
            );
        }

        #endregion


        #region Categories

        public async Task SynchronizeCategoriesAsync()
        {
            await Execute(
                InsightsReportingConstants.TIME_CATEGORIES_SYNC,
                async () =>
                {
                    if (!LocalDBExists)
                    {    
                        await Init();
                    }

                    // For public demo, only allow pull, not push.
                    // Disabled in the backend service code as well.
                    // await _MobileServiceClient.SyncContext.PushAsync();

                    await _CategoryTable
                        .PullAsync("syncCategories", _CategoryTable.CreateQuery());
                }
            );
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync(string parentCategoryId = null)
        {
            return await Execute<IEnumerable<Category>>(
                InsightsReportingConstants.TIME_CATEGORIES_GET,
                async () =>
                {
                    if (String.IsNullOrWhiteSpace(parentCategoryId))
                    {
                        var rootCategories = 
                            await _CategoryTable
                                .Where(category => category.ParentCategoryId == null)
                                .ToEnumerableAsync();

                        var rootCategory = rootCategories.SingleOrDefault();

                        if (rootCategory == null)
                        {
                            throw new Exception("The catalog category hierarchy contains no root. This should never happen.");
                        }
                        return 
                            await _CategoryTable
                                .Where(category => category.ParentCategoryId.ToLower() == rootCategory.Id.ToLower())
                                .OrderBy(category => category.Sequence)
                                .ToEnumerableAsync();
                    }
                    else
                    {
                        return 
                            await _CategoryTable
                                .Where(category => category.ParentCategoryId.ToLower() == parentCategoryId.ToLower())
                                .OrderBy(category => category.Sequence)
                                .ToEnumerableAsync();
                    }
                },
                new List<Category>());
        }

        #endregion


        #region Products

        public async Task SynchronizeProductsAsync()
        {
            await Execute(
                InsightsReportingConstants.TIME_PRODUCTS_SYNC,
                async () =>
                {
                    if (!LocalDBExists)
                    {    
                        await Init();
                    }

                    // For public demo, only allow pull, not push.
                    // Disabled in the backend service code as well.
                    // await _MobileServiceClient.SyncContext.PushAsync();

                    await _ProductTable
                        .PullAsync("syncProducts", _ProductTable.CreateQuery());
                }
            );
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(string categoryId)
        {
            return await Execute<IEnumerable<Product>>(
                InsightsReportingConstants.TIME_PRODUCTS_GET, 
                async () =>
                    await _ProductTable
                        .Where(product => product.CategoryId.ToLower() == categoryId.ToLower())
                        .ToEnumerableAsync(), 
                new List<Product>());
        }

        public async Task<IEnumerable<Product>> GetAllChildProductsAsync(string topLevelCategoryId)
        {
            return await Execute<IEnumerable<Product>>(
                InsightsReportingConstants.TIME_PRODUCTS_GET_ALLCHILDREN, 
                async () =>
                {
                    if (String.IsNullOrWhiteSpace(topLevelCategoryId))
                        throw new ArgumentException("topLevelCategoryId must not be null or empty", "topLevelCategoryId");

                    var rootCategories = 
                        await _CategoryTable
                            .Where(category => category.ParentCategoryId == null)
                            .ToEnumerableAsync();

                    var rootCategory = rootCategories.SingleOrDefault();

                    if (rootCategory == null)
                    {
                        throw new Exception("The catalog category hierarchy contains no root. This should never happen.");
                    }

                    var categories = 
                        await _CategoryTable
                            .Where(category => category.Id.ToLower() == topLevelCategoryId.ToLower())
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

                    List<Product> products = new List<Product>();

                    foreach (var c in leafLevelCategories)
                    {
                        products.AddRange(await GetProductsAsync(c.Id));
                    }

                    return products;
                },
                new List<Product>()
            );
        }

        public async Task<Product> GetProductByNameAsync(string productName)
        {
            return await Execute<Product>(
                InsightsReportingConstants.TIME_PRODUCTS_GET_BYNAME, 
                async () =>
                {
                    var products = 
                        await _ProductTable
                            .Where(p => p.Name.ToLower() == productName.ToLower())
                            .ToEnumerableAsync();

                    return products.SingleOrDefault();
                },
                null
            );
        }

        public async Task<IEnumerable<Product>> SearchAsync(string searchTerm)
        {
            return await Execute<IEnumerable<Product>>(
                InsightsReportingConstants.TIME_PRODUCTS_SEARCH, 
                async () =>
                {
                    var products = 
                        await _ProductTable
                            .Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()) || x.Description.ToLower().Contains(searchTerm.ToLower()))
                            .ToEnumerableAsync();

                    return products.Distinct();
                },
                new List<Product>()
            );
        }

        #endregion


        #region some nifty helpers

        static async Task Execute(string insightsIdentifier, Func<Task> execute)
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

        static async Task<T> Execute<T>(string insightsIdentifier, Func<Task<T>> execute, T defaultReturnObject)
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

        async Task<IEnumerable<Category>> GetLeafLevelCategories(string id)
        {
            var resultCategories = new List<Category>();

            var categories = 
                await _CategoryTable
                    .Where(c => c.Id == id)
                    .ToEnumerableAsync();

            var category = categories.SingleOrDefault();

            if (category.HasSubCategories)
            {
                var subCategories = 
                    await _CategoryTable
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

