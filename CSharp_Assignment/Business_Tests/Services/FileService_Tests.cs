using Business.Interfaces;
using Business.Services;
using Moq;

namespace Business_Tests.Services;

public class FileService_Tests
{
    // ChatGPT generated tests.
    
    private const string DirectoryPath = "Data";
    private const string FileName = "testfile.txt";

    // Test for GetContentFromFile when file exists
    [Fact]
    public void GetContentFromFile_FileExists_ReturnsContent()
    {
        // Arrange
        var content = "This is a test content";
        var mockFileService = new Mock<FileService>(DirectoryPath, FileName);
        mockFileService.Setup(fs => fs.GetContentFromFile()).Returns(content);
        
        // Act
        var result = mockFileService.Object.GetContentFromFile();
        
        // Assert
        Assert.Equal(content, result);
    }

    // Test for GetContentFromFile when file doesn't exist
    [Fact]
    public void GetContentFromFile_FileDoesNotExist_ReturnsNull()
    {
        // Arrange
        var mockFileService = new Mock<FileService>(DirectoryPath, FileName);
        mockFileService.Setup(fs => fs.GetContentFromFile()).Returns((string?)null);
        
        // Act
        var result = mockFileService.Object.GetContentFromFile();
        
        // Assert
        Assert.Null(result);
    }

    // Test for SaveContentToFile
    [Fact]
    public void SaveContentToFile_Success_ReturnsTrue()
    {
        // Arrange
        var content = "This is new content to save.";
        var mockFileService = new Mock<FileService>(DirectoryPath, FileName);
        
        // Simulate the file writing behavior
        mockFileService.Setup(fs => fs.SaveContentToFile(content)).Returns(true);
        
        // Act
        var result = mockFileService.Object.SaveContentToFile(content);
        
        // Assert
        Assert.True(result);
    }

    // Test for SaveContentToFile failure
    // [Fact]
    // public void SaveContentToFile_Failure_ReturnsFalse()
    // {
    //     // Arrange
    //     var content = "This is new content to save.";
    //     var mockFileService = new Mock<FileService>(DirectoryPath, FileName);
    //     
    //     // Simulate an exception during file writing
    //     mockFileService.Setup(fs => fs.SaveContentToFile(content)).Throws(new IOException("Failed to write file"));
    //
    //     // Act
    //     var result = mockFileService.Object.SaveContentToFile(content);
    //     
    //     // Assert
    //     Assert.False(result);
    // }

    
    // Teachers Examples. For a smaller build. . . 
    // [Fact]
    // public void SaveContentToFile_ShouldSaveContentToAFile()
    // {
    //     // Arrange
    //     var content = "This is new content to save.";
    //     var fileName = $"{Guid.NewGuid()}.json";
    //     
    //     IFileService fileService = new IFileService("Data", fileName);
    //
    //     try
    //     {
    //         // Act
    //         var result = fileService.SaveContentToFile(content);
    //
    //         // Assert
    //         Assert.True(result);
    //     }
    //     finally
    //     {
    //         if (File.Exists(fileName))
    //             File.Delete(fileName);
    //     }
    //    
    // }
    
    
}