namespace QuickTypeGenerator;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

public static class Program
{
    const string MinSchema = "https://raw.githubusercontent.com/deepnight/ldtk/master/docs/MINIMAL_JSON_SCHEMA.json";
    static readonly string[] Namespaces = {
        "using System;",
        "using System.Collections.Generic;",
        "using System.Text.Json.Serialization;",
        "",
        "using Microsoft.Xna.Framework;",
    };

    static readonly string[] IgnoreClasses = {
        "AutoRuleDef",
        "FieldDef"
    };

    static readonly string[] IgnoreFields = {
        "__FORCED_REFS",
        "levelFields",
    };

    static readonly Dictionary<string, EnumItems> Enums = new();

    public static async Task<int> Main()
    {
        using HttpClient wc = new();
        string json = await wc.GetStringAsync(MinSchema);

        ParseJson(json);

        return 0;
    }

    static void ParseJson(string json)
    {
        using StreamWriter file = new("../LDtk/LDtkJson.cs");

        JsonNode root = JsonNode.Parse(json);

        string rootClassPath = root["$ref"].GetValue<string>();
        var rootClass = GetNode(root, rootClassPath);

        file.WriteLine("namespace LDtk;");
        file.WriteLine();
        file.WriteLine("#nullable disable");
        file.WriteLine("#pragma warning disable CS8618, CS1591, CS8632, IDE1006");
        file.WriteLine();
        file.WriteLine("// LDtk " + root["version"].GetValue<string>());
        file.WriteLine();
        file.WriteLine(string.Join(Environment.NewLine, Namespaces));
        file.WriteLine();

        CreateClass("LDtkFile", rootClass, file);

        foreach ((string key, JsonNode type) in root["otherTypes"].AsObject().OrderBy(x => ToCSharpName(x.Key).TrimStart('_')))
        {
            if (IgnoreClasses.Contains(key))
            {
                continue;
            }

            CreateClass(ConvertClassName(key), type, file);
        }
        file.WriteLine();
        foreach ((string key, var val) in Enums.OrderBy(x => x.Key))
        {
            // file.WriteLine($"/// <summary> {val.Description} </summary>");
            file.Write($"public enum {key} ");
            file.Write("{");
            foreach (string item in val.Values)
            {
                file.Write($" {item},");
            }
            file.WriteLine("}");
            file.WriteLine();
        }
        file.WriteLine("#pragma warning restore");
        file.WriteLine("#nullable restore");
    }

    static JsonNode GetNode(JsonNode node, string path)
    {
        foreach (string part in path.Split('/'))
        {
            if (part == "#")
            {
                node = node.Root;
                continue;
            }

            try
            {
                node = node[part];
            }
            catch (Exception)
            {
                Console.WriteLine(node.GetPath());
                throw;
            }
        }

        return node;
    }

    static void CreateClass(string className, JsonNode node, StreamWriter file)
    {
        // file.WriteLine($"/// <summary> {node.Root["description"]} <br/> {node["description"] ?? className} </summary>");
        file.WriteLine($"public partial class {className}");
        file.WriteLine("{");

        JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
        var properties = node["properties"].Deserialize<Dictionary<string, Property>>(options).OrderBy(v => ToCSharpName(v.Key).TrimStart('_'));

        bool start = false;

        foreach ((string key, Property prop) in properties)
        {
            if (IgnoreFields.Contains(key))
            {
                continue;
            }

            string name = ToCSharpName(key);
            string description = prop.Description;

            string type = "";
            if (prop.Type != null)
            {
                type = GetType(prop);
            }
            else if (prop.Ref != null)
            {
                type = ParseRef(prop.Ref);
            }
            else if (prop.Enum != null)
            {
                type = name;

                if (!Enums.ContainsKey(name))
                {
                    Enums.Add(name, new()
                    {
                        Description = description,
                        Values = prop.Enum.Where(v => v != null).ToArray()
                    });
                }
            }
            else if (prop.OneOf != null)
            {
                type = OneOf(prop.OneOf);
            }
            else
            {
                type = "object";
            }

            if (description?.Length == 0)
            {
                description = name;
            }

            if (start)
            {
                file.WriteLine();
            }
            start = true;

            // file.WriteLine($"    /// <summary> {description} </summary>");
            file.WriteLine($"    [JsonPropertyName(\"{key}\")]");
            file.WriteLine($"    public {type} {name} {{ get; set; }}");
        }
        file.WriteLine("}");
        file.WriteLine();
    }

    static string OneOf(Items[] oneOf)
    {
        return "int";
    }

    static string ToCSharpName(string key)
    {
        if (key[0] == '_')
        {
            key = key[2..];
            return "_" + char.ToUpper(key[0]) + key[1..];
        }
        return char.ToUpper(key[0]) + key[1..];
    }

    static string GetType(Property value)
    {
        string[] types = value.Type;

        string baseType = types[0];
        string nullableType = "";
        if (types.Length > 1 && types[1] != null)
        {
            nullableType = types[1] switch
            {
                "null" => "?",
                _ => throw new NotImplementedException(types[1]),
            };
        }

        string arrayType = "";
        if (value.Items?.Ref != null)
        {
            arrayType = ParseRef(value.Items?.Ref);
        }
        if (value.Items?.Type != null)
        {
            arrayType = GetTypeName(value.Items.Type[0]);
        }

        return arrayType + GetTypeName(baseType) + nullableType;
    }

    static string ParseRef(string typeRef)
    {
        string[] parts = typeRef.Split("/");
        if (parts.Length != 0)
        {
            typeRef = GetCustomType(parts[^1]);
        }

        return typeRef;
    }

    static string GetTypeName(string type) => type switch
    {
        "boolean" => "bool",
        "integer" => "int",
        "number" => "float",
        "string" => "string",
        "array" => "[]",
        "object" => "object",
        "color" => "Color",
        "enum" => "string",
        "file" => "string",
        "any" => "object",
        "null" => "?",
        _ => throw new NotImplementedException(type),
    };

    static string ConvertClassName(string type) => type switch
    {
        "EntityDef" => "EntityDefinition",
        "EntityReferenceInfos" => "EntityReference",
        "FieldDef" => "FieldDefinition",
        "EnumDef" => "EnumDefinition",
        "EnumDefValues" => "EnumValueDefinition",
        "LayerDef" => "LayerDefinition",
        "IntGridValueGroupDef" => "IntGridValueGroupDefinition",
        "TilesetDef" => "TilesetDefinition",
        "World" => "LDtkWorld",
        "Level" => "LDtkLevel",
        "IntGridValueDef" => "IntGridValueDefinition",
        "TilesetRect" => "TilesetRectangle",
        "LevelBgPosInfos" => "LevelBackgroundPosition",
        "TableOfContentEntry" => "LDtkTableOfContentEntry",
        "Tile" => "TileInstance",
        _ => type,
    };

    static string GetCustomType(string type) => type switch
    {
        "AutoRuleDef" => "object",
        "FieldDefinition" => "object",
        _ => ConvertClassName(type),
    };
}

class Property
{
    public string Description { get; set; }
    public string[] Type { get; set; }
    public Items Items { get; set; }
    public Items[] OneOf { get; set; }
    public string[] Enum { get; set; }

    [JsonPropertyName("$ref")]
    public string Ref { get; set; }
}

class Items
{
    [JsonPropertyName("$ref")]
    public string Ref { get; set; }

    public string[] Type { get; set; }
}

class EnumItems
{
    public string Description { get; set; }
    public string[] Values { get; set; }
}
