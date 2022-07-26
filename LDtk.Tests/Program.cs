namespace LDtk.Tests;

using LDtk;

using Xunit;

public class UnitTest1
{
    [Fact]
    public void ExternalLevels()
    {
        LDtkFile file = LDtkFile.FromFile("./Files/ExternalLevels/ExternalLevelsWorld.ldtk");
        Assert.True(file.ExternalLevels);

        ValidateWorlds(file);

        LDtkWorld world = file.LoadWorld(LDtkTypes.ExternalLevelsWorld.Worlds.World.Iid);

        ValidateWorldsLevels(world);
    }

    private static void ValidateWorlds(LDtkFile file)
    {
        foreach (LDtkWorld item in file.Worlds)
        {
            LDtkWorld world = file.LoadWorld(item.Iid);
            Assert.NotNull(world);
            Assert.True(File.Exists(world.FilePath));
            Assert.Null(world.Content);
        }
    }

    [Fact]
    public void TestApiFile()
    {
        LDtkFile file = LDtkFile.FromFile("./Files/TestApi/TestWorld.ldtk");
        Assert.False(file.ExternalLevels);

        ValidateWorlds(file);

        LDtkWorld world = file.LoadWorld(LDtkTypes.TestWorld.Worlds.World.Iid);

        ValidateWorldsLevels(world);
    }

    private static void ValidateWorldsLevels(LDtkWorld world)
    {
        foreach (LDtkLevel item in world.Levels)
        {
            LDtkLevel level = world.LoadLevel(item.Iid);
            Assert.NotNull(level);
            Assert.True(File.Exists(level.FilePath));
        }
    }
}
