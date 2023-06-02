namespace LDtk.Tests;

using LDtk;

using LDtkTypes.TestWorld;

using Xunit;

public class TestWorldTests
{
    private const string FilePath = "./Files/TestApi/TestWorld.ldtk";

    [Fact]
    public void LoadFile()
    {
        LDtkFile file = LDtkFile.FromFile(FilePath);
        Assert.NotNull(file);
    }

    [Fact]
    public void LoadFileReflection()
    {
        LDtkFile file = LDtkFile.FromFileReflection(FilePath);
        Assert.NotNull(file);
    }

    [Theory]
    [InlineData(typeof(FileNotFoundException), "notfound.ldtk")]
    [InlineData(typeof(ArgumentException), "")]
    [InlineData(typeof(ArgumentNullException), null)]
    public void LoadFileThrowsNotFoundPath(Type expectedException, string path)
    {
        Assert.Throws(expectedException, () =>
        {
            LDtkFile file = LDtkFile.FromFile(path);
        });
    }

    [Fact]
    public void LoadWorldNull()
    {
        LDtkFile file = LDtkFile.FromFile(FilePath);
        Assert.NotNull(file);

        LDtkWorld world = file.LoadWorld(Guid.Empty);
        Assert.Null(world);
    }

    [Fact]
    public void LoadWorldIid()
    {
        LDtkFile file = LDtkFile.FromFile(FilePath);
        Assert.NotNull(file);

        LDtkWorld world = file.LoadWorld(Worlds.World.Iid);
        Assert.NotNull(world);
    }

    [Fact]
    public void LevelGetEntityRef()
    {
        LDtkFile file = LDtkFile.FromFile(FilePath);
        LDtkWorld world = file.LoadWorld(Worlds.World.Iid);
        LDtkLevel level = world.LoadLevel(Worlds.World.Everything);

        EntityRefTest[] entityRefTests = level.GetEntities<EntityRefTest>();
        EntityRefTest entityRefTest = entityRefTests[0];
        Assert.NotNull(entityRefTest);

        EntityRefTest entityRefFile = file.GetEntityRef<EntityRefTest>(entityRefTest.target);
        Assert.NotNull(entityRefFile);

        EntityRefTest entityRefWorld = world.GetEntityRef<EntityRefTest>(entityRefTest.target);
        Assert.NotNull(entityRefWorld);

        EntityRefTest entityRefLevel = level.GetEntityRef<EntityRefTest>(entityRefTest.target);
        Assert.NotNull(entityRefLevel);

        Assert.True(entityRefWorld.Iid == entityRefFile.Iid && entityRefFile.Iid == entityRefLevel.Iid);
    }
}
