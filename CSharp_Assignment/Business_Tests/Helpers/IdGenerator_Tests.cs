using Business.Helpers;

namespace Business_Tests.Helpers;

public class IdGenerator_Tests
{
    [Fact]
    public void Generate_UniqueIdentifier_ShouldReturnStringOfTypeGuid()
    {
        var id = IdGenerator.Generate_UniqueIdentifier();
        
        Assert.False(string.IsNullOrEmpty(id));
        Assert.True(Guid.TryParse(id, out _));
    }
    
    [Fact]
    public void Generate_UniqueIdentifier_ShouldReturnUniqueIdentifiers()
    {
        // Act
        var id1 = IdGenerator.Generate_UniqueIdentifier();
        var id2 = IdGenerator.Generate_UniqueIdentifier();

        // Assert
        Assert.NotEqual(id1, id2);
    }
}