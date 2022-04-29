namespace QuickTypeGenerator;

using System;
using System.Collections.Generic;
using System.IO;

public class SourceFile
{
    public List<string> lines = new();
    public List<int> linesToRemove = new();

    public int Count => lines.Count;

    public void RemoveLine(int index)
    {
        if (!linesToRemove.Contains(index))
        {
            linesToRemove.Add(index);
        }
        else
        {
            throw new ArgumentException("Line already removed");
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
