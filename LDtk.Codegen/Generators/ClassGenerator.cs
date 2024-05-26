namespace LDtk.Codegen.Generators;

using LDtk.Codegen;

public class ClassGenerator : BaseGenerator
{
    public ClassGenerator(LDtkFile ldtkFile, Options options) : base(ldtkFile, options) { }

    public void Generate()
    {
        // Level Classes
        GenClass(Options.LevelClassName, LDtkFile.Defs.LevelFields, string.Empty, false);

        // Entity Classes
        foreach (EntityDefinition e in LDtkFile.Defs.Entities)
        {
            GenClass(e.Identifier, e.FieldDefs, "Entities", true);
        }
    }

    void GenClass(string identifier, FieldDefinition[] fields, string folder, bool isEntity)
    {
        Line($"namespace {Options.Namespace};");
        Blank();
        Line("// This file was automatically generated, any modifications will be lost!");
        Line("#pragma warning disable");
        Blank();
        Line("using LDtk;");
        Line("using Microsoft.Xna.Framework;");
        Blank();

        string classDef = $"public partial class {identifier}";

        if (isEntity && Options.EntityInterface)
        {
            classDef += " : ILDtkEntity";
        }

        Line(classDef);
        StartBlock();
        {
            if (isEntity)
            {
                Line("public string Identifier { get; set; }");
                Line("public System.Guid Iid { get; set; }");
                Line("public int Uid { get; set; }");
                Line("public Vector2 Position { get; set; }");
                Line("public Vector2 Size { get; set; }");
                Line("public Vector2 Pivot { get; set; }");
                Line("public Rectangle Tile { get; set; }");
                Blank();
                Line("public Color SmartColor { get; set; }");
                Blank();
            }

            foreach (FieldDefinition value in fields)
            {
                string type = Converter.ConvertFieldDefinitionTypes(value._Type, Options.PointAsVector2);

                if (value.CanBeNull)
                {
                    type += "?";
                }

                Line($"public {type} {value.Identifier} {{ get; set; }}");
            }
        }
        EndBlock();
        Line("#pragma warning restore");

        Output(folder, identifier);
    }
}
