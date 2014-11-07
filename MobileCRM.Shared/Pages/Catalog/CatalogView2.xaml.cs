using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MobileCRM.Shared.ViewModels.Catalog;


namespace MobileCRM.Shared.Pages.Catalog
{
    public partial class CatalogView2
    {
        public CatalogView2(CatalogViewModel vm)
        {
            InitializeComponent();

            this.BindingContext = vm;
        }
    }
}
