namespace LDtk.Codegen.Generators;

using Raylib_CsLo.Codegen;

public class EnumGenerator : BaseGenerator
{
    LDtkFile ldtkFile;
    Options options;

    public EnumGenerator(LDtkFile ldtkFile, Options options)
    {
        this.ldtkFile = ldtkFile;
        this.options = options;
    }

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
        Line($"namespace {options.Namespace};");
        Blank();
        Line($"#pragma warning disable");
        Line($"public enum {e.Identifier}");
        StartBlock();
        foreach (EnumValueDefinition value in e.Values)
        {
            Line($"{value.Id},");
        }
        EndBlock();
        Line($"#pragma warning restore");

        Output(options, "Enums", e.Identifier);
    }
}
