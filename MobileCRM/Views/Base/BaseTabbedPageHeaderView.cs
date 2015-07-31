using System;
using Xamarin.Forms;

namespace MobileCRM.Views.Base
{
    public abstract class BaseTabbedPageHeaderView : ContentView
    {
        public Image BackButtonImage { get; protected set; }

        public Label BackButtonLabel { get; protected set; }

        public Label DoneActionLabel { get; protected set; }
    }
}

