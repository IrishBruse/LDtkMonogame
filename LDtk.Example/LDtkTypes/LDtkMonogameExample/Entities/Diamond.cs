namespace LDtkTypes;

// This file was automatically generated, any modifications will be lost!
#pragma warning disable

using LDtk;
using Microsoft.Xna.Framework;

public partial class Diamond : ILDtkEntity
{
    public static Diamond Default() => new()
    {
        Identifier = "Diamond",
        Uid = 88,
        Size = new Vector2(12f, 12f),
        Pivot = new Vector2(0.5f, 1f),
        Tile = new TilesetRectangle()
        {
            X = 0,
            Y = 0,
            W = 12,
            H = 12
        },
        SmartColor = new Color(46, 85, 236, 255),

    };

    public string Identifier { get; set; }
    public System.Guid Iid { get; set; }
    public int Uid { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }
    public Vector2 Pivot { get; set; }
    public Rectangle Tile { get; set; }

    public Color SmartColor { get; set; }

    public float Timer { get; set; }
}
#pragma warning restore
