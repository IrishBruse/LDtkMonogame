using Microsoft.Xna.Framework;

namespace LDtk.Examples.Platformer
{
    public class Door : LDtkEntity
    {
        // LDtk entity fields
        public string levelIdentifier;
        public int destinationDoor;

        public bool opening;
        public Rect collider;
        private float animationTimer;

        public void Update(float deltaTime)
        {
            if (opening)
            {
                if (Tile.Location.X < 166)
                {
                    animationTimer += deltaTime;

                    if (animationTimer >= .1f)
                    {
                        animationTimer -= .1f;
                        Tile.Offset(56, 0);
                    }
                }
                else
                {
                    opening = false;
                }
            }
            else
            {
                Rectangle tile = Tile;
                tile.Location = Point.Zero;
                Tile = tile;
            }
        }
    }
}