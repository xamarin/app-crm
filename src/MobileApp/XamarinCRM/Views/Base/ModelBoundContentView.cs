
using Xamarin.Forms;
using XamarinCRM.ViewModels.Base;

namespace XamarinCRM.Views.Base
{
    /// <summary>
    /// A generically typed ContentView that enforces the type of its BindingContext according to TViewModel.
    /// </summary>
    public abstract class ModelBoundContentView<TViewModel> : ContentView where TViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets the generically typed ViewModel from the underlying BindingContext.
        /// </summary>
        /// <value>The generically typed ViewModel.</value>
        protected TViewModel ViewModel
        {
            get { return base.BindingContext as TViewModel; }
        }

        /// <summary>
        /// Sets the underlying BindingContext as the defined generic type.
        /// </summary>
        /// <value>The generically typed ViewModel.</value>
        /// <remarks>Enforces a generically typed BindingContext, instead of the underlying loosely object-typed BindingContext.</remarks>
        public new TViewModel BindingContext
        {
            set 
            { 
                base.BindingContext = value; 
                base.OnPropertyChanged("BindingContext");
            }
        }
    }
}

