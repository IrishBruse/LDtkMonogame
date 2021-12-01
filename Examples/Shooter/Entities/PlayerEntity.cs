using LDtk.Renderer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shooter.LDtkTypes;

namespace Shooter.Entities;
public class PlayerEntity
{
    public Vector2 Position { get => data.Position; set => data.Position = value; }

    private readonly Player data;
    private readonly Texture2D texture;
    private readonly LDtkRenderer renderer;

    public PlayerEntity(Player player, Texture2D texture, LDtkRenderer renderer)
    {
        data = player;
        this.texture = texture;
        this.renderer = renderer;
    }

    public void Update(float deltaTime)
    {
        Position += new Vector2(deltaTime * 5, 0);
    }

    public void Draw(float totalTime)
    {
        renderer.RenderEntity(data, texture, SpriteEffects.None, (int)(totalTime * 10) % 2);
    }
}
