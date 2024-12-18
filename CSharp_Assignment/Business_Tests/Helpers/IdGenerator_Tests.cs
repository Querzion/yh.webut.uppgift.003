using Business.Helpers;

namespace Business_Tests.Helpers;

public class IdGeneratorTests
{
    public void GenerateUniqueIdentifier_ShouldReturnStringOfTypeGuid()
    {
        string id = IdGenerator.GenerateUniqueIdentifier();
        
        Assert.False(string.IsNullOrEmpty(id));
        Assert.True(Guid.TryParse(id, out _));
    }
}