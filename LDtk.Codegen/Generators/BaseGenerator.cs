namespace LDtk.Codegen.Generators;

using System;
using System.IO;
using System.Text;

using LDtk.Codegen;

public class BaseGenerator
{
    private int indent;
    private StringBuilder FileContents { get; set; } = new();

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

    public static string Call(string functionName, string contents) => $"{functionName}({contents})";

    public void Blank() => FileContents.AppendLine();

    public void Line(string line)
    {
        if (line != string.Empty)
        {
            for (int i = 0; i < indent; i++)
            {
                if (Commented && i == 0)
                {
                    FileContents.Append("    //  ");
                }
                else
                {
                    FileContents.Append("    ");
                }
            }
        }

        FileContents.AppendLine(line);
    }

    public void DebugLine(string line)
    {
        if (Debug)
        {
            Line($"// {line}");
        }
    }

    public void DocumentationBlock(string description) => Line($"/// <summary> {description} </summary>");

    public void Output(Options options, string folder, string identifier)
    {
        string file = Path.Join(options.Output, Path.GetFileNameWithoutExtension(options.Input), folder, identifier + ".cs");
        Directory.CreateDirectory(Path.GetDirectoryName(file));
        File.WriteAllText(file, FileContents.ToString());
        Console.WriteLine("Generating -> " + folder + "/" + identifier + ".cs");
        FileContents.Clear();
    }
}
