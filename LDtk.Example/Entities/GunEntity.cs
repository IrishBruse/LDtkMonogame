namespace LDtkMonogameExample.Entities;

using System;

using LDtk.Renderer;

using LDtkMonogameExample;
using LDtkMonogameExample.AABB;

using LDtkTypes.World;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class GunEntity
{
    public Vector2 Position
    {
        get => data.Position;
        set => data.Position = value;
    }
    public bool Taken { get; set; }
    public Box Collider { get; set; }

    private Gun_Pickup data;
    private Texture2D texture;
    private LDtkRenderer renderer;

    public GunEntity(Gun_Pickup data, Texture2D texture, LDtkRenderer renderer)
    {
        this.data = data;
        this.texture = texture;
        this.renderer = renderer;

        Collider = new Box(Vector2.Zero, new Vector2(10, 16), data.Pivot);
    }

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

        if (LDtkMonogameGame.DebugF3)
        {
            renderer.SpriteBatch.DrawRect(Collider, new Color(128, 255, 0, 128));
        }
    }
}
