using MobileCRM.Pages.Base;
using MobileCRM.ViewModels.Accounts;
using Xamarin.Forms;

namespace MobileCRM.Pages.Accounts
{
    public class AccountNotesView : BaseView
    {
        AccountDetailsViewModel viewModel;

        public AccountNotesView(AccountDetailsViewModel vm)
        {
            SetBinding(TitleProperty, new Binding("Title"));
            SetBinding(IconProperty, new Binding("Icon"));
            this.BindingContext = viewModel = vm;
            this.Content = this.BuildView();
        }
        //end ctor

        StackLayout BuildView()
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
        }
        //end BuildView
    }
}