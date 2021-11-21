
using System.Collections.Generic;

namespace LDtk.Codegen;
public class CompilationUnitClass : CompilationUnitFragment
{
    public string BaseClass { get; set; } = null;
    public List<CompilationUnitField> Fields = new();

    public override void Render(CompilationUnitSource source)
    {
        string extends = "";
        if (BaseClass != null)
        {
            extends = $" : {BaseClass}";
        }

        source.AddLine($"using LDtk;");
        source.AddLine($"using Microsoft.Xna.Framework;");
        source.AddLine("");
        source.AddLine($"public class {name}{extends}");
        source.StartBlock();

        foreach (CompilationUnitField field in Fields)
        {
            field.Render(source);
        }
        source.EndBlock();
    }
}
