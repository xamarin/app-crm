using Xamarin.Forms;
using MobileCRM.ViewModels.Sales;

namespace MobileCRM.Pages.Sales
{
    public class LeadDetailTabbedPage : TabbedPage
    {
        public LeadDetailTabbedPage(LeadDetailViewModel viewModel)
        {
            Children.Add(new LeadDetailPage()
                {
                    BindingContext = viewModel,
                    Title = TextResources.Details,
                    Icon = new FileImageSource() { File = "LeadDetailTab" }
                });

            Children.Add(new LeadContactDetailPage()
                {
                    BindingContext = viewModel,
                    Title = TextResources.Contact,
                    Icon = new FileImageSource() { File = "LeadContactDetailTab" }
                });
        }
    }
}
