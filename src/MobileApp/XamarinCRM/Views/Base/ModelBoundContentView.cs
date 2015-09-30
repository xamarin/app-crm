//
//  Copyright 2015  Xamarin Inc.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
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

