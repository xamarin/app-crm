using MobileCRM.Models;
using Xamarin.Forms;
using MobileCRM.Statics;

namespace MobileCRM.Views.Products
{
    public class ProductDetailRibbonView : ContentView
    {
        public ProductDetailRibbonView(CatalogProduct catalogProduct)
        {
            BackgroundColor = Palette._009;

            HeightRequest = 20;

            Thickness padding = new Thickness(20, 15);

            Padding = padding;

            Image addToOrderImage = new Image()
            {
                Aspect = Aspect.AspectFit
            };
            Device.OnPlatform(
                iOS: () => addToOrderImage.Source = new FileImageSource(){ File = "add_ios_blue" }, 
                Android: () => addToOrderImage.Source = new FileImageSource() { File = "add_android_blue" }
            );

            Label addToOrderTextLabel = new Label()
            {
                Text = TextResources.Customers_Orders_EditOrder_AddToOrder.ToUpper(),
                TextColor = Palette._006,
                XAlign = TextAlignment.Start,
                YAlign = TextAlignment.Center,
            };

            Label priceValueLabel = new Label()
            {
                Text = string.Format("{0:C}", catalogProduct.Price),
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                XAlign = TextAlignment.End,
                YAlign = TextAlignment.Center
            };

            TapGestureRecognizer addToOrderTapGestureRecognizer = new TapGestureRecognizer()
            { 
                NumberOfTapsRequired = 1,
                Command = new Command( async () =>
                    {
                        MessagingCenter.Send<CatalogProduct>(catalogProduct, MessagingServiceConstants.ADD_PRODUCT_TO_ORDER);
                        await Navigation.PopModalAsync();
                    })
            };

            addToOrderImage.GestureRecognizers.Add(addToOrderTapGestureRecognizer);
            addToOrderTextLabel.GestureRecognizers.Add(addToOrderTapGestureRecognizer);

            RelativeLayout relativeLayout = new RelativeLayout();

            const double imagePaddingPercent = .35;

            relativeLayout.Children.Add(
                view: addToOrderImage,
                yConstraint: Constraint.RelativeToParent(parent => parent.Height * imagePaddingPercent),
                xConstraint: Constraint.RelativeToParent(parent => parent.Height * imagePaddingPercent),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Height - (parent.Height * imagePaddingPercent * 2)),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height - (parent.Height * imagePaddingPercent * 2)));

            relativeLayout.Children.Add(
                view: addToOrderTextLabel,
                xConstraint: Constraint.RelativeToView(addToOrderImage, (parent, view) => view.X + view.Width + parent.Height * imagePaddingPercent),
                widthConstraint: Constraint.RelativeToView(addToOrderImage, (parent, view) => parent.Width - view.Width),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height)
            );

            relativeLayout.Children.Add(
                view: priceValueLabel, 
                xConstraint: Constraint.RelativeToView(addToOrderTextLabel, (parent, view) => view.X + view.Width), 
                yConstraint: Constraint.RelativeToParent(parent => 0), 
                widthConstraint: Constraint.RelativeToView(addToOrderTextLabel, (parent, view) => parent.Width - view.X), 
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height));

            Padding = padding;

            Content = relativeLayout;
        }
    }
}


