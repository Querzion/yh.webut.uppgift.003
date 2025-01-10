using System.Net;
using Business.Interfaces;
using Business.Services;

namespace Business_Tests.Services;

public class IContactFileService_Tests
{
    [Fact]
    public void SaveContentToFile_ShouldSaveContentToAFile()
    {
        // Arrange
        var content = "This is new content to save.";
        var fileName = $"{Guid.NewGuid()}.json";
        
        IContactFileService contactFileService = new ContactFileService("Data", fileName);

        try
        {
            // Act
            var result = contactFileService.SaveContentToFile(content);

            // Assert
            Assert.True(result);
        }
        finally
        {
            if (File.Exists(fileName))
                File.Delete(fileName);
        }
    }

    [Fact]
    public void GetContentFromFile_ShouldReturnAContent()
    {
        // Arrange
        var content = "This is new content to save.";
        // var fileName = @$"c:\{Guid.NewGuid()}.json";
        var fileName = $"{Guid.NewGuid()}.json";
        File.WriteAllText(fileName, content);
        
        IContactFileService contactFileService = new ContactFileService("Data", fileName);
        contactFileService.SaveContentToFile(content);
        
        try
        {
            // Act
            var result = contactFileService.GetContentFromFile();

            // Assert
            Assert.Equal(content, result);
        }
        finally
        {
            if (File.Exists(fileName))
                File.Delete(fileName);
        }
    }
}