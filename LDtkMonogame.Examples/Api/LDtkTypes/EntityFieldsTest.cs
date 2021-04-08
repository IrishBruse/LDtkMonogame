using Microsoft.Xna.Framework;

namespace LDtk.Examples.Api
{
    public partial class EntityFieldsTest : LDtkEntity
    {
        public int integer;
        public float floatingPoint;
        public bool boolean;
        public string string_singleLine;
        public string string_multiLines;
        public SomeEnum someEnum;
        public Color color;
        public Point point;
        public string filePath;
        public int[] array_Integer;
        public SomeEnum[] array_Enum;
        public Vector2[] array_points;
        public string[] array_multilines;

        public override string ToString()
        {
            return integer + "\n" +
            floatingPoint + "\n" +
            boolean + "\n" +
            string_singleLine + "\n" +
            string_multiLines + "\n" +
            someEnum + "\n" +
            color + "\n" +
            point + "\n" +
            filePath + "\n" +
            string.Join(",", array_Integer) + "\n" +
            string.Join(",", array_Enum) + "\n" +
            string.Join(",", array_points) + "\n" +
            string.Join(",", array_multilines);
        }
    }
}