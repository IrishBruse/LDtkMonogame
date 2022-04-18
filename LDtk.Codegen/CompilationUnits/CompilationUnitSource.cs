namespace LDtk.Codegen.CompilationUnits;

using System.Collections.Generic;
using System.Globalization;
using System.Text;
using LDtk.Codegen.Core;

public class CompilationUnitSource
{
    readonly StringBuilder verbatimSrc;
    int currIndent;
    readonly CodeSettings cs;
    readonly SortedSet<string> imports;

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

        imports.Add(package);
    }

    public void AddLine(string line)
    {
        for (int i = 0; i < currIndent; i++)
        {
            verbatimSrc.Append(cs.IndentString);
        }

        verbatimSrc.Append(line);
        verbatimSrc.Append(cs.NewLine);
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
        StringBuilder code = new();

        if (cs.GeneratedFileHeader != null)
        {
            code.AppendLine(cs.GeneratedFileHeader);
        }

        code.AppendLine("#pragma warning disable");

        foreach (string use in imports)
        {
            code.AppendLine(CultureInfo.InvariantCulture, $"using {use};");
        }

        code.AppendLine();
        code.Append(verbatimSrc);

        code.AppendLine("#pragma warning restore");

        return code.ToString();
    }
}
