#r @"packages/FAKE.3.5.4/tools/FakeLib.dll"
#load "build-helpers.fsx"
open Fake
open System
open System.IO
open System.Linq
open BuildHelpers
open Fake.XamarinHelper

Target "ios-debug" (fun () ->
    Exec "tools/NuGet/NuGet.exe restore ../src/MobileApp/XamarinCRM.iOS/packages.config -PackagesDirectory ../src/MobileApp/packages/"

    solutionFile |> RestoreComponents (fun defaults -> {defaults with ToolPath = "tools/xpkg/xamarin-component.exe" })

    iOSBuild (fun defaults ->
        {defaults with
            ProjectPath = "solutions/XamarinCRM.iOS.sln"
            Configuration = "Debug|iPhoneSimulator"
            Target = "Build"
        })
)

Target "android-debug" (fun () ->
    Exec "tools/NuGet/NuGet.exe restore ../src/MobileApp/XamarinCRM.Android/packages.config -PackagesDirectory ../src/MobileApp/packages/"
    
    iOSBuild (fun defaults ->
        {defaults with
            ProjectPath = "solutions/XamarinCRM.Android.sln"
            Configuration = "Debug|iPhoneSimulator"
            Target = "Build"
        })
)

RunTarget() 