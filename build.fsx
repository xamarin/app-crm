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

let iOSProject = "src/MobileApp/XamarinCRM.iOS"

let androidProject = "src/MobileApp/XamarinCRM.Android"

let RestorePackagesToHintPath = Exec "tools/NuGet/NuGet.exe" ("restore " + solutionFile + " -PackagesDirectory " + packageOutputPath)

Target "ios-simulator-debug" (fun () ->
    RestorePackagesToHintPath
    
    iOSBuild (fun defaults ->
        {defaults with
            ProjectPath = solutionFile
            Platform = "iPhoneSimulator"
            Configuration = "Debug"
            Target = "Build"
        })
)

Target "ios-simulator-release" (fun () ->
    RestorePackagesToHintPath
    
    iOSBuild (fun defaults ->
        {defaults with
            ProjectPath = solutionFile
            Platform = "iPhoneSimulator"
            Configuration = "Release"
            Target = "Build"
        })
)

Target "ios-iphone-debug" (fun () ->
    RestorePackagesToHintPath
    
    iOSBuild (fun defaults ->
        {defaults with
            ProjectPath = solutionFile
            Platform = "iPhone"
            Configuration = "Debug"
            Target = "Build"
            BuildIpa = true
        })
    TeamCityHelper.PublishArtifact (iOSProject + "/bin/iPhone/Debug/*.ipa")
)

Target "ios-iphone-release" (fun () ->
    RestorePackagesToHintPath
    
    iOSBuild (fun defaults ->
        {defaults with
            ProjectPath = solutionFile
            Platform = "iPhone"
            Configuration = "Release"
            Target = "Build"
            BuildIpa = true
        })
    TeamCityHelper.PublishArtifact (iOSProject + "/bin/iPhone/Release/*.ipa")
)

Target "ios-iphone-inhouse" (fun () ->
    RestorePackagesToHintPath
    
    iOSBuild (fun defaults ->
        {defaults with
            ProjectPath = solutionFile
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