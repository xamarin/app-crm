using System;
using Xamarin.Forms;
using MobileCRM.Layouts;
using MobileCRM.Statics;
using System.ComponentModel;

namespace MobileCRM
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
                    view: new BoxView() { BackgroundColor = Palette._014, HeightRequest = 1 },
                    widthConstraint: Constraint.RelativeToParent(parent => parent.Width),
                    heightConstraint: Constraint.Constant(1));
                
                stackLayout.Children.Add(borderLayout);

                base.Content = stackLayout; 
            }
        }
    }
}

