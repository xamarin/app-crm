@echo off
cls
if not exist packages\FAKE\tools\Fake.exe (
  .nuget\nuget.exe install FAKE -OutputDirectory packages -ExcludeVersion
)
if not exist packages\NUnit.Runners\tools\nunit-console.exe (
  .nuget\nuget.exe install NUnit.Runners -OutputDirectory packages -ExcludeVersion
)
packages\FAKE\tools\FAKE.exe build.fsx %* 2>&1