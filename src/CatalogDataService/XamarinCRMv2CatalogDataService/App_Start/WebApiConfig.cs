using System.Data.Entity;
using System.IO;
using System.Web.Hosting;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using Newtonsoft.Json;
using XamarinCRMv2DataService.Models;

namespace XamarinCRMv2DataService
{
    public static class WebApiConfig
    {
        public static void Register()
        {
            // Use this class to set configuration options for your mobile service
            ConfigOptions options = new ConfigOptions();

            // Use this class to set WebAPI configuration options
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));

#if DEBUG
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
#endif

            Database.SetInitializer(new MobileServiceInitializer());
        }
    }

    // Using ClearDatabaseSchemaAlways<T> here instead of DropCreateDatabaseAlways<T> is necessary 
    // because the Azure Mobile Service account won't have permisions to drop the DB in a multi-schema database.
    internal class MobileServiceInitializer : ClearDatabaseSchemaAlways<MobileServiceContext>
    {
        protected override void Seed(MobileServiceContext context)
        {
            var seedingSqlPath = HostingEnvironment.MapPath(@"~/App_Data/");

            string folder = Path.Combine(seedingSqlPath, "SeedingSQL");

            foreach (var filePath in Directory.EnumerateFiles(folder, "*.sql"))
            {
                context.Database.ExecuteSqlCommand(File.ReadAllText(filePath));
            }
        }
    }
}

