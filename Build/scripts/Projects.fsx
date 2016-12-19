[<AutoOpen>]
module Projects = 

    let Name = "BitDiffer"

    type DotNetFrameworkIdentifier = { 
        MSBuild: string; 
        Nuget: string; 
        DefineConstants: string; 
    }

    type DotNetFramework = 
        | Net46
        static member All = [Net46] 
        member this.Identifier = 
            match this with
            | Net46 -> { MSBuild = "v4.6"; Nuget = "net46"; DefineConstants = "TRACE;NET46"; }

    type Project =
        | AgaControls
        | Client
        | Common
        | Console
        | Core
        | Extractor

    type PrivateProject =
        | Tests
        | Reference1
        | Reference2
        | Subject1
        | Subject2

    type DotNetProject = 
        | Project of Project
        | PrivateProject of PrivateProject

        static member All = 
            seq [
                Project Project.AgaControls; 
                Project Project.Client; 
                Project Project.Common; 
                Project Project.Console; 
                Project Project.Core; 
                Project Project.Extractor; 
                PrivateProject PrivateProject.Tests;
                PrivateProject PrivateProject.Reference1;
                PrivateProject PrivateProject.Reference2;
                PrivateProject PrivateProject.Subject1;
                PrivateProject PrivateProject.Subject2;
            ]

        member this.Name =
            match this with
            | Project p ->
                match p with
                | AgaControls -> "Aga.Controls"
                | Client -> "BitDiffer.Client"
                | Common -> "BitDiffer.Common"
                | Console -> "BitDiffer.Console"
                | Core -> "BitDiffer.Core"
                | Extractor -> "BitDiffer.Extractor"
            | PrivateProject p ->
                match p with
                | Tests -> "BitDiffer.Tests"
                | Reference1 -> "BitDiffer.Tests.Reference1"
                | Reference2 -> "BitDiffer.Tests.Reference2"
                | Subject1 -> "BitDiffer.Tests.Subject1"
                | Subject2 -> "BitDiffer.Tests.Subject2"
      
        static member TryFindName (name: string) =
            DotNetProject.All
            |> Seq.map(fun p -> p.Name)
            |> Seq.tryFind(fun p -> p.ToLowerInvariant() = name.ToLowerInvariant())

    type DotNetFrameworkProject = { framework: DotNetFramework; project: DotNetProject }


