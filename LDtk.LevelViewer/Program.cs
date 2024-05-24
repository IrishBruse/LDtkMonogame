namespace LDtkMonogameExample;

using System;

public static class Program
{
    [STAThread]
    static void Main()
    {
        using Entry game = new();
        game.Run();
    }
}
