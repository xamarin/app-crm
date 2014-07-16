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

        //ToolbarItems.Add(new ToolbarItem("Done", null, async () =>
        //{
        //    //var confirmed = await DisplayAlert("Unsaved Changes", "Save changes?", "Save", "Discard");
        //    var confirmed = await DisplayAlert("Save Changes?", "", "Cancel", "Save");
        //    if (!confirmed)
        //    {
        //        // TODO: Tell the view model, aka BindingContext, to save.
        //        viewModel.SaveContactCommand.Execute(null);
					
        //    }
        //    else
        //    {
        //        await viewModel.GoBack();
        //        Console.WriteLine("cancel changes!");
        //    }
        //}));

		} //end ctor


	} //end class
}
