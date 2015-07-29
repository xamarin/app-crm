using Xamarin.Forms;
using MobileCRM.ViewModels.Sales;

namespace MobileCRM.Pages.Sales
{
    public class LeadDetailTabbedPage : TabbedPage
    {
        public LeadDetailTabbedPage(LeadDetailViewModel viewModel)
        {
            Children.Add(new LeadDetailPage(viewModel)
                {
                    Title = TextResources.Details,
                    Icon = new FileImageSource() { File = "LeadDetailTab" }
                });

            Children.Add(new LeadContactDetailPage(viewModel)
                {
                    Title = TextResources.Contact,
                    Icon = new FileImageSource() { File = "LeadContactDetailTab" }
                });
        }
    }
}
