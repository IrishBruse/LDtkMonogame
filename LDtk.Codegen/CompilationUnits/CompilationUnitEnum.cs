
using System.Collections.Generic;

namespace LDtk.Codegen;
public class CompilationUnitEnum : CompilationUnitFragment
{
    public List<string> Literals = new();

    public override void Render(CompilationUnitSource source)
    {
        source.AddLine($"public enum {name}");
        source.StartBlock();

        foreach (string literal in Literals)
        {
            source.AddLine($"{literal},");
        }
        source.EndBlock();
    }
}
