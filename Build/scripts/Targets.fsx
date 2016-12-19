#r "../../packages/build/FAKE/tools/FakeLib.dll"

#load @"Projects.fsx"
#load @"Paths.fsx"
#load @"Tooling.fsx"
#load @"Versioning.fsx"
#load @"Releasing.fsx"
#load @"Building.fsx"
#load @"Testing.fsx"

open Fake

open Building
open Testing
open Versioning
open Releasing

Target "Build" <| fun _ -> traceHeader "STARTING BUILD"

Target "Clean" <| fun _ -> Build.Clean()

Target "BuildApp" <| fun _ -> Build.Compile()

Target "Test"  <| fun _ -> Tests.RunUnitTests()

Target "Version" <| fun _ -> Versioning.PatchSolutionInfo()

Target "Release" <| fun _ -> Release.NugetPack()
   
"Clean" 
  =?> ("Version", hasBuildParam "version")
  ==> "BuildApp"
  =?> ("Test", (not ((getBuildParam "skiptests") = "1")))
  ==> "Build"

"Version"
  ==> "Release"

"Build"
  ==> "Release"

RunTargetOrDefault "Build"

