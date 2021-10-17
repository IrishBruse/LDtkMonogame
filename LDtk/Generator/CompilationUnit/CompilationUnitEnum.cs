using System.Collections.Generic;
#pragma warning disable CS1591
namespace LDtk.Generator
{
    public class CompilationUnitEnum : CompilationUnitFragment
    {
        public List<string> Literals = new List<string>();

        public override void Render(CompilationUnitSource source)
        {
            source.AddLine($"public enum {Name}");
            source.StartBlock();

            foreach (string literal in Literals)
            {
                source.AddLine($"{literal},");
            }
            source.EndBlock();
        }
    }
}
#pragma warning restore CS1591