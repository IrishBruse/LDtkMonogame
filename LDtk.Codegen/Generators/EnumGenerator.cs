namespace LDtk.Codegen.Generators;

using System;
using System.IO;
using Raylib_CsLo.Codegen;

public class EnumGenerator : BaseGenerator
{
    LDtkFile ldtkFile;
    readonly Options options;

    public EnumGenerator(LDtkFile ldtkFile, Options options)
    {
        this.ldtkFile = ldtkFile;
        this.options = options;
    }

    public void Generate()
    {
        foreach (EnumDefinition e in ldtkFile.Defs.Enums)
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

            string file = Path.Join(options.Output, "Enums", e.Identifier + ".cs");
            Directory.CreateDirectory(Path.GetDirectoryName(file));
            File.WriteAllText(file, fileContents.ToString());
            Console.WriteLine("Generating -> Enums/" + e.Identifier);
            fileContents.Clear();
        }
    }
}
