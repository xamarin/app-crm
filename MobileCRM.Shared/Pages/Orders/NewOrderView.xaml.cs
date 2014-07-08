using MobileCRM.Shared.Models;
using MobileCRM.Shared.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCRM.Shared.Pages.Accounts
{
	public partial class NewOrderView
	{
    NewOrderViewModel viewModel;
		public NewOrderView (string accountId)
		{
			InitializeComponent ();
      this.BindingContext = this.viewModel = new NewOrderViewModel(Navigation, accountId);

      foreach (var item in Order.ItemTypes)
      {
        ItemPicker.Items.Add(item);
      }

      ItemPicker.SelectedIndex = viewModel.Item;

      foreach (var item in Order.ItemLevels)
      {
        ItemLevelPicker.Items.Add(item);
      }

      ItemLevelPicker.SelectedIndex = viewModel.Item;
		}
	}
}
