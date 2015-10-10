#r @"packages/FAKE/tools/FakeLib.dll"
#load "build-helpers.fsx"
open Fake
open System
open System.IO
open System.Linq;
open BuildHelpers
open Fake.XMLHelper
open Fake.XamarinHelper

let private androidKeystorePassword = getBuildParamOrDefault "android_keystore_password" "not_provided"

let private googleMapsForAndroidv2ApiKey = getBuildParamOrDefault "google_maps_for_android_v2_api_key" "not_provided"

let private mobileAppSourcePath = "../../src/MobileApp/"

let private solutionFile = (mobileAppSourcePath + "XamarinCRM.sln")

let private nuGetPackageOutputPath = (mobileAppSourcePath + "packages/")

let private iOSProjectPath = (mobileAppSourcePath + "XamarinCRM.iOS/")

let private iOSBuildOutputPath = "src/MobileApp/XamarinCRM.iOS/bin/" // This path isn't relative to ~/automation/fake like the rest of the paths. The tool that uses it runs in the repo's root folder.

let private androidProjectPath = (mobileAppSourcePath + "XamarinCRM.Android/")

let private androidProjectFile = (androidProjectPath + "XamarinCRM.Android.csproj")

let private androidBuildOutputPath = (androidProjectPath + "bin/")

// a function that restores all the packages in the solution to a particular directory, instead of the default, which would be relative to the working directory
let private RestorePackagesToHintPath = 
    Exec "tools/NuGet/NuGet.exe" ("restore " + solutionFile + " -PackagesDirectory " + nuGetPackageOutputPath)

// a function that encapsulates the different iOS combinations
let private TemplatediOSBuild configName platform provisioningCategory =

    CleanDir (iOSBuildOutputPath + configName)
    
    RestorePackagesToHintPath
    
    iOSBuild (fun defaults ->
        {defaults with
            ProjectPath = solutionFile
            Platform = platform
            Configuration = ("iOS " + configName + " (" + provisioningCategory + ")")
            Target = "Build"
            BuildIpa = true
        })
    TeamCityHelper.PublishArtifact (iOSBuildOutputPath + platform + "/" + configName + "/*.ipa")

// a function that encapsulates the different Android combinations
let private TemplatedAndroidBuild configName keystoreFile keystoreAlias keystorePassword googleMapsApiKey =

    RestorePackagesToHintPath
    
    // Delete the (androidProjectPath + "Resources/values/api-keys.xml") file, because of what happens right below.
    DeleteFile (androidProjectPath + "Resources/values/api-keys.xml")

    // Insert Google Maps For Android v2 API Key into api-keys.xml file in Android project
    // The actual values/api-keys.xml does not exist in source. It gets copied into place by an MSBuild target in the csproj file, from the valuesTemplate/api-keys.xml file.
    // So, we place the api key value into the valuesTemplate/api-keys.xml file instead, knowing that it will get copied into place.

    XmlPokeInnerText (androidProjectPath + "Resources/valuesTemplate/api-keys.xml") "(/resources/string[@name='GoogleMapsKey'])[1]" googleMapsForAndroidv2ApiKey

    // Build, sign, and zip-align
    AndroidPackage (fun defaults ->
        {defaults with
            ProjectPath = androidProjectFile
            Configuration = configName
            OutputPath = (androidBuildOutputPath + configName)
        }) 
    |> AndroidSignAndAlign (fun defaults ->
        {defaults with
            ZipalignPath = "tools/zipalign"
            KeystorePath = (mobileAppSourcePath + keystoreFile)
            KeystorePassword = androidKeystorePassword // TODO: Don't store this in the build script for a real app! This gets passed in at the top of the build script.
            KeystoreAlias = keystoreAlias
        })
    |> fun file -> TeamCityHelper.PublishArtifact file.FullName

// This target is mostly for a sanity check, to make sure the app builds with debug settings.
Target "ios-iphone-debug" (fun () ->
    TemplatediOSBuild "Debug" "iPhone" "Development"
)

// This target is an iOS release build, signed for App Store distribution.
Target "ios-iphone-appstore" (fun () ->
    TemplatediOSBuild "AppStore" "iPhone" "Distribution"
)

// This target is an iOS release build, signed for InHouse distribution.
Target "ios-iphone-inhouse" (fun () ->
    TemplatediOSBuild "InHouse" "iPhone" "Distribution"
)

// This target is an iOS release build, signed for Ad-Hoc distribution.
Target "ios-iphone-adhoc" (fun () ->
    TemplatediOSBuild "Ad-Hoc" "iPhone" "Distribution"
)

// this target is an Android debug build, signed for AdHoc distribution
Target "android-debug" (fun () ->
    TemplatedAndroidBuild "Debug" "debug.keystore" "androiddebugkey" androidKeystorePassword googleMapsForAndroidv2ApiKey
)

// this target is an Android release build, signed for AdHoc distribution
Target "android-release" (fun () ->
    TemplatedAndroidBuild "Release" "XamarinCRMAndroid.keystore" "XamarinCRMAndroid" androidKeystorePassword googleMapsForAndroidv2ApiKey
)

RunTarget() 