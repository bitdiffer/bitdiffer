#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"

#load @"Projects.fsx"
#load @"Paths.fsx"
#load @"Tooling.fsx"

open Fake 

open Paths
open Projects
open Tooling

type Build() = 

    static member Compile() = 
        Tooling.MsBuild.Build("Rebuild", Projects.DotNetFramework.Net46.Identifier)

    static member Clean() =
        CleanDir Paths.BuildOutput
        Projects.DotNetProject.All |> Seq.iter(fun p -> CleanDir(Paths.BinFolder p.Name))