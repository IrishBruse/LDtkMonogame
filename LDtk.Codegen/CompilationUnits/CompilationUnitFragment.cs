namespace LDtk.Codegen.CompilationUnits;

public abstract class CompilationUnitFragment
{
    public string Name { get; set; }

    public abstract void Render(CompilationUnitSource source);
}
