namespace LDtkTypes;

// This file was automatically generated, any modifications will be lost!
#pragma warning disable

using LDtk;
using Microsoft.Xna.Framework;

public partial class Door : ILDtkEntity
{
    public static Door Default() => new()
    {
        Identifier = "Door",
        Uid = 52,
        Size = new Vector2(56f, 56f),
        Pivot = new Vector2(0.5f, 1f),
        Tile = new TilesetRectangle()
        {
            X = 0,
            Y = 0,
            W = 56,
            H = 56
        },
        SmartColor = new Color(120, 226, 102, 255),

    };

    public string Identifier { get; set; }
    public System.Guid Iid { get; set; }
    public int Uid { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }
    public Vector2 Pivot { get; set; }
    public Rectangle Tile { get; set; }

    public Color SmartColor { get; set; }

    public string LevelIdentifier { get; set; }
    public bool Opening { get; set; }
    public float AnimationTimer { get; set; }
}
#pragma warning restore
