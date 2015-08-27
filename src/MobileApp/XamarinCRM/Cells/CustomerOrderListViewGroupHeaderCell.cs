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

            var contentView = new ContentView() { Content = title, HeightRequest = Sizes.MediumRowHeight, Padding = new Thickness(10, 0) };

            contentView.SetBinding(StackLayout.BackgroundColorProperty, "Key", converter: new OrderListHeaderViewBackgroudColorConverter());

            View = contentView;
        }
    }
}

