using System;
using LDtk;

namespace Examples
{
    public class Door : Entity
    {
        // LDtk entity fields
        public string levelIdentifier;
        public int destinationDoor;

        public bool opening;
        public Rect collider;

        float animationTimer;

        public void Update(float deltaTime)
        {
            if (opening == true && tile.Location.X < 166)
            {
                animationTimer += deltaTime;

                if (animationTimer >= .1f)
                {
                    animationTimer -= .1f;
                    tile.Offset(56, 0);
                }
            }
        }
    }
}