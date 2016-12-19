#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"

#load "Projects.fsx"

open System
open System.IO
open System.Diagnostics
open System.Net

open Fake

open Projects

[<AutoOpen>]
module Paths =
    open Projects

    let Repository = "https://github.com/grennis/bitdiffer"

    let BuildFolder = "Build"
    let BuildOutput = sprintf "%s/output" BuildFolder

    let ProjectOutputFolder (project:DotNetProject) (framework:DotNetFramework) = 
        sprintf "%s/%s/%s" BuildOutput framework.Identifier.MSBuild project.Name

    let Tool tool = sprintf "packages/build/%s" tool
    let NugetOutput = sprintf "%s/_packages" BuildOutput
    let SourceFolder = "Source"
    
    let Output(folder) = sprintf "%s/%s" BuildOutput folder
    let Source(folder) = sprintf "%s/%s" SourceFolder folder
    let Build(folder) = sprintf "%s/%s" BuildFolder folder

    let BinFolder(folder) = 
        let f = replace @"\" "/" folder
        sprintf "%s/%s/bin/Release" SourceFolder f