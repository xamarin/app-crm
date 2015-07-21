using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using MobileCRM.Localization;
using MobileCRM.Models;
using Xamarin.Forms;

namespace MobileCRM.Pages.Sales
{
    public class LeadDetailTabbedPage : TabbedPage
    {
        public LeadDetailTabbedPage(Account model = null)
        {
            string headerTitle = (model != null) ? TextResources.Leads_EditLead : TextResources.Leads_NewLead;

            Children.Add(new LeadDetailPage(headerTitle, model)
            {
                Title = TextResources.Details
            });

            Children.Add(new LeadContactDetailPage(headerTitle, model)
            {
                Title = TextResources.Contact
            });
        }
    }
}
