using System;
using Xamarin.Forms;
using MobileCRM.ViewModels;

namespace MobileCRM
{
    public abstract class ModelBoundContentView<TViewModel> : ContentView where TViewModel : BaseViewModel
    {
        protected TViewModel ViewModel
        {
            get { return base.BindingContext as TViewModel; }
        }

        /// <summary>
        /// Gets or sets the binding context.
        /// </summary>
        /// <value>The binding context.</value>
        /// <remarks>Enforces the proper binding context type at compile time.</remarks>
        public new TViewModel BindingContext
        {
            set { base.BindingContext = value; }
        }
    }
}

