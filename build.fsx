#r @"tools/FAKE/tools/FakeLib.dll"
#load "build-helpers.fsx"
open Fake
open System
open System.IO
open System.Linq
open BuildHelpers
open Fake.XamarinHelper

let iOSProject = "src/MobileApp/XamarinCRM.iOS"

Target "ios-simulator-debug" (fun () ->
    RestorePackages "src/MobileApp/XamarinCRM.sln"
    
    iOSBuild (fun defaults ->
        {defaults with
            ProjectPath = "src/MobileApp/XamarinCRM.sln"
            Platform = "iPhoneSimulator"
            Configuration = "Debug"
            Target = "Build"
        })
)

Target "ios-simulator-release" (fun () ->
    RestorePackages "src/MobileApp/XamarinCRM.sln"
    
    iOSBuild (fun defaults ->
        {defaults with
            ProjectPath = "src/MobileApp/XamarinCRM.sln"
            Platform = "iPhoneSimulator"
            Configuration = "Release"
            Target = "Build"
        })
)

Target "ios-iphone-debug" (fun () ->
    RestorePackages "src/MobileApp/XamarinCRM.sln"
    
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
    RestorePackages "src/MobileApp/XamarinCRM.sln"
    
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
    RestorePackages "src/MobileApp/XamarinCRM.sln"
    
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