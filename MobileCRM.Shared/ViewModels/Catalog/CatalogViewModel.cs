using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MobileCRM.Shared.Interfaces;
using MobileCRM.Shared.Models;

namespace MobileCRM.Shared.ViewModels.Catalog
{
    public class CatalogViewModel : BaseViewModel
    {
        public CatalogViewModel()
        {
            this.Title = "Product Catalog";
            this.Icon = "list.png";
        } //end ctor

    }
}
