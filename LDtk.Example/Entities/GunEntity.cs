namespace LDtkMonogameExample.Entities;

using System;

using LDtk.Renderer;

using LDtkMonogameExample;
using LDtkMonogameExample.AABB;

using LDtkTypes.World;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class GunEntity(Gun_Pickup data, Texture2D texture, LDtkRenderer renderer)
{
    public Vector2 Position
    {
        get => data.Position;
        set => data.Position = value;
    }

    public bool Taken { get; set; }

    public Box Collider { get; set; } = new Box(Vector2.Zero, new Vector2(10, 16), data.Pivot);

    readonly Gun_Pickup data = data;
    readonly Texture2D texture = texture;
    readonly LDtkRenderer renderer = renderer;

    public void Update(float totalTime)
    {
        if (Taken)
        {
            return;
        }

        Position += new Vector2(0, -MathF.Sin(totalTime * 1.5f) * .1f);

        Collider.Position = Position;
    }

    public void Draw()
    {
        if (Taken)
        {
            return;
        }

        renderer.RenderEntity(data, texture);

        if (Entry.DebugF3)
        {
            renderer.SpriteBatch.DrawRect(Collider, new Color(128, 255, 0, 128));
        }
    }
}
