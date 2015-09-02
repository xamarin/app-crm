#Xamarin CRM (a Xamarin.Forms demo app)

Xamarin CRM is a demo app whose imagined purpose is to serve the mobile workforce of a fictitious company that sells 3D printer hardware and supplies. The app empowers salespeople track their sales performance, manage leads, view their contacts, manage orders, and browse the product catalog.

![](https://github.com/xamarin/demo-xamarincrm-internal/blob/master/src/MobileApp/markdown-graphics/XamarinCRM_screenshots.png)

####Supported platforms:
#####&nbsp;&nbsp;&nbsp;&nbsp;iOS
#####&nbsp;&nbsp;&nbsp;&nbsp;Android

####The app consists of three parts:
  1. A Xamarin.Forms mobile app for iOS and Android.
  2. A javascript-backed Azure Mobile Service (Azure REST API) for customer and order data.
  3. A .NET Web API-backed Entity Framework based Azure Mobile Service for catalog data.

**The Azure Mobile Services (2 & 3) do not need to be deployed by you.** There is already an instance of each of those services up and running in Azure, and the Xamarin app is configured by default to consume those service instances. We've included the code for those services so that you may run your own service instances on Azure if you'd like.

##Xamarin.Forms app (Xamarin CRM)
####Featured technologies:
* Xamarin.Forms (http://xamarin.com/forms)
* Xamarin.Forms.Maps (https://developer.xamarin.com/guides/cross-platform/xamarin-forms/user-interface/map)
* .NET Portable HttpClient (https://www.nuget.org/packages/Microsoft.Net.Http)
* Active Directory Authentication Library (ADAL) (https://blog.xamarin.com/put-adal-xamarin.forms)
* Azure Mobile Services libraries (https://azure.microsoft.com/en-us/documentation/services/mobile-services)
* Syncfusion Essential Studio charts (http://www.syncfusion.com/products/xamarin)

####Highlights:


##Azure Mobile Service for product catalog data (.NET backend)
Under construction

## Azure Mobile Service for customer data (javascript backend)
Under construction
