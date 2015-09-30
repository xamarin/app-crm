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
using System.Threading.Tasks;
using System.Collections.Generic;
using XamarinCRM.Models;

namespace XamarinCRM.Clients
{
    public interface IDataClient
    {
        Task SeedLocalDataAsync();

        bool LocalDBExists { get; }

        Task SynchronizeAccountsAsync();

        Task SynchronizeOrdersAsync();

        Task SynchronizeCategoriesAsync();

        Task SynchronizeProductsAsync();

        Task SaveOrderAsync(Order item);

        Task DeleteOrderAsync(Order item);

        Task SaveAccountAsync(Account item);

        Task DeleteAccountAsync(Account item);

        Task<IEnumerable<Account>> GetAccountsAsync(bool leads = false);

        Task<IEnumerable<Order>> GetOpenOrdersForAccountAsync(string accountId);

        Task<IEnumerable<Order>> GetClosedOrdersForAccountAsync(string accountId);

        Task<IEnumerable<Order>> GetAllOrdersAsync();

        Task<IEnumerable<Category>> GetCategoriesAsync(string parentCategoryId = null);

        Task<IEnumerable<Product>> GetProductsAsync(string categoryId);

        Task<IEnumerable<Product>> GetAllChildProductsAsync(string topLevelCategoryId);

        Task<Product> GetProductByNameAsync(string productName);

        Task<IEnumerable<Product>> SearchAsync(string searchTerm);
    }
}

