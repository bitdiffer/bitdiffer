#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"

#load @"Projects.fsx"
#load @"Paths.fsx"
#load @"Tooling.fsx"
#load @"Versioning.fsx"

open Fake 

open Paths
open Projects
open Versioning
open Tooling

type Release() = 
    static member NugetPack() =
        CreateDir Paths.NugetOutput

        let nuspec = Paths.Build (sprintf "%s.nuspec" Projects.Name)
        let nugetOutFile =  Paths.Output(sprintf "%s.%s.nupkg" Projects.Name Versioning.FileVersion)

        Tooling.Nuget.Exec ["pack"; nuspec; "-version"; Versioning.FileVersion; "-outputdirectory"; Paths.BuildOutput; ] |> ignore
        traceFAKE "%s" Paths.BuildOutput
        MoveFile Paths.NugetOutput nugetOutFile
        