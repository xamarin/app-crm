@echo off
cls
if not exist packages\FAKE\tools\Fake.exe (
  tools\NuGet\NuGet.exe install FAKE -OutputDirectory packages -ExcludeVersion
)
if not exist packages\NUnit.Runners\tools\nunit-console.exe (
  tools\NuGet\NuGet.exe install NUnit.Runners -OutputDirectory packages -ExcludeVersion
)
packages\FAKE\tools\FAKE.exe build.fsx %* 2>&1