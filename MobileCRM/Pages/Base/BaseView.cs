using Xamarin.Forms;

namespace MobileCRM.Pages.Base
{
    public class BaseView : ContentPage
    {
        public BaseView()
        {
            SetBinding(TitleProperty, new Binding("Title"));
            SetBinding(IconProperty, new Binding("Icon"));
        }
    }
}
