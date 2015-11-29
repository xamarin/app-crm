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
using Xamarin;
using Xamarin.Forms;
using XamarinCRM.Layouts;
using XamarinCRM.Pages.Base;
using XamarinCRM.Statics;
using XamarinCRM.Views.Customers;

namespace XamarinCRM.Pages.Customers
{
    public class CustomerSalesPage : ModelBoundContentPage<CustomerSalesViewModel>
    {
        public CustomerSalesPage()
        {
            #region header
            Label companyTitleLabel = new Label()
            {
                Text = TextResources.Customers_Orders_EditOrder_CompanyTitle,
                TextColor = Palette._007,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.End,
                LineBreakMode = LineBreakMode.TailTruncation
            };

            Label companyNameLabel = new Label()
            {
                TextColor = Device.OnPlatform(Palette._006, Palette._006, Color.White),
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Start,
                LineBreakMode = LineBreakMode.TailTruncation
            };
            companyNameLabel.SetBinding(Label.TextProperty, "Account.Company");

            RelativeLayout headerLabelsRelativeLayout = new RelativeLayout() { HeightRequest = RowSizes.LargeRowHeightDouble };

            headerLabelsRelativeLayout.Children.Add(
                view: companyTitleLabel,
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height / 2));

            headerLabelsRelativeLayout.Children.Add(
                view: companyNameLabel,
                yConstraint: Constraint.RelativeToParent(parent => parent.Height / 2),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height / 2));
            
            #endregion

            #region weekly sales chart
            CustomerWeeklySalesChartView customerWeeklySalesChartView = new CustomerWeeklySalesChartView();
            #endregion

            #region category sales chart
            CustomerCategorySalesChartView customerCategorySalesChartView = new CustomerCategorySalesChartView();
            #endregion


            #region compose view hierarchy
            Content = new ScrollView()
            { 
                Content = new StackLayout()
                {
                    Spacing = 0,
                    Children =
                    {
                        customerWeeklySalesChartView,
                        customerCategorySalesChartView
                    }
                }
            };
            #endregion
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (!ViewModel.IsInitialized)
            {
                await ViewModel.ExecuteLoadSeedDataCommand(ViewModel.Account);
                ViewModel.IsInitialized = true;
            }

            Insights.Track(InsightsReportingConstants.PAGE_CUSTOMERSALES);
        }
    }
}

