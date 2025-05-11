namespace LDtkMonogameExample;

using System;

public static class Program
{
    [STAThread]
    static void Main()
    {
        using GameBase game = new();
        game.Run();
    }
}
