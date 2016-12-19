#I @"../../packages/build/FAKE/tools"
#I @"../../packages/build/FSharp.Text.RegexProvider/lib/net40"
#r @"FakeLib.dll"
#r @"FSharp.Text.RegexProvider.dll"

#load @"Projects.fsx"
#load @"Paths.fsx"

open Projects
open Paths

open System
open System.Diagnostics
open System.IO
open System.Text.RegularExpressions
open FSharp.Text.RegexProvider

open Fake 
open AssemblyInfoFile
open SemVerHelper

type AssemblyFileRegex = Regex< @"\[assembly\: AssemblyFileVersion(Attribute)?\(""(?<Version>[^""]+)""\)\]", noMethodPrefix = true >

type Versioning() = 
    
    static let solutionInfo = Paths.Source "BitDiffer.Shared/SolutionInfo.cs"

    static member FileVersion =      
        let assemblyFileContents = ReadFileAsString solutionInfo
        let m = AssemblyFileRegex().Match assemblyFileContents
        let currentFileVersion = m.Version.Value.Trim()  
        
        getBuildParamOrDefault "version" currentFileVersion   

    static member PatchSolutionInfo() =
        let fileVersion = Versioning.FileVersion

        CreateCSharpAssemblyInfo solutionInfo [
            Attribute.Version fileVersion
            Attribute.FileVersion fileVersion
            Attribute.InformationalVersion fileVersion
            Attribute.Company ""
            Attribute.Copyright ""
            Attribute.Trademark ""
        ]