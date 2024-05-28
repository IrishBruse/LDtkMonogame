namespace LDtk.Codegen.Generators;

public class EnumGenerator(LDtkFile ldtkFile, Options options) : BaseGenerator(ldtkFile, options)
{
    public void Generate()
    {
        foreach (EnumDefinition e in LDtkFile.Defs.Enums)
        {
            GenEnum(e);
        }

        foreach (EnumDefinition e in LDtkFile.Defs.ExternalEnums)
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
