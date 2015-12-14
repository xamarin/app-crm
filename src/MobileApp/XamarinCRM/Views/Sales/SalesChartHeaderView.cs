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
using Xamarin.Forms;
using XamarinCRM.Statics;

namespace XamarinCRM.Views.Sales
{
    public class SalesChartHeaderView : ContentView
    {
        readonly Label _WeeklyAverageValueLabel;

        #region Example of custom bindable property

        public static readonly BindableProperty WeeklyAverageProperty = BindableProperty.Create<SalesChartHeaderView, string>(p => p.WeeklyAverage, null);

        public string WeeklyAverage
        {
            get { return (string)GetValue(WeeklyAverageProperty); }
            set { SetValue(WeeklyAverageProperty, value); }
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            switch(propertyName)
            {
                case "WeeklyAverage":
                    _WeeklyAverageValueLabel.Text = (string)GetValue(WeeklyAverageProperty);
                    break;
            }
        }

        #endregion Example of custom bindable property

        public SalesChartHeaderView()
        {
            Label headerTitleLabel = new Label()
            { 
                Text = TextResources.SalesDashboard_SalesChart_Header_Title,
                TextColor = Device.OnPlatform(Palette._006, Palette._006, Color.White),
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Start
            };

            Label weeklyAverageTitleLabel = new Label()
            {
                Text = TextResources.SalesDashboard_SalesChart_Header_WeeklyAverageTitle.ToUpperInvariant(),
                TextColor = Palette._007,
                FontSize = Device.OnPlatform(
                    iOS: Device.GetNamedSize(NamedSize.Micro, typeof(Label)) * .9,
                    Android: Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                    WinPhone: Device.GetNamedSize(NamedSize.Micro, typeof(Label))),
                HorizontalTextAlignment = TextAlignment.End,
                VerticalTextAlignment = TextAlignment.Center
            };

            _WeeklyAverageValueLabel = new Label()
            {
                TextColor = Device.OnPlatform(Palette._006, Palette._006, Color.White),
                FontSize = Device.OnPlatform(
                    iOS: Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                    Android: Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                    WinPhone: Device.GetNamedSize(NamedSize.Large, typeof(Label))) * 1.1,
                HorizontalTextAlignment = TextAlignment.End,
                VerticalTextAlignment = TextAlignment.End
            };

            Device.OnPlatform(
                Android: () => _WeeklyAverageValueLabel.FontAttributes = FontAttributes.Bold);

            RelativeLayout relativeLayout = new RelativeLayout();

            relativeLayout.Children.Add(
                view: headerTitleLabel,
                xConstraint: Constraint.RelativeToParent(parent => 0),
                yConstraint: Constraint.RelativeToParent(parent => 0),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width / 2),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height)
            );
                
            relativeLayout.Children.Add(
                view: weeklyAverageTitleLabel,
                xConstraint: Constraint.RelativeToParent(parent => parent.Width / 2),
                yConstraint: Constraint.RelativeToParent(parent => 0),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width / 2),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height / 4)
            );
           
            relativeLayout.Children.Add(
                view: _WeeklyAverageValueLabel,
                xConstraint: Constraint.RelativeToParent(parent => parent.Width / 2),
                yConstraint: Constraint.RelativeToParent(parent => parent.Height / 4),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width / 2),
                heightConstraint: Constraint.RelativeToParent(parent => (parent.Height / 4) * 3)
            );

            //BackgroundColor = Palette._009;

            //Device.OnPlatform(iOS: () => BackgroundColor = Color.White, Android: () => BackgroundColor = Palette._009);

            HeightRequest = RowSizes.MediumRowHeightDouble;

            Padding = new Thickness(20, 20, 20, 0);

            Content = relativeLayout;
        }
    }
}


