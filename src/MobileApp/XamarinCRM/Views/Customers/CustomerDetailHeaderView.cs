using Xamarin.Forms;
using XamarinCRM.Layouts;
using XamarinCRM.Statics;
using XamarinCRM.ViewModels.Customers;
using XamarinCRM.Views.Base;
using XamarinCRM.Converters;

namespace XamarinCRM.Views.Customers
{
    public class CustomerDetailHeaderView : ModelBoundContentView<CustomerDetailViewModel>
    {
        public CustomerDetailHeaderView()
        {
            #region company image
            Image companyImage = new Image() { Aspect = Aspect.AspectFill };
            companyImage.SetBinding(Image.SourceProperty, "Account.ImageUrl");
            #endregion

            #region gradient image
            Image gradientImage = new Image() { Aspect = Aspect.Fill, Source = new FileImageSource() { File = "bottom_up_gradient" }, HeightRequest = 75, BindingContext = companyImage };
            gradientImage.SetBinding(Image.IsVisibleProperty, "IsLoading", converter: new InverseBooleanConverter());
            #endregion

            #region activity indicator
            ActivityIndicator imageLoadingIndicator = new ActivityIndicator() { BindingContext = companyImage };
            imageLoadingIndicator.SetBinding(ActivityIndicator.IsEnabledProperty, "IsLoading");
            imageLoadingIndicator.SetBinding(ActivityIndicator.IsVisibleProperty, "IsLoading"); // here, since we're bound to the companyImage already, we can just reference the IsLoading property
            imageLoadingIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsLoading"); // here, since we're bound to the companyImage already, we can just reference the IsLoading property
            #endregion

            #region company label
            Label companyLabel = new Label()
            { 
                TextColor = Color.White, 
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Large, typeof(Label)), Device.GetNamedSize(NamedSize.Large, typeof(Label)), Device.GetNamedSize(NamedSize.Large, typeof(Label))),
                LineBreakMode = LineBreakMode.TailTruncation
            };
            companyLabel.SetBinding(Label.TextProperty, "Account.Company");
            companyLabel.SetBinding(Label.TextColorProperty, new Binding("IsLoading", source: companyImage, converter: new CompanyLabelBooleanToColorConverter()));
            #endregion

            #region industry label
            Label industryLabel = new Label()
            { 
                TextColor = Palette._008,
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Small, typeof(Label)), Device.GetNamedSize(NamedSize.Small, typeof(Label)), Device.GetNamedSize(NamedSize.Small, typeof(Label))),
                LineBreakMode = LineBreakMode.TailTruncation
            };
            industryLabel.SetBinding(Label.TextProperty, "Account.Industry");
            industryLabel.SetBinding(Label.TextColorProperty, new Binding("IsLoading", source: companyImage, converter: new IndustryLabelBooleanToColorConverter()));
            #endregion

            #region compose view hierarchy
            StackLayout stackLayout = new UnspacedStackLayout()
            {
                Children =
                {
                    companyLabel,
                    industryLabel
                },
                Padding = new Thickness(20) 
            };
            AbsoluteLayout absoluteLayout = new AbsoluteLayout() { HeightRequest = 150 };
            absoluteLayout.Children.Add(companyImage, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
            absoluteLayout.Children.Add(imageLoadingIndicator, new Rectangle(0, .5, 1, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.WidthProportional | AbsoluteLayoutFlags.PositionProportional);
            absoluteLayout.Children.Add(gradientImage, new Rectangle(0, 1, 1, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.WidthProportional | AbsoluteLayoutFlags.PositionProportional);
            absoluteLayout.Children.Add(stackLayout, new Rectangle(0, 1, 1, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.WidthProportional | AbsoluteLayoutFlags.PositionProportional);
            #endregion

            Content = absoluteLayout;
        }
    }
}

