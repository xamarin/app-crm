using MobileCRM.Shared.Models;
using MobileCRM.Shared.ViewModels.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileCRM.Shared.Pages.Accounts
{
	public partial class AccountContactView
	{
		public AccountContactView (AccountDetailsViewModel vm)
		{

				InitializeComponent();

				SetBinding(Page.TitleProperty, new Binding("Title"));
        //SetBinding(Page.IconProperty, new Binding("Icon"));

        this.Icon = "contactcard.png";

				this.BindingContext = vm;



		} //end ctor
	} //end class
} //end ns
