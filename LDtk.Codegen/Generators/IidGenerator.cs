namespace LDtk.Codegen.Generators;

public class IidGenerator : BaseGenerator
{
    LDtkFile ldtkFile;
    readonly Options options;

    public IidGenerator(LDtkFile ldtkFile, Options options)
    {
        this.ldtkFile = ldtkFile;
        this.options = options;
    }

    public void Generate()
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
        Line($"public static class Worlds");
        StartBlock();
        foreach (LDtkWorld w in ldtkFile.Worlds)
        {
            Line($"public static class {w.Identifier}");
            StartBlock();
            Line($"public static readonly System.Guid Iid = System.Guid.Parse(\"{w.Iid}\");");
            Blank();
            foreach (LDtkLevel l in w.Levels)
            {
                Line($"public static readonly System.Guid {l.Identifier} = System.Guid.Parse(\"{l.Iid}\");");
            }
            EndBlock();
        }
        EndBlock();

        if (options.BlockScopeNamespace)
        {
            EndBlock();
        }

        Line($"#pragma warning restore");

        Output(options, "Iids", "Worlds");
    }
}
