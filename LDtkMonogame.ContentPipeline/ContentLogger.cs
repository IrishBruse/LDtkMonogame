using Microsoft.Xna.Framework.Content.Pipeline;

namespace LDtk.ContentPipeline
{

    public class ContentLogger
    {
        public static ContentBuildLogger Logger { get; set; }

        public static void Log(string message)
        {
            Logger?.LogMessage(message);
        }
    }
}