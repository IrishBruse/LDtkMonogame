
using System.Collections.Generic;

namespace LDtk.Codegen.CompilationUnits;

public class CompilationUnit : CompilationUnitFragment
{
    public string classNamespace;
    public List<CompilationUnitFragment> fragments = new List<CompilationUnitFragment>();

    public override void Render(CompilationUnitSource source)
    {
        if (classNamespace != null)
        {
            source.AddLine($"namespace {classNamespace}");
            source.StartBlock();
        }

        foreach (CompilationUnitFragment fragment in fragments)
        {
            fragment.Render(source);
        }

        if (classNamespace != null)
        {
            source.EndBlock();
        }
    }
}
