namespace QuickTypeGenerator;

using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

public class Program
{
    static readonly string MinimalFilePath = "../LDtk/LDtkJson.cs";
    static readonly string FullFilePath = "../LDtk.Codegen/LDtkJsonFull.cs";

    static readonly string[] Usings = {
        "using System;",
        "using System.Text.Json.Serialization;",
        "using Microsoft.Xna.Framework;",
    };

    public static void Main()
    {
        Quicktype.Generate();

        SourceFile sourceFile = new();
        ProcessFile(sourceFile, Quicktype.MinimalFilePath);
        sourceFile.Output(MinimalFilePath);

        sourceFile = new();
        ProcessFile(sourceFile, Quicktype.FullFilePath);
        sourceFile.Output(FullFilePath);
    }

    static void ProcessFile(SourceFile file, string path)
    {
        InitializeFile(file, path);

        for (int i = file.Count - 1; i >= 0; i--)
        {
            file[i] = Regex.Replace(file[i], @"Aaaaaaaaaaaa", "_");

            TypeConversion(file, i);
            CleanupDocComments(file, i);

            RemoveVariable(file, i, "_ForcedRefs _ForcedRefs");
            RemoveVariable(file, i, "AutoLayerRuleDefinition AutoRuleDef");
            RemoveVariable(file, i, "AutoLayerRuleDefinition[] Rules");
            RemoveClass(file, i, "_ForcedRefs");
            RemoveClass(file, i, "AutoLayerRuleDefinition");

            ForceLayerTypeToEnum(file, i);

            if (file[i].EndsWith("///"))
            {
                file.RemoveLine(i);
            }
        }
    }

    static void ForceLayerTypeToEnum(SourceFile file, int i)
    {
        // TODO ldtk/quicktype bug
        // Make LayerType Type string variable into enum
        if (file[i].Contains("IntGrid, Entities, Tiles or AutoLayer"))
        {
            file[i + 2] = file[i + 2].Replace("string", "LayerType");
        }
    }

    static void RemoveClass(SourceFile file, int i, string match)
    {
        if (file[i].Contains("public partial class " + match))
        {
            RemoveLineWithComments(file, i);

            int currentLine = i + 1;
            file.RemoveLine(currentLine++);
            int indent = 1;

            // Remove class body
            while (indent > 0)
            {
                indent += file[currentLine].Count(c => c == '{');
                indent -= file[currentLine].Count(c => c == '}');
                file.RemoveLine(currentLine++);
            }
        }
    }

    static void RemoveVariable(SourceFile file, int i, string match)
    {
        if (file[i].Contains(match))
        {
            RemoveLineWithComments(file, i);
        }
    }

    static void RemoveLineWithComments(SourceFile file, int i)
    {
        int currentLine = i;
        file.RemoveLine(currentLine--);

        if (file[currentLine].Contains("</summary>"))
        {
            while (!file[currentLine].Contains("<summary>"))
            {
                file.RemoveLine(currentLine--);
            }
            file.RemoveLine(currentLine--);
        }
    }

    static void InitializeFile(SourceFile file, string path)
    {
        file.Add("// This file was auto generated, any changes will be lost.");
        file.Add("#pragma warning disable");
        foreach (string use in Usings)
        {
            file.Add(use);
        }
        file.AddRange(File.ReadAllLines(path));
        file.Add("#pragma warning restore");
    }

    static string CleanupDocComments(SourceFile file, int index)
    {
        // Doc comment cleanup
        if (file[index].Contains("///"))
        {
            file[index] = file[index].Replace("<br/> ", "");
            file[index] = file[index].Replace("<br/>", "");

            file[index] = file[index].Replace("`", "");
            file[index] = file[index].Replace("*", "");
            file[index] = file[index].Replace("IID", "Guid");
            file[index] = Regex.Replace(file[index], "\\[(.*)\\]\\(.*\\)", "$1");

            file[index] = file[index].Replace("Array<...> (eg. Array<Int>, Array<Point>", "<![CDATA[ Array<...> (eg. Array<Int>, Array<Point> ]]>");
        }

        return file[index];
    }

    static string TypeConversion(SourceFile file, int index)
    {
        file[index] = file[index].Replace("double", "float");
        file[index] = file[index].Replace("long", "int");

        file[index] = file[index].Replace("string Color", "Color Color");
        file[index] = file[index].Replace("string BgColor", "Color BgColor");

        file[index] = file[index].Replace("int[] Px", "Point Px");
        file[index] = file[index].Replace("int[] Src", "Point Src");
        file[index] = file[index].Replace("int[] _Grid", "Point _Grid");
        file[index] = file[index].Replace("int[] TopLeftPx", "Point TopLeftPx");

        file[index] = file[index].Replace("float[] _Pivot", "Vector2 _Pivot");
        file[index] = file[index].Replace("float[] Scale", "Vector2 Scale");

        file[index] = file[index].Replace("float[] CropRect", "Rectangle CropRect");
        file[index] = file[index].Replace("float[] _TileSrcRect", "Rectangle _TileSrcRect");

        // string -> Guid/Iid
        file[index] = Regex.Replace(file[index], "(public string )(.*)(Iid )", "public Guid $2Iid ");

        return file[index];
    }
}
