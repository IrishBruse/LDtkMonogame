namespace LDtk.Codegen;

public abstract class CompilationUnitFragment
{
    public string name;
    public abstract void Render(CompilationUnitSource source);
}
