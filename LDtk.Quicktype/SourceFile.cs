namespace QuickTypeGenerator;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

public class SourceFile
{
    public List<string> Lines { get; set; } = new();
    public List<int> LinesToRemove { get; set; } = new();
    public bool IsFull { get; set; }

    public SourceFile(bool isFull)
    {
        IsFull = isFull;
    }

    public int Count => Lines.Count;

    public void RemoveLine(int index)
    {
        if (!LinesToRemove.Contains(index))
        {
            LinesToRemove.Add(index);
        }
        else
        {
            Console.WriteLine("Line already removed: " + index);
        }
    }

    public void Output(string path)
    {
        LinesToRemove.Sort();

        for (int i = LinesToRemove.Count - 1; i >= 0; i--)
        {
            Lines.RemoveAt(LinesToRemove[i]);
        }

        File.WriteAllLines(path, Lines);

        // Run dotnet format on generated files

        Process.Start(new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = @"format --include ./" + Path.GetFileName(path),
            WorkingDirectory = Path.GetDirectoryName(path),
        }).WaitForExit();
    }

    public string this[int key]
    {
        get => Lines[key];
        set => Lines[key] = value;
    }

    public void Add(string line) => Lines.Add(line);

    public void AddRange(string[] lines) => Lines.AddRange(lines);
}
