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
using XamarinCRM.Layouts;
using XamarinCRM.Statics;
using System.ComponentModel;

namespace XamarinCRM.Views.Custom
{
    public class ContentViewWithBottomBorder : ContentView, INotifyPropertyChanged
    {
        public new View Content
        {
            get { return base.Content; }
            set 
            { 
                StackLayout stackLayout = new UnspacedStackLayout();

                stackLayout.Children.Add(value);

                RelativeLayout borderLayout = new RelativeLayout() { HeightRequest = 1 };

                borderLayout.Children.Add(
                    view: new BoxView() { BackgroundColor = Palette._013, HeightRequest = 1 },
                    widthConstraint: Constraint.RelativeToParent(parent => parent.Width),
                    heightConstraint: Constraint.Constant(1));
                
                stackLayout.Children.Add(borderLayout);

                base.Content = stackLayout; 
            }
        }
    }
}

