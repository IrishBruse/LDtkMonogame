namespace DocGenerator;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

using LDtk;
using LDtk.ContentPipeline;
using LDtk.Renderer;

public class Program
{
    private LDtkFile f;
    private LDtkRenderer r;
    private LDtkFileReader l;

    private const string XMLDocPath = "./bin/Debug/net6.0/LDtkMonogame.xml";
    private const string SummaryTemplatePath = "../../LDtk.Documentation/src/SUMMARY_TEMPLATE.md";
    private const string ApiFolder = "../../LDtk.Documentation/src/Api/";
    private static HashSet<string> excludedFiles = new(){
        "LDtkFileReader",
        "LDtkLevelReader",
        "When",
        "LdtkCustomCommand",
        "LDtkFieldParser",
    };

    private static Dictionary<string, TypeDocs> docs = new();
    private static Dictionary<string, Type> types = new();
    public static void Main()
    {
        DeleteApiFolder();

        GenerateTypes();

        XmlDocument xmlCommentsDocument = new();

        string text = File.ReadAllText(XMLDocPath);

        xmlCommentsDocument.LoadXml(text);

        XmlNodeList list = xmlCommentsDocument.DocumentElement.SelectSingleNode("/doc/members").ChildNodes;

        string[] commentlessTypes = {
            "EnumDefinition",
            "EnumValueDefinition",
            "LayerDefinition",
            "LayerInstance",
            "LdtkCustomCommand",
            "EntityDefinition",
            "EntityInstance",
            "FieldInstance",
        };

        foreach (string item in commentlessTypes)
        {
            docs.Add(item, new TypeDocs(item, item));
        }

        foreach (XmlNode node in list)
        {
            string[] parts = node.Attributes[0].InnerText.Split(":");
            string data = parts[1];

            data = RemoveNamespace(data);

            switch (parts[0])
            {
                case "T":
                {
                    string type = data.Split(".")[0];
                    if (excludedFiles.Contains(type))
                    {
                        continue;
                    }
                    docs.Add(type, new TypeDocs(type, node.FirstChild.InnerText));
                }
                break;

                case "P":
                Process(node.FirstChild.InnerText, data, false, false);
                break;

                case "F":
                Process(node.FirstChild.InnerText, data, true, false);
                break;

                case "M":
                Process(node.FirstChild.InnerText, data, false, true);
                break;
            }
        }

        List<TypeDocs> items = docs.Values.ToList();
        items.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));

        using StreamWriter indexFileWriter = File.CreateText(ApiFolder + "index.md");

        indexFileWriter.WriteLine("# APi");
        indexFileWriter.WriteLine();
        indexFileWriter.WriteLine("The docs inside api are auto generated  ");
        indexFileWriter.WriteLine("This is the list of classes in LDtkMonogame  ");
        indexFileWriter.WriteLine();

        StringBuilder output = new();

        foreach (TypeDocs item in items)
        {
            indexFileWriter.WriteLine($"  - [{item.Name}](./{item.Name}.md)");
            _ = output.AppendLine($"  - [{item.Name}](./Api/{item.Name}.md)");

            string txt
            = $"# {item.Name}\n"
            + $"\n"
            + $"{ToMarkdownText(item.Description)}\n"
            + $"\n";

            if (item.Methods.Count > 0)
            {
                txt
                += $"## Methods\n"
                + $"\n"
                + $"{string.Join("\n", item.Methods)}\n"
                + $"\n";
            }

            if (item.Properties.Count > 0)
            {
                txt
                += $"## Properties\n"
                + $"\n"
                + $"{string.Join("\n", item.Properties)}\n"
                + $"\n";
            }

            if (item.Fields.Count > 0)
            {
                txt
                += $"## Fields\n"
                + $"\n"
                + $"{string.Join("\n", item.Fields)}\n"
                + $"\n";
            }

            string path = ApiFolder + item.Name + ".md";

            File.AppendAllText(path, txt);
        }

        string summaryTemplate = File.ReadAllText(SummaryTemplatePath);
        summaryTemplate = summaryTemplate.Replace("API_REPLACE", output.ToString());
        File.WriteAllText(SummaryTemplatePath.Replace("_TEMPLATE", ""), summaryTemplate);
    }

    private static void GenerateTypes()
    {
        foreach (AssemblyName assemblyName in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
        {
            if (assemblyName.Name != "LDtkMonogame")
            {
                continue;
            }

            Assembly assembly = Assembly.Load(assemblyName);
            foreach (Type type in assembly.GetTypes())
            {
                types.Add(type.Name, type);
            }
        }
    }

    private static void Process(string description, string data, bool isField, bool isMethod)
    {
        string[] variableParts = data.Split(".");

        if (!docs.TryGetValue(variableParts[0], out TypeDocs doc))
        {
            Console.WriteLine("notFound: " + variableParts[0]);
            return;
        }

        StringBuilder markdown = new();
        _ = markdown.AppendLine(ToMarkdownText(description));
        _ = markdown.AppendLine();
        _ = markdown.AppendLine("```csharp");
        if (isField)
        {
            _ = markdown.AppendLine(ProcessField(data));
        }
        else if (isMethod)
        {
            _ = markdown.AppendLine(ProcessMethod(data));
        }
        else
        {
            _ = markdown.AppendLine(ProcessProperty(data));
        }
        _ = markdown.AppendLine("```");

        if (isField)
        {
            doc.Fields.Add(markdown.ToString());
        }
        else if (isMethod)
        {
            doc.Methods.Add(markdown.ToString());
        }
        else
        {
            doc.Properties.Add(markdown.ToString());
        }
    }

    private static string ProcessField(string data)
    {
        string[] variableParts = data.Split(".");

        if (!types.TryGetValue(variableParts[0], out Type t))
        {
            return "Could not find type " + variableParts[0];
        }

        FieldInfo ty = t.GetField(variableParts[1], BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
        return $"public {RemoveNamespaceFromType(ty.FieldType.ToString())} {ty.Name};";
    }

    private static string ProcessProperty(string data)
    {
        string[] variableParts = data.Split(".");

        if (!types.TryGetValue(variableParts[0], out Type t))
        {
            return "Could not find type " + variableParts[0];
        }

        PropertyInfo ty = t.GetProperty(variableParts[1], BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

        if (ty == null)
        {
            return data;
        }
        else
        {
            return $"public {RemoveNamespaceFromType(ty.PropertyType.ToString())} {ty.Name} {{ get; set; }}";
        }
    }

    private static string ProcessMethod(string data)
    {
        string[] methodParts = data.Split("(");
        string[] variableParts = methodParts[0][..^1].Split(".");

        if (!types.TryGetValue(variableParts[0], out Type t))
        {
            return "Could not find type " + variableParts[0];
        }

        MethodInfo ty = t
            .GetMethods(BindingFlags.Instance | BindingFlags.OptionalParamBinding | BindingFlags.CreateInstance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(m => methodParts[0].Replace("``1", "")
            .EndsWith(m.Name, StringComparison.CurrentCulture))
            .FirstOrDefault();

        if (ty == null)
        {
            return data;
        }
        else
        {
            string parametersTemplate = methodParts.Length > 1 ? methodParts[1][..^1] : "";

            string[] parameters = parametersTemplate.Split(",");

            for (int i = 0; i < parameters.Length; i++)
            {
                parameters[i] = RemoveNamespaceFromType(parameters[i]);
            }

            string genericT = methodParts[0].Contains("``1") ? "<T>" : string.Empty;
            return $"public {RemoveNamespaceFromType(ty.ReturnType.ToString())} {ty.Name}{genericT}({string.Join(",", parameters).Replace("``0,", "")})";
        }
    }

    private static string RemoveNamespace(string data)
    {
        return data.Replace("LDtk.Renderer.", "").Replace("LDtk.ContentPipeline.", "").Replace("LDtk.", "");
    }

    private static string RemoveNamespaceFromType(string data)
    {
        if (data.StartsWith("System.Nullable", StringComparison.CurrentCulture))
        {
            string[] nullableType = data.Split("[");
            return ProcessTypeName(nullableType[1][..^1].Split(".")[^1]) + "?";
        }
        else
        {
            return ProcessTypeName(data.Split(".")[^1]);
        }
    }
    private static string ProcessTypeName(string input)
    {
        return input
        .Replace("Boolean", "bool")
        .Replace("Int32", "int")
        .Replace("Single", "float")
        .Replace("Object", "object")
        .Replace("Void", "void")
        .Replace("String", "string");
    }

    private static void DeleteApiFolder()
    {
        try
        {
            Directory.Delete(ApiFolder, true);
        }
        catch (Exception)
        {
        }

        _ = Directory.CreateDirectory(ApiFolder);
    }

    private static string ToMarkdownText(string input)
    {
        string[] lines = input.Split("\n");
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = lines[i].Trim();
        }

        return string.Join("  \n", lines);
    }
}

[DebuggerDisplay("{Name}: {Description}")]
public class TypeDocs
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> Properties { get; set; } = new();
    public List<string> Fields { get; set; } = new();
    public List<string> Methods { get; set; } = new();

    public TypeDocs(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
