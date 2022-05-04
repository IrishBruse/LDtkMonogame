namespace Raylib_CsLo.Codegen;

using System.Text;

public class BaseGenerator
{
    int indent;
    public StringBuilder fileContents = new();

    public bool Debug { get; protected set; }
    public bool Commented { get; protected set; }

    public void StartBlock()
    {
        Line("{");
        indent++;
    }

    public void EndBlock()
    {
        indent--;
        Line("}");
    }

    public static string Call(string functionName, string contents)
    {
        return $"{functionName}({contents})";
    }

    public void Blank()
    {
        fileContents.AppendLine();
    }

    public void Line(string line)
    {
        if (line != string.Empty)
        {
            for (int i = 0; i < indent; i++)
            {
                if (Commented && i == 0)
                {
                    fileContents.Append("    //  ");
                }
                else
                {
                    fileContents.Append("    ");
                }
            }
        }

        fileContents.AppendLine(line);
    }

    public void DebugLine(string line)
    {
        if (Debug)
        {
            Line($"// {line}");
        }
    }

    public void DocumentationBlock(string description)
    {
        Line($"/// <summary> {description} </summary>");
    }
}
