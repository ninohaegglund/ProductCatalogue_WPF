
using System.Collections.ObjectModel;

using Moq;
using Shared.Models;
using Shared.Services;

namespace Shared.Tests;

public class ProductService_Tests
{
    private readonly Mock<IFileService> _fileServiceMock;
    private readonly ProductService _productService;

    public ProductService_Tests()
    {
        _fileServiceMock = new Mock<IFileService>();

        
        _fileServiceMock.Setup(fs => fs.LoadFromFile()).Returns("");

       
        _productService = new ProductService(_fileServiceMock.Object);
    }

    [Fact]
    public void AddToList_ShouldAddProduct_WhenProductIsValid()
    {
        // Arrange
        var product = new Product { Name = "Test Product", Price = 10.0M };

        // Act
        var result = _productService.AddToList(product);

        // Assert
        Assert.True(result); 
        Assert.Single(_productService.GetProducts()); 
        Assert.Contains(product, _productService.GetProducts()); 
    }
 

    [Fact]
    public void GetProducts_ShouldReturnAllProducts()
    {
        // Arrange
        var product1 = new Product { Name = "Product 1", Price = 5.0M };
        var product2 = new Product { Name = "Product 2", Price = 10.0M };
        _productService.AddToList(product1);
        _productService.AddToList(product2);

        // Act
        var products = _productService.GetProducts();

        // Assert
        Assert.Equal(2, ((Collection<Product>)products).Count); //Making sure that the correct number of products is returned
    }
}

//Utöka dina enhetstester till att inkludera tester för:
//Att ta bort en produkt från listan;
//Att uppdatera en produkt.;
//Att spara och läsa in produkter från fil.