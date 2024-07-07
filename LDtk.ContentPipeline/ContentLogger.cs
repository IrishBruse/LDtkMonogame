namespace LDtk.ContentPipeline;

using Microsoft.Xna.Framework.Content.Pipeline;

public static class ContentLogger
{
    public static ContentBuildLogger Logger { get; set; }

    public static void LogMessage(string message) =>
#if true
        Logger?.LogMessage(message);
#endif

}
