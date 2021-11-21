using System;
using System.Collections.Generic;
using System.IO;

namespace LDtk.Codegen;

public class MultiFileOutput : ICodeOutput
{
    public string OutputDir { get; set; }
    public bool PrintFragments { get; set; }

    public void OutputCode(List<CompilationUnitFragment> fragments, LdtkGeneratorContext ctx)
    {
        Directory.CreateDirectory(OutputDir);

        foreach (CompilationUnitFragment fragment in fragments)
        {
            CompilationUnit cuFile = new CompilationUnit
            {
                Namespace = ctx.CodeSettings.Namespace,
                name = fragment.name
            };

            cuFile.Fragments.Add(fragment);

            CompilationUnitSource source = new CompilationUnitSource(ctx.CodeSettings);
            cuFile.Render(source);

            if (PrintFragments == true)
            {
                Console.WriteLine("Generating -> " + fragment.name + ".cs");
            }

            string filePath = OutputDir + "/" + fragment.name + ".cs";
            File.WriteAllText(filePath, source.GetSourceCode());
        }
    }
}
