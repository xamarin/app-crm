using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MobileCRM.WindowsPhone.Resources;
using Xamarin.Forms;
using Xamarin;
using MobileCRM.Shared.CustomControls;
using MobileCRM.Shared.Pages;
using MobileCRM.Shared;

namespace MobileCRM.WindowsPhone
{
	public partial class MainPage : PhoneApplicationPage
	{
		// Constructor
		public MainPage()
		{
			InitializeComponent();


      Forms.Init();
			FormsMaps.Init();

			// Set our view from the "main" layout resource
			Content = MobileCRM.Shared.App.RootPage.ConvertPageToUIElement(this);
		}

	
	}
}