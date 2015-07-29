using Xamarin.Forms;
using MobileCRM.Views.Base;
using MobileCRM.Localization;

namespace MobileCRM.Views
{
    public class IosTabbedPageHeaderView : BaseTabbedPageHeaderView
    {
        public IosTabbedPageHeaderView(string headerTitle, string doneActionTitle)
        {
            // a relative layout allows us to arrange visual elements in relationship to other visual elements, like parent views and sibling views.
            RelativeLayout relativeLayout = new RelativeLayout();

            #region back button
            BackButtonImage = new Image()
            {
                Source = "back_ios",
                Aspect = Aspect.AspectFit
            };

            // Padding for the back button image. 
            // The value doesn't actually get applied to any particular Padding property. It just us helps with the math of relatively positioning the back button image.
            const double paddingPercent = .25;

            relativeLayout.Children.Add(
                view: BackButtonImage,
                xConstraint: Constraint.RelativeToParent(parent => 0),
                yConstraint: Constraint.RelativeToParent(parent => parent.Height * paddingPercent),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Height - (parent.Height * paddingPercent * 2)),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height - (parent.Height * paddingPercent * 2))
            );
            #endregion

            #region outerContentView
            ContentView outerContentView = new ContentView()
            {
                Padding = new Thickness(0, 20, 0, 0)
            };
            #endregion

            #region setup innerContentView
            ContentView innerContentView = new ContentView()
            {
                Padding = new Thickness(10, 0), // give the content some padding on the left and right
                HeightRequest = 44, // set the height of the content view
            };
            #endregion

            #region back button label
            BackButtonLabel = new Label()
            {
                Text = TextResources.Back,
                TextColor = Palette._013,
                FontSize = Device.OnPlatform(
                    iOS: Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                    Android: Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                    WinPhone: Device.GetNamedSize(NamedSize.Medium, typeof(Label))),
                YAlign = TextAlignment.Center,
                XAlign = TextAlignment.Start
            };

            relativeLayout.Children.Add(
                view: BackButtonLabel,
                xConstraint: Constraint.RelativeToView(BackButtonImage, (parent, view) => view.Y + view.Width / 2),
                yConstraint: Constraint.RelativeToParent(parent => 0),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width * .15),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height)
            );
            #endregion

            #region title label
            if (!string.IsNullOrWhiteSpace(headerTitle))
            {
                var titleLabel = new Label()
                {
                    Text = headerTitle,
                    TextColor = Color.Black,
                    FontSize = Device.OnPlatform(
                        iOS: Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                        Android: Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        WinPhone: Device.GetNamedSize(NamedSize.Medium, typeof(Label))),
                    YAlign = TextAlignment.Center,
                    XAlign = TextAlignment.Start,
                    FontAttributes = FontAttributes.Bold,
                    LineBreakMode = LineBreakMode.TailTruncation
                };

                relativeLayout.Children.Add(
                    view: titleLabel,
                    xConstraint: Constraint.RelativeToParent(parent => parent.Width * .25),
                    yConstraint: Constraint.RelativeToParent(parent => 0),
                    widthConstraint: Constraint.RelativeToParent(parent => parent.Width * .50),
                    heightConstraint: Constraint.RelativeToParent(parent => parent.Height)
                );
            }
            #endregion

            #region done action title
            if (!string.IsNullOrWhiteSpace(doneActionTitle))
            {
                DoneActionLabel = new Label()
                {
                    Text = doneActionTitle,
                    TextColor = Palette._013,
                    FontSize = Device.OnPlatform(
                        iOS: Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                        Android: Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        WinPhone: Device.GetNamedSize(NamedSize.Medium, typeof(Label))),
                    YAlign = TextAlignment.Center,
                    XAlign = TextAlignment.End
                };

                relativeLayout.Children.Add(
                    view: DoneActionLabel,
                    xConstraint: Constraint.RelativeToParent(parent => parent.Width - (parent.Width * .25)),
                    yConstraint: Constraint.RelativeToParent(parent => 0),
                    widthConstraint: Constraint.RelativeToParent(parent => (parent.Width * .25)),
                    heightConstraint: Constraint.RelativeToParent(parent => parent.Height)
                );
            }
            #endregion

            #region compase the view hierarchy
            innerContentView.Content = relativeLayout;

            outerContentView.Content = innerContentView;

            Content = outerContentView;
            #endregion
        }
    }
}
