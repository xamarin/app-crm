// The MIT License (MIT)
// 
// Copyright (c) 2015 Xamarin
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
using System.Threading.Tasks;
using System.Collections.Generic;
using XamarinCRM.Models;

namespace XamarinCRM.Services
{
    public interface IDataService
    {
        Task SeedLocalDataAsync();

        bool LocalDBExists { get; }

        bool IsSeeded { get; }

        Task SynchronizeAccountsAsync();

        Task SynchronizeOrdersAsync();

        Task SynchronizeCategoriesAsync();

        Task SynchronizeProductsAsync();

        Task SaveOrderAsync(Order item);

        Task DeleteOrderAsync(Order item);

        Task SaveAccountAsync(Account item);

        Task DeleteAccountAsync(Account item);

        Task<IEnumerable<Account>> GetAccountsAsync(bool includeLeads = false);

        Task<IEnumerable<Order>> GetOpenOrdersForAccountAsync(string accountId);

        Task<IEnumerable<Order>> GetClosedOrdersForAccountAsync(string accountId);

        Task<IEnumerable<Order>> GetAllOrdersAsync();

        Task<IEnumerable<Category>> GetCategoriesAsync(string parentCategoryId = null);

        Task<IEnumerable<Product>> GetProductsAsync(string categoryId);

        Task<IEnumerable<Product>> GetAllChildProductsAsync(string topLevelCategoryId);

        Task<Category> GetTopLevelCategory(string categoryId);

        Task<Product> GetProductByNameAsync(string productName);

        Task<IEnumerable<Product>> SearchAsync(string searchTerm);
    }
}

