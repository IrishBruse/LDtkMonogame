namespace LDtkMonogameExample;

using System;

public static class Program
{
    [STAThread]
    private static void Main()
    {
        using LDtkMonogameGame game = new();
        game.Run();
    }
}
