
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
using Foundation;
using Syncfusion.SfChart.XForms.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamarin;

using XamarinCRM.Statics;

namespace XamarinCRM.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            new SfChartRenderer(); // This is necessary for initializing SyncFusion charts.

            #if DEBUG
            Xamarin.Calabash.Start();
            #endif

            // Azure Mobile Services initilization
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            // SQLite initialization
            SQLitePCL.CurrentPlatform.Init();

            // Xamarin.Forms initialization
            Forms.Init();

            // Xamarin.Forms.Maps initialization
            FormsMaps.Init();

            // Bootstrap the "core" Xamarin.Forms app
            LoadApplication(new App());

			// Apply OS-specific color theming
			ConfigureApplicationTheming ();

            return base.FinishedLaunching(app, options);
        }

		void ConfigureApplicationTheming ()
		{
			UIProgressView.Appearance.ProgressTintColor = Palette._003.ToUIColor ();
		}
    }
}