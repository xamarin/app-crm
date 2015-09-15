using XamarinCRM.Models;
using Xamarin.Forms;
using XamarinCRM.Statics;

namespace XamarinCRM.Views.Products
{
    public class ProductDetailDescriptionView : ContentView
    {
        public ProductDetailDescriptionView(CatalogProduct catalogProduct)
        {
            Label nameLabel = new Label()
            { 
                Text = catalogProduct.Name,
                TextColor = Device.OnPlatform(Palette._006, Palette._006, Palette._013),
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };
            
            Label descriptionLabel = new Label()
            { 
                Text = catalogProduct.Description,
                TextColor = Palette._007,
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label))
            };

            Thickness padding = new Thickness(20);

            RelativeLayout relativeLayout = new RelativeLayout();

            relativeLayout.Children.Add(
                view: nameLabel, 
                xConstraint: Constraint.RelativeToParent(parent => 0),
                yConstraint: Constraint.RelativeToParent(parent => 0),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width)
            );

            relativeLayout.Children.Add(
                view: descriptionLabel, 
                xConstraint: Constraint.RelativeToParent(parent => 0),
                yConstraint: Constraint.RelativeToView(nameLabel, (parent, siblingView) => siblingView.Height + padding.VerticalThickness / 2),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width)
            );

            Padding = padding;

            Content = relativeLayout;
        }
    }
}


