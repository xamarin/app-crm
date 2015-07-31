using MobileCRM.Pages.Base;
using MobileCRM.Views;
using Xamarin.Forms;
using MobileCRM.ViewModels.Sales;
using System;
using MobileCRM.Views.Base;
using System.Threading.Tasks;
using MobileCRM.Layouts;

namespace MobileCRM.Pages.Sales
{
    public abstract class BaseLeadDetailPage : ModelBoundContentPage<LeadDetailViewModel>
    {
        BaseTabbedPageHeaderView _TabbedPageHeaderView;

        protected StackLayout StackLayout { get; private set; }

        protected BaseLeadDetailPage(LeadDetailViewModel viewModel)
        {
            if (viewModel == null)
            {
                const string paramName = "viewModel";
                throw new ArgumentNullException(paramName, string.Format("{0} cannot be constructed with a null {1}", GetType().Name, paramName));
            }

            BindingContext = viewModel;

            StackLayout = new UnspacedStackLayout();

            Device.OnPlatform(
                iOS: () => _TabbedPageHeaderView = new IosTabbedPageHeaderView(Title, TextResources.Leads_LeadDetail_SaveButtonText.ToUpper()),
                Android: () => _TabbedPageHeaderView = new AndroidTabbedPageHeaderView(Title, TextResources.Leads_LeadDetail_SaveButtonText.ToUpper()));

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

            if (_TabbedPageHeaderView.DoneActionLabel != null)
            {
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
                                    ViewModel.SaveLeadCommand.Execute(null);

                                    await ViewModel.PopModalAsync();
                                }
                            }),
                        NumberOfTapsRequired = 1
                    });
            }

            StackLayout.Children.Add(_TabbedPageHeaderView);

            Content = StackLayout;
        }
    }
}
