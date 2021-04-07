using System;
using LDtk.Json;
using Microsoft.Xna.Framework.Content;

namespace LDtk.ContentPipeline
{
#pragma warning disable CS1591
    public class LDtkReader : ContentTypeReader<World>
    {
        protected override World Read(ContentReader input, World existingInstance)
        {
            if (existingInstance != null)
            {
                return existingInstance;
            }

            return new World(input.ReadString(), input.ContentManager);
        }
    }
#pragma warning restore CS1591
}