using MobileCRM.Localization;
using MobileCRM.Models;
using MobileCRM.Pages.Base;
using MobileCRM.Views;
using Xamarin.Forms;

namespace MobileCRM.Pages.Sales
{
    public abstract class BaseLeadDetailPage : BaseContentPage
    {
        protected Account Model { get; private set; }

        protected StackLayout StackLayout { get; private set; }

        readonly string _DoneButtonText;

        protected BaseLeadDetailPage(string title, string doneButtonText, Account model = null)
        {
            Model = model;

            Title = title;

            _DoneButtonText = doneButtonText;

            StackLayout = new StackLayout();

            TabbedPageHeaderView tabbedPageHeaderView = new TabbedPageHeaderView(Title, _DoneButtonText);

            tabbedPageHeaderView.BackButtonImage.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () => await Navigation.PopModalAsync()),
                NumberOfTapsRequired = 1
            });

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
                            // TODO: execute save action

                            await Navigation.PopModalAsync();
                        }
                    }),
                    NumberOfTapsRequired = 1
                });

            StackLayout.Children.Add(tabbedPageHeaderView);

            Content = StackLayout;
        }
    }
}
