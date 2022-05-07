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

        SourceFile sourceFile = new(false);
        ProcessFile(sourceFile, Quicktype.MinimalFilePath);
        sourceFile.Output(MinimalFilePath);

        sourceFile = new(true);
        ProcessFile(sourceFile, Quicktype.FullFilePath);
        sourceFile.Output(FullFilePath);
    }

    static void InitializeFile(SourceFile file, string path)
    {
        file.Add("// This file was auto generated, any changes will be lost.");
        file.Add("#pragma warning disable IDE1006,CA1711,CA1720");
        foreach (string use in Usings)
        {
            file.Add(use);
        }
        file.AddRange(File.ReadAllLines(path));
        file.Add("#pragma warning restore");
    }


    static void ProcessFile(SourceFile file, string path)
    {
        InitializeFile(file, path);

        for (int i = file.Count - 1; i >= 0; i--)
        {
            file[i] = Regex.Replace(file[i], @"Aaaaaaaaaaaa", "_");

            TypeConversion(file, i);
            CleanupDocComments(file, i);

            ForceLayerTypeToEnum(file, i);

            if (!file.isFull)
            {
                RemoveStuff(file, i);
            }

            RemoveLineWithComment(file, i, "public enum TypeEnum");

            RemoveLineWithComment(file, i, "_ForcedRefs _ForcedRefs");
            RemoveClass(file, i, "_ForcedRefs");

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

    static void RemoveLineWithComment(SourceFile file, int i, string match)
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

    static string CleanupDocComments(SourceFile file, int i)
    {
        // Doc comment cleanup
        if (file[i].Contains("///"))
        {
            file[i] = file[i].Replace("<br/> ", "");
            file[i] = file[i].Replace("<br/>", "");

            file[i] = file[i].Replace("`", "");
            file[i] = file[i].Replace("*", "");
            file[i] = file[i].Replace("IID", "Guid");
            file[i] = Regex.Replace(file[i], "\\[(.*)\\]\\(.*\\)", "$1");

            file[i] = file[i].Replace("Array<...> (eg. Array<Int>, Array<Point>", "<![CDATA[ Array<...> (eg. Array<Int>, Array<Point> ]]>");
        }

        return file[i];
    }

    static string TypeConversion(SourceFile file, int i)
    {
        file[i] = file[i].Replace("double", "float");
        file[i] = file[i].Replace("long", "int");

        file[i] = file[i].Replace("string Color", "Color Color");
        file[i] = file[i].Replace("string BgColor", "Color BgColor");

        file[i] = file[i].Replace("int[] Px", "Point Px");
        file[i] = file[i].Replace("int[] Src", "Point Src");
        file[i] = file[i].Replace("int[] _Grid", "Point _Grid");
        file[i] = file[i].Replace("int[] TopLeftPx", "Point TopLeftPx");

        file[i] = file[i].Replace("float[] _Pivot", "Vector2 _Pivot");
        file[i] = file[i].Replace("float[] Scale", "Vector2 Scale");

        file[i] = file[i].Replace("float[] CropRect", "Rectangle CropRect");
        file[i] = file[i].Replace("float[] _TileSrcRect", "Rectangle _TileSrcRect");

        file[i] = file[i].Replace("TypeEnum Type", "LayerType Type");

        // string -> Guid/Iid
        file[i] = Regex.Replace(file[i], "(public string )(.*)(Iid )", "public Guid $2Iid ");

        return file[i];
    }

    static string RemoveStuff(SourceFile file, int i)
    {
        RemoveLineWithComment(file, i, "LDtkLevel[] Levels");
        RemoveLineWithComment(file, i, "AutoLayerRuleDefinition AutoRuleDef");
        RemoveLineWithComment(file, i, "AutoLayerRuleDefinition[] Rules");

        RemoveClass(file, i, "AutoLayerRuleDefinition");
        RemoveClass(file, i, "AutoLayerRuleGroup");

        return file[i];
    }
}
