using System.Collections.Generic;
using System.IO;
#pragma warning disable CS1591
namespace LDtk.Generator
{
    public class SingleFileOutput : ICodeOutput
    {
        public string OutputDir { get; set; }
        public string Filename { get; set; }

        public void OutputCode(List<CompilationUnitFragment> fragments, LdtkGeneratorContext ctx)
        {
            Directory.CreateDirectory(OutputDir);

            CompilationUnit cu = new CompilationUnit();
            cu.Name = Filename;
            cu.Namespace = ctx.CodeSettings.Namespace;
            cu.Fragments = fragments;

            CompilationUnitSource source = new CompilationUnitSource(ctx.CodeSettings);
            cu.Render(source);

            string filePath = OutputDir + "/" + Filename;
            File.WriteAllText(filePath, source.GetSourceCode());

        }
    }

}
#pragma warning restore CS1591