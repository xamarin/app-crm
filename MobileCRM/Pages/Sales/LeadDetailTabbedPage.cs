using Xamarin.Forms;
using MobileCRM.ViewModels.Sales;
using MobileCRM.Localization;

namespace MobileCRM.Pages.Sales
{
    public class LeadDetailTabbedPage : TabbedPage
    {
        public LeadDetailTabbedPage(LeadDetailViewModel viewModel)
        {
            Children.Add(new LeadDetailPage(viewModel)
                {
                    Title = TextResources.Details,
//                    Icon = new FileImageSource() { File = "" }
                });

            Children.Add(new LeadContactDetailPage(viewModel)
                {
                    Title = TextResources.Contact,
//                    Icon = new FileImageSource() { File = "" }
                });
        }
    }
}
