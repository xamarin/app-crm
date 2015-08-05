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
        BaseTabbedPageHeaderView _TabbedPageHeaderView;

        protected StackLayout StackLayout { get; private set; }

        public BaseCustomerDetailPage()
        {
            StackLayout = new UnspacedStackLayout();

            Device.OnPlatform(
                iOS: () => _TabbedPageHeaderView = new IosTabbedPageHeaderView(TextResources.MainTabs_Customers)
                , Android: () => _TabbedPageHeaderView = new AndroidTabbedPageHeaderView(TextResources.MainTabs_Customers)
            );

            if (_TabbedPageHeaderView != null)
            {
                _TabbedPageHeaderView.BackButtonImage.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(async () => await ViewModel.PopModalAsync()),
                        NumberOfTapsRequired = 1
                    });


                Device.OnPlatform(iOS: () => 
                    _TabbedPageHeaderView.BackButtonLabel.GestureRecognizers.Add(new TapGestureRecognizer()
                        {
                            Command = new Command(async () => await ViewModel.PopModalAsync()),
                            NumberOfTapsRequired = 1
                        }));

//                if (_TabbedPageHeaderView.DoneActionLabel != null)
//                {
//                    _TabbedPageHeaderView.DoneActionLabel.GestureRecognizers.Add(
//                        new TapGestureRecognizer()
//                        {
//                            Command = new Command(async () =>
//                                {
//                                    var answer = 
//                                        await DisplayAlert(
//                                            title: TextResources.Leads_SaveConfirmTitle,
//                                            message: TextResources.Leads_SaveConfirmDescription,
//                                            accept: TextResources.Save,
//                                            cancel: TextResources.Cancel);
//
//                                    if (answer)
//                                    {
//                                        ViewModel.SaveAccountCommand.Execute(null);
//
//                                        await ViewModel.PopModalAsync();
//                                    }
//                                }),
//                            NumberOfTapsRequired = 1
//                        });
//                }

                StackLayout.Children.Add(_TabbedPageHeaderView);
            }
        }
    }
}

