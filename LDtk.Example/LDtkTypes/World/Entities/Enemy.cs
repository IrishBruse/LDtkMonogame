namespace LDtkTypes;

// This file was automatically generated, any modifications will be lost!
#pragma warning disable

using LDtk;
using Microsoft.Xna.Framework;

public partial class Enemy : ILDtkEntity
{
    public static Enemy Default() => new()
    {
        Identifier = "Enemy",
        Uid = 98,
        Size = new Vector2(16f, 16f),
        Pivot = new Vector2(0.5f, 0.5f),
        Tile = new TilesetRectangle()
        {
            X = 16,
            Y = 16,
            W = 16,
            H = 16
        },
        SmartColor = new Color(255, 107, 25, 255),

        Color = new Color(166, 80, 80, 1),
    };

    public string Identifier { get; set; }
    public System.Guid Iid { get; set; }
    public int Uid { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }
    public Vector2 Pivot { get; set; }
    public Rectangle Tile { get; set; }

    public Color SmartColor { get; set; }

    public Vector2[] Wander { get; set; }
    public EnemyType Type { get; set; }
    public Color Color { get; set; }
}
#pragma warning restore
