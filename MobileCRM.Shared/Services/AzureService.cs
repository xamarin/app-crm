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
using System.Linq;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(AzureService))]

namespace MobileCRM.Shared.Services
{
    public class AzureService : IDataManager
    {
      IMobileServiceSyncTable<Order> orderTable;
      IMobileServiceSyncTable<Contact> contactTable;


      IMobileServiceSyncTable<Account> accountTable;

      public IMobileServiceClient MobileService { get; set; }


      public AzureService()
      {
          MobileService = AuthInfo.Instance.GetMobileServiceClient();

          //MobileService = new MobileServiceClient(
          //    AuthInfo.APPLICATION_URL, AuthInfo.APPLICATION_KEY);
      }


      public bool DoesLocalDBExist()
      {

          return MobileService.SyncContext.IsInitialized;
      }


      public async Task Init()
      {
          

        if (MobileService.SyncContext.IsInitialized)    
          return;

        var path = "syncstore.db";
        var store = new MobileServiceSQLiteStore(path);
        store.DefineTable<Order>();
        store.DefineTable<Account>();
        store.DefineTable<Contact>();

        try
        {
          await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

        }
        catch(Exception ex)
        {
          Debug.WriteLine(@"Sync Failed: {0}", ex.Message);
       
        }
        

        orderTable = MobileService.GetSyncTable<Order>();

        accountTable = MobileService.GetSyncTable<Account>();

        contactTable = MobileService.GetSyncTable<Contact>();
      }


      public async Task SeedData()
      {
          try
          {
              await Init();

              await orderTable.PullAsync();
              await accountTable.PullAsync();
              await contactTable.PullAsync();

          }
          catch (Exception exc)
          {
              Debug.WriteLine("ERROR AzureService.SeedData(): " + exc.Message);
          }

      }

#region Orders

      
      public async Task SyncOrders()
      {
        try
        {

          await Init();


            //SYI: For public demo, only allow pull, not push.
            //await MobileService.SyncContext.PushAsync();


            await orderTable.PullAsync();
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


      public async Task SaveOrderAsync(Order item)
      { 
        if (item.Id == null)
          await orderTable.InsertAsync(item);
        else 
          await orderTable.UpdateAsync(item);

        //await SyncOrders();
      }

      public async Task DeleteOrderAsync(Order item)
      {
        try
        {
          await orderTable.DeleteAsync(item);
          //await SyncOrders(); ;
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
          //await MobileService.SyncContext.PushAsync();
          await accountTable.PullAsync();
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



      public async Task SaveAccountAsync(Account item)
      {
        try
        {

          if (item.Id == null)
            await accountTable.InsertAsync(item);
          else
            await accountTable.UpdateAsync(item);

          //await SyncAccounts();

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

      public async Task DeleteAccountAsync(Account item)
      {
        try
        {
          await accountTable.DeleteAsync(item);
          //await SyncAccounts();
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
          //await SyncAccounts();

          return await accountTable.Where(a =>a.IsLead == leads).OrderBy(b => b.Company).ToEnumerableAsync();
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

      public async Task<IEnumerable<Order>> GetAccountOrdersAsync(string accountId)
      {
        try
        {
          //await SyncOrders();

          return await orderTable.Where(j => j.AccountId == accountId &&
                                         j.IsOpen == true).OrderBy(j => j.DueDate).ToEnumerableAsync();
        }
        catch (MobileServiceInvalidOperationException ex)
        {
          Debug.WriteLine(@"ERROR {0}", ex.Message);
        }
        catch (Exception ex2)
        {
          Debug.WriteLine(@"ERROR {0}", ex2.Message);
        }
        return new List<Order>();
      }

      public async Task<IEnumerable<Order>> GetAccountOrderHistoryAsync(string accountId)
      {
          try
          {
              //await SyncOrders();

              return await orderTable.Where(j => j.AccountId == accountId &&
                                             j.IsOpen == false).OrderByDescending(j => j.ClosedDate).ToEnumerableAsync();
          }
          catch (MobileServiceInvalidOperationException ex)
          {
              Debug.WriteLine(@"ERROR {0}", ex.Message);
          }
          catch (Exception ex2)
          {
              Debug.WriteLine(@"ERROR {0}", ex2.Message);
          }
          return new List<Order>();
      }


      public async Task<IEnumerable<Order>> GetAllAccountOrdersAsync()
      {
          try
          {
              //await SyncOrders();

              return await orderTable.Where(j => j.IsOpen == false).ToEnumerableAsync();
          }
          catch (MobileServiceInvalidOperationException ex)
          {
              Debug.WriteLine(@"ERROR {0}", ex.Message);
          }
          catch (Exception ex2)
          {
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

          await contactTable.PullAsync();
        }
        catch(MobileServiceInvalidOperationException e)
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
          //await SyncContacts();

          //SYI - Sort contacts by last name
          IMobileServiceTableQuery<Contact> query = contactTable.OrderBy(c => c.LastName);
          return await query.ToListAsync();
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
