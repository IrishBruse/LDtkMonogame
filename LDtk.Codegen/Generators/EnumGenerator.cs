namespace LDtk.Codegen.Generators;

public class EnumGenerator(LDtkFile ldtkFile) : BaseGenerator
{
    readonly LDtkFile ldtkFile = ldtkFile;

    public void Generate()
    {
        foreach (EnumDefinition e in ldtkFile.Defs.Enums)
        {
            GenEnum(e);
        }

        foreach (EnumDefinition e in ldtkFile.Defs.ExternalEnums)
        {
            GenEnum(e);
        }
    }

    void GenEnum(EnumDefinition e)
    {
        Line($"namespace {Options.Namespace};");
        Blank();
        Line("// This file was automatically generated, any modifications will be lost!");
        Blank();
        Line("#pragma warning disable");
        Line($"public enum {e.Identifier}");
        StartBlock();
        foreach (EnumValueDefinition value in e.Values)
        {
            Line($"{value.Id},");
        }
        EndBlock();
        Line("#pragma warning restore");

        Output("Enums", e.Identifier);
    }
}
