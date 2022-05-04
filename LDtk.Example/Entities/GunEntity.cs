namespace LDtkMonogameExample.Entities;

using System;
using LDtk.Renderer;
using LDtkMonogameExample.AABB;
using LDtkTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class GunEntity
{
    public Vector2 Position
    {
        get
        {
            return data.Position;
        }

        set
        {
            data.Position = value;
        }
    }

    public bool taken;

    public Box collider;
    Gun_Pickup data;
    Texture2D texture;
    LDtkRenderer renderer;

    public GunEntity(Gun_Pickup data, Texture2D texture, LDtkRenderer renderer)
    {
        this.data = data;
        this.texture = texture;
        this.renderer = renderer;

        collider = new Box(Vector2.Zero, new Vector2(10, 16), data.Pivot);
    }

    public void Update(float totalTime)
    {
        if (taken)
        {
            return;
        }

        Position += new Vector2(0, -MathF.Sin(totalTime * 1.5f) * .1f);

        collider.Position = Position;
    }

    public void Draw()
    {
        if (taken)
        {
            return;
        }

        renderer.RenderEntity(data, texture);

        if (LDtkMonogameGame.DebugF3)
        {
            renderer.SpriteBatch.DrawRect(collider, new Color(128, 255, 0, 128));
        }
    }
}
