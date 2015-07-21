using Xamarin.Forms;

namespace MobileCRM.Views.Sales
{
    public partial class SalesChartHeaderView_new : ContentView
    {
        public SalesChartHeaderView_new()
        {
            InitializeComponent();
        }

        public static readonly double HeaderTitleLabelFontSize = 
            Device.GetNamedSize(NamedSize.Medium, typeof(Label));

        public static readonly double WeeklyAverageTitleLabelFontSize = 
            Device.OnPlatform(
                iOS: Device.GetNamedSize(NamedSize.Micro, typeof(Label)) * 0.9,
                Android: Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                WinPhone: Device.GetNamedSize(NamedSize.Micro, typeof(Label)));

        public static readonly double WeeklyAverageValueLabelFontSize = 
            Device.OnPlatform(
                iOS: Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                Android: Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                WinPhone: Device.GetNamedSize(NamedSize.Large, typeof(Label))) * 1.1;
    }
}

