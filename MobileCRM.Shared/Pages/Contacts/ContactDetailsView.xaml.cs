using MobileCRM.Shared.Models;
using MobileCRM.Shared.ViewModels.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileCRM.Shared.Pages.Contacts
{
	public partial class ContactDetailsView
	{

		public ContactDetailsView (ContactDetailsViewModel viewModel)
		{
				InitializeComponent ();

				SetBinding(Page.TitleProperty, new Binding("Title"));
				SetBinding(Page.IconProperty, new Binding("Icon"));

				this.BindingContext = viewModel;

		} //end ctor


	} //end class
}
