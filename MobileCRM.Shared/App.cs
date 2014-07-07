using MobileCRM.Shared.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobileCRM.Shared
{
    public static class App
    {
      private static Page homeView;
      public static Page RootPage
      {
        get { return homeView ?? (homeView = new HomeView()); }
      }
    }
}
