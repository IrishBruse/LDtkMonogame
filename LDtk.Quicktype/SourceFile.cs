namespace QuickTypeGenerator;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

public class SourceFile
{
    public List<string> lines = new();
    public List<int> linesToRemove = new();
    public bool isFull;

    public SourceFile(bool isFull)
    {
        this.isFull = isFull;
    }

    public int Count => lines.Count;

    public void RemoveLine(int index)
    {
        if (!linesToRemove.Contains(index))
        {
            linesToRemove.Add(index);
        }
        else
        {
            Console.WriteLine("Line already removed: " + index);
        }
    }

    public void Output(string path)
    {
        linesToRemove.Sort();

        for (int i = linesToRemove.Count - 1; i >= 0; i--)
        {
            lines.RemoveAt(linesToRemove[i]);
        }

        File.WriteAllLines(path, lines);

        // Run dotnet format on generated files

        Process.Start(new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = @"format --include ./" + Path.GetFileName(path),
            WorkingDirectory = Path.GetDirectoryName(path),
        });
    }

    public string this[int key]
    {
        get
        {
            return lines[key];
        }

        set
        {
            lines[key] = value;
        }
    }

    public void Add(string line)
    {
        lines.Add(line);
    }

    public void AddRange(string[] lines)
    {
        this.lines.AddRange(lines);
    }
}
