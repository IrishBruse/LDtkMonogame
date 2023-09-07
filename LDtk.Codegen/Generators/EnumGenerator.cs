namespace LDtk.Codegen.Generators;

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
        Line($"public enum {e.Identifier}");
        StartBlock();
        foreach (EnumValueDefinition value in e.Values)
        {
            Line($"{value.Id},");
        }
        EndBlock();

        if (options.BlockScopeNamespace)
        {
            EndBlock();
        }

        Line($"#pragma warning restore");

        Output(options, "Enums", e.Identifier);
    }
}
