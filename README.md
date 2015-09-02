#Xamarin CRM
###(a Xamarin.Forms demo)

Xamarin CRM is a demo app whose imagined purpose is to serve the mobile workforce of a fictitious company that sells 3D printer hardware and supplies. The app empowers salespeople track their sales performance, manage leads, view their contacts, manage orders, and browse the product catalog.

<img src="https://github.com/xamarin/demo-xamarincrm-internal/blob/master/src/MobileApp/markdown-graphics/XamarinCRM_screenshots.png" alt="App screenshot collage" width="100%">

####Supported platforms: iOS and Android

####The source code consists of three parts:
  1. A Xamarin.Forms mobile app for iOS and Android.
  2. A javascript-backed Azure Mobile Service (Azure REST API) for customer and order data.
  3. A .NET Web API-backed Entity Framework based Azure Mobile Service for product catalog data.

**The Azure Mobile Services (2 & 3) do not need to be deployed by you.** There is already an instance of each of those services up and running in Azure, and the mobile app is configured by default to consume those service instances. We've included the code for those services so that you may run your own service instances on Azure if you'd like.

##Xamarin.Forms app (Xamarin CRM)

####[Setup Instructions](https://github.com/xamarin/demo-xamarincrm-internal/wiki/Setup-Xamarin-CRM-app)

####[Install the app NOW without building from source code (coming soon)](https://github.com/xamarin/demo-xamarincrm-internal/wiki/Install-the-app-NOW-without-building-from-source-code)

####Featured technologies
* [Xamarin.Forms](http://xamarin.com/forms)
* [Xamarin.Forms.Maps](https://developer.xamarin.com/guides/cross-platform/xamarin-forms/user-interface/map)
* [.NET Portable HttpClient] (https://www.nuget.org/packages/Microsoft.Net.Http)
* [Active Directory Authentication Library (ADAL)](https://blog.xamarin.com/put-adal-xamarin.forms)
* [Azure Mobile Services libraries](https://azure.microsoft.com/en-us/documentation/services/mobile-services)
* [Syncfusion Essential Studio charts](http://www.syncfusion.com/products/xamarin)

####Highlights
######Over 95% shared code between platforms:
<img src="https://github.com/xamarin/demo-xamarincrm-internal/blob/master/src/MobileApp/markdown-graphics/XamarinCRM_shared_code.png" alt="Over 95% shared code" width="75%">

######Natively rendered controls:
<img src="https://github.com/xamarin/demo-xamarincrm-internal/blob/master/src/MobileApp/markdown-graphics/XamarinCRM_native_controls.png" alt="Natively rendered controls" width="50%">

######OAuth authentication using Microsoft's ADAL (Active Directory Authentication Library):
<img src="https://github.com/xamarin/demo-xamarincrm-internal/blob/master/src/MobileApp/markdown-graphics/XamarinCRM_authentication.png" alt="ADAL OAuth authentication" width="50%">

######Beautiful charts with Syncfusion Essential Studio:
<img src="https://github.com/xamarin/demo-xamarincrm-internal/blob/master/src/MobileApp/markdown-graphics/XamarinCRM_graphs.png" alt="Syncfusion charts" width="100%">

######Native mapping on each platform:
<img src="https://github.com/xamarin/demo-xamarincrm-internal/blob/master/src/MobileApp/markdown-graphics/XamarinCRM_native_maps.png" alt="Native mapping" width="50%">

######Easy list views with data-binding:
<img src="https://github.com/xamarin/demo-xamarincrm-internal/blob/master/src/MobileApp/markdown-graphics/XamarinCRM_bindable_list.png" alt="Easy list data-binding" width="50%">

##Azure Mobile Service for product catalog data (.NET backend)
####[Setup Instructions (coming soon)](https://github.com/xamarin/demo-xamarincrm-internal/wiki/Setup-Xamarin-CRM-Azure-Mobile-Service-for-product-catalog-data)

####Service API documentation:
######URL: https://xamarincrmv2-catalogdataservice.azure-mobile.net/help

######Credentials (for the catalog data service, NOT the app login):
**Username:** [blank] (literally an empty field)

**Password:** IibptMvpFmJRBisbVyiCheBukYjzsD75

## Azure Mobile Service for customer data (javascript backend)
####[Setup Instructions (coming)](https://github.com/xamarin/demo-xamarincrm-internal/wiki/Setup-Xamarin-CRM-Azure-Mobile-Service-for-customer-and-order-data)
