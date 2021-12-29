
using System.Collections.Generic;

namespace LDtk.Codegen.CompilationUnits;

public class CompilationUnit : CompilationUnitFragment
{
    public string ClassNamespace { get; set; }
    public List<CompilationUnitFragment> Fragments { get; set; } = new List<CompilationUnitFragment>();

    public override void Render(CompilationUnitSource source)
    {
        if (ClassNamespace != null)
        {
            source.AddLine($"namespace {ClassNamespace}");
            source.StartBlock();
        }

        foreach (CompilationUnitFragment fragment in Fragments)
        {
            fragment.Render(source);
        }

        if (ClassNamespace != null)
        {
            source.EndBlock();
        }
    }
}
