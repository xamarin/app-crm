using Xamarin.Forms;
using MobileCRM.Layouts;
using MobileCRM.Pages.Base;
using MobileCRM.Views.Base;
using MobileCRM.Views;
using MobileCRM.ViewModels;

namespace MobileCRM.Pages.Customers
{
    public abstract class BaseCustomerDetailPage<TViewModelType> : ModelEnforcedContentPage<TViewModelType> where TViewModelType : BaseViewModel
    {
        public BaseArtificialNavigationHeader TabbedPageHeaderView { get; private set; }

        protected StackLayout StackLayout { get; private set; }

        public BaseCustomerDetailPage()
        {
            StackLayout = new UnspacedStackLayout();

            Device.OnPlatform(
                iOS: () => TabbedPageHeaderView = new IosArtificialNavigationHeader(TextResources.MainTabs_Customers)
                , Android: () => TabbedPageHeaderView = new AndroidArtificialNavigationHeader(TextResources.MainTabs_Customers)
            );

            if (TabbedPageHeaderView != null)
            {
                TabbedPageHeaderView.BackButtonImage.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(async () => await ViewModel.PopModalAsync()),
                        NumberOfTapsRequired = 1
                    });


                Device.OnPlatform(iOS: () => 
                    TabbedPageHeaderView.BackButtonLabel.GestureRecognizers.Add(new TapGestureRecognizer()
                        {
                            Command = new Command(async () => await ViewModel.PopModalAsync()),
                            NumberOfTapsRequired = 1
                        }));

                StackLayout.Children.Add(TabbedPageHeaderView);
            }
        }
    }
}

