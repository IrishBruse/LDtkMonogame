using System.Collections.Generic;
using System.IO;
using LDtk.Codegen.CompilationUnits;
using LDtk.Codegen.Core;

namespace LDtk.Codegen.Outputs;

public class SingleFileOutput : ICodeOutput
{
    public string OutputDir { get; set; }
    public string Filename { get; set; }

    public void OutputCode(List<CompilationUnitFragment> fragments, LdtkGeneratorContext ctx)
    {
        _ = Directory.CreateDirectory(OutputDir);

        CompilationUnit cu = new()
        {
            name = Filename,
            classNamespace = ctx.CodeSettings.Namespace,
            fragments = fragments
        };

        CompilationUnitSource source = new(ctx.CodeSettings);
        cu.Render(source);

        string filePath = OutputDir + "/" + Filename;
        File.WriteAllText(filePath, source.GetSourceCode());

    }
}
