using MobileCRM.Localization;
using Xamarin.Forms;

namespace MobileCRM.Views.Sales
{
    public class SalesChartHeaderView : ContentView
    {
        public Label WeeklyAverageValueLabel;

        public SalesChartHeaderView()
        {
            RelativeLayout relativeLayout = new RelativeLayout();

            Label headerTitleLabel = new Label()
            { 
                Text = TextResources.SalesChartHeaderTitle,
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                XAlign = TextAlignment.Start,
                YAlign = TextAlignment.Center
            };

            Device.OnPlatform(iOS: () => headerTitleLabel.TextColor = Color.Black);

            Label weeklyAverageTitleLabel = new Label()
            {
                Text = TextResources.SalesChartHeaderWeeklyAverageTitle.ToUpperInvariant(),
                TextColor = Palette._011,
                FontSize = Device.OnPlatform(
                    iOS: Device.GetNamedSize(NamedSize.Micro, typeof(Label)) * .9,
                    Android: Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                    WinPhone: Device.GetNamedSize(NamedSize.Micro, typeof(Label))),
                XAlign = TextAlignment.End,
                YAlign = TextAlignment.Center
            };

            WeeklyAverageValueLabel = new Label()
            {
                TextColor = Color.White,
                FontSize = Device.OnPlatform(
                    iOS: Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                    Android: Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                    WinPhone: Device.GetNamedSize(NamedSize.Large, typeof(Label))) * 1.1,
                XAlign = TextAlignment.End,
                YAlign = TextAlignment.End
            };

            Device.OnPlatform(
                iOS: () => WeeklyAverageValueLabel.TextColor = Color.Black,
                Android: () => WeeklyAverageValueLabel.FontAttributes = FontAttributes.Bold);

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

            BackgroundColor = Palette._008;

            Device.OnPlatform(iOS: () => BackgroundColor = Color.White, Android: () => BackgroundColor = Palette._008);

            HeightRequest = 40;

            Padding = new Thickness(20, 10, 20, 0);

            Content = relativeLayout;
        }
    }
}


