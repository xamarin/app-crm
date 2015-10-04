module BuildHelpers

open Fake
open Fake.XamarinHelper
open System
open System.IO
open System.Linq

let Exec command args =
    let result = Shell.Exec(command, args)

    if result <> 0 then failwithf "%s exited with error %d" command result

let RestorePackages solutionFile =
    Exec "tools/NuGet/NuGet.exe" ("restore " + solutionFile)
    solutionFile |> RestoreComponents (fun defaults -> {defaults with ToolPath = "tools/xpkg/xamarin-component.exe" })

let RunNUnitTests dllPath xmlPath =
    Exec "/Library/Frameworks/Mono.framework/Versions/Current/bin/nunit-console4" (dllPath + " -xml=" + xmlPath)
    TeamCityHelper.sendTeamCityNUnitImport xmlPath

let RunUITests appPath =
    let testAppFolder = Path.Combine("tests", "TipCalc.UITests", "testapps")
    
    if Directory.Exists(testAppFolder) then Directory.Delete(testAppFolder, true)
    Directory.CreateDirectory(testAppFolder) |> ignore

    let testAppPath = Path.Combine(testAppFolder, DirectoryInfo(appPath).Name)

    Directory.Move(appPath, testAppPath)

    RestorePackages "tests/TipCalc.UITests/TipCalc.UITests.sln"

    MSBuild "tests/TipCalc.UITests/bin/Debug" "Build" [ ("Configuration", "Debug"); ("Platform", "Any CPU") ] [ "tests/TipCalc.UITests/TipCalc.UITests.sln" ] |> ignore

    RunNUnitTests "tests/TipCalc.UITests/bin/Debug/TipCalc.UITests.dll" "tests/TipCalc.UITests/bin/Debug/testresults.xml"

let RunTestCloudTests appFile deviceList =
    MSBuild "tests/TipCalc.UITests/bin/Debug" "Build" [ ("Configuration", "Debug"); ("Platform", "Any CPU") ] [ "tests/TipCalc.UITests/TipCalc.UITests.sln" ] |> ignore

    let testCloudToken = Environment.GetEnvironmentVariable("TestCloudApiToken")
    let args = String.Format(@"submit ""{0}"" {1} --devices {2} --series ""master"" --locale ""en_US"" --assembly-dir ""tests/TipCalc.UITests/bin/Debug"" --nunit-xml tests/TipCalc.UITests/testapps/testresults.xml", appFile, testCloudToken, deviceList)

    Exec "packages/Xamarin.UITest.0.6.1/tools/test-cloud.exe" args

    TeamCityHelper.sendTeamCityNUnitImport "Apps/tests/apps/testresults.xml"