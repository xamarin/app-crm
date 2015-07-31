using Xamarin.Forms;
using MobileCRM.ViewModels;

namespace MobileCRM.Pages.Base
{
    public abstract class ModelEnforcedContentPage<TViewModel> : ContentPage where TViewModel : BaseViewModel
    {
        protected TViewModel ViewModel
        {
            get { return base.BindingContext as TViewModel; }
        }

        /// <summary>
        /// Sets the binding context.
        /// </summary>
        /// <value>The binding context.</value>
        /// <remarks>Enforces the proper binding context type at compile time.</remarks>
        public new TViewModel BindingContext
        {
            set { base.BindingContext = value; }
        }

        protected ModelEnforcedContentPage()
        {
            this.SetBinding(Page.TitleProperty, "Title");
        }
    }
}

