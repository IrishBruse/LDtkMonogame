namespace LDtkMonogameExample.Entities;

using System;

using LDtk.Renderer;

using LDtkMonogameExample;
using LDtkMonogameExample.AABB;

using LDtkTypes;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class EnemyEntity(Enemy data, Texture2D texture, ExampleRenderer renderer)
{
    readonly Enemy data = data;
    readonly Texture2D texture = texture;
    readonly ExampleRenderer renderer = renderer;
    bool flip;
    int nextWander;
    bool dead;
    Vector2 velocity;

    public Box Collider { get; set; } = new Box(new Vector2(0, 0), new Vector2(16, 10), data.Pivot);

    public void Update(float deltaTime)
    {
        Collider.Position = data.Position;

        if (dead)
        {
            velocity += new Vector2(0, 30 * deltaTime);
            data.Position += velocity;
            return;
        }

        if (data.Wander.Length == 0)
        {
            return;
        }

        Vector2 target = data.Wander[nextWander];

        int speed = 20;

        if (data.Type == EnemyType.Slug)
        {
            speed = 10;
        }

        data.Position = data.Position.MoveTowards(target, deltaTime * speed, out bool done);
        flip = data.Position.X - target.X > 0;

        if (done)
        {
            nextWander = (nextWander + 1) % data.Wander.Length;
        }
    }

    public void Draw(float totalTime)
    {
        int currentAnimationFrame = 0;
        if (!dead)
        {
            currentAnimationFrame = (int)(totalTime * (data.Type == EnemyType.Slug ? 5 : 10)) % 2;
        }

        renderer.RenderEntity(data, texture, (SpriteEffects)(flip ? 1 : 0) + (dead ? 2 : 0), currentAnimationFrame % 2);

        if (Entry.DebugF1)
        {
            for (int i = 0; i < data.Wander.Length; i++)
            {
                renderer.SpriteBatch.Draw(Entry.Pixel, data.Position, null, Color.Red, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                renderer.SpriteBatch.Draw(Entry.Pixel, data.Wander[i], null, Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }
        }

        if (Entry.DebugF3)
        {
            renderer.SpriteBatch.DrawRect(Collider, new Color(128, 255, 0, 128));
        }
    }

    public void Kill(float deltaTime)
    {
        Random rng = new();

        velocity = new Vector2(rng.Next(2) == 0 ? -20 : 20, -200) * deltaTime;
        dead = true;
    }
}
