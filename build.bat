@echo off
".nuget\NuGet.exe" Install FAKE -OutputDirectory packages -ExcludeVersion
".nuget\NuGet.exe" Install NUnit.Runners -Version 2.6.4 -OutputDirectory packages -ExcludeVersion
"packages\FAKE\tools\Fake.exe" build.fsx