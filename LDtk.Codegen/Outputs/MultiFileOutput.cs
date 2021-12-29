using System;
using System.Collections.Generic;
using System.IO;
using LDtk.Codegen.CompilationUnits;
using LDtk.Codegen.Core;

namespace LDtk.Codegen.Outputs;

public class MultiFileOutput : ICodeOutput
{
    public string OutputDir { get; set; }
    public bool PrintFragments { get; set; }

    public void OutputCode(List<CompilationUnitFragment> fragments, LdtkGeneratorContext ctx)
    {
        Directory.CreateDirectory(OutputDir);

        foreach (CompilationUnitFragment fragment in fragments)
        {
            CompilationUnit cuFile = new CompilationUnit()
            {
                ClassNamespace = ctx.CodeSettings.Namespace,
                Name = fragment.Name
            };

            cuFile.Fragments.Add(fragment);

            CompilationUnitSource source = new(ctx.CodeSettings);
            cuFile.Render(source);

            if (PrintFragments)
            {
                Console.WriteLine("Generating -> " + fragment.Name + ".cs");
            }

            string filePath = OutputDir + "/" + fragment.Name + ".cs";
            File.WriteAllText(filePath, source.GetSourceCode());
        }
    }
}
