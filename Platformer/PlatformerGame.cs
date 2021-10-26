using Comora;
using LDtk;
using LDtk.Renderer;

namespace Examples.Platformer
{
    public class PlatformerGame : BaseExample
    {
        private LDtkWorld world;
        private LDtkLevel level;
        private LDtkRenderer renderer;

        Camera camera;

        public PlatformerGame() : base()
        {
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            Window.Title = "LDtkMonogame - Api";

            camera = new Camera(GraphicsDevice);
            renderer = new LDtkRenderer(spriteBatch, Content);
        }
    }
}