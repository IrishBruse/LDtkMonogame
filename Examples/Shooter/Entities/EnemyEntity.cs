namespace Shooter.Entities;

using LDtk.Renderer;
using LDtkTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class EnemyEntity
{
    private readonly Enemy data;
    private readonly Texture2D texture;
    private readonly LDtkRenderer renderer;
    private bool flip;
    private int nextWander;

    public EnemyEntity(Enemy data, Texture2D texture, LDtkRenderer renderer)
    {
        this.data = data;
        this.texture = texture;
        this.renderer = renderer;
    }

    public void Update(float deltaTime)
    {
        Vector2 target = data.Wander[nextWander].ToVector2();

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
        int currentAnimationFrame = (int)(totalTime * (data.Type == EnemyType.Slug ? 5 : 10)) % 2;
        renderer.RenderEntity(data, texture, (SpriteEffects)(flip ? 1 : 0), currentAnimationFrame % 2);

        if (ShooterGame.DebugF1)
        {
            for (int i = 0; i < data.Wander.Length; i++)
            {
                renderer.SpriteBatch.Draw(ShooterGame.Pixel, data.Position, null, Color.Red, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                renderer.SpriteBatch.Draw(ShooterGame.Pixel, data.Wander[i].ToVector2(), null, Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }
        }
    }
}
