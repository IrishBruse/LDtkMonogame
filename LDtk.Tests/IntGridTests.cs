namespace LDtk.Tests;

using Microsoft.Xna.Framework;

using Xunit;

public class IntGridTests
{
    [Theory]
    [InlineData(0f, 0f)]
    [InlineData(-1.00f, -1.00f)]
    [InlineData(14.99f, 14.99f)]
    [InlineData(-1.01f, -1.01f, -1, -1)]
    [InlineData(15.00f, 15.00f, 1, 1)]
    public void FromWorldToGridSpaceVector2(float worldX, float worldY, int expectedGridSpaceX = 0, int expectedGridSpaceY = 0)
    {
        LDtkIntGrid intGrid = new()
        {
            WorldPosition = new Point(-1, -1),
            TileSize = 16
        };

        Vector2 positionInWorld = new(worldX, worldY);
        Point expectedPositionInGridSpace = new(expectedGridSpaceX, expectedGridSpaceY);
        Assert.Equal(intGrid.FromWorldToGridSpace(positionInWorld), expectedPositionInGridSpace);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(-1, -1)]
    [InlineData(14, 14)]
    [InlineData(-2, -2, -1, -1)]
    [InlineData(15, 15, 1, 1)]
    public void FromWorldToGridSpacePoint(int worldX, int worldY, int expectedGridSpaceX = 0, int expectedGridSpaceY = 0)
    {
        LDtkIntGrid intGrid = new()
        {
            WorldPosition = new Point(-1, -1),
            TileSize = 16
        };

        Point positionInWorld = new(worldX, worldY);
        Point expectedPositionInGridSpace = new(expectedGridSpaceX, expectedGridSpaceY);
        Assert.Equal(intGrid.FromWorldToGridSpace(positionInWorld), expectedPositionInGridSpace);
    }

    [Theory]
    [InlineData(0, 0, true)]
    [InlineData(1, 1, true)]
    [InlineData(2, 2, false)]
    [InlineData(-1, -1, false)]
    public void ContainsPoint(int x, int y, bool isPointInGrid)
    {
        LDtkIntGrid intGrid = new()
        {
            GridSize = new Point(2, 2)
        };

        Assert.Equal(intGrid.Contains(new Point(x, y)), isPointInGrid);
    }

    [Theory]
    [InlineData(0f, 0f, true)]
    [InlineData(1.99f, 1.99f, true)]
    [InlineData(2.00f, 2.00f, false)]
    [InlineData(-.01f, -.01f, false)]
    public void ContainsVector2(float x, float y, bool isPointInGrid)
    {
        LDtkIntGrid intGrid = new()
        {
            GridSize = new Point(2, 2)
        };

        Assert.Equal(intGrid.Contains(new Vector2(x, y)), isPointInGrid);
    }
}
