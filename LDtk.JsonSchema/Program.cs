namespace QuickTypeGenerator;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public static class Program
{
    const string MinSchema = "https://raw.githubusercontent.com/deepnight/ldtk/master/docs/MINIMAL_JSON_SCHEMA.json";
    const string FullSchema = "https://raw.githubusercontent.com/deepnight/ldtk/master/docs/JSON_SCHEMA.json";

    static readonly string[] Namespaces = {
        "using System;",
        "using System.Collections.Generic;",
        "using System.Text.Json.Serialization;",
        "",
        "using Microsoft.Xna.Framework;",
    };

    static readonly Dictionary<string, Dictionary<string, string>> ClassPropertyTypeOverrides = new()
    {
        {"LDtkFile", new() {
            {"WorldLayout", "WorldLayout?"},
            {"BgColor",     "Color"},
        }},
        {"LDtkLevel", new() {
            {"_BgColor",    "Color"},
        }},
        {"LayerDefinition", new() {
            {"_Type",       "LayerType"},
        }},
        {"LayerInstance", new() {
            {"_Type",       "LayerType"},
        }},
        {"EntityInstance", new() {
            {"_Grid",       "Point"},
            {"_Pivot",      "Vector2"},
            {"_SmartColor", "Color"},
            {"Px",          "Point"},
        }},
        {"TileInstance", new() {
            {"Px",          "Point"},
            {"Src",         "Point"},
        }},
        {"FieldInstance", new() {
            {"RealEditorValues","object[]"},
        }},
        {"EntityDefinition", new() {
            {"Color",       "Color"},
        }},
        {"LevelBackgroundPosition", new() {
            {"Scale",       "Vector2"},
            {"TopLeftPx",   "Point"},
        }},
        {"AutoLayerRuleGroup", new() {
            {"Color",       "Color?"},
        }},
        {"IntGridValueDefinition", new() {
            {"Color",       "Color?"},
        }},
        {"AutoRuleDef", new() {
            {"TileRectsIds","int[][]"},
        }},
    };

    static readonly Dictionary<string, EnumItems> Enums = new();

    public static async Task<int> Main()
    {
        using HttpClient wc = new();

        string json = "";

        string[] ignoreClasses = {
            "FieldDefinition",
            "AutoRuleDef",
        };

        string[] ignoreFields = {
            "__FORCED_REFS",
            "LevelFields"
        };

        if (!File.Exists("MinSchema.json"))
        {
            json = await wc.GetStringAsync(MinSchema);
            File.WriteAllText("MinSchema.json", json);
        }
        else
        {
            json = File.ReadAllText("MinSchema.json");
        }
        ParseJson(json, "LDtk", "../LDtk/LDtkJson.cs", ignoreClasses, ignoreFields);

        if (!File.Exists("FullSchema.json"))
        {
            json = await wc.GetStringAsync(FullSchema);
            File.WriteAllText("FullSchema.json", json);
        }
        else
        {
            json = File.ReadAllText("FullSchema.json");
        }
        ParseJson(json, "LDtk.Codegen", "../LDtk.Codegen/LDtkJsonFull.cs", new[] { "" }, new[] { "__FORCED_REFS" });

        return 0;
    }

    static void ParseJson(string json, string ns, string output, string[] ignoreClasses, string[] ignoreFields)
    {
        using StreamWriter file = new(output);

        JsonNode root = JsonNode.Parse(json);

        string rootClassPath = root["$ref"].GetValue<string>();
        var rootClass = GetNode(root, rootClassPath);

        file.WriteLine($"namespace {ns};");
        file.WriteLine();
        file.WriteLine("#nullable disable");
        file.WriteLine("#pragma warning disable CS8618, CS1591, CS8632, IDE1006");
        file.WriteLine();
        file.WriteLine("// LDtk " + root["version"].GetValue<string>());
        file.WriteLine();
        file.WriteLine(string.Join(Environment.NewLine, Namespaces));
        file.WriteLine();

        CreateClass("LDtkFile", rootClass, file, ignoreFields);

        foreach ((string key, JsonNode type) in root["otherTypes"].AsObject().OrderBy(x => ToCSharpName(x.Key).TrimStart('_')))
        {
            if (ignoreClasses.Contains(ConvertClassName(key)))
            {
                continue;
            }

            CreateClass(ConvertClassName(key), type, file, ignoreFields);
        }

        foreach ((string key, var val) in Enums.OrderBy(x => x.Key))
        {
            file.WriteLine($"/// <summary> {ParseDocComment(val.Description)} </summary>");
            file.Write($"public enum {key} ");
            file.Write("{");
            foreach (string item in val.Values)
            {
                file.Write($" {item},");
            }
            file.WriteLine(" }");
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

    static void CreateClass(string className, JsonNode node, StreamWriter file, string[] ignoreFields)
    {
        string doc = ((string)node["description"]) ?? ((string)node["title"]);
        file.WriteLine($"/// <summary> {ParseDocComment(doc)} </summary>");
        file.WriteLine($"public partial class {className}");
        file.WriteLine("{");

        JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
        var properties = node["properties"].Deserialize<Dictionary<string, Property>>(options).OrderBy(v => ToCSharpName(v.Key).TrimStart('_'));

        bool start = false;

        foreach ((string key, Property prop) in properties)
        {
            prop.Name = ToCSharpName(key);

            if (ignoreFields.Contains(prop.Name))
            {
                continue;
            }

            string description = prop.Description;

            string type = "";
            if (prop.Type != null)
            {
                type = GetType(prop);
            }
            else if (prop.Ref != null)
            {
                type = ParseRefToType(prop.Ref);
            }
            else if (prop.Enum != null)
            {
                type = prop.Name + "?";

                if (!Enums.ContainsKey(prop.Name))
                {
                    Enums.Add(prop.Name, new()
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

            if (ClassPropertyTypeOverrides.TryGetValue(className, out Dictionary<string, string> overrides) && overrides.TryGetValue(prop.Name, out string overrideType))
            {
                type = overrideType;
            }

            if (description?.Length == 0)
            {
                description = prop.Name;
            }

            if (start)
            {
                file.WriteLine();
            }
            start = true;

            file.WriteLine($"    /// <summary> {ParseDocComment(description)} </summary>");
            file.WriteLine($"    [JsonPropertyName(\"{key}\")]");
            file.WriteLine($"    public {type} {prop.Name} {{ get; set; }}");
        }
        file.WriteLine("}");
        file.WriteLine();
    }

    static string OneOf(Items[] oneOf)
    {
        string type = "";
        bool nullable = false;

        foreach (Items item in oneOf)
        {
            if (item.Ref != null)
            {
                type += ParseRefToType(item.Ref);
            }
            else
            {
                if (item.Type[0] == "null")
                {
                    nullable = true;
                }
                else
                {
                    string itemType = ConvertJsonTypeToCSharpType(item.Type[0]);
                    type += itemType;
                }
            }
        }

        return type + (nullable ? "?" : "");
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
            arrayType = ParseRefToType(value.Items?.Ref);
        }
        else if (value.Items?.Type != null)
        {
            arrayType = ConvertJsonTypeToCSharpType(value.Items.Type[0]);
        }
        else if (value.Items?.Enum != null)
        {
            Enums.Add(value.Name.TrimEnd('s'), new()
            {
                Description = value.Description,
                Values = value.Items?.Enum
            });
            arrayType = ToCSharpName(value.Name.TrimEnd('s'));
        }

        if (value.Name.Contains("Iid"))
        {
            baseType = "Guid";
        }

        return arrayType + ConvertJsonTypeToCSharpType(baseType) + nullableType;
    }

    static string ParseRefToType(string typeRef)
    {
        string[] parts = typeRef.Split("/");
        if (parts.Length != 0)
        {
            typeRef = GetCustomType(parts[^1]);
        }

        return typeRef;
    }

    static string ConvertJsonTypeToCSharpType(string type) => type switch
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
        _ => type,
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

    static string ParseDocComment(string doc)
    {
        string documentation = doc
        .Replace(" the the ", " the ")// Temp Fix already fixed for next update
        .Replace("<...>", "&lt;...&gt;")
        .Replace("<Int>", "&lt;Int&gt;")
        .Replace("<Point>", "&lt;Point&gt;")
        .Replace("<...>", "&lt;...&gt;")
        .Replace("`<`", "&lt;")
        .Replace("`>`", "&gt;");

        return Regex.Replace(Regex.Replace(documentation, "\\*\\*(.+?)\\*\\*", "<b>$1</b>"), "`(.+?)`", "<c>$1</c>");
    }
}

class Property
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string[] Type { get; set; }
    public Items Items { get; set; }
    public Items[] OneOf { get; set; }

    [JsonPropertyName("$ref")]
    public string Ref { get; set; }
    public string[] Enum { get; set; }
}

class Items
{
    [JsonPropertyName("$ref")]
    public string Ref { get; set; }

    public string[] Type { get; set; }
    public string[] Enum { get; set; }
}

class EnumItems
{
    public string Description { get; set; }
    public string[] Values { get; set; }
}
