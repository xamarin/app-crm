using Xamarin.Forms;
using XamarinCRM.Layouts;
using XamarinCRM.Statics;
using XamarinCRM.ViewModels.Customers;
using XamarinCRM.Views.Base;

namespace XamarinCRM.Views.Customers
{
    public class CustomerDetailHeaderView : ModelTypedContentView<CustomerDetailViewModel>
    {
        public CustomerDetailHeaderView()
        {
            AbsoluteLayout absoluteLayout = new AbsoluteLayout() { HeightRequest = 150 };

            StackLayout stackLayout = new UnspacedStackLayout() { Padding = new Thickness(20) };

            Image companyImage = new Image() { Aspect = Aspect.AspectFill };
            companyImage.SetBinding(Image.SourceProperty, "Account.ImageUrl");

            Image gradientImage = new Image() { Aspect = Aspect.Fill, Source = new FileImageSource() { File = "bottom_up_gradient" }, HeightRequest = 75, BindingContext = companyImage };
            gradientImage.SetBinding(Image.IsVisibleProperty, "IsLoading", converter: new InvertedBooleanConverter());

            ActivityIndicator imageLoadingIndicator = new ActivityIndicator() { BindingContext = companyImage };
            imageLoadingIndicator.SetBinding(ActivityIndicator.IsEnabledProperty, "IsLoading");
            imageLoadingIndicator.SetBinding(ActivityIndicator.IsVisibleProperty, "IsLoading");
            imageLoadingIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsLoading");

            Label companyLabel = new Label()
            { 
                TextColor = Color.White, 
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Large, typeof(Label)), Device.GetNamedSize(NamedSize.Large, typeof(Label)), Device.GetNamedSize(NamedSize.Large, typeof(Label))),
                LineBreakMode = LineBreakMode.TailTruncation
            };
            companyLabel.SetBinding(Label.TextProperty, "Account.Company");

            Label industryLabel = new Label()
            { 
                TextColor = Palette._016,
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Small, typeof(Label)), Device.GetNamedSize(NamedSize.Small, typeof(Label)), Device.GetNamedSize(NamedSize.Small, typeof(Label))),
                LineBreakMode = LineBreakMode.TailTruncation
            };
            industryLabel.SetBinding(Label.TextProperty, "Account.Industry");

            stackLayout.Children.Add(companyLabel);
            stackLayout.Children.Add(industryLabel);

            absoluteLayout.Children.Add(companyImage, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
            absoluteLayout.Children.Add(imageLoadingIndicator, new Rectangle(0, .5, 1, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.WidthProportional | AbsoluteLayoutFlags.PositionProportional);
            absoluteLayout.Children.Add(gradientImage, new Rectangle(0, 1, 1, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.WidthProportional | AbsoluteLayoutFlags.PositionProportional);
            absoluteLayout.Children.Add(stackLayout, new Rectangle(0, 1, 1, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.WidthProportional | AbsoluteLayoutFlags.PositionProportional);

            Content = absoluteLayout;
        }
    }
}

