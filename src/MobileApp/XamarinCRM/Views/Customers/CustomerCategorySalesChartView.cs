using System;
using XamarinCRM.Views.Base;
using Syncfusion.SfChart.XForms;

namespace XamarinCRM.Views.Customers
{
    public class CustomerCategorySalesChartView : ModelTypedContentView<CustomerSalesViewModel>
    {
        public CustomerCategorySalesChartView()
        {
            #region chart
            SfChart chart = new SfChart();
            #endregion
        }
    }
}

