using System;
using Xamarin.UITest.Queries;
using Xamarin.UITest;

// ANDROID

namespace MobileCRM.Test
{
    public static class PlatformQueries
    {
        public static Func<AppQuery, AppQuery> SlideoutMenu = x => x.Id("home");
        public static Func<AppQuery, AppQuery> LoadingIcon = x => x.Class ("ProgressBar");
        public static Func<AppQuery, AppQuery> MapView = x => x.Class ("MapView");
        public static Func<AppQuery, AppQuery> Entry = x => x.Class ("EntryEditText");
        public static Func<AppQuery, AppQuery> Picker = x => x.Class("NumberPicker");
        public static Func<AppQuery, AppQuery> PickerConfirm = x => x.Text("OK");
        public static Func<AppQuery, AppQuery> EntryCell = x => x.Class("EntryCellEditText");
        public static Func<AppQuery, AppQuery> Signature = x => x.Class ("SignatureCanvasView");
        public static Func<AppQuery, AppQuery> List = x => x.Class ("ListView");
        public static Func<AppQuery, AppQuery> TextFieldLabel = x => x.Class("EditText");

        public static Func<AppQuery, AppQuery> EntryWithIndex (int index) {
            return x => x.Class ("EntryEditText").Index (index);
        }

        public static Func<AppQuery, AppQuery> EntryCellWithIndex (int index)
        {
            return x => x.Class ("EntryCellEditText").Index(index);
        }

    }
}

