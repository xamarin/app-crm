#VervetaCRM Demo (Xamarin.Forms)

VervetaCRM is a demo mobile CRM app for salespeople at a fictional office-supply company (named after the vervet monkey).  The app lets  salespeople track their sales performance, see their contacts, view customer location maps, and capture orders with user signatures.

Using [Xamarin.Forms](http://xamarin.com/forms), over 90% code was re-used to create a native app for iOS, Android, and Windows Phone. 

This [video overview](https://www.youtube.com/watch?v=19Hs8wzeC7w) provides an overview of how the app was created and architected.

##Installation
Instructions for viewing and compiling the code are available in the [setup guide](https://github.com/xamarin/VervetaCRM/wiki/Setup-Instructions).

![](https://github.com/xamarin/VervetaCRM/blob/master/markdown-graphics/VervetaDashboard.png)

![](https://github.com/xamarin/VervetaCRM/blob/master/markdown-graphics/VervetaMaps.png)

![](https://github.com/xamarin/VervetaCRM/blob/master/markdown-graphics/VervetaCatalog.png)


#Featured Technologies
The demo app utilizes several technologies and frameworks to reflect a real-world mobile app architecture and maximum code re-use across mobile platforms:

##Xamarin.Forms
**[MVVM Architecture](http://www.google.com/url?q=http%3A%2F%2Fdeveloper.xamarin.com%2Fguides%2Fcross-platform%2Fxamarin-forms%2Fxaml-for-xamarin-forms%2Fdata_bindings_to_mvvm%2F&sa=D&sntz=1&usg=AFQjCNFxdmJBNbm8-tWJ0CIXuN0fN2v6aA):** The app utilizes the MVVM architecture, using examples of both C# and XAML to define user views.

**[Custom Renderers](http://www.google.com/url?q=http%3A%2F%2Fdeveloper.xamarin.com%2Fguides%2Fcross-platform%2Fxamarin-forms%2Fcustom-renderer%2F&sa=D&sntz=1&usg=AFQjCNGPqAndxlsRuCnyC65HcRW7YFoVsw):** enable writing platform-specific (e.g. - iOS, Android) user interface code to take advantage of native OS UI capabilities.

**[Dependency Service](http://www.google.com/url?q=http%3A%2F%2Fdeveloper.xamarin.com%2Fguides%2Fcross-platform%2Fxamarin-forms%2Fdependency-service%2F&sa=D&sntz=1&usg=AFQjCNFLXIS_LTyi3e1o5aSz0mdLjGbc8w)**: access native hardware features of the device such as phone and GPS.

##Components & Libraries

![](https://github.com/xamarin/VervetaCRM/blob/master/markdown-graphics/SigPad-ComponentStore.png) 

**[Signature Pad](https://www.google.com/url?q=https%3A%2F%2Fcomponents.xamarin.com%2Fview%2Fsignature-pad&sa=D&sntz=1&usg=AFQjCNHTI8Me1wTHH6vZOYlCrPRySjiPQw)**: Available from the Xamarin [Component Store](https://components.xamarin.com/) to capture and display user signatures.  This component highlights Xamarin.Forms extensibility; using custom renderers, it was easy to consume the platform-specific signature pad components into the Xamarin.Forms shared UI code.

**[OxyPlot](http://www.google.com/url?q=http%3A%2F%2Foxyplot.org%2F&sa=D&sntz=1&usg=AFQjCNGe7LMm2dEX-hGl3z0xWLu2Yvso0A)**: An outstanding, open-source .NET-based plotting and charting library available as a Portable Class Library (PCL) via NuGet.

##Cloud & Mobile Architecture

[Azure Mobile Services](http://azure.microsoft.com/en-us/services/mobile-services/): this app uses Azure Mobile Services (AMS) as the cloud backend for authentication and data.  It integrates with Azure Active Directory to create a consistent sign-on experience for mobile users.  The AMS component simplifies the implementation and uses oAuth to both authenticate a user and provide a token.

Data is synchronized with an Azure SQL cloud database and a [SQLite](http://sqlite.org/) database that runs on the device – providing fast, offline data access and a consistent data access API.

#License

The source code for this project is open-source under an [MIT license](http://www.google.com/url?q=http%3A%2F%2Fopensource.org%2Flicenses%2FMIT&sa=D&sntz=1&usg=AFQjCNHDbo7qf6bLsFB0hul9yFpGyirUdg).


#Authors


Steven Yi, James Montemagno, Glenn Wester
