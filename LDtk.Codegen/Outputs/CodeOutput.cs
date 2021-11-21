using System.Collections.Generic;


namespace LDtk.Codegen;

public interface ICodeOutput
{
    void OutputCode(List<CompilationUnitFragment> fragments, LdtkGeneratorContext ctx);
}
