#I @"../../packages/build/FAKE/tools"
#r @"FakeLib.dll"

#load "Projects.fsx"
#load "Paths.fsx"

open System
open System.IO
open System.Diagnostics
open System.Net

open Fake

open Projects
open Paths

[<AutoOpen>]
module Tooling = 
    (* helper functions *)
    #if mono_posix
    #r "Mono.Posix.dll"
    open Mono.Unix.Native
    let private applyExecutionPermissionUnix path =
        let _,stat = Syscall.lstat(path)
        Syscall.chmod(path, FilePermissions.S_IXUSR ||| stat.st_mode) |> ignore
    #else
    let private applyExecutionPermissionUnix path = ()
    #endif

    let execProcessWithTimeout proc arguments timeout = 
        let args = arguments |> String.concat " "
        ExecProcess (fun info ->
            info.FileName <- proc
            info.WorkingDirectory <- "."
            info.Arguments <- args
        ) timeout

    let private defaultTimeout = TimeSpan.FromMinutes 15.0

    let execProcess proc arguments =
        let exitCode = execProcessWithTimeout proc arguments defaultTimeout
        match exitCode with
        | 0 -> exitCode
        | _ -> failwithf "Calling %s resulted in unexpected exitCode %i" proc exitCode 

    let nugetFile =
        let targetLocation = "Build/tools/nuget/nuget.exe" 
        if (fileExists targetLocation = false)
        then
            trace (sprintf "Nuget not found at %s. Downloading now" targetLocation)
            let url = "http://dist.nuget.org/win-x86-commandline/latest/nuget.exe" 
            CreateDir "Build/tools/nuget"
            use webClient = new WebClient()
            webClient.DownloadFile(url, targetLocation)
            trace "nuget downloaded"
        targetLocation 

    type BuildTooling(path) =
        member this.Path = path
        member this.Exec arguments = execProcess this.Path arguments

    let Nuget = new BuildTooling(nugetFile)

    type MsBuildTooling() =

        member this.Build(target, framework:Projects.DotNetFrameworkIdentifier) =
            let solution = sprintf "%s.sln" Projects.Name |> Paths.Source
            let outputPath = Paths.BuildOutput |> Path.GetFullPath
            let setParams defaults =
                { defaults with
                    Verbosity = Some Quiet
                    Targets = [target]
                    Properties =
                        [
                            "OutputPathBaseDir", outputPath
                            "Optimize", "True"
                            "Configuration", "Release"
                            "TargetFrameworkVersion", framework.MSBuild
                            "DefineConstants", framework.DefineConstants
                        ]
                 }
        
            build setParams solution 

    let MsBuild = new MsBuildTooling()
