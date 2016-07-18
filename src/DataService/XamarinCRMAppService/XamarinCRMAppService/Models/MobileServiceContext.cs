

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Microsoft.Azure.Mobile.Server.Tables;
using XamarinCRM.Models;

namespace XamarinCRMAppService.Models
{
    public class MobileServiceContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to alter your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
        //
        // To enable Entity Framework migrations in the cloud, please ensure that the 
        // service name, set by the 'MS_MobileServiceName' AppSettings in the local 
        // Web.config, is the same as the service name when hosted in Azure.

        private const string connectionStringName = "Name=MS_TableConnectionString";

        public MobileServiceContext() : base(connectionStringName)
        {
            // Increasing timeout because database creation in Azure SQL seems to take a while (roughly 120 seconds).
            Database.CommandTimeout = 300; 
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasKey(a => a.Id);

            modelBuilder.Entity<Order>().HasKey(o => o.Id);

            modelBuilder.Entity<Category>().HasKey(c => c.Id);

            modelBuilder.Entity<Product>().HasKey(p => p.Id);

            string schema = System.Configuration.ConfigurationManager.AppSettings.Get("MS_MobileServiceName");
            if (!string.IsNullOrEmpty(schema))
            {
                // If the MS_MobileServiceName value has not been set in AppSettings (via Web.config or portal AppSettings), then the default schema will be used ("dbo").
                modelBuilder.HasDefaultSchema(schema);
            }

            modelBuilder.Conventions.Add(
                new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
                    "ServiceTableColumn", (property, attributes) => attributes.Single().ColumnType.ToString()));
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }
    }
}
