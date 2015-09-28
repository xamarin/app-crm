using Xamarin.Forms;
using XamarinCRM.Views.Base;
using XamarinCRM.ViewModels.Sales;

namespace XamarinCRM.Views.Sales
{
    public partial class SalesDashboardChartView : SalesDashboardChartViewXaml
    {
        public SalesDashboardChartView()
        {
            InitializeComponent();
        }
    }

    public partial class SalesDashboardChartViewXaml : ModelBoundContentView<SalesDashboardChartViewModel> { }
}

