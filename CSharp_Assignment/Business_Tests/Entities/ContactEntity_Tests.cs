using Business_Tests.Helpers;

namespace Business_Tests.Entities;

public class ContactEntity_Tests
{
    // While writing the test JetBrains just swooshed in a lot of text from nowhere.
    // I was about to press tab on Correctly and it just added text. Changed the info to my own. ;P
    [Fact]
    public void ContactEntity_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var contactEntity = TestData.GetSampleContactEntity();

        // Act & Assert
        Assert.Equal("1", contactEntity.Id);
        Assert.Equal("Slisk", contactEntity.FirstName);
        Assert.Equal("Lindqvist", contactEntity.LastName);
        Assert.Equal("slisk.lindqvist@querzion.com", contactEntity.Email);
        Assert.Equal("Västgötagatan 10j", contactEntity.Address);
        Assert.Equal("681 40", contactEntity.PostalCode);
        Assert.Equal("Kristinehamn", contactEntity.City);
        Assert.Equal("+46700797082", contactEntity.PhoneNumber);
    }
}