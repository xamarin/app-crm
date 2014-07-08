using MobileCRM.Shared.Models;
using MobileCRM.Shared.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCRM.Shared.Pages.Accounts
{
	public partial class AccountOrderDetailsView
	{
    private OrderDetailsViewModel viewModel;
		public AccountOrderDetailsView (Order order)
		{
			InitializeComponent ();
      this.BindingContext = this.viewModel = new OrderDetailsViewModel(Navigation, order);
		}


    public void ApproveOrderClicked(object sender, EventArgs e)
    {
      var signature = string.Empty;
      if(Signature.GetPointString != null)
        signature = Signature.GetPointString.Invoke();

      if(string.IsNullOrWhiteSpace(signature) || signature == "[]")
      {
        DisplayAlert("Error", "Signature required", "OK", null);
        return;
      }
      viewModel.Order.Signature = signature;
      viewModel.ApproveOrderCommand.Execute(null);
    }


    public void CancelClicked(object sender, EventArgs e)
    {
      Navigation.PopModalAsync();
    }
	}
}
