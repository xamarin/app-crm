using MobileCRM.Pages.Base;
using MobileCRM.Views;
using Xamarin.Forms;
using MobileCRM.ViewModels.Sales;
using System;
using MobileCRM.Localization;
using MobileCRM.Views.Base;

namespace MobileCRM.Pages.Sales
{
    public abstract class BaseLeadDetailPage : BaseContentPage
    {
        protected LeadDetailViewModel ViewModel
        {
            get { return BindingContext as LeadDetailViewModel; }
        }

        protected StackLayout StackLayout { get; private set; }

        protected BaseLeadDetailPage(string doneButtonText, LeadDetailViewModel viewModel)
        {
            if (viewModel == null)
            {
                const string paramName = "viewModel";
                throw new ArgumentNullException(paramName, string.Format("{0} cannot be constructed with a null {1}", GetType().Name, paramName));
            }

            BindingContext = viewModel;

            Title = ViewModel.Title;

            StackLayout = new StackLayout();

            BaseTabbedPageHeaderView tabbedPageHeaderView = null;

            Device.OnPlatform(
                iOS: () => tabbedPageHeaderView = new IosTabbedPageHeaderView(Title, doneButtonText),
                Android: () => tabbedPageHeaderView = new AndroidTabbedPageHeaderView(Title, doneButtonText));

            tabbedPageHeaderView.BackButtonImage.GestureRecognizers.Add(new TapGestureRecognizer()
                {
                    Command = new Command(async () => await ViewModel.Navigation.PopModalAsync()),
                    NumberOfTapsRequired = 1
                });


            Device.OnPlatform(iOS: () => 
                tabbedPageHeaderView.BackButtonLabel.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(async () => await ViewModel.Navigation.PopModalAsync()),
                        NumberOfTapsRequired = 1
                    }));

            tabbedPageHeaderView.DoneActionLabel.GestureRecognizers.Add(
                new TapGestureRecognizer()
                {
                    Command = new Command(async () =>
                        {
                            var answer = await DisplayAlert(
                                             title: TextResources.Leads_SaveConfirmTitle,
                                             message: TextResources.Leads_SaveConfirmDescription,
                                             accept: TextResources.Save,
                                             cancel: TextResources.Cancel);

                            if (answer)
                            {
                                ViewModel.SaveLeadCommand.Execute(null);

                                await ViewModel.Navigation.PopModalAsync();
                            }
                        }),
                    NumberOfTapsRequired = 1
                });

            StackLayout.Children.Add(tabbedPageHeaderView);

            Content = StackLayout;
        }
    }
}
