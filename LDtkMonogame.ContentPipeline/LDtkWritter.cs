using System;
using LDtk.Json;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace LDtk.ContentPipeline
{
    [ContentTypeWriter]
    public class LDtkWritter : ContentTypeWriter<LDtkProject>
    {
        protected override void Write(ContentWriter output, LDtkProject json)
        {
            try
            {
                ContentLogger.Log($"Writting");

                output.Write(json.ToJson());

                // output.Write(json.BackupLimit);
                // output.Write(json.BackupOnSave);
                // output.Write(json.BgColor);
                // output.Write(json.DefaultGridSize);
                // output.Write(json.DefaultLevelBgColor);
                // output.Write(json.DefaultLevelHeight);
                // output.Write(json.DefaultLevelWidth);
                // output.Write(json.DefaultPivotX);
                // output.Write(json.DefaultPivotY);

                // output.Write(json.Defs.Entities.Length);
                // for (int i = 0; i < json.Defs.Entities.Length; i++)
                // {
                // output.Write(json.Defs.Entities[i]);
                // }

                // output.Write(json.Defs.Enums);
                // output.Write(json.Defs.ExternalEnums);
                // output.Write(json.Defs.Layers);
                // output.Write(json.Defs.LevelFields);
                // output.Write(json.Defs.Tilesets);

                // output.Write(json.ExportPng);
                // output.Write(json.ExportTiled);
                // output.Write(json.ExternalLevels);
                // output.Write(json.Flags);
                // output.Write(json.JsonVersion);
                // output.Write(json.Levels);
                // output.Write(json.MinifyJson);
                // output.Write(json.NextUid);
                // output.Write(json.PngFilePattern);
                // output.Write(json.WorldGridHeight);
                // output.Write(json.WorldGridWidth);
                // output.Write(json.WorldLayout);
            }
            catch (Exception ex)
            {
                ContentLogger.Log("Test");
                ContentLogger.Log(ex.Message);
                ContentLogger.Log(ex.StackTrace);
                throw;
            }
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "LDtk.ContentPipeline.LDtkReader, LDtkMonogame";
        }
    }
}