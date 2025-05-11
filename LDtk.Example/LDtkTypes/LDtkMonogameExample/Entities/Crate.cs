namespace LDtkTypes.Platformer;

// This file was automatically generated, any modifications will be lost!
#pragma warning disable

using LDtk;
using Microsoft.Xna.Framework;

public partial class Crate : ILDtkEntity
{
    public static Crate Default() => new()
    {
        Identifier = "Crate",
        Uid = 56,
        Size = new Vector2(21f, 21f),
        Pivot = new Vector2(0.5f, 1f),
        Tile = new TilesetRectangle()
        {
            X = 0,
            Y = 0,
            W = 21,
            H = 21
        },
        SmartColor = new Color(239, 239, 88, 255),

        integer = 3,
        dec = 3f,
        boolean = true,
        name = "test",
        multilines = "test",
    };

    public string Identifier { get; set; }
    public System.Guid Iid { get; set; }
    public int Uid { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }
    public Vector2 Pivot { get; set; }
    public Rectangle Tile { get; set; }

    public Color SmartColor { get; set; }

    public int integer { get; set; }
    public float dec { get; set; }
    public bool boolean { get; set; }
    public string? name { get; set; }
    public string? multilines { get; set; }
    public Vector2? point { get; set; }
    public Color color { get; set; }
    public Alphabet alphabet { get; set; }
    public bool Damaged { get; set; }
    public float Timer { get; set; }
}
#pragma warning restore
