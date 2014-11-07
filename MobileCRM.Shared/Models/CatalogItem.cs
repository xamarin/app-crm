using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCRM.Shared.Models
{
    public class CatalogItem
    {
        public const string ITEM_PAPER = "Paper";
        public const string ITEM_PRINTER = "Printer";
        public const string ITEM_SCANNER = "Scanner";
        public const string ITEM_INK = "Ink";
        public const string ITEM_COMBO = "Combo";

        public CatalogItem()
        {
            ItemName = ImageSrc = Description = string.Empty;
            SuggestedPrice = 0;
        } //end ctor

        public string ItemName { get; set; }

        public string ImageSrc { get; set; }

        public int SuggestedPrice { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }


        public static CatalogItem CreateCatalogItem(string catalogItem)
        {
            CatalogItem ci = null;

            switch (catalogItem)
            {
                case ITEM_PAPER:
                    ci = new CatalogItem()
                    {
                        ItemName = ITEM_PAPER,
                        ImageSrc = "catalog_paper.jpg",
                        SuggestedPrice = 200,
                        Summary = "Copy & Print Paper, 8 1/2 x 11, 20 Lb.",
                        Description = "Highest quality, brightest whites. Guaranteed to make TPS reports shine and please the Pointy Haired Boss",
                    };
                    break;

                case ITEM_PRINTER:
                    ci = new CatalogItem()
                    {
                        ItemName = ITEM_PRINTER,
                        ImageSrc = "catalog_printer.jpg",
                        SuggestedPrice = 800,
                        Summary = "Multifunction Laser All-In-One Color Printer.",
                        Description = "Prints up to 35 pages per minute. 50,000 sheet monthly volume. Compatible with PCs and Macs.",
                    };
                    break;

                case ITEM_SCANNER:
                    ci = new CatalogItem()
                    {
                        ItemName = ITEM_SCANNER,
                        ImageSrc = "catalog_scanner.jpg",
                        SuggestedPrice = 1500,
                        Summary = "XL9000 Industrial Scanner.",
                        Description =  "Efficient color scanning at 25 pages per minute. Automatic document feeder. Scans non-standard sizes.",
                    };
                    break;

                case ITEM_INK:
                    ci = new CatalogItem()
                    {
                        ItemName = ITEM_INK,
                        ImageSrc = "catalog_ink.jpg",
                        SuggestedPrice = 350,
                        Summary =  "Replacement CMYK Inkjet Ink.",
                        Description = "Compatible with all AP9000 printers. Prints up to 300 pages. Compatible with regular and high-gloss paper stock.",
                    };
                    break;

                case ITEM_COMBO:
                    ci = new CatalogItem()
                    {
                        ItemName = ITEM_COMBO,
                        ImageSrc = "catalog_combo.jpg",
                        SuggestedPrice = 2500,
                        Summary = "All In One Monthly Combo.",
                        Description = "Unlimited monthly supplies of paper, ink, and scanner and printer maintenance. Not valid for late-night office parties.",
                    };
                    break;
            }

            return ci;
        }

    }
}
