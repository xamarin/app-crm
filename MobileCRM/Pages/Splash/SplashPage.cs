using System;
using Xamarin.Forms;
using MobileCRM.Statics;
using MobileCRM.ViewModels.Splash;
using MobileCRM.Pages.Base;
using System.Threading.Tasks;

namespace MobileCRM.Pages.Splash
{
    public class SplashPage : ModelTypedContentPage<SplashPageViewModel>
    {
        public SplashPage()
        {
            #region background view
            Image backgroundImageView = new Image() { Source = new FileImageSource() { File = "splash" }, Aspect = Aspect.AspectFill };
            double splashImageHeight = 568; // This is the height of the splash.png in points. Used for bottom-aligning the splash image.
            #endregion

            #region username title label
            Label usernameTitleLabel = new Label()
            {
                Text = TextResources.Splash_UsernameTitleLabel,
                TextColor = Palette._008,
                FontSize = Device.OnPlatform(
                    Device.GetNamedSize(NamedSize.Micro, typeof(Label)), 
                    Device.GetNamedSize(NamedSize.Small, typeof(Label)), 
                    Device.GetNamedSize(NamedSize.Default, typeof(Label))),
                XAlign = TextAlignment.Center,
                YAlign = TextAlignment.End,
            };

            #endregion

            #region username value label
            Label usernameValueLabel = new Label()
            {
                TextColor = Color.Black,
                FontSize = Device.OnPlatform(
                    Device.GetNamedSize(NamedSize.Default, typeof(Label)), 
                    Device.GetNamedSize(NamedSize.Medium, typeof(Label)), 
                    Device.GetNamedSize(NamedSize.Default, typeof(Label))),
                XAlign = TextAlignment.Center,
                YAlign = TextAlignment.Start,
            };
            usernameValueLabel.SetBinding(Label.TextProperty, "Username");
            #endregion

            #region password title label
            Label passwordTitleLabel = new Label()
            {
                Text = TextResources.Splash_PasswordTitleLabel,
                TextColor = Palette._008,
                FontSize = Device.OnPlatform(
                    Device.GetNamedSize(NamedSize.Micro, typeof(Label)), 
                    Device.GetNamedSize(NamedSize.Small, typeof(Label)), 
                    Device.GetNamedSize(NamedSize.Default, typeof(Label))),
                XAlign = TextAlignment.Center,
                YAlign = TextAlignment.End,
            };
            #endregion

            #region password value label
            Label passwordValueLabel = new Label()
            {
                TextColor = Color.Black,
                FontSize = Device.OnPlatform(
                    Device.GetNamedSize(NamedSize.Default, typeof(Label)), 
                    Device.GetNamedSize(NamedSize.Medium, typeof(Label)), 
                    Device.GetNamedSize(NamedSize.Default, typeof(Label))),
                XAlign = TextAlignment.Center,
                YAlign = TextAlignment.Start,
            };
            passwordValueLabel.SetBinding(Label.TextProperty, "Password");
            #endregion

            #region sign in button
            Label signInButtonLabel = new Label()
            {
                Text = TextResources.Splash_SignIn.ToUpper(),
                FontSize = Device.OnPlatform(
                    Device.GetNamedSize(NamedSize.Small, typeof(Label)), 
                    Device.GetNamedSize(NamedSize.Medium, typeof(Label)), 
                    Device.GetNamedSize(NamedSize.Default, typeof(Label))),
                XAlign = TextAlignment.Center,
                YAlign = TextAlignment.Center,
                BackgroundColor = Palette._002,
                TextColor = Color.White
            };
            signInButtonLabel.GestureRecognizers.Add(
                new TapGestureRecognizer()
                { 
                    NumberOfTapsRequired = 1, 
                    Command = new Command(SignInButtonTapped) 
                });
            #endregion

            #region sign in activity indicator
            ActivityIndicator signInActivityIndicator = new ActivityIndicator();
            signInActivityIndicator.SetBinding(ActivityIndicator.IsVisibleProperty, "IsPresentingLoginUI");
            signInActivityIndicator.SetBinding(ActivityIndicator.IsEnabledProperty, "IsPresentingLoginUI");
            signInActivityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsPresentingLoginUI");
            #endregion

            #region xamarin logo and name
            Image xamarinLogoImage = new Image() { Source = new FileImageSource() { File = "xamarin_logo_plus_name_inline" }, Aspect = Aspect.AspectFit };
            #endregion

            #region compose layout
            double labelHeight = Sizes.SmallRowHeight;
            const double buttonPaddingPercent = .05;
            const double buttonHeight = 50;
            const double logoHeight = 25;
            const double logoWidthPercent = .33;

            RelativeLayout relativeLayout = new RelativeLayout();

            relativeLayout.Children.Add(
                view: backgroundImageView,
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width),
                heightConstraint: Constraint.Constant(splashImageHeight),
                yConstraint: Constraint.RelativeToParent(parent => parent.Height - splashImageHeight)
            );

            // The background image is bottom-aligned, so let's add the button and labels in reverse order, from bottom to top.

            relativeLayout.Children.Add(
                view: xamarinLogoImage,
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width * logoWidthPercent),
                heightConstraint: Constraint.Constant(logoHeight),
                xConstraint: Constraint.RelativeToParent(parent => (parent.Width - (parent.Width * logoWidthPercent)) / 2),
                yConstraint: Constraint.RelativeToParent(parent => parent.Height - 30 - logoHeight)
            );
                
            relativeLayout.Children.Add(
                view: signInButtonLabel,
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width - (parent.Width * buttonPaddingPercent * 2)),
                heightConstraint: Constraint.Constant(buttonHeight),
                xConstraint: Constraint.RelativeToParent(parent => parent.Width * buttonPaddingPercent),
                yConstraint: Constraint.RelativeToView(xamarinLogoImage, (parent, view) => view.Y - 75)
            );

            // The signInActivityIndicator is stacked directly on top of the signInButtonLabel (Z-axis), and only visible once the signInButtonLabel has been clicked,
            // thus preventing a second tap on the signInButtonLabel.
            relativeLayout.Children.Add(
                view: signInActivityIndicator,
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width - (parent.Width * buttonPaddingPercent * 2)),
                heightConstraint: Constraint.Constant(buttonHeight),
                xConstraint: Constraint.RelativeToParent(parent => parent.Width * buttonPaddingPercent),
                yConstraint: Constraint.RelativeToView(xamarinLogoImage, (parent, view) => view.Y - 75)
            );

            relativeLayout.Children.Add(
                view: passwordValueLabel,
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width),
                heightConstraint: Constraint.Constant(labelHeight),
                yConstraint: Constraint.RelativeToView(signInButtonLabel, (parent, view) => view.Y - 90 - labelHeight)
            );

            relativeLayout.Children.Add(
                view: passwordTitleLabel,
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width),
                heightConstraint: Constraint.Constant(labelHeight),
                yConstraint: Constraint.RelativeToView(passwordValueLabel, (parent, view) => view.Y - labelHeight)
            );

            relativeLayout.Children.Add(
                view: usernameValueLabel,
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width),
                heightConstraint: Constraint.Constant(labelHeight),
                yConstraint: Constraint.RelativeToView(passwordTitleLabel, (parent, view) => view.Y - labelHeight)
            );

            relativeLayout.Children.Add(
                view: usernameTitleLabel,
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width),
                heightConstraint: Constraint.Constant(labelHeight),
                yConstraint: Constraint.RelativeToView(usernameValueLabel, (parent, view) => view.Y - labelHeight)
            );
            #endregion

            Content = relativeLayout;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await ViewModel.LoadDemoCredentials();
        }

        async Task<bool> Authenticate()
        {
            var success = await App.Authenticate();
            ViewModel.IsPresentingLoginUI = false;
            return !success ? await this.Authenticate() : success;
        }

        async void SignInButtonTapped()
        {
            // trigger the activity indicator through the IsPresentingLoginUI property on the ViewModel
            ViewModel.IsPresentingLoginUI = true;

            if (await Authenticate())
            {
                // Pop off the modally presented SplashPage.
                // Note that we're not popping the ADAL auth UI here; that's done automatcially by the ADAL library when the Authenticate() method returns.
                await ViewModel.PopModalAsync();

                // Broadcast a message that we have sucessdully authenticated.
                // This is mostly just for Android. We need to trigger Android to call the SalesDashboardPage.OnAppearing() method,
                // because unlike iOS, Android does not call the OnAppearing() method each time that the Page actually appears on screen.
                MessagingCenter.Send(this, MessagingServiceConstants.AUTHENTICATED);

                // Since we're all done logging in, turn off the activity indicator.
                ViewModel.IsPresentingLoginUI = false;
            }
        }
    }
}

