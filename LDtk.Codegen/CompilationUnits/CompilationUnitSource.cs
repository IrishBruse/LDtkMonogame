using System.Collections.Generic;
using System.Text;

namespace LDtk.Codegen
{
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

            code.AppendLine("#pragma warning disable IDE1006");

            foreach (string use in imports)
            {
                code.AppendLine($"using {use};");
            }

            code.AppendLine();
            code.Append(verbatimSrc);

            code.AppendLine("#pragma warning restore IDE1006");

            return code.ToString();
        }
    }
}
