using MobileCRM.Models;
using Xamarin.Forms;
using MobileCRM.Statics;

namespace MobileCRM.Views.Products
{
    public class ProductDetailRibbonView : ContentView
    {
        public ProductDetailRibbonView(CatalogProduct catalogProduct)
        {
            BackgroundColor = Palette._008;

            HeightRequest = 20;

            Thickness padding = new Thickness(20, 20);

            Padding = padding;

            Label nameLabel = new Label()
            { 
                Text = catalogProduct.Name.ToUpperInvariant(),
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                XAlign = TextAlignment.Start,
                YAlign = TextAlignment.Center,
                LineBreakMode = LineBreakMode.TailTruncation
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

            RelativeLayout relativeLayout = new RelativeLayout();

            relativeLayout.Children.Add(
                view: nameLabel, 
                xConstraint: Constraint.RelativeToParent(parent => 0), 
                yConstraint: Constraint.RelativeToParent(parent => 0), 
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width * 0.70), 
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height));

            relativeLayout.Children.Add(
                view: priceValueLabel, 
                xConstraint: Constraint.RelativeToView(nameLabel, (parent, siblingView) => siblingView.Width), 
                yConstraint: Constraint.RelativeToParent(parent => 0), 
                widthConstraint: Constraint.RelativeToView(nameLabel, (parent, siblingView) => parent.Width - siblingView.Width), 
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height));

            Padding = padding;

            Content = relativeLayout;
        }
    }
}


