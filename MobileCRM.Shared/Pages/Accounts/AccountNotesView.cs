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
    public class AccountNotesView : BaseView
    {

        AccountDetailsViewModel viewModel;

        public AccountNotesView(AccountDetailsViewModel vm)
        {

            SetBinding(Page.TitleProperty, new Binding("Title"));
            SetBinding(Page.IconProperty, new Binding("Icon"));

            this.BindingContext = viewModel = vm;

            this.Content = this.BuildView();

        } //end ctor

        private StackLayout BuildView()
        {
            Editor editor = new Editor();
            editor.SetBinding(Editor.TextProperty, "Account.Notes");
         
            TableView tblView = new TableView() 
            {
                Root = new TableRoot() 
                {
                    new TableSection("NOTES")
                }
            };

            StackLayout stack = new StackLayout()
            {
                Padding = 10,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = 
                {
                    tblView,
                    editor
                }
            };

            return stack;
        } //end BuildView

    }
}
