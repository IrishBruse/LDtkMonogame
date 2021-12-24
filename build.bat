dotnet clean

dotnet restore

dotnet build ./LDtk/
dotnet build ./LDtk.Codegen/
dotnet build ./LDtk.ContentPipeline/

dotnet build ./Examples/Api/
dotnet build ./Examples/Platformer/
dotnet build ./Examples/Shooter/

dotnet build ./Examples/Api/
dotnet build ./Examples/Platformer/
dotnet build ./Examples/Shooter/
