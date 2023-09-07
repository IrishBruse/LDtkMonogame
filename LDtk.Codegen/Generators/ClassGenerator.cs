namespace LDtk.Codegen.Generators;

using LDtk.Codegen;

public class ClassGenerator : BaseGenerator
{
    LDtkFile ldtkFile;
    Options options;

    public ClassGenerator(LDtkFile ldtkFile, Options options)
    {
        this.ldtkFile = ldtkFile;
        this.options = options;
    }

    public void Generate()
    {
        // Level Classes
        GenClass(options.LevelClassName, ldtkFile.Defs.LevelFields, "", false);

        // Entity Classes
        foreach (EntityDefinition e in ldtkFile.Defs.Entities)
        {
            GenClass(e.Identifier, e.FieldDefs, "Entities", true);
        }
    }

    void GenClass(string identifier, FieldDefinition[] fields, string folder, bool isEntity)
    {
        Line($"// This file was automatically generated, any modifications will be lost!");
        Line($"#pragma warning disable");

        if (options.BlockScopeNamespace)
        {
            Line($"namespace {options.Namespace}");
            StartBlock();
        }
        else
        {
            Line($"namespace {options.Namespace};");
        }

        Blank();
        Line("using LDtk;");
        Blank();
        Line("using Microsoft.Xna.Framework;");
        Blank();

        string classDef = $"public class {identifier}";

        if (isEntity)
        {
            classDef += " : ILDtkEntity";
        }

        Line(classDef);
        StartBlock();
        {
            if (isEntity)
            {
                Line($"public string Identifier {{ get; set; }}");
                Line($"public System.Guid Iid {{ get; set; }}");
                Line($"public int Uid {{ get; set; }}");
                Line($"public Vector2 Position {{ get; set; }}");
                Line($"public Vector2 Size {{ get; set; }}");
                Line($"public Vector2 Pivot {{ get; set; }}");
                Line($"public Rectangle Tile {{ get; set; }}");
                Blank();
                Line($"public Color SmartColor {{ get; set; }}");
                Blank();
            }

            foreach (FieldDefinition value in fields)
            {
                string type = Converter.ConvertFieldDefinitionTypes(value._Type, options.PointAsVector2);

                if (value.CanBeNull)
                {
                    type += "?";
                }

                Line($"public {type} {value.Identifier} {{ get; set; }}");
            }
        }
        EndBlock();

        if (options.BlockScopeNamespace)
        {
            EndBlock();
        }

        Line($"#pragma warning restore");

        Output(options, folder, identifier);
    }
}
