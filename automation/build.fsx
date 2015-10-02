#r @"packages/FAKE.3.5.4/tools/FakeLib.dll"
#load "build-helpers.fsx"
open Fake
open System
open System.IO
open System.Linq
open BuildHelpers
open Fake.XamarinHelper

Target "core-debug" (fun () ->
    RestorePackages "solutions/XamarinCRM.sln"

    MSBuild "../src/MobileApp/XamarinCRM/bin/Debug" "Build" [ ("Configuration", "Debug"); ("Platform", "Any CPU") ] [ "solutions/XamarinCRM.sln" ] |> ignore
)

Target "ios-debug" (fun () ->
    RestorePackages "solutions/XamarinCRM.iOS.sln"

    iOSBuild (fun defaults ->
        {defaults with
            ProjectPath = "solutions/XamarinCRM.iOS.sln"
            Configuration = "Debug|iPhoneSimulator"
            Target = "Build"
        })
)

Target "android-debug" (fun () ->
    RestorePackages "solutions/XamarinCRM.Android.sln"

    iOSBuild (fun defaults ->
        {defaults with
            ProjectPath = "solutions/XamarinCRM.Android.sln"
            Configuration = "Debug|iPhoneSimulator"
            Target = "Build"
        })
)

"core-debug"
  ==> "ios-debug"
  
"core-debug"
  ==> "android-debug"

RunTarget() 