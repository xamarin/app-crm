using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using MobileCRM.Shared.Interfaces;
using MobileCRM.Shared.Models;
using MobileCRM.Shared.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
[assembly: Xamarin.Forms.Dependency(typeof(AzureService))]

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
#if __IOS__
        SQLitePCL.CurrentPlatform.Init();
#endif


        //comment back in to enable Azure Mobile Services.
        MobileService = new MobileServiceClient(
          "https://" + "xamarindemos" + ".azure-mobile.net/",
          "TyohPJmeLEvNnEzTmXfLcrMGGWSgwZ43");


        
      }

      public async Task Init()
      {
        if (MobileService.SyncContext.IsInitialized)
          return;

        var path = "syncstore.db";
#if __ANDROID__
        path = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), path);

        if (!System.IO.File.Exists(path))
        {
          System.IO.File.Create(path).Dispose();
        }
#elif WINDOWS_PHONE
        path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, path);
#endif
        var store = new MobileServiceSQLiteStore(path);
        store.DefineTable<Job>();
        store.DefineTable<Account>();
        store.DefineTable<Contact>();

        try
        {
          await MobileService.SyncContext.InitializeAsync(store);

        }
        catch(Exception ex)
        {
          Debug.WriteLine(@"Sync Failed: {0}", ex.Message);
       
        }
        

        jobTable = MobileService.GetSyncTable<Job>();
        accountTable = MobileService.GetSyncTable<Account>();
        contactTable = MobileService.GetSyncTable<Contact>(); 
      }

#region Jobs

      
      public async Task SyncJobs()
      {

        try
        {
        
          await Init();
          await MobileService.SyncContext.PushAsync();
          await jobTable.PullAsync();
        }
        catch (MobileServiceInvalidOperationException e)
        {
          Debug.WriteLine(@"Sync Failed: {0}", e.Message);
        }
      }


      public Task SaveJobAsync(Job item)
      {
        if (item.Id == null)
          return jobTable.InsertAsync(item);

        return jobTable.UpdateAsync(item);
      }

      public async Task DeleteJobAsync(Job item)
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
          await Init();
          await MobileService.SyncContext.PushAsync();
          await accountTable.PullAsync();
        }
        catch (MobileServiceInvalidOperationException e)
        {
          Debug.WriteLine(@"Sync Failed: {0}", e.Message);
        }
      }



      public async Task SaveAccountAsync(Account item)
      {
        if (item.Id == null)
          await accountTable.InsertAsync(item);
        else
          await accountTable.UpdateAsync(item);
      }

      public async Task DeleteAccountAsync(Account item)
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

      public async Task<IEnumerable<Account>> GetAccountsAsync(bool leads = false)
      {
        try
        {
          await SyncAccounts();
          await SyncJobs();
          return await accountTable.Where(a =>a.IsLead == leads).ToEnumerableAsync();
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

      public async Task<IEnumerable<Job>> GetAccountJobsAsync(string accountId, bool proposed = false, bool archived = false)
      {
        try
        {
          await SyncAccounts();
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
          await Init();
          await MobileService.SyncContext.PushAsync();
          await contactTable.PullAsync();
        }
        catch(MobileServiceInvalidOperationException e)
        {
          Debug.WriteLine(@"Sync Failed: {0}", e.Message);
        }
      }

      public async Task SaveContactAsync(Contact item)
      {
        try
        { 
          if (item.Id == null)
            await contactTable.InsertAsync(item);
          else
            await contactTable.UpdateAsync(item);

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
          await SyncContacts();
          return await contactTable.ReadAsync();
        }
        catch (MobileServiceInvalidOperationException ex)
        {
          Debug.WriteLine(@"ERROR {0}", ex.Message);
        }
        catch(SQLitePCL.SQLiteException sqex)
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
