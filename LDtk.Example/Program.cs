namespace LDtkMonogameExample;

using System;

public static class Program
{
    [STAThread]
    private static void Main()
    {
        using Entry game = new();
        game.Run();
    }
}
