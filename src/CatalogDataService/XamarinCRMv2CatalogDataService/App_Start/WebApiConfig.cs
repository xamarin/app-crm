using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using Newtonsoft.Json;
using XamarinCRMv2CatalogDataService.DataObjects;
using XamarinCRMv2CatalogDataService.Models;

namespace XamarinCRMv2CatalogDataService
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
        /// <summary>
        /// Use this method to populate the data context with data.
        /// </summary>
        /// <param name="context">A <see cref="MobileServiceContext"/></param>
        protected override void Seed(MobileServiceContext context)
        {
            // setup categories

            // root level category
            var rootCategory = new Category { Name = "Root", Description = "Root-level category. No products should be direct children of this category.", ParentCategory = null, Id = Guid.NewGuid().ToString() };

            // root level categories
            var category_printerKits = new Category { Name = "3D Printer Kits", Description = "Complete 3D printer kits", Sequence = 1, ParentCategory = rootCategory, Id = Guid.NewGuid().ToString() };
            var category_filament = new Category { Name = "3D Filament", Description = "The plastic filament for printing 3D objects", Sequence = 2, ParentCategory = rootCategory, Id = Guid.NewGuid().ToString() };
            var category_parts = new Category { Name = "Parts", Description = "Parts for your 3D printer", Sequence = 3, ParentCategory = rootCategory, Id = Guid.NewGuid().ToString() };

            // ptinter categories
            var category_printerKits_abs = new Category { Name = "ABS 3D Printer Kits", Description = "Printer kits that are pre-configured for ABS filament.", Sequence = 1, ParentCategory = category_printerKits, Id = Guid.NewGuid().ToString() };
            var category_printerKits_pla = new Category { Name = "PLA 3D Printer Kits", Description = "Printer kits that are pre-configured for PLA filament.", Sequence = 2, ParentCategory = category_printerKits, Id = Guid.NewGuid().ToString() };

            // fialment categories
            var category_filament_abs = new Category { Name = "ABS Filament", Description = "A petroleum-based high-temp plastic, suitable for engineering applications.", Sequence = 1, ParentCategory = category_filament, Id = Guid.NewGuid().ToString() };
            var category_filament_pla = new Category { Name = "PLA Filament", Description = "A plant-based non-toxic low-warp plastic, suitable for hobbyists.", Sequence = 2, ParentCategory = category_filament, Id = Guid.NewGuid().ToString() };

            // parts categories
            var category_parts_extruders = new Category { Name = "Extruders", Description = "The printhead that extrudes the filament into shapes.", Sequence = 1, ParentCategory = category_parts, Id = Guid.NewGuid().ToString() };
            var category_parts_buildPlates = new Category { Name = "Build Plates", Description = "The flat surface upon which 3D objects are printed.", Sequence = 2, ParentCategory = category_parts, Id = Guid.NewGuid().ToString() };
            var category_parts_coolingFans = new Category { Name = "Cooling Fans", Description = "Extruders get hot. Keep them cool with fans!", Sequence = 3, ParentCategory = category_parts, Id = Guid.NewGuid().ToString() };
            var category_parts_stepperMotors = new Category { Name = "Stepper Motors", Description = "Precise motors are an important part of 3D printers.", Sequence = 4, ParentCategory = category_parts, Id = Guid.NewGuid().ToString() };

            rootCategory.SubCategories.Add(category_printerKits);
            rootCategory.SubCategories.Add(category_filament);
            rootCategory.SubCategories.Add(category_parts);

            category_printerKits.SubCategories.Add(category_printerKits_abs);
            category_printerKits.SubCategories.Add(category_printerKits_pla);

            category_filament.SubCategories.Add(category_filament_abs);
            category_filament.SubCategories.Add(category_filament_pla);

            category_parts.SubCategories.Add(category_parts_extruders);
            category_parts.SubCategories.Add(category_parts_buildPlates);
            category_parts.SubCategories.Add(category_parts_coolingFans);
            category_parts.SubCategories.Add(category_parts_stepperMotors);

            // add the categories to the context
            (new List<Category> { rootCategory }).ForEach(c => context.Set<Category>().Add(c));

            // setup products

            var products = new List<Product>
            {
                // ABS printers
                new Product { Name = "ABS-3DSGNR", Description = "Industrial ABS printer for printing 3D signs", Category = category_printerKits_abs, Price = 324.99, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/printer01.jpg", Id = Guid.NewGuid().ToString()},
                new Product { Name = "ABS-CELL", Description = "Consumer ABS printer specializing in cell phone accessories", Category = category_printerKits_abs, Price = 349.99, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/printer02.jpg", Id = Guid.NewGuid().ToString() },
                new Product { Name = "ABS-SCULPT", Description = "A great ABS printer for fine art sculpture duplication", Category = category_printerKits_abs, Price = 374.99, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/printer03.jpg", Id = Guid.NewGuid().ToString() },
                new Product { Name = "ABS-COMPLX", Description = "A really complex looking ABS printer ", Category = category_printerKits_abs, Price = 399.99, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/printer04.jpg", Id = Guid.NewGuid().ToString() },

                // PLA printers
                new Product { Name = "PLA-HOBY", Description = "A great PLA printer for hobbyist projects", Category = category_printerKits_pla, Price = 324.99, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/printer05.jpg", Id = Guid.NewGuid().ToString() },
                new Product { Name = "PLA-KILR", Description = "PLA printer for custom killer robots", Category = category_printerKits_pla, Price = 349.99, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/printer06.jpg", Id = Guid.NewGuid().ToString() },
                new Product { Name = "PLA-DELIKT", Description = "Precision PLA printer for delicate objects", Category = category_printerKits_pla, Price = 374.99, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/printer07.jpg", Id = Guid.NewGuid().ToString() },
                new Product { Name = "PLA-REPLCE", Description = "PLA printer for replacement part creation", Category = category_printerKits_pla, Price = 399.99, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/printer08.jpg", Id = Guid.NewGuid().ToString() },
                new Product { Name = "PLA-JEWEL", Description = "PLA printer for costume jewelry ", Category = category_printerKits_pla, Price = 424.99, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/printer09.jpg", Id = Guid.NewGuid().ToString() },

                // ABS filament
                new Product { Name = "FIL-ABS-RED", Description = "Red ABS filement, red, 500 grams", Price = 24.99, Category = category_filament_abs, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/filament-red.jpg", Id = Guid.NewGuid().ToString() },
                new Product { Name = "FIL-ABS-ORG", Description = "Orange ABS filement, orange, 500 grams", Price = 24.99, Category = category_filament_abs, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/filament-orange.jpg", Id = Guid.NewGuid().ToString() },
                new Product { Name = "FIL-ABS-YLW", Description = "Yellow ABS filement, yellow, 500 grams", Price = 24.99, Category = category_filament_abs, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/filament-yellow.jpg", Id = Guid.NewGuid().ToString() },
                new Product { Name = "FIL-ABS-GRN", Description = "Green ABS filement, green, 500 grams", Price = 24.99, Category = category_filament_abs, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/filament-green.jpg", Id = Guid.NewGuid().ToString() },
                new Product { Name = "FIL-ABS-BLU", Description = "Blue ABS filement, blue, 500 grams", Price = 24.99, Category = category_filament_abs, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/filament-blue.jpg", Id = Guid.NewGuid().ToString() },
                new Product { Name = "FIL-ABS-IDG", Description = "Indigo ABS filement, indigo, 500 grams", Price = 24.99, Category = category_filament_abs, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/filament-indigo.jpg", Id = Guid.NewGuid().ToString() },
                new Product { Name = "FIL-ABS-VLT", Description = "Violet ABS filement, violet, 500 grams", Price = 24.99, Category = category_filament_abs, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/filament-violet.jpg", Id = Guid.NewGuid().ToString() },

                // PLA filament
                new Product { Name = "FIL-PLA-RED", Description = "Red PLA filement, red, 500 grams", Price = 34.99, Category = category_filament_pla, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/filament-red.jpg", Id = Guid.NewGuid().ToString() },
                new Product { Name = "FIL-PLA-ORG", Description = "Orange PLA filement, orange, 500 grams", Price = 34.99, Category = category_filament_pla, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/filament-orange.jpg", Id = Guid.NewGuid().ToString() },
                new Product { Name = "FIL-PLA-YLW", Description = "Yellow PLA filement, yellow, 500 grams", Price = 34.99, Category = category_filament_pla, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/filament-yellow.jpg", Id = Guid.NewGuid().ToString() },
                new Product { Name = "FIL-PLA-GRN", Description = "Green PLA filement, green, 500 grams", Price = 34.99, Category = category_filament_pla, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/filament-green.jpg", Id = Guid.NewGuid().ToString() },
                new Product { Name = "FIL-PLA-BLU", Description = "Blue PLA filement, blue, 500 grams", Price = 34.99, Category = category_filament_pla, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/filament-blue.jpg", Id = Guid.NewGuid().ToString() },
                new Product { Name = "FIL-PLA-IDG", Description = "Indigo PLA filement, indigo, 500 grams", Price = 34.99, Category = category_filament_pla, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/filament-indigo.jpg", Id = Guid.NewGuid().ToString() },
                new Product { Name = "FIL-PLA-VLT", Description = "Violet PLA filement, violet, 500 grams", Price = 34.99, Category = category_filament_pla, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/filament-violet.jpg", Id = Guid.NewGuid().ToString() },

                // extruders
                new Product { Name = "EXTR-001", Description = "1.75 mm filament extruder, 0.35 mm nozzle", Price = 49.99, Category = category_parts_extruders, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/extruder01.jpg", Id = Guid.NewGuid().ToString()},
                new Product { Name = "EXTR-002", Description = "2.0 mm filament extruder, 0.50 mm nozzle ", Price = 52.99, Category = category_parts_extruders, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/extruder02.jpg", Id = Guid.NewGuid().ToString()},
                new Product { Name = "EXTR-003", Description = "1.75 mm filament extruder, 0.35 mm nozzle", Price = 67.99, Category = category_parts_extruders, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/extruder03.jpg", Id = Guid.NewGuid().ToString()},
                new Product { Name = "EXTR-004", Description = "1.75 mm filament extruder, 0.50 mm nozzle", Price = 69.99, Category = category_parts_extruders, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/extruder04.jpg", Id = Guid.NewGuid().ToString()},

                // build plates
                new Product { Name = "BLD-PLT-ABS", Description = "A heated build plate for ABS filament projects.", Price = 15.00, Category = category_parts_buildPlates, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/buildPlate02.jpg", Id = Guid.NewGuid().ToString()},
                new Product { Name = "BLD-PLT-PLA", Description = "A build plate for PLA filament projects.", Price = 15.00, Category = category_parts_buildPlates, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/buildPlate01.jpg", Id = Guid.NewGuid().ToString()},

                // cooling fans
                new Product { Name = "FAN-001", Description = "Extruder cooling fan.", Price = 19.00, Category = category_parts_coolingFans, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/fan01.jpg", Id = Guid.NewGuid().ToString()},
                new Product { Name = "FAN-002", Description = "Extruder cooling fan.", Price = 19.00, Category = category_parts_coolingFans, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/fan02.jpg", Id = Guid.NewGuid().ToString()},
                new Product { Name = "FAN-003", Description = "Extruder cooling fan.", Price = 19.00, Category = category_parts_coolingFans, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/fan03.jpg", Id = Guid.NewGuid().ToString()},

                // stepper motors
                new Product { Name = "MOT-06V", Description = "6V extruder stepper motor", Price = 35.00, Category = category_parts_stepperMotors, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/stepperMotor01.jpg", Id = Guid.NewGuid().ToString()},
                new Product { Name = "MOT-09V", Description = "9V extruder stepper motor", Price = 35.00, Category = category_parts_stepperMotors, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/stepperMotor02.jpg", Id = Guid.NewGuid().ToString()},
                new Product { Name = "MOT-12V", Description = "12V extruder stepper motor", Price = 35.00, Category = category_parts_stepperMotors, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/stepperMotor03.jpg", Id = Guid.NewGuid().ToString()},
            };

            // add the products to the context
            products.ForEach(p => context.Set<Product>().Add(p));

            base.Seed(context);
        }
    }
}

