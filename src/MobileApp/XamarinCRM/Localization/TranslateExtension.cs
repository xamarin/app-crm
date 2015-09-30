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
using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinCRM.Localization
{
    // You exclude the 'Extension' suffix when using in Xaml markup
    [ContentProperty ("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        readonly CultureInfo ci;
        const string ResourceId = "XamarinCRM.TextResources.resources";

        public TranslateExtension() {
            ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo ();
        }

        public string Text { get; set; }

        public object ProvideValue (IServiceProvider serviceProvider)
        {
            if (Text == null)
                return "";

            ResourceManager resmgr = new ResourceManager(ResourceId
                , typeof(TranslateExtension).GetTypeInfo().Assembly);

            var translation = resmgr.GetString (Text, ci);

            if (translation == null) {
                #if DEBUG
                throw new ArgumentException (
                    String.Format ("Key '{0}' was not found in resources '{1}' for culture '{2}'.", Text, ResourceId, ci.Name),
                    "Text");
                #else
                translation = Text; // HACK: returns the key, which GETS DISPLAYED TO THE USER
                #endif
            }
            return translation;
        }
    }
}

