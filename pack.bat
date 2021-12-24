dotnet build -c Release ./LDtk -f netcoreapp3.1
dotnet build -c Release ./LDtk -f net5.0
dotnet build -c Release ./LDtk -f net6.0

dotnet build -c Release ./LDtk.Codegen

dotnet build -c Release ./LDtk.ContentPipeline

dotnet pack -c Release
