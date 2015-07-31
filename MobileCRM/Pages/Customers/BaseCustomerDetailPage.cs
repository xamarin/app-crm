using System;
using Xamarin.Forms;
using MobileCRM.Layouts;
using MobileCRM.ViewModels.Accounts;
using MobileCRM.Pages.Base;
using MobileCRM.Views.Base;
using MobileCRM.Views;

namespace MobileCRM
{
    public class BaseCustomerDetailPage : ModelBoundContentPage<AccountDetailsViewModel>
    {
        BaseTabbedPageHeaderView _TabbedPageHeaderView;

        protected StackLayout StackLayout { get; private set; }

        public BaseCustomerDetailPage()
        {
            StackLayout = new UnspacedStackLayout();

            Device.OnPlatform(
                iOS: () => _TabbedPageHeaderView = new IosTabbedPageHeaderView(Title, TextResources.Leads_LeadDetail_SaveButtonText.ToUpper())
//                , Android: () => _TabbedPageHeaderView = new AndroidTabbedPageHeaderView(Title, TextResources.Leads_LeadDetail_SaveButtonText.ToUpper())
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

                _TabbedPageHeaderView.DoneActionLabel.GestureRecognizers.Add(
                    new TapGestureRecognizer()
                    {
                        Command = new Command(async () =>
                            {
                                var answer = 
                                    await DisplayAlert(
                                        title: TextResources.Leads_SaveConfirmTitle,
                                        message: TextResources.Leads_SaveConfirmDescription,
                                        accept: TextResources.Save,
                                        cancel: TextResources.Cancel);

                                if (answer)
                                {
                                    ViewModel.SaveAccountCommand.Execute(null);

                                    await ViewModel.PopModalAsync();
                                }
                            }),
                        NumberOfTapsRequired = 1
                    });

                StackLayout.Children.Add(_TabbedPageHeaderView);
            }
        }
    }
}

