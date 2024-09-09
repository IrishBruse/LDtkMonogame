namespace LDtk;

using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.Xna.Framework;

/// <summary> The json source generator for LDtk files. </summary>
[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Metadata)]
[JsonSerializable(typeof(JsonElement))]
[JsonSerializable(typeof(bool))]
[JsonSerializable(typeof(bool[]))]
[JsonSerializable(typeof(float))]
[JsonSerializable(typeof(float[]))]
[JsonSerializable(typeof(int))]
[JsonSerializable(typeof(int[]))]
[JsonSerializable(typeof(string))]
[JsonSerializable(typeof(string[]))]
[JsonSerializable(typeof(Color))]
[JsonSerializable(typeof(Color[]))]
[JsonSerializable(typeof(Point))]
[JsonSerializable(typeof(Point[]))]
[JsonSerializable(typeof(Vector2))]
[JsonSerializable(typeof(Vector2[]))]
[JsonSerializable(typeof(EntityReference))]
[JsonSerializable(typeof(EntityReference[]))]
[JsonSerializable(typeof(TilesetRectangle))]
[JsonSerializable(typeof(TilesetRectangle[]))]
[JsonSerializable(typeof(LDtkFile))]
public partial class LDtkJsonSourceGenerator : JsonSerializerContext;
