@echo off
cls
if not exist automation\FAKE\tools\Fake.exe (
  .nuget\nuget.exe install FAKE -OutputDirectory automation -ExcludeVersion
)
if not exist automation\NUnit.Runners\tools\nunit-console.exe (
  .nuget\nuget.exe install NUnit.Runners -OutputDirectory automation -ExcludeVersion
)
automation\FAKE\tools\FAKE.exe build.fsx %* 2>&1