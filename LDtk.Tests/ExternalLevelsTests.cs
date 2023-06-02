namespace LDtk.Tests;

using Xunit;

public class ExternalLevelsTests
{
    private const string FilePath = "./Files/ExternalLevels/ExternalLevelsWorld.ldtk";

    [Fact]
    public void ExternalLevels()
    {
        LDtkFile file = LDtkFile.FromFile(FilePath);
        Assert.True(file.ExternalLevels);

        foreach (LDtkWorld item in file.Worlds)
        {
            LDtkWorld world = file.LoadWorld(item.Iid);
            Assert.NotNull(world);
            Assert.True(File.Exists(world.FilePath));
            Assert.Null(world.Content);
        }
    }
}
