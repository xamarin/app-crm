#r @"packages/FAKE/tools/FakeLib.dll"
#load "build-helpers.fsx"
open Fake
open System
open System.IO
open System.Linq
open BuildHelpers
open Fake.XamarinHelper

let androidKeystorePassword = getBuildParamOrDefault "android_keystore_password" "not_provided"

let RelativeToRepoRoot path = 
    ("../../" + path)

let mobileAppSourcePath = "src/MobileApp/"

let mobileAppRelativeSourcePath = RelativeToRepoRoot mobileAppSourcePath

let solutionFile = (mobileAppRelativeSourcePath + "XamarinCRM.sln")

let nuGetPackageOutputPath = (mobileAppRelativeSourcePath + "packages/")

let iOSProjectPath = (mobileAppRelativeSourcePath + "XamarinCRM.iOS/")

let iOSBuildOutputPath = "src/MobileApp/XamarinCRM/XamarinCRM.iOS/bin/"

let androidProjectPath = (mobileAppRelativeSourcePath + "XamarinCRM.Android/")

let androidProjectFile = (androidProjectPath + "XamarinCRM.Android.csproj")

let androidBuildOutputPath = (androidProjectPath + "bin/")

let RestorePackagesToHintPath = 
    Exec "tools/NuGet/NuGet.exe" ("restore " + solutionFile + " -PackagesDirectory " + nuGetPackageOutputPath)

// You may or may not want all of the following targets for your purposes. Modify to your liking.

// This target is mostly for a sanity check, to make sure the app builds with debug settings.
Target "ios-iphone-debug" (fun () ->
    RestorePackagesToHintPath
    
    iOSBuild (fun defaults ->
        {defaults with
            ProjectPath = solutionFile
            Platform = "iPhone"
            Configuration = "iOS Debug (Development)"
            Target = "Build"
            BuildIpa = true
        })
    TeamCityHelper.PublishArtifact (iOSBuildOutputPath + "Debug/*.ipa")
)

// This target is a release build, signed for App Store distribution.
Target "ios-iphone-appstore" (fun () ->
    RestorePackagesToHintPath
    
    iOSBuild (fun defaults ->
        {defaults with
            ProjectPath = solutionFile
            Platform = "iPhone"
            Configuration = "iOS AppStore (Distribution)"
            Target = "Build"
            BuildIpa = true
        })
    TeamCityHelper.PublishArtifact (iOSBuildOutputPath + "AppStore/*.ipa")
)

// This target is a release build, signed for InHouse distribution.
Target "ios-iphone-inhouse" (fun () ->
    RestorePackagesToHintPath
    
    iOSBuild (fun defaults ->
        {defaults with
            ProjectPath = solutionFile
            Platform = "iPhone"
            Configuration = "iOS InHouse (Distribution)"
            Target = "Build"
            BuildIpa = true
        })
    TeamCityHelper.PublishArtifact (iOSBuildOutputPath + "InHouse/*.ipa")
)

// This target is a release build, signed for Ad-Hoc distribution.
Target "ios-iphone-adhoc" (fun () ->
    RestorePackagesToHintPath
    
    iOSBuild (fun defaults ->
        {defaults with
            ProjectPath = solutionFile
            Platform = "iPhone"
            Configuration = "iOS Ad-Hoc (Distribution)"
            Target = "Build"
            BuildIpa = true
        })
    TeamCityHelper.PublishArtifact (iOSBuildOutputPath + "Ad-Hoc/*.ipa")
)

Target "android-release" (fun () ->
    RestorePackagesToHintPath

    AndroidPackage (fun defaults ->
        {defaults with
            ProjectPath = androidProjectFile
            Configuration = "Release"
            OutputPath = (androidBuildOutputPath + "Release")
        }) 
    |> AndroidSignAndAlign (fun defaults ->
        {defaults with
            ZipalignPath = "tools/zipalign"
            KeystorePath = (mobileAppRelativeSourcePath + "XamarinCRMAndroid.keystore")
            KeystorePassword = androidKeystorePassword // TODO: Don't store this in the build script for a real app! This gets passed in at the top of the build script.
            KeystoreAlias = "XamarinCRMAndroid"
        })
    |> fun file -> TeamCityHelper.PublishArtifact file.FullName
)

RunTarget() 