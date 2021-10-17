using System.Collections.Generic;

#pragma warning disable CS1591
namespace LDtk.Generator
{
    public interface ICodeOutput
    {
        void OutputCode(List<CompilationUnitFragment> fragments, LdtkGeneratorContext ctx);
    }
}
#pragma warning restore CS1591