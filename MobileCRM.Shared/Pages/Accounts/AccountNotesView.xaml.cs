using MobileCRM.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCRM.Shared.Pages.Accounts
{
	public partial class AccountNotesView
	{
		public AccountNotesView (Account account)
		{
			InitializeComponent ();
      this.BindingContext = account;
		}
	}
}
