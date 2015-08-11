using MobileCRM.Pages.Base;
using Xamarin.Forms;
using MobileCRM.Layouts;
using MobileCRM.ViewModels.Customers;

namespace MobileCRM.Pages.Accounts
{
    public class AccountNotesView : BaseView
    {
        CustomerDetailViewModel viewModel;

        public AccountNotesView(CustomerDetailViewModel vm)
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

            StackLayout stack = new UnspacedStackLayout()
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