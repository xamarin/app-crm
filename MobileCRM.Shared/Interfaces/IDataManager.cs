using MobileCRM.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileCRM.Shared.Interfaces
{
    public interface IDataManager
    {
      Task SaveJobAsync(Job item);
      Task DeleteJobAsync(Job item);

      Task SaveAccountAsync(Account item);
      Task DeleteAccountAsync(Account item);
      Task<IEnumerable<Account>> GetAccountsAsync(bool leads);

      Task<IEnumerable<Job>> GetAccountJobsAsync(string accountId, bool proposed, bool archived);


      Task SaveContactAsync(Contact item);
      Task DeleteContactAsync(Contact item);
      Task<IEnumerable<Contact>> GetContactsAsync();
      Task<Contact> GetContactAsync(string contactId);
    }
}
