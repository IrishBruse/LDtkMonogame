namespace LDtk
{
    /// <summary>
    /// Layer Types
    /// </summary>
    internal static class LayerType
    {
        public const string Tiles = "Tiles";
        public const string IntGrid = "IntGrid";
        public const string AutoLayer = "AutoLayer";
        public const string Entities = "Entities";
    }

    /// <summary>
    /// World Layout
    /// </summary>
    internal static class WorldLayoutEnum
    {
        public const string Horizontal = "LinearHorizontal";
        public const string Vertical = "LinearVertical";
        public const string Free = "Free";
        public const string GridVania = "GridVania";
    }

    /// <summary>
    /// Entity and Level Field
    /// </summary>
    internal static class Field
    {
        public const string IntType = "Int";
        public const string FloatType = "Float";
        public const string BoolType = "Bool";
        public const string EnumType = "Enum";
        public const string LocalEnumType = "LocalEnum";
        public const string StringType = "String";
        public const string ColorType = "Color";
        public const string PointType = "Point";
    }
}