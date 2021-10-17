#pragma warning disable CS1591
namespace LDtk.Generator
{
    public abstract class CompilationUnitFragment
    {
        public string Name;
        public abstract void Render(CompilationUnitSource source);
    }
}
#pragma warning restore CS1591