namespace LDtkMonogameExample;

using System;

public static class Program
{
    [STAThread]
    static void Main()
    {
        using LDtkMonogameGame game = new();
        game.Run();
    }
}
