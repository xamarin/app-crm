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
using XamarinCRM.Converters;
using Xamarin.Forms;
using XamarinCRM.Statics;

namespace XamarinCRM.Cells.Sales
{
    public class LeadListItemCell : ViewCell
    {
        public Label CompanyNameLabel { get; private set; }

        public Label OpportunityStageLabel { get; private set; }

        public Label LeadAmountLabel { get; private set; }

        public ProgressBar ProgressBar { get; private set; }

        public LeadListItemCell()
        {
            StyleId = "none";
            #region companyNameLabel
            CompanyNameLabel = new Label()
            {
                TextColor = Palette._006,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)) * 1.2,
                VerticalTextAlignment = TextAlignment.End,
                LineBreakMode = LineBreakMode.TailTruncation
            };

            CompanyNameLabel.SetBinding(
                Label.TextProperty,
                new Binding("Company"));
            #endregion

            #region opportunityStageLabel
            OpportunityStageLabel = new Label()
            {
                TextColor = Palette._007,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                VerticalTextAlignment = TextAlignment.End,
                LineBreakMode = LineBreakMode.TailTruncation
            };
                        
            OpportunityStageLabel.SetBinding(
                Label.TextProperty,
                new Binding(
                    path: "OpportunityStage"));
            #endregion

            #region leadAmountLabel
            LeadAmountLabel = new Label()
            {
                TextColor = Palette._007,
                HorizontalTextAlignment = TextAlignment.End,
                FontSize = Device.OnPlatform(
                    iOS: Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                    Android: Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                    WinPhone: Device.GetNamedSize(NamedSize.Medium, typeof(Label))),
                LineBreakMode = LineBreakMode.TailTruncation
            };
                        
            LeadAmountLabel.SetBinding(
                targetProperty: Label.TextProperty,
                binding: new Binding(
                    path: "OpportunitySize", 
                    stringFormat: "{0:C}"));
            #endregion

            #region progressBar
            ProgressBar = new ProgressBar();

            ProgressBar.SetBinding(
                targetProperty: ProgressBar.ProgressProperty,
                binding: new Binding(
                    path: "OpportunityStagePercent",
                    converter: new WholePercentToDecimalPercent() // use the WholePercentToDecimalPercent value converter to change the whole percent value to a decimal percent value
                ));
            #endregion

            // A ContentView, which will serve as the "top-level" of the cell's view hierarchy. 
            // It also allows a Padding to be set; something that can't be done with a plain View.
            var contentView = new ContentView();

            // set the padding of the contentView
            contentView.Padding = new Thickness(10, 0);

            // A container for the "top-level" of the cell's view hierarchy.
            RelativeLayout relativeLayout = new RelativeLayout();

            // add the companyNameLabel to the relativeLayout
            relativeLayout.Children.Add(
                view: CompanyNameLabel,
                xConstraint: Constraint.RelativeToParent(parent => 0),
                yConstraint: Constraint.RelativeToParent(Parent => 0),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height / 3));

            // add the percentCopleteLabel to the relativeLayout
            relativeLayout.Children.Add(
                view: OpportunityStageLabel,
                xConstraint: Constraint.RelativeToParent(parent => 0),
                yConstraint: Constraint.RelativeToParent(parent => parent.Height / 3),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width / 2),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height / 3));

            // add the leadAmountLabel to the relativeLayout
            relativeLayout.Children.Add(
                view: LeadAmountLabel,
                xConstraint: Constraint.RelativeToParent(parent => parent.Width / 2),
                yConstraint: Constraint.RelativeToParent(parent => parent.Height / 3),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width / 2),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height / 3));

            Constraint progressBarConstraint = Constraint.Constant(0);

            Device.OnPlatform(
                Default: () => progressBarConstraint = Constraint.RelativeToParent(parent => ((parent.Height / 3) * 2)),
                iOS: () => progressBarConstraint = Constraint.RelativeToParent(parent => ((parent.Height / 3) * 2) * 1.20));

            // add the progressBar to the relativeLayout
            relativeLayout.Children.Add(
                view: ProgressBar,
                xConstraint: Constraint.RelativeToParent(parent => 0),
                yConstraint: progressBarConstraint,
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height / 3));

            // Assign the relativeLayout to Content of contentView
            // This lets us take advantage of ContentView's padding.
            contentView.Content = relativeLayout;

            // assign contentView to the View property
            View = contentView;
        }
    }
}
