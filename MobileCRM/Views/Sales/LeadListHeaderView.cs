using System;
using MobileCRM.Localization;
using Xamarin.Forms;
using MobileCRM.Statics;

namespace MobileCRM.Views.Sales
{
    public class LeadListHeaderView : ContentView
    {
        public Action _NewLeadClickedAction;

        public LeadListHeaderView(Action newLeadClickedAction)
        {
            _NewLeadClickedAction = newLeadClickedAction;

            #region outerContentView
            ContentView outerContentView = new ContentView();
            #endregion

            #region setup innerContentView
            ContentView innerContentView = new ContentView()
            {
                Padding = new Thickness(10, 0, 0, 0), // give the content some padding on the left and right
                HeightRequest = 44, // set the height of the content view
            };

            Device.OnPlatform(Android: () => innerContentView.Padding = new Thickness(10, 0));
            #endregion

            #region setup labels
            // The "LEADS" label in the header
            Label headerTitleLabel = new Label()
            {
                Text = TextResources.Leads_LeadListHeaderTitle.ToUpperInvariant(),
                TextColor = Palette._005,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                XAlign = TextAlignment.Start,
                YAlign = TextAlignment.Center
            };

            // The "+" button for adding new leads
            Label newLeadLabel = new Label()
            {
                Text = "+",
                TextColor = Color.Gray,
                BackgroundColor = Color.Transparent,
                FontSize = Device.OnPlatform(
                    iOS: Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                    Android: Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                    WinPhone: Device.GetNamedSize(NamedSize.Large, typeof(Label))) * 1.5,
                XAlign = TextAlignment.Center,
                YAlign = TextAlignment.Center
            };

            newLeadLabel.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() => _NewLeadClickedAction.Invoke()),
                NumberOfTapsRequired = 1
            });

            Device.OnPlatform(iOS: () => newLeadLabel.FontAttributes = FontAttributes.None);
            #endregion

            #region add elements to relative layout
            RelativeLayout relativeLayout = new RelativeLayout();

            relativeLayout.Children.Add(
                view: headerTitleLabel,
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width * .75),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height)
            );

            relativeLayout.Children.Add(
                view: newLeadLabel,
                xConstraint: Constraint.RelativeToParent(parent => parent.Width - parent.Height),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Height),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height)
            );
            #endregion

            #region compose the view hierarchy
            innerContentView.Content = relativeLayout;

            outerContentView.Content = innerContentView;

            Content = outerContentView;
            #endregion
        }
    }
}
