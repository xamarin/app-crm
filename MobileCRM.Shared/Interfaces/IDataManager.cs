using MobileCRM.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileCRM.Shared.Interfaces
{
    public interface IDataManager
    {

        Task SeedData();

        bool DoesLocalDBExist();


      Task SyncContacts();
      Task SyncAccounts();
      Task SyncOrders();

      Task SaveOrderAsync(Order item);
      Task DeleteOrderAsync(Order item);

      Task SaveAccountAsync(Account item);
      Task DeleteAccountAsync(Account item);
      Task<IEnumerable<Account>> GetAccountsAsync(bool leads);

      //Task<IEnumerable<Order>> GetAccountOrdersAsync(string accountId, bool open);
      Task<IEnumerable<Order>> GetAccountOrdersAsync(string accountId);
      Task<IEnumerable<Order>> GetAccountOrderHistoryAsync(string accountId);

      Task<IEnumerable<Order>> GetAllAccountOrdersAsync();

      Task SaveContactAsync(Contact item);
      Task DeleteContactAsync(Contact item);
      Task<IEnumerable<Contact>> GetContactsAsync();
      Task<Contact> GetContactAsync(string contactId);




    }
}
