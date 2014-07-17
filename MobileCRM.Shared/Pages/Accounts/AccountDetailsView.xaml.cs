using MobileCRM.CustomControls;
using MobileCRM.Shared.Models;
using MobileCRM.Shared.ViewModels.Accounts;
using MobileCRM.Shared.ViewModels.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;


namespace MobileCRM.Shared.Pages.Accounts
{
	public partial class AccountDetailsView
	{
		AccountDetailsViewModel viewModel;
		public AccountDetailsView(AccountDetailsViewModel vm)
		{
			InitializeComponent ();

			SetBinding(Page.TitleProperty, new Binding("Title"));
			SetBinding(Page.IconProperty, new Binding("Icon"));

			this.BindingContext = vm;


      var items = new List<BarItem>();
      items.Add(new BarItem { Name = "a", Value = 10 });
      items.Add(new BarItem { Name = "b", Value = 15 });
      items.Add(new BarItem { Name = "c", Value = 20 });
      items.Add(new BarItem { Name = "d", Value = 5 });
      items.Add(new BarItem { Name = "e", Value = 14 });
      Chart.Items = items;

			//TODO: Move map to its own tab

			//var pin = new Pin
			//{
			//  Type = PinType.Place,
			//  Position = new Position(vm.Account.Latitude, vm.Account.Longitude),
			//  Label = vm.Account.Company,
			//  Address = vm.Account.AddressString
			//};
			//MainMap.Pins.Add(pin);
			//MainMap.MoveToRegion(MapSpan.FromCenterAndRadius(pin.Position, Distance.FromMiles(.25)));
			
		}
	}
}
