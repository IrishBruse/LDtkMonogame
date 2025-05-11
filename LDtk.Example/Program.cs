namespace LDtkMonogameExample;

using System;

using LDtkMonogameExample.Platformer;
using LDtkMonogameExample.Shooter;

public static class Program
{
    [STAThread]
    static void Main()
    {
        using PlatformerGame game = new();
        game.Run();
    }
}
