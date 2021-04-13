using Microsoft.Xna.Framework.Content.Pipeline;

namespace LDtk.ContentPipeline
{
    public static class ContentLogger
    {
        public static ContentBuildLogger Logger { get; set; }

        public static void LogMessage(string message)
        {
            Logger?.LogMessage(message);
        }
    }
}