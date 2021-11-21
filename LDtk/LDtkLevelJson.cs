using System.Text.Json.Serialization;
using Microsoft.Xna.Framework;
using Color = Microsoft.Xna.Framework.Color;

#pragma warning disable 1591, 1570, IDE1006
namespace LDtk
{
    /// <summary>
    /// This section contains all the level data. It can be found in 2 distinct forms, depending
    /// on Project current settings:  - If "*Separate level files*" is **disabled** (default):
    /// full level data is *embedded* inside the main Project JSON file, - If "*Separate level
    /// files*" is **enabled**: level data is stored in *separate* standalone `.ldtkl` files (one
    /// per level). In this case, the main Project JSON file will still contain most level data,
    /// except heavy sections, like the `layerInstances` array (which will be null). The
    /// `externalRelPath` string points to the `ldtkl` file.  A `ldtkl` file is just a JSON file
    /// containing exactly what is described below.
    /// </summary>
    public partial class LDtkLevel
    {
        /// <summary>
        /// Background color of the level (same as `bgColor`, except the default value is
        /// automatically used here if its value is `null`)
        /// </summary>
        [JsonPropertyName("__bgColor")]
        public Color _BgColor { get; set; }

        /// <summary>
        /// Background color of the level (same as `bgColor`, except the default value is
        /// automatically used here if its value is `null`)
        /// </summary>
        [JsonPropertyName("__bgPos")]
        public LevelBackgroundPosition _BgPos { get; set; }
        /// <summary>
        /// An array listing all other levels touching this one on the world map. In "linear" world
        /// layouts, this array is populated with previous/next levels in array, and `dir` depends on
        /// the linear horizontal/vertical layout.
        /// </summary>
        [JsonPropertyName("__neighbours")]
        public NeighbourLevel[] _Neighbours { get; set; }

        /// <summary>
        /// The *optional* relative path to the level background image.
        /// </summary>
        [JsonPropertyName("bgRelPath")]
        public string BgRelPath { get; set; }

        /// <summary>
        /// This value is not null if the project option "*Save levels separately*" is enabled. In
        /// this case, this **relative** path points to the level Json file.
        /// </summary>
        [JsonPropertyName("externalRelPath")]
        public string ExternalRelPath { get; set; }

        /// <summary>
        /// An array containing this level custom field values.
        /// </summary>
        [JsonPropertyName("fieldInstances")]
        public FieldInstance[] FieldInstances { get; set; }

        /// <summary>
        /// Unique String identifier
        /// </summary>
        [JsonPropertyName("identifier")]
        public string Identifier { get; set; }

        /// <summary>
        /// An array containing all Layer instances. **IMPORTANT**: if the project option "*Save
        /// levels separately*" is enabled, this field will be `null`.<br/>  This array is **sorted
        /// in display order**: the 1st layer is the top-most and the last is behind.
        /// </summary>
        [JsonPropertyName("layerInstances")]
        public LayerInstance[] LayerInstances { get; set; }

        /// <summary>
        /// An enum defining the way the background image (if any) is positioned on the level. See
        /// `__bgPos` for resulting position info. Possible values: &lt;`null`&gt;, `Unscaled`,
        /// `Contain`, `Cover`, `CoverDirty`
        /// </summary>
        [JsonPropertyName("bgPos")]
        public BgPos? LevelBgPos { get; set; }

        /// <summary>
        /// Height of the level in pixels
        /// </summary>
        [JsonPropertyName("pxHei")]
        public int PxHei { get; set; }

        /// <summary>
        /// Width of the level in pixels
        /// </summary>
        [JsonPropertyName("pxWid")]
        public int PxWid { get; set; }

        /// <summary>
        /// Unique Int identifier
        /// </summary>
        [JsonPropertyName("uid")]
        public int Uid { get; set; }

        /// <summary>
        /// World X coordinate in pixels
        /// </summary>
        [JsonPropertyName("worldX")]
        public int WorldX { get; set; }

        /// <summary>
        /// World Y coordinate in pixels
        /// </summary>
        [JsonPropertyName("worldY")]
        public int WorldY { get; set; }
    }

    /// <summary>
    /// Level background image position info
    /// </summary>
    public partial class LevelBackgroundPosition
    {
        /// <summary>
        /// An array of 4 float values describing the cropped sub-rectangle of the displayed
        /// background image. This cropping happens when original is larger than the level bounds.
        /// Array format: `[ cropX, cropY, cropWidth, cropHeight ]`
        /// </summary>
        [JsonPropertyName("cropRect")]
        public Rectangle CropRect { get; set; }

        /// <summary>
        /// An array containing the `[scaleX,scaleY]` values of the **cropped** background image,
        /// depending on `bgPos` option.
        /// </summary>
        [JsonPropertyName("scale")]
        public Vector2 Scale { get; set; }

        /// <summary>
        /// An array containing the `[x,y]` pixel coordinates of the top-left corner of the
        /// **cropped** background image, depending on `bgPos` option.
        /// </summary>
        [JsonPropertyName("topLeftPx")]
        public Point TopLeftPx { get; set; }
    }

}
#pragma warning restore 1591, 1570, IDE1006
