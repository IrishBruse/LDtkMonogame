using System;
using System.Collections.Generic;
using System.IO;
#pragma warning disable CS1591
namespace LDtk.Generator
{
    public class MultiFileOutput : ICodeOutput
    {
        public string OutputDir { get; set; }
        public bool PrintFragments { get; set; }

        public void OutputCode(List<CompilationUnitFragment> fragments, LdtkGeneratorContext ctx)
        {
            Directory.CreateDirectory(OutputDir);

            foreach (CompilationUnitFragment fragment in fragments)
            {
                CompilationUnit cuFile = new CompilationUnit();
                cuFile.Namespace = ctx.CodeSettings.Namespace;
                cuFile.Name = fragment.Name;
                cuFile.Fragments.Add(fragment);

                CompilationUnitSource source = new CompilationUnitSource(ctx.CodeSettings);
                cuFile.Render(source);

                if (PrintFragments == true)
                {
                    Console.WriteLine("    Generating -> " + fragment.Name + ".cs");
                }

                string filePath = OutputDir + "/" + fragment.Name + ".cs";
                File.WriteAllText(filePath, source.GetSourceCode());
            }
        }
    }

}
#pragma warning restore CS1591