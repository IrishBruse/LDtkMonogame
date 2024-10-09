namespace LDtk.Codegen.Generators;

using System;
using System.IO;
using System.Text;

using LDtk.Codegen;
using LDtk.Full;

public class BaseGenerator(LDtkFileFull ldtkFile, Options options)
{
    internal readonly LDtkFileFull LDtkFile = ldtkFile;
    internal readonly Options Options = options;

    int indent;

    StringBuilder FileContents { get; } = new();

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

    public void EndBlockSeparator()
    {
        indent--;
        Line("},");
    }

    public void EndCodeBlock()
    {
        indent--;
        Line("};");
    }

    public static string Call(string functionName, string contents)
    {
        return $"{functionName}({contents})";
    }

    public void Blank()
    {
        _ = FileContents.AppendLine();
    }

    public void Line(string line)
    {
        if (!string.IsNullOrEmpty(line))
        {
            for (int i = 0; i < indent; i++)
            {
                if (Commented && i == 0)
                {
                    _ = FileContents.Append("    //  ");
                }
                else
                {
                    _ = FileContents.Append("    ");
                }
            }
        }

        _ = FileContents.AppendLine(line);
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

    public void Output(string folder, string identifier)
    {
        string file = Path.Join(Options.Output, Path.GetFileNameWithoutExtension(Options.Input), folder, identifier + ".cs");
        _ = Directory.CreateDirectory(Path.GetDirectoryName(file));
        File.WriteAllText(file, FileContents.ToString());
        Console.WriteLine("Generating -> " + Path.Combine(folder, identifier) + ".cs");
        _ = FileContents.Clear();
    }
}
