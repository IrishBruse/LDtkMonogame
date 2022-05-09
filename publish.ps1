write-host "Enter nuget api key: " -nonewline
$key = read-host

dotnet pack -c Release -o ./Nuget/

$packages = Get-ChildItem -Path .\Nuget\*.nupkg

foreach ($package in $packages) {
    dotnet nuget push --source https://api.nuget.org/v3/index.json --api-key $key $package
}
rmdir .\Nuget\ -Force -Recurse -Confirm:$false
