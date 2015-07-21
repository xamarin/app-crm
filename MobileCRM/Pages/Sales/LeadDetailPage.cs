using MobileCRM.Models;
using Xamarin.Forms;

namespace MobileCRM.Pages.Sales
{
    public class LeadDetailPage : BaseLeadDetailPage
    {
        public LeadDetailPage(string title, Account model = null)
            : base(title, "Done", model)
        {
            StackLayout.Children.Add(new Label()
            {
                Text = "Lead Details Content", 
                XAlign = TextAlignment.Center, 
                YAlign = TextAlignment.Center
            });
        }
    }
}
