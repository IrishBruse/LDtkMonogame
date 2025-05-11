namespace LDtkMonogameExample;

using Microsoft.Xna.Framework;

interface IMonogame
{
    public void Initialize();
    public void Update(GameTime gameTime);
    public void Draw(GameTime gameTime);
}
