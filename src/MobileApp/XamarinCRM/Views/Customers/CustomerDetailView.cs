using Xamarin.Forms;
using XamarinCRM.Layouts;
using XamarinCRM.Statics;
using XamarinCRM.ViewModels.Customers;
using XamarinCRM.Views.Base;
using XamarinCRM.Converters;

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
            gradientImage.SetBinding(Image.IsVisibleProperty, "IsLoading", converter: new InverseBooleanConverter());

            ActivityIndicator imageLoadingIndicator = new ActivityIndicator() { BindingContext = companyImage };
            imageLoadingIndicator.SetBinding(ActivityIndicator.IsEnabledProperty, "IsLoading");
            imageLoadingIndicator.SetBinding(ActivityIndicator.IsVisibleProperty, "IsLoading"); // here, since we're bound to the companyImage already, we can just reference the IsLoading property
            imageLoadingIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsLoading"); // here, since we're bound to the companyImage already, we can just reference the IsLoading property


            Label companyLabel = new Label()
            { 
                TextColor = Color.White, 
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Large, typeof(Label)), Device.GetNamedSize(NamedSize.Large, typeof(Label)), Device.GetNamedSize(NamedSize.Large, typeof(Label))),
                LineBreakMode = LineBreakMode.TailTruncation
            };
            companyLabel.SetBinding(Label.TextProperty, "Account.Company");
            companyLabel.SetBinding(VisualElement.IsEnabledProperty, new Binding("IsLoading", source: companyImage, converter: new InverseBooleanConverter())); // here, since we're alresdy bound to a different context, we can reference the IsLoading property of companyImage through the expanded form of the Binding constructor, specifying a soure
            companyLabel.SetBinding(VisualElement.IsVisibleProperty, new Binding("IsLoading", source: companyImage, converter: new InverseBooleanConverter())); // here, since we're alresdy bound to a different context, we can reference the IsLoading property of companyImage through the expanded form of the Binding constructor, specifying a soure

            Label industryLabel = new Label()
            { 
                TextColor = Palette._016,
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Small, typeof(Label)), Device.GetNamedSize(NamedSize.Small, typeof(Label)), Device.GetNamedSize(NamedSize.Small, typeof(Label))),
                LineBreakMode = LineBreakMode.TailTruncation
            };
            industryLabel.SetBinding(Label.TextProperty, "Account.Industry");
            industryLabel.SetBinding(VisualElement.IsEnabledProperty, new Binding("IsLoading", source: companyImage, converter: new InverseBooleanConverter())); // here, since we're alresdy bound to a different context, we can reference the IsLoading property of companyImage through the expanded form of the Binding constructor, specifying a soure
            industryLabel.SetBinding(VisualElement.IsVisibleProperty, new Binding("IsLoading", source: companyImage, converter: new InverseBooleanConverter())); // here, since we're alresdy bound to a different context, we can reference the IsLoading property of companyImage through the expanded form of the Binding constructor, specifying a soure

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

