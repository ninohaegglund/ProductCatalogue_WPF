using Moq;
using Shared.Services;

namespace Shared.Tests;

public class FileService_Tests
{
    private readonly Mock<IFileService> _mockFileService;
    private readonly ProductService _productService;

    public FileService_Tests()
    {
        _mockFileService = new Mock<IFileService>();
        _productService = new ProductService(_mockFileService.Object);
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
        //Arrange
        string jsonContent = "[{\"Id\":\"12345\",\"Name\":\"Test Product 1\",\"Price\":10.0}," +
                             "{\"Id\":\"67890\",\"Name\":\"Test Product 2\",\"Price\":20.0}]";
        _mockFileService.Setup(fs => fs.LoadFromFile()).Returns(jsonContent);
        //Act
        _productService.LoadProductList();
        var products = _productService.GetProducts().ToList();
       
        //Assert
        Assert.Equal(2, products.Count);
        Assert.Equal("Test Product 1", products[0].Name);
        Assert.Equal(10.0M, products[0].Price);
        Assert.Equal("Test Product 2", products[1].Name);
        Assert.Equal(20.0M, products[1].Price);

        //Need to verify twice because of LoadFromFile in the constructor of ProductService
        _mockFileService.Verify(fs => fs.LoadFromFile(), Times.Exactly(2)); 

    }

}
