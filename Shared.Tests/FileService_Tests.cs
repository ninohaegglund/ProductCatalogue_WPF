using Moq;
using Shared.Services;

namespace Shared.Tests;

public class FileService_Tests
{
    private readonly Mock<IFileService> _mockFileService;

    public FileService_Tests()
    {
        _mockFileService = new Mock<IFileService>();
    }
    [Fact]
    public void SaveToFile_ShouldSaveContentToJsonFile()
    {
        //Arrange
        string savedContent = "[{\"Name\":\"Test Product\",\"Price\":10.0}]";
        _mockFileService.Setup(fs => fs.SaveToFile(savedContent)).Returns(true);
        //Act
        bool result = _mockFileService.Object.SaveToFile(savedContent);
        //Assert
        Assert.True(result);
        _mockFileService.Verify(fs =>  fs.SaveToFile(savedContent), Times.Once);
    }
    [Fact]
    public void LoadFromFile_ShouldGetContent_WhenJsonFileIsPresent()
    {

    }
}

//Utöka dina enhetstester till att inkludera tester för:
//Att ta bort en produkt från listan;
//Att uppdatera en produkt.;
//Att spara och läsa in produkter från fil.
