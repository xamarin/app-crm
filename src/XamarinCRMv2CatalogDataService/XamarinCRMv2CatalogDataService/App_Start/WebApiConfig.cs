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
#endif

            // To display errors in the browser during development, uncomment the following
            // line. Comment it out again when you deploy your service for production use.

#if DEBUG
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
#endif

            Database.SetInitializer(new MobileServiceInitializer());
        }
    }

    // using ClearDatabaseSchemaAlways<T> here instead of DropCreateDatabaseAlways<T> is necessary 
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
            var category_printerKits = new Category { Name = "3D Printer Kits", Description = "Complete 3D printer kits.", ParentCategory = rootCategory, Id = Guid.NewGuid().ToString() };
            var category_filament = new Category { Name = "3D Filament", Description = "The plastic material which 3D-printed objects are made of.", ParentCategory = rootCategory, Id = Guid.NewGuid().ToString() };
            var category_parts = new Category { Name = "Parts", Description = "Parts for your 3D printer", ParentCategory = rootCategory, Id = Guid.NewGuid().ToString() };

            // ptinter categories
            var category_printerKits_abs = new Category { Name = "ABS 3D Printer Kits", Description = "Printer kits that are pre-configured for ABS filament.", ParentCategory = category_printerKits, Id = Guid.NewGuid().ToString() };
            var category_printerKits_pla = new Category { Name = "PLA 3D Printer Kits", Description = "Printer kits that are pre-configured for PLA filament.", ParentCategory = category_printerKits, Id = Guid.NewGuid().ToString() };

            // fialment categories
            var category_filament_abs = new Category { Name = "ABS Filament", Description = "A petroleum-based high-temp plastic, suitable for engineering applications.", ParentCategory = category_filament, Id = Guid.NewGuid().ToString() };
            var category_filament_pla = new Category { Name = "PLA Filament", Description = "A plant-based non-toxic low-warp plastic, suitable for hobbyists.", ParentCategory = category_filament, Id = Guid.NewGuid().ToString() };

            // parts categories
            var category_parts_extruders = new Category { Name = "Extruders", Description = "The printhead that extrudes the filament into shapes.", ParentCategory = category_parts, Id = Guid.NewGuid().ToString() };
            var category_parts_buildPlates = new Category { Name = "Build Plates", Description = "The flat surface upon which 3D objects are printed.", ParentCategory = category_parts, Id = Guid.NewGuid().ToString() };
            var category_parts_coolingFans = new Category { Name = "Cooling Fans", Description = "Extruders get hot. Keep them cool with fans!", ParentCategory = category_parts, Id = Guid.NewGuid().ToString() };
            var category_parts_stepperMotors = new Category { Name = "Stepper Motors", Description = "Precise motors are an important part of 3D printers.", ParentCategory = category_parts, Id = Guid.NewGuid().ToString() };

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

            var categories = new List<Category>
            {
                rootCategory
            };

            // add the categories to the context
            categories.ForEach(c => context.Set<Category>().Add(c));

            // setup products

            var products = new List<Product>
            {
                // ABS printers
                new Product { Name = "ABS Printer 1", Description = "A great ABS printer for light industrial applications", Category = category_printerKits_abs, Price = 324.99, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/printer01.jpg", Id = Guid.NewGuid().ToString()},
                new Product { Name = "ABS Printer 2", Description = "A great ABS printer for light industrial applications", Category = category_printerKits_abs, Price = 349.99, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/printer02.jpg", Id = Guid.NewGuid().ToString() },
                new Product { Name = "ABS Printer 3", Description = "A great ABS printer for light industrial applications", Category = category_printerKits_abs, Price = 374.99, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/printer03.jpg", Id = Guid.NewGuid().ToString() },
                new Product { Name = "ABS Printer 4", Description = "A great ABS printer for light industrial applications", Category = category_printerKits_abs, Price = 399.99, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/printer04.jpg", Id = Guid.NewGuid().ToString() },

                // PLA printers
                new Product { Name = "PLA Printer 1", Description = "A great PLA printer for hobbyist projects", Category = category_printerKits_pla, Price = 324.99, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/printer05.jpg", Id = Guid.NewGuid().ToString() },
                new Product { Name = "PLA Printer 2", Description = "A great PLA printer for hobbyist projects", Category = category_printerKits_pla, Price = 349.99, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/printer06.jpg", Id = Guid.NewGuid().ToString() },
                new Product { Name = "PLA Printer 3", Description = "A great PLA printer for hobbyist projects", Category = category_printerKits_pla, Price = 374.99, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/printer07.jpg", Id = Guid.NewGuid().ToString() },
                new Product { Name = "PLA Printer 4", Description = "A great PLA printer for hobbyist projects", Category = category_printerKits_pla, Price = 399.99, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/printer08.jpg", Id = Guid.NewGuid().ToString() },
                new Product { Name = "PLA Printer 5", Description = "A great PLA printer for hobbyist projects", Category = category_printerKits_pla, Price = 424.99, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/printer09.jpg", Id = Guid.NewGuid().ToString() },

                // ABS filament
                new Product { Name = "ABS Filament - Red", Description = "Red ABS filement, red, 500 grams", Price = 24.99, Category = category_filament_abs, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/filament-red.jpg", Id = Guid.NewGuid().ToString() },
                new Product { Name = "ABS Filament - Orange", Description = "Orange ABS filement, orange, 500 grams", Price = 24.99, Category = category_filament_abs, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/filament-orange.jpg", Id = Guid.NewGuid().ToString() },
                new Product { Name = "ABS Filament - Yellow", Description = "Yellow ABS filement, yellow, 500 grams", Price = 24.99, Category = category_filament_abs, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/filament-yellow.jpg", Id = Guid.NewGuid().ToString() },
                new Product { Name = "ABS Filament - Green", Description = "Green ABS filement, green, 500 grams", Price = 24.99, Category = category_filament_abs, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/filament-green.jpg", Id = Guid.NewGuid().ToString() },
                new Product { Name = "ABS Filament - Blue", Description = "Blue ABS filement, blue, 500 grams", Price = 24.99, Category = category_filament_abs, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/filament-blue.jpg", Id = Guid.NewGuid().ToString() },
                new Product { Name = "ABS Filament - Indigo", Description = "Indigo ABS filement, indigo, 500 grams", Price = 24.99, Category = category_filament_abs, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/filament-indigo.jpg", Id = Guid.NewGuid().ToString() },
                new Product { Name = "ABS Filament - Violet", Description = "Violet ABS filement, violet, 500 grams", Price = 24.99, Category = category_filament_abs, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/filament-violet.jpg", Id = Guid.NewGuid().ToString() },

                // PLA filament
                new Product { Name = "PLA Filament - Red", Description = "Red PLA filement, red, 500 grams", Price = 34.99, Category = category_filament_pla, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/filament-red.jpg", Id = Guid.NewGuid().ToString() },
                new Product { Name = "PLA Filament - Orange", Description = "Orange PLA filement, orange, 500 grams", Price = 34.99, Category = category_filament_pla, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/filament-orange.jpg", Id = Guid.NewGuid().ToString() },
                new Product { Name = "PLA Filament - Yellow", Description = "Yellow PLA filement, yellow, 500 grams", Price = 34.99, Category = category_filament_pla, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/filament-yellow.jpg", Id = Guid.NewGuid().ToString() },
                new Product { Name = "PLA Filament - Green", Description = "Green PLA filement, green, 500 grams", Price = 34.99, Category = category_filament_pla, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/filament-green.jpg", Id = Guid.NewGuid().ToString() },
                new Product { Name = "PLA Filament - Blue", Description = "Blue PLA filement, blue, 500 grams", Price = 34.99, Category = category_filament_pla, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/filament-blue.jpg", Id = Guid.NewGuid().ToString() },
                new Product { Name = "PLA Filament - Indigo", Description = "Indigo PLA filement, indigo, 500 grams", Price = 34.99, Category = category_filament_pla, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/filament-indigo.jpg", Id = Guid.NewGuid().ToString() },
                new Product { Name = "PLA Filament - Violet", Description = "Violet PLA filement, violet, 500 grams", Price = 34.99, Category = category_filament_pla, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/filament-violet.jpg", Id = Guid.NewGuid().ToString() },

                // extruders
                new Product { Name = "Extruder 1", Description = "A great extruder for ABS filament.", Price = 49.99, Category = category_parts_extruders, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/extruder01.jpg", Id = Guid.NewGuid().ToString()},
                new Product { Name = "Extruder 2", Description = "A great extruder for ABS filament.", Price = 52.99, Category = category_parts_extruders, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/extruder02.jpg", Id = Guid.NewGuid().ToString()},
                new Product { Name = "Extruder 3", Description = "A great extruder for PLA filament.", Price = 67.99, Category = category_parts_extruders, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/extruder03.jpg", Id = Guid.NewGuid().ToString()},
                new Product { Name = "Extruder 4", Description = "A great extruder for PLA filament.", Price = 69.99, Category = category_parts_extruders, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/extruder04.jpg", Id = Guid.NewGuid().ToString()},

                // build plates
                new Product { Name = "ABS Build Plate", Description = "A heated build plate for ABS filament projects.", Price = 15.00, Category = category_parts_buildPlates, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/buildPlate02.jpg", Id = Guid.NewGuid().ToString()},
                new Product { Name = "PLA Build Plate", Description = "A build plate for PLA filament projects.", Price = 15.00, Category = category_parts_buildPlates, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/buildPlate01.jpg", Id = Guid.NewGuid().ToString()},

                // cooling fans
                new Product { Name = "Cooling Fan 1", Description = "Extruder cooling fan.", Price = 19.00, Category = category_parts_coolingFans, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/fan01.jpg", Id = Guid.NewGuid().ToString()},
                new Product { Name = "Cooling Fan 2", Description = "Extruder cooling fan.", Price = 19.00, Category = category_parts_coolingFans, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/fan02.jpg", Id = Guid.NewGuid().ToString()},
                new Product { Name = "Cooling Fan 3", Description = "Extruder cooling fan.", Price = 19.00, Category = category_parts_coolingFans, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/fan03.jpg", Id = Guid.NewGuid().ToString()},

                // stepper motors
                new Product { Name = "Stepper Motor 1", Description = "Ultra-precise extruder stepper motor.", Price = 35.00, Category = category_parts_stepperMotors, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/stepperMotor01.jpg", Id = Guid.NewGuid().ToString()},
                new Product { Name = "Stepper Motor 2", Description = "Ultra-precise extruder stepper motor.", Price = 35.00, Category = category_parts_stepperMotors, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/stepperMotor02.jpg", Id = Guid.NewGuid().ToString()},
                new Product { Name = "Stepper Motor 3", Description = "Ultra-precise extruder stepper motor.", Price = 35.00, Category = category_parts_stepperMotors, ImageUrl = "https://xamarincrmv2.blob.core.windows.net/images/stepperMotor03.jpg", Id = Guid.NewGuid().ToString()},
            };

            // add the products to the context
            products.ForEach(p => context.Set<Product>().Add(p));

            base.Seed(context);
        }
    }
}

