using AABB;
using LDtk.Renderer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shooter.Entities;

public class BulletEntity
{
    public Vector2 Position { get; set; }
    public bool flip;

    public bool hit = false;

    public readonly Box collider;
    private readonly Texture2D texture;
    private readonly LDtkRenderer renderer;

    public BulletEntity(Texture2D texture, LDtkRenderer renderer)
    {
        this.texture = texture;
        this.renderer = renderer;

        collider = new Box(Vector2.Zero, new Vector2(8, 8), new Vector2(0, -.5f));
    }

    public void Update(float deltaTime)
    {
        Position += new Vector2(192 * (flip ? -1 : 1), 0) * deltaTime;

        collider.Position = Position;
    }

    public void Draw()
    {
        renderer.SpriteBatch.Draw(texture, Position, new Rectangle(16 * 4, 0, 16, 16), Color.White, 0, new Vector2(0, .5f), Vector2.One, (SpriteEffects)(flip ? 1 : 0), 0);

        if (ShooterGame.DebugF3)
        {
            renderer.SpriteBatch.DrawRect(collider, new Color(128, 255, 0, 128));
        }
    }
}
