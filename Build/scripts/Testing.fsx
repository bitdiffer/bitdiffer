#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"

#load @"Projects.fsx"
#load @"Paths.fsx"
#load @"Tooling.fsx"

open System.IO
open Fake 
open Paths
open Projects
open Tooling

[<AutoOpen>]
module Tests = 
    let RunUnitTests() = 
        let testDir =  Paths.Output "v4.6/BitDiffer.Tests"
        !! (testDir @@ "BitDiffer.Tests.dll")
        |> MSTest.MSTest(fun p ->
            { p with
                WorkingDir = Path.GetFullPath <| testDir           
            })