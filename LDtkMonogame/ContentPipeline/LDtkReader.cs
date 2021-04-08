using System;
using LDtk.Json;
using Microsoft.Xna.Framework.Content;

namespace LDtk.ContentPipeline
{
#pragma warning disable CS1591
    public class LDtkReader : ContentTypeReader<LDtkWorld>
    {
        protected override LDtkWorld Read(ContentReader input, LDtkWorld existingInstance)
        {
            if (existingInstance != null)
            {
                return existingInstance;
            }

            return new LDtkWorld(input.ReadString(), input.ContentManager);
        }
    }
#pragma warning restore CS1591
}