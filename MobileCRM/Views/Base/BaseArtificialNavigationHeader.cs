using System;
using Xamarin.Forms;

namespace MobileCRM
{
    public abstract class BaseArtificialNavigationHeader : ContentView
    {
        public Image BackButtonImage { get; protected set; }

        public Label BackButtonLabel { get; protected set; }

        public Label TitleLabel { get; protected set; }

        public Label DoneActionLabel { get; protected set; }
    }
}

