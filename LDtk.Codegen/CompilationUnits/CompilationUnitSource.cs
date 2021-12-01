using System.Collections.Generic;
using System.Text;
using LDtk.Codegen.Core;

namespace LDtk.Codegen.CompilationUnits;

public class CompilationUnitSource
{
    private readonly StringBuilder verbatimSrc;
    private int currIndent;
    private readonly CodeSettings cs;
    private readonly SortedSet<string> imports;

    public CompilationUnitSource(CodeSettings cs)
    {
        this.cs = cs;
        verbatimSrc = new StringBuilder();
        currIndent = 0;
        imports = new SortedSet<string>();
    }

    public void Using(string package)
    {
        if (package == null)
        {
            return;
        }

        _ = imports.Add(package);
    }

    public void AddLine(string line)
    {
        for (int i = 0; i < currIndent; i++)
        {
            _ = verbatimSrc.Append(cs.IndentString);
        }

        _ = verbatimSrc.Append(line);
        _ = verbatimSrc.Append(cs.NewLine);
    }

    public void StartBlock()
    {
        AddLine("{");
        currIndent++;
    }

    public void EndBlock()
    {
        currIndent--;
        AddLine("}");
    }

    public string GetSourceCode()
    {
        StringBuilder code = new StringBuilder();

        if (cs.GeneratedFileHeader != null)
        {
            _ = code.AppendLine(cs.GeneratedFileHeader);
        }

        _ = code.AppendLine("#pragma warning disable IDE1006");

        foreach (string use in imports)
        {
            _ = code.AppendLine($"using {use};");
        }

        _ = code.AppendLine();
        _ = code.Append(verbatimSrc);

        _ = code.AppendLine("#pragma warning restore IDE1006");

        return code.ToString();
    }
}
