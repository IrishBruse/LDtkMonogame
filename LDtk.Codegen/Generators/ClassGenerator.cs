namespace LDtk.Codegen.Generators;

using System;
using System.IO;
using Raylib_CsLo.Codegen;

public class ClassGenerator : BaseGenerator
{
    LDtkFile ldtkFile;
    readonly Options options;

    public ClassGenerator(LDtkFile ldtkFile, Options options)
    {
        this.ldtkFile = ldtkFile;
        this.options = options;
    }

    public void Generate()
    {
        // Entity Classes
        foreach (EntityDefinition e in ldtkFile.Defs.Entities)
        {
            GenClass(e.Identifier, e.FieldDefs, "Entities");
        }

        // Level Classes
        GenClass(options.LevelClassName, ldtkFile.Defs.LevelFields, "");
    }

    void GenClass(string identifier, FieldDefinition[] fields, string folder)
    {
        Line($"// This file was automatically generated, any modifications will be lost!");
        Blank();
        Line($"namespace {options.Namespace};");
        Blank();
        Line($"#pragma warning disable");
        Line("using Microsoft.Xna.Framework;");
        Line("using LDtk;");
        Blank();
        Line($"public class {identifier} : ILDtkEntity");
        StartBlock();
        {
            Line($"public string Identifier {{ get; set; }}");
            Line($"public long Uid {{ get; set; }}");
            Line($"public Vector2 Position {{ get; set; }}");
            Line($"public Vector2 Size {{ get; set; }}");
            Line($"public Vector2 Pivot {{ get; set; }}");
            Line($"public Rectangle Tile {{ get; set; }}");
            Blank();
            Line($"public Color EditorVisualColor {{ get; set; }}");
            Blank();
            foreach (FieldDefinition value in fields)
            {
                string type = Converter.ConvertFieldDefinitionTypes(value._Type);
                Line($"public {type} {value.Identifier} {{ get; set; }}");
            }
        }
        EndBlock();

        Line($"#pragma warning restore");

        string file = Path.Join(options.Output, folder, identifier + ".cs");
        Directory.CreateDirectory(Path.GetDirectoryName(file));
        File.WriteAllText(file, fileContents.ToString());
        Console.WriteLine("Generating -> " + folder + "/" + identifier);
        fileContents.Clear();
    }
}
