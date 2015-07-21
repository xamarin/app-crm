using Microsoft.Phone.Controls;
using Xamarin;
using Xamarin.Forms;

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

      Insights.Initialize("e548c92073ff9ed3a0bc529d2edf896009d81c9c");

			// Set our view from the "main" layout resource
			Content = MobileCRM.App.RootPage.ConvertPageToUIElement(this);
		}

	
	}
}