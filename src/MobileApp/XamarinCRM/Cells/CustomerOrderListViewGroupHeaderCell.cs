using Xamarin.Forms;
using XamarinCRM.Converters;
using XamarinCRM.Statics;

namespace XamarinCRM
{
    public class CustomerOrderListViewGroupHeaderCell : ViewCell
    {
        public CustomerOrderListViewGroupHeaderCell()
        {
            var title = new Label
            {
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Medium, typeof(Label)), Device.GetNamedSize(NamedSize.Medium, typeof(Label)), Device.GetNamedSize(NamedSize.Default, typeof(Label))),
                TextColor = Color.White,
                VerticalOptions = LayoutOptions.Center,
                FontAttributes = FontAttributes.Bold
            };
            title.SetBinding(Label.TextProperty, "Key");

            var stackLayout = new StackLayout
            {
                BackgroundColor = Palette._003,
                Padding = new Thickness(5, 0, 0, 0),
                Orientation = StackOrientation.Horizontal,
                Children = { title }
            };
            stackLayout.SetBinding(StackLayout.BackgroundColorProperty, "Key", converter: new OrderListHeaderViewBackgroudColorConverter());

            View = stackLayout;
        }
    }
}

