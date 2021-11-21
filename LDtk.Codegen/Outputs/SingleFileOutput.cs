using System.Collections.Generic;
using System.IO;

namespace LDtk.Codegen
{
    public class SingleFileOutput : ICodeOutput
    {
        public string OutputDir { get; set; }
        public string Filename { get; set; }

        public void OutputCode(List<CompilationUnitFragment> fragments, LdtkGeneratorContext ctx)
        {
            Directory.CreateDirectory(OutputDir);

            CompilationUnit cu = new()
            {
                name = Filename,
                Namespace = ctx.CodeSettings.Namespace,
                Fragments = fragments
            };

            CompilationUnitSource source = new(ctx.CodeSettings);
            cu.Render(source);

            string filePath = OutputDir + "/" + Filename;
            File.WriteAllText(filePath, source.GetSourceCode());

        }
    }

}
