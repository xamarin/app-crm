// The MIT License (MIT)
//
// Copyright (c) 2015 Xamarin
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
using System;
using XamarinCRM.ViewModels.Base;
using XamarinCRM.Models.Local;
using System.Collections.Generic;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinCRM.Pages.About;

namespace XamarinCRM.ViewModels
{
    public class AboutItemListViewModel : BaseViewModel
    {
        public List<AboutItemViewModel> Items { get; private set; }

        public string Overview { get; private set; }

        public string ListHeading { get; private set; }

        public AboutItemListViewModel()
        {
            Items = new List<AboutItemViewModel>()
            {
                new AboutItemViewModel()
                {
                    Title = "Xamarin Platform",
                    Uri = "https://xamarin.com/platform"
                },

                new AboutItemViewModel()
                {
                    Title = "Xamarin.Forms",
                    Uri = "https://xamarin.com/forms"
                },

                new AboutItemViewModel()
                {
                    Title = "Xamarin Insights",
                    Uri = "https://xamarin.com/insights"
                },

                new AboutItemViewModel()
                {
                    Title = "Syncfusion Essentials for Xamarin.Forms",
                    Uri = "http://www.syncfusion.com/products/xamarin"
                },

                new AboutItemViewModel()
                {
                    Title = "Azure App Services Mobile",
                    Uri = "https://azure.microsoft.com/en-us/services/app-service/"
                },

                new AboutItemViewModel()
                {
                    Title = "Microsoft ADAL (Active Directory Authentication Library)",
                    Uri = "https://github.com/Azure/azure-content/blob/master/articles/active-directory/active-directory-authentication-libraries.md"
                },

                new AboutItemViewModel()
                {
                    Title = "Plugins for Xamarin (community-contributed)",
                    Uri = "https://github.com/xamarin/plugins"
                },

                new AboutItemViewModel()
                {
                    Title = "Newtonsoft Json.NET",
                    Uri = "http://www.newtonsoft.com/json"
                },
            };

            Overview = 
                "Xamarin CRM is a demo app whose imagined purpose is to serve the mobile workforce of a " +
            "fictitious company that sells 3D printer hardware and supplies. The app empowers salespeople " +
            "to track their sales performance, manage leads, view their contacts, manage orders, and " +
            "browse a product catalog.";

            ListHeading = 
                "The app is built with Xamarin Platform and Xamarin.Forms, and takes advantage of " +
                "several other supporting technologies:";
        }
    }
}

