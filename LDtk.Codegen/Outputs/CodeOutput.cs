namespace LDtk.Codegen.Outputs;

using System.Collections.Generic;
using LDtk.Codegen.CompilationUnits;
using LDtk.Codegen.Core;

public interface ICodeOutput
{
    void OutputCode(List<CompilationUnitFragment> fragments, LdtkGeneratorContext ctx);
}
