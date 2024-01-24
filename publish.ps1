$version="1.2.0"

dotnet pack ./LDtk/LDtk.csproj -c Release -o ./Nuget/ /p:version=$version
dotnet pack ./LDtk.Codegen/LDtk.Codegen.csproj -c Release -o ./Nuget/ /p:version=$version
dotnet pack ./LDtk.ContentPipeline/LDtk.ContentPipeline.csproj -c Release -o ./Nuget/ /p:version=$version

$packages = Get-ChildItem -Path ./Nuget/*.nupkg

foreach ($package in $packages) {
    dotnet nuget push --source https://api.nuget.org/v3/index.json --api-key $env:NugetApiKey $package
}
