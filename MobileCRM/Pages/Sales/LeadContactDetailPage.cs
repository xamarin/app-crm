using MobileCRM.Models;
using Xamarin.Forms;

namespace MobileCRM.Pages.Sales
{
    public class LeadContactDetailPage : BaseLeadDetailPage
    {
        public LeadContactDetailPage(string title, Account model = null)
            : base(title, "Done", model)
        {
            StackLayout.Children.Add(new Label()
            {
                Text = "Contact Details Content", 
                XAlign = TextAlignment.Center, 
                YAlign = TextAlignment.Center
            });
        }
    }
}
