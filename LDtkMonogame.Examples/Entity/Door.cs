using LDtk;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Examples
{
    public class Door : ISprite
    {
        Vector2 position;
        public Vector2 Position
        {
            get => position; set => position = value;
        }

        Vector2 pivot;
        public Vector2 Pivot
        {
            get => pivot; set => pivot = value;
        }

        Texture2D texture;
        public Texture2D Texture
        {
            get => texture; set => texture = value;
        }

        Vector2 frameSize;
        public Vector2 FrameSize
        {
            get => frameSize; set => frameSize = value;
        }

        public string destinationLevel;
        public int destinationDoor;

        public Rect trigger;
        public bool opening;

        public void Update(float deltaTime)
        {

        }
    }
}