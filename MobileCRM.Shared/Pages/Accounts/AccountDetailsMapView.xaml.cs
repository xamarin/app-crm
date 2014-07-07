using MobileCRM.Shared.Helpers;
using MobileCRM.Shared.ViewModels.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace MobileCRM.Shared.Pages.Accounts
{
	public partial class AccountDetailsMapView
	{
    AccountDetailsViewModel viewModel;
		public AccountDetailsMapView (AccountDetailsViewModel viewModel)
		{
			InitializeComponent ();
      this.BindingContext = this.viewModel = viewModel;


      UpdateCell.Tapped += async (sender, args) =>
        {
          var pin = await viewModel.UpdateAddressAsync();
          MyMap.Pins.Clear();
          MyMap.Pins.Add(pin);


          MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(pin.Position, Distance.FromMiles(0.3)));
       
        };
		}
	}
}
