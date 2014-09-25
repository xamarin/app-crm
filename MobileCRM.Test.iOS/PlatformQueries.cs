using System;
using Xamarin.UITest.Queries;
using Xamarin.UITest;

// IOS

namespace MobileCRM.Test
{
    public static class PlatformQueries
    {
        public static Func<AppQuery, AppQuery> SlideoutMenu = x => x.Id ("slideout.png");
        public static Func<AppQuery, AppQuery> LoadingIcon = x => x.Class ("UIActivityIndicatorView");
        public static Func<AppQuery, AppQuery> MapView = x => x.Class("MKMapView");
        public static Func<AppQuery, AppQuery> Entry = x => x.Class ("UITextField");
        public static Func<AppQuery, AppQuery> Picker = x => x.Class("UIPickerTableView");
        public static Func<AppQuery, AppQuery> PickerConfirm = x => x.Text("Done");
        public static Func<AppQuery, AppQuery> EntryCell = x => x.Class ("UITextField");
        public static Func<AppQuery, AppQuery> Signature = x => x.Class ("SignaturePadView");
        public static Func<AppQuery, AppQuery> List = x => x.Class("UITableView");
        public static Func<AppQuery, AppQuery> TextFieldLabel = x => x.Class("UITextFieldLabel");

        public static Func<AppQuery, AppQuery> EntryWithIndex(int index) {
            return x => x.Class ("UITextField").Index(index);
        }

        public static Func<AppQuery, AppQuery> EntryCellWithIndex(int index) {
            return x => x.Class ("UITextField").Index(index);
        }
    }
}

