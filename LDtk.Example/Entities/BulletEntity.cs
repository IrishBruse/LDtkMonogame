namespace LDtkMonogameExample.Entities;

using LDtk.Renderer;

using LDtkMonogameExample;
using LDtkMonogameExample.AABB;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class BulletEntity
{
    public Vector2 Position { get; set; }
    public bool Flip { get; set; }

    public bool Hit { get; set; }

    public Box Collider { get; set; }
    private readonly Texture2D texture;
    private readonly LDtkRenderer renderer;

    public BulletEntity(Texture2D texture, LDtkRenderer renderer)
    {
        this.texture = texture;
        this.renderer = renderer;

        Collider = new Box(Vector2.Zero, new Vector2(8, 8), new Vector2(0, -.5f));
    }

    public void Update(float deltaTime)
    {
        Position += new Vector2(192 * (Flip ? -1 : 1), 0) * deltaTime;

        Collider.Position = Position;
    }

    public void Draw()
    {
        renderer.SpriteBatch.Draw(texture, Position, new Rectangle(16 * 4, 0, 16, 16), Color.White, 0, new Vector2(0, .5f), Vector2.One, (SpriteEffects)(Flip ? 1 : 0), 0);

        if (LDtkMonogameGame.DebugF3)
        {
            renderer.SpriteBatch.DrawRect(Collider, new Color(128, 255, 0, 128));
        }
    }
}
