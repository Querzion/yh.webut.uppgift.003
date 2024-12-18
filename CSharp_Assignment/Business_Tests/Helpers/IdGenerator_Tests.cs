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
}