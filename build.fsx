#r @"packages/FAKE/tools/FakeLib.dll"
#load "build-helpers.fsx"
open Fake
open System
open System.IO
open System.Linq
open BuildHelpers
open Fake.XamarinHelper

let mobileAppPath = "src/MobileApp/"

let solutionFile = (mobileAppPath + "XamarinCRM.sln")

let packageOutputPath = (mobileAppPath + "packages")

Target "ios-simulator-debug" (fun () ->
    Exec "tools/NuGet/NuGet.exe" ("restore " + solutionFile) ("-PackagesDirectory " + packageOutputPath)
    
    iOSBuild (fun defaults ->
        {defaults with
            ProjectPath = "src/MobileApp/XamarinCRM.sln"
            Platform = "iPhoneSimulator"
            Configuration = "Debug"
            Target = "Build"
        })
)

Target "ios-simulator-release" (fun () ->
    Exec "tools/NuGet/NuGet.exe" ("restore " + solutionFile) ("-PackagesDirectory " + packageOutputPath)
    
    iOSBuild (fun defaults ->
        {defaults with
            ProjectPath = "src/MobileApp/XamarinCRM.sln"
            Platform = "iPhoneSimulator"
            Configuration = "Release"
            Target = "Build"
        })
)

Target "ios-iphone-debug" (fun () ->
    Exec "tools/NuGet/NuGet.exe" ("restore " + solutionFile) ("-PackagesDirectory " + packageOutputPath)
    
    iOSBuild (fun defaults ->
        {defaults with
            ProjectPath = "src/MobileApp/XamarinCRM.sln"
            Platform = "iPhone"
            Configuration = "Debug"
            Target = "Build"
            BuildIpa = true
        })
    TeamCityHelper.PublishArtifact (iOSProject + "/bin/iPhone/Debug/*.ipa")
)

Target "ios-iphone-release" (fun () ->
    Exec "tools/NuGet/NuGet.exe" ("restore " + solutionFile) ("-PackagesDirectory " + packageOutputPath)
    
    iOSBuild (fun defaults ->
        {defaults with
            ProjectPath = "src/MobileApp/XamarinCRM.sln"
            Platform = "iPhone"
            Configuration = "Release"
            Target = "Build"
            BuildIpa = true
        })
    TeamCityHelper.PublishArtifact (iOSProject + "/bin/iPhone/Release/*.ipa")
)

Target "ios-iphone-inhouse" (fun () ->
    Exec "tools/NuGet/NuGet.exe" ("restore " + solutionFile) ("-PackagesDirectory " + packageOutputPath)
    
    iOSBuild (fun defaults ->
        {defaults with
            ProjectPath = "src/MobileApp/XamarinCRM.sln"
            Platform = "iPhone"
            Configuration = "Inhouse"
            Target = "Build"
            BuildIpa = true
        })
    TeamCityHelper.PublishArtifact (iOSProject + "/bin/iPhone/Inhouse/*.ipa")
)

"ios-iphone-release"
  ==> "ios-iphone-inhouse"

RunTarget() 