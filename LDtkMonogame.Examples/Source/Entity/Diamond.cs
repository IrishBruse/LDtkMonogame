using System;
using Examples;
using LDtk;
using Microsoft.Xna.Framework;

public class Diamond : Entity
{
    public Rect collider;

    public bool collected;
    public bool delete;
    float timer;

    public void Update(float deltaTime, float totalTime)
    {
        if (collected == true)
        {
            if ((9 + (int)(timer / 0.1f)) < 12)
            {
                timer += deltaTime;
                Tile = new Rectangle((9 + (int)(timer / 0.1f)) * (int)Size.X, 0, (int)Size.X, (int)Size.Y);
            }
            else
            {
                delete = true;
            }
        }
        else
        {
            int currentFrame = (int)((totalTime * 10) % 10) * (int)Size.X;
            Tile = new Rectangle(currentFrame, 0, (int)Size.X, (int)Size.Y);
            Position += new Vector2(0, -MathF.Sin((float)totalTime * 2) * 0.1f);
        }
    }
}