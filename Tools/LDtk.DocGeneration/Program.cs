namespace DocGenerator;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

public class Program
{

    private const string XMLDocPath = "../../LDtk/bin/Debug/net6.0/LDtkMonogame.xml";
    private const string SummaryTemplatePath = "../../LDtk.Documentation/src/SUMMARY_TEMPLATE.md";

    private static HashSet<string> excludedFiles = new(){
        "LDtkFileReader",
        "LDtkLevelReader",
        "When",
        "LdtkCustomCommand",
        "LDtkFieldParser",
    };

    public static void Main()
    {
        ParseDocumentation();
    }

    private static void ParseDocumentation()
    {
        Dictionary<string, Type> types = new();

        foreach (AssemblyName assemblyName in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
        {
            if (assemblyName.Name != "LDtkMonogame")
            {
                continue;
            }

            Assembly assembly = Assembly.Load(assemblyName);
            foreach (Type type in assembly.GetTypes())
            {
                types.Add(type.FullName, type);
            }
        }

        try
        {
            Directory.Delete("../../LDtk.Documentation/src/Api/", true);
        }
        catch (Exception)
        {
        }

        _ = Directory.CreateDirectory("../../LDtk.Documentation/src/Api/");

        XmlDocument doc = new();

        string text = File.ReadAllText(XMLDocPath);

        doc.LoadXml(text);

        XmlNodeList list = doc.DocumentElement.SelectSingleNode("/doc/members").ChildNodes;

        Dictionary<string, TypeDocs> docs = new();

        foreach (XmlNode node in list)
        {
            string[] parts = node.Attributes[0].InnerText.Split(":");
            string data = parts[1];

            switch (parts[0])
            {
                case "T":
                {
                    string type = data.Split(".")[^1];
                    if (excludedFiles.Contains(type))
                    {
                        continue;
                    }
                    docs.Add(type, new TypeDocs(type, node.FirstChild.InnerText));
                }
                break;

                case "P":
                {
                    string type = data.Split(".")[^2];
                    if (excludedFiles.Contains(type))
                    {
                        continue;
                    }
                    string property = data.Split(".")[^1];

                    string key = data[0..data.LastIndexOf(".", StringComparison.CurrentCulture)];

                    if (!docs.TryGetValue(type, out TypeDocs val))
                    {
                        Console.WriteLine(data);
                        break;
                    }

                    if (!types.TryGetValue(key, out Type t))
                    {
                        val.Properties.Add($"- types **{data}**");
                        break;
                    }

                    PropertyInfo ty = t.GetProperty(property, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
                    if (ty != null)
                    {
                        string returnType = ProcessReturnType(ty.PropertyType, false);
                        val.Properties.Add($"{ToMarkdownText(node.FirstChild.InnerText)}\n```csharp\npublic {returnType} {property} {{ get; set; }}\n```\n");
                    }
                    else
                    {
                        val.Properties.Add($"- else **{data}**");
                    }
                }
                break;

                case "F":
                {
                    string type = data.Split(".")[^2];
                    if (excludedFiles.Contains(type))
                    {
                        continue;
                    }

                    string field = data.Split(".")[^1];

                    string key = data[0..data.LastIndexOf(".", StringComparison.CurrentCulture)];

                    if (!docs.TryGetValue(type, out TypeDocs val))
                    {
                        Console.WriteLine(data);
                        break;
                    }

                    if (!types.TryGetValue(key, out Type t))
                    {
                        val.Fields.Add($"- types **{data}**");
                        break;
                    }

                    FieldInfo ty = t.GetField(field, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
                    if (ty != null)
                    {
                        string returnType = ProcessReturnType(ty.FieldType, ty.IsStatic);
                        val.Fields.Add($"{ToMarkdownText(node.FirstChild.InnerText)}\n```csharp\npublic {returnType} {field} {{ get; set; }}\n```\n");
                    }
                    else
                    {
                        val.Fields.Add($"- else **{data}**");
                    }
                }
                break;

                case "M":
                {
                    string[] memberParts = data.Split("(");
                    string memeberData = memberParts[0];

                    string[] parameters;

                    if (memberParts.Length > 1)
                    {
                        parameters = memberParts[1][..^1].Split(",");
                        for (int i = 0; i < parameters.Length; i++)
                        {
                            parameters[i] = parameters[i].Split(".")[^1];
                        }
                    }
                    else
                    {
                        parameters = Array.Empty<string>();
                    }

                    string type = memeberData.Split(".")[^2];
                    if (excludedFiles.Contains(type))
                    {
                        continue;
                    }
                    string field = memeberData.Split(".")[^1];

                    string key = memeberData[0..memeberData.LastIndexOf(".", StringComparison.CurrentCulture)];

                    if (!docs.TryGetValue(type, out TypeDocs val))
                    {
                        Console.WriteLine(data);
                        break;
                    }

                    if (!types.TryGetValue(key, out Type t))
                    {
                        val.Methods.Add($"- types **{memeberData}**");
                        break;
                    }

                    if (field.EndsWith("#ctor", StringComparison.CurrentCulture))
                    {
                        val.Methods.Add($"{ToMarkdownText(node.FirstChild.InnerText)}\n```csharp\npublic {type}({string.Join(",", parameters)})\n```\n");
                        break;
                    }

                    MethodInfo ty = t.GetMethod(data, BindingFlags.Instance | BindingFlags.OptionalParamBinding | BindingFlags.CreateInstance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                    MethodInfo[] tys = t.GetMethods(BindingFlags.Instance | BindingFlags.OptionalParamBinding | BindingFlags.CreateInstance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

                    // TODO fix this
                    MethodInfo method = tys.Where(m => m.Name == field).FirstOrDefault();

                    if (ty != null)
                    {
                        string returnType = ProcessReturnType(ty.ReturnType, ty.IsStatic);
                        val.Methods.Add($"{ToMarkdownText(node.FirstChild.InnerText)}\n```csharp\npublic {returnType} {field}({string.Join(",", parameters)});\n```\n");
                    }
                    else if (method != null)
                    {
                        string returnType = ProcessReturnType(method.ReturnType, method.IsStatic);
                        ParameterInfo[] ps = method.GetParameters();
                        IEnumerable<string> methodParams = ps.Select((p) => ProcessTypeName(p.ParameterType.Name.ToString()) + " " + p.Name);
                        val.Methods.Add($"{ToMarkdownText(node.FirstChild.InnerText)}\n```csharp\npublic {returnType} {field}({string.Join(", ", methodParams)});\n```\n");
                    }
                    else
                    {
                        val.Methods.Add($"- else **{memeberData}**");
                    }
                }
                break;

                default:
                Console.WriteLine(data);
                break;
            }
        }

        foreach (TypeDocs item in docs.Values)
        {
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

            string path = "../../LDtk.Documentation/src/Api/" + item.Name + ".md";

            Console.WriteLine(item.Name);
            Console.WriteLine(path);

            File.AppendAllText(path, txt);
        }

        using StreamWriter indexFileWriter = File.CreateText("../../LDtk.Documentation/src/Api/index.md");

        indexFileWriter.WriteLine("# APi");
        indexFileWriter.WriteLine();
        indexFileWriter.WriteLine("The docs inside api are auto generated  ");
        indexFileWriter.WriteLine("This is the list of classes in LDtkMonogame  ");
        indexFileWriter.WriteLine();

        string summaryTemplate = File.ReadAllText(SummaryTemplatePath);

        StringBuilder output = new();
        foreach (string item in docs.Keys)
        {
            indexFileWriter.WriteLine($"  - [{item}](./{item}.md)");
            _ = output.AppendLine($"  - [{item}](./Api/{item}.md)");
        }

        summaryTemplate = summaryTemplate.Replace("API_REPLACE", output.ToString());

        File.WriteAllText(SummaryTemplatePath.Replace("_TEMPLATE", ""), summaryTemplate);

    }

    private static string ProcessReturnType(Type declType, bool isStatic)
    {
        string returnType;
        if (isStatic)
        {
            returnType = "static " + ProcessTypeName(declType.Name);
        }
        else if (declType.Name.StartsWith("Nullable", StringComparison.CurrentCulture))
        {
            returnType = ProcessTypeName(declType.GenericTypeArguments[0].Name) + "?";
        }
        else if (declType.Name.StartsWith("IEnumerable", StringComparison.CurrentCulture))
        {
            returnType = "IEnumerable<" + ProcessTypeName(declType.GenericTypeArguments[0].Name) + ">";
        }
        else
        {
            returnType = ProcessTypeName(declType.Name);
        }

        return returnType;
    }

    private static string ProcessMethod(string input, string type)
    {
        return input.Replace("``1", "<T>");
    }

    private static string ProcessTypeName(string input)
    {
        return input
        .Replace("Boolean", "bool")
        .Replace("Int32", "int")
        .Replace("Single", "float")
        .Replace("String", "string");
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
