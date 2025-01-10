using Business_Tests.Helpers;
using Business.Entities;
using Business.Interfaces;
using Business.Repositories;
using Moq;

namespace Business_Tests.Repositories;

public class ContactRepository_Tests
{
    // ChatGPT generated tests, which I have changed a bit.
    
    private readonly Mock<IContactFileService> _mockContactFileService;
    private readonly ContactRepository _contactRepository;

    public ContactRepository_Tests()
    {
        _mockContactFileService = new Mock<IContactFileService>();
        _contactRepository = new ContactRepository(_mockContactFileService.Object);
    }

    [Fact]
    public void Serialize_ShouldReturnJsonString_WhenListIsProvided()
    {
        // Arrange: Use TestData to get sample data
        var list = new List<ContactEntity> { TestData.GetSampleContactEntity() };

        // Act
        var result = _contactRepository.Serialize(list);

        // Assert: Verify the expected content is in the JSON string
        Assert.Contains("\"Id\":\"1\"", result);
        Assert.Contains("\"FirstName\":\"Slisk\"", result);
        Assert.Contains("\"LastName\":\"Lindqvist\"", result);
    }

    [Fact]
    public void Deserialize_ShouldReturnList_WhenJsonStringIsProvided()
    {
        // Arrange: Use TestData to get the sample JSON data
        var json = TestData.GetSampleContactEntityJson();

        // Act
        var result = _contactRepository.Deserialize(json);

        // Assert: Verify the deserialized list has the expected values
        Assert.Single(result);
        Assert.Equal("Slisk", result[0].FirstName);
        Assert.Equal("Lindqvist", result[0].LastName);
        Assert.Equal("+46700797082", result[0].PhoneNumber);
    }

    [Fact]
    public void SaveToFile_ShouldReturnTrue_WhenFileIsSavedSuccessfully()
    {
        // Arrange: Use TestData to get sample data
        var list = new List<ContactEntity> { TestData.GetSampleContactEntity() };

        // Mock SaveContentToFile to return true
        _mockContactFileService.Setup(service => service.SaveContentToFile(It.IsAny<string>())).Returns(true);

        // Act
        var result = _contactRepository.SaveToFile(list);

        // Assert: Verify SaveContentToFile was called and result is true
        Assert.True(result);
        _mockContactFileService.Verify(service => service.SaveContentToFile(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void SaveToFile_ShouldReturnFalse_WhenExceptionOccurs()
    {
        // Arrange: Use TestData to get sample data
        var list = new List<ContactEntity> { TestData.GetSampleContactEntity() };

        // Mock SaveContentToFile to throw an exception
        _mockContactFileService.Setup(service => service.SaveContentToFile(It.IsAny<string>())).Throws<Exception>();

        // Act
        var result = _contactRepository.SaveToFile(list);

        // Assert: Verify result is false when exception occurs
        Assert.False(result);
    }

    [Fact]
    public void GetFromFile_ShouldReturnList_WhenFileContentIsValidJson()
    {
        // Arrange: Use TestData to get the sample JSON data
        var json = TestData.GetSampleContactEntityJson();
        _mockContactFileService.Setup(service => service.GetContentFromFile()).Returns(json);

        // Act
        var result = _contactRepository.GetFromFile();

        // Assert: Verify that the deserialized list is correct
        Assert.Single(result);
        Assert.Equal("Slisk", result[0].FirstName);
        Assert.Equal("Lindqvist", result[0].LastName);
        Assert.Equal("+46700797082", result[0].PhoneNumber);
    }

    [Fact]
    public void GetFromFile_ShouldReturnEmptyList_WhenFileContentIsInvalidJson()
    {
        // Arrange: Return invalid JSON
        var json = "invalid json";
        _mockContactFileService.Setup(service => service.GetContentFromFile()).Returns(json);

        // Act
        var result = _contactRepository.GetFromFile();

        // Assert: Verify that the result is an empty list
        Assert.Empty(result);
    }

    [Fact]
    public void GetFromFile_ShouldReturnEmptyList_WhenFileDoesNotExist()
    {
        // Arrange: Return null to simulate file not existing
        _mockContactFileService.Setup(service => service.GetContentFromFile()).Returns(null as string);

        // Act
        var result = _contactRepository.GetFromFile();

        // Assert: Verify that the result is an empty list
        Assert.Empty(result);
    }
}