using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using MobileCRM.Shared.Interfaces;
using MobileCRM.Shared.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MobileCRM.Shared.Services
{
    public class AzureService : IDataManager
    {
      IMobileServiceSyncTable<Job> jobTable;
      IMobileServiceSyncTable<Contact> contactTable;
      IMobileServiceSyncTable<Account> accountTable;

      public IMobileServiceClient MobileService { get; set; }

      public AzureService()
      {
#if __ANDROID__ || __IOS__
        Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
#endif

        //comment back in to enable Azure Mobile Services.
        MobileService = new MobileServiceClient(
          "https://" + "dotnetrocks" + ".azure-mobile.net/",
          "crxRndhkdBSjxbvYgzpGuElxmgvWfz81");

        var path = "mobilecrm.db";

#if __ANDROID__
        path = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), path);

        if (!System.IO.File.Exists(path))
        {
          System.IO.File.Create(path).Dispose();
        }
#elif __IOS__
        var docsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        var libraryPath = System.IO.Path.Combine(docsPath, "../Library/");
        path = System.IO.Path.Combine(libraryPath, path);
#elif WINDOWS_PHONE
        path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, path);
#endif

        var store = new MobileServiceSQLiteStore(path);
        store.DefineTable<Job>();
        store.DefineTable<Account>();
        store.DefineTable<Contact>();

        MobileService.SyncContext.InitializeAsync(store).Wait();
      }

#region Jobs

      public async Task SyncJobs()
      {
        try
        {
          await MobileService.SyncContext.PushAsync();
          await jobTable.PullAsync();
        }
        catch (MobileServiceInvalidOperationException e)
        {
          Debug.WriteLine(@"Sync Failed: {0}", e.Message);
        }
      }


      public Task SaveJob(Job item)
      {
        if (item.Id == null)
          return jobTable.InsertAsync(item);

        return jobTable.UpdateAsync(item);
      }

      public async Task DeleteJob(Job item)
      {
        try
        {
          await jobTable.DeleteAsync(item);
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

#endregion

#region Accounts

      public async Task SyncAccounts()
      {
        try
        {

          await MobileService.SyncContext.PushAsync();
          await accountTable.PullAsync();
        }
        catch (MobileServiceInvalidOperationException e)
        {
          Debug.WriteLine(@"Sync Failed: {0}", e.Message);
        }
      }

      

      public Task SaveAccount(Account item)
      {
        if (item.Id == null)
          return accountTable.InsertAsync(item);

        return accountTable.UpdateAsync(item);
      }

      public async Task DeleteAccount(Account item)
      {
        try
        {
          await accountTable.DeleteAsync(item);
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

      public async Task<IEnumerable<Account>> GetAccounts()
      {
        try
        {
          await SyncAccounts();
          return await accountTable.ReadAsync();
        }
        catch (MobileServiceInvalidOperationException ex)
        {
          Debug.WriteLine(@"ERROR {0}", ex.Message);
        }
        catch (Exception ex2)
        {
          Debug.WriteLine(@"ERROR {0}", ex2.Message);
        }
        return new List<Account>();
      }

      public async Task<IEnumerable<Job>> GetAccountJobs(string accountId, bool proposed = false, bool archived = false)
      {
        try
        {
          await SyncJobs();
          return await jobTable.Where(j => j.AccountId == accountId &&
                                           j.IsProposed == proposed &&
                                           j.IsArchived == archived).ToEnumerableAsync();
        }
        catch (MobileServiceInvalidOperationException ex)
        {
          Debug.WriteLine(@"ERROR {0}", ex.Message);
        }
        catch (Exception ex2)
        {
          Debug.WriteLine(@"ERROR {0}", ex2.Message);
        }
        return new List<Job>();
      }
      

#endregion

#region Contacts

      public async Task SyncContacts()
      {
        try
        {

          await MobileService.SyncContext.PushAsync();
          await contactTable.PullAsync();
        }
        catch(MobileServiceInvalidOperationException e)
        {
          Debug.WriteLine(@"Sync Failed: {0}", e.Message);
        }
      }

      public Task SaveContact(Contact item)
      {
        if (item.Id == null)
          return contactTable.InsertAsync(item);

        return contactTable.UpdateAsync(item);
      }

      public async Task DeleteContact(Contact item)
      {
        try
        {
          await contactTable.DeleteAsync(item);
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

      public async Task<IEnumerable<Contact>> GetContacts()
      {
        try
        {
          await SyncContacts();
          return await contactTable.ReadAsync();
        }
        catch (MobileServiceInvalidOperationException ex)
        {
          Debug.WriteLine(@"ERROR {0}", ex.Message);
        }
        catch (Exception ex2)
        {
          Debug.WriteLine(@"ERROR {0}", ex2.Message);
        }
        return new List<Contact>();
      }

      public async Task<Contact> GetContact(string contactId)
      {
        try
        {
          await SyncContacts();
          return await contactTable.LookupAsync(contactId);
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



      static readonly AzureService instance = new AzureService();
      /// <summary>
      /// Gets the instance of the Azure Web Service
      /// </summary>
      public static AzureService Instance
      {
        get
        {
          return instance;
        }
      }
    }
}
