
using System.Collections.Generic;

namespace LDtk.Codegen.CompilationUnits;
public class CompilationUnitEnum : CompilationUnitFragment
{
    public List<string> literals = new List<string>();

    public override void Render(CompilationUnitSource source)
    {
        source.AddLine($"public enum {name}");
        source.StartBlock();

        foreach (string literal in literals)
        {
            source.AddLine($"{literal},");
        }

        source.EndBlock();
    }
}
