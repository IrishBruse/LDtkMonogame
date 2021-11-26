namespace LDtk.Codegen.CompilationUnits;

public abstract class CompilationUnitFragment
{
    public string name;
    public abstract void Render(CompilationUnitSource source);
}
