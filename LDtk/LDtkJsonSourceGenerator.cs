namespace LDtk;

using System.Text.Json.Serialization;

/// <summary> The json source generator for LDtk files. </summary>
[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Metadata)]
[JsonSerializable(typeof(LDtkFile))]
[JsonSerializable(typeof(EntityReference))]
public partial class LDtkJsonSourceGenerator : JsonSerializerContext;
