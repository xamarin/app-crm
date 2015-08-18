using Xamarin.Forms;
using XamarinCRM.Statics;

namespace XamarinCRM.Views.Sales
{
    public class SalesChartHeaderView : ContentView
    {
        public Label WeeklyAverageValueLabel;

        public SalesChartHeaderView()
        {
            Label headerTitleLabel = new Label()
            { 
                Text = TextResources.SalesDashboard_SalesChart_Header_Title,
                TextColor = Device.OnPlatform(Palette._006, Color.White, Color.White),
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                XAlign = TextAlignment.Start,
                YAlign = TextAlignment.Start
            };

            Label weeklyAverageTitleLabel = new Label()
            {
                Text = TextResources.SalesDashboard_SalesChart_Header_WeeklyAverageTitle.ToUpperInvariant(),
                TextColor = Palette._007,
                FontSize = Device.OnPlatform(
                    iOS: Device.GetNamedSize(NamedSize.Micro, typeof(Label)) * .9,
                    Android: Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                    WinPhone: Device.GetNamedSize(NamedSize.Micro, typeof(Label))),
                XAlign = TextAlignment.End,
                YAlign = TextAlignment.Center
            };

            WeeklyAverageValueLabel = new Label()
            {
                TextColor = Device.OnPlatform(Palette._006, Color.White, Color.White),
                FontSize = Device.OnPlatform(
                    iOS: Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                    Android: Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                    WinPhone: Device.GetNamedSize(NamedSize.Large, typeof(Label))) * 1.1,
                XAlign = TextAlignment.End,
                YAlign = TextAlignment.End
            };

            Device.OnPlatform(
                Android: () => WeeklyAverageValueLabel.FontAttributes = FontAttributes.Bold);

            RelativeLayout relativeLayout = new RelativeLayout();

            relativeLayout.Children.Add(
                view: headerTitleLabel,
                xConstraint: Constraint.RelativeToParent(parent => 0),
                yConstraint: Constraint.RelativeToParent(parent => 0),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width / 2),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height)
            );
                
            relativeLayout.Children.Add(
                view: weeklyAverageTitleLabel,
                xConstraint: Constraint.RelativeToParent(parent => parent.Width / 2),
                yConstraint: Constraint.RelativeToParent(parent => 0),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width / 2),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height / 4)
            );
           
            relativeLayout.Children.Add(
                view: WeeklyAverageValueLabel,
                xConstraint: Constraint.RelativeToParent(parent => parent.Width / 2),
                yConstraint: Constraint.RelativeToParent(parent => parent.Height / 4),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width / 2),
                heightConstraint: Constraint.RelativeToParent(parent => (parent.Height / 4) * 3)
            );

            BackgroundColor = Palette._009;

            Device.OnPlatform(iOS: () => BackgroundColor = Color.White, Android: () => BackgroundColor = Palette._009);

            HeightRequest = Sizes.MediumRowHeight;

            Padding = new Thickness(20, 20, 20, 0);

            Content = relativeLayout;
        }
    }
}


