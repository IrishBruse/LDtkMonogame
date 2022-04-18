namespace LDtk.Codegen.CompilationUnits;

using System.Collections.Generic;

public class ClassCompilationUnit : CompilationUnitFragment
{
    public string BaseClass { get; set; }
    public List<CompilationUnitField> Fields { get; set; } = new List<CompilationUnitField>();

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
        source.AddLine($"public class {Name}{extends}");
        source.StartBlock();

        foreach (CompilationUnitField field in Fields)
        {
            field.Render(source);
        }

        source.EndBlock();
    }
}
