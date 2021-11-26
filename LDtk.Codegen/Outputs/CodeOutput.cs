using System.Collections.Generic;
using LDtk.Codegen.CompilationUnits;
using LDtk.Codegen.Core;

namespace LDtk.Codegen.Outputs;

public interface ICodeOutput
{
    void OutputCode(List<CompilationUnitFragment> fragments, LdtkGeneratorContext ctx);
}
